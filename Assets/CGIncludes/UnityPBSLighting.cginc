#ifndef UNITY_PBS_LIGHTING_INCLUDED
#define UNITY_PBS_LIGHTING_INCLUDED

#include "UnityStandardConfig.cginc"
#include "UnityStandardBRDF.cginc"
#include "UnityStandardUtils.cginc"

//-------------------------------------------------------------------------------------
// Default BRDF to use:

#if (SHADER_TARGET < 30)
	// Fallback to low fidelity one for pre-SM3.0
	#define UNITY_BRDF_PBS BRDF3_Unity_PBS
#elif defined(SHADER_API_MOBILE)
	// Somewhat simplified for mobile
	#define UNITY_BRDF_PBS BRDF2_Unity_PBS
#else
	// Full quality for SM3+ PC / consoles
	#define UNITY_BRDF_PBS BRDF1_Unity_PBS
#endif


//-------------------------------------------------------------------------------------
// BRDF for lights extracted from *indirect* directional lightmaps (baked and realtime).
// Baked directional lightmap with *direct* light uses UNITY_BRDF_PBS.
// For better quality change to BRDF1_Unity_PBS.
// No directional lightmaps in SM2.0.

#define UNITY_BRDF_PBS_LIGHTMAP_INDIRECT BRDF2_Unity_PBS
#define UNITY_BRDF_GI BRDF_Unity_Indirect

//-------------------------------------------------------------------------------------

struct UnityGI
{
	UnityLight light;
	#ifdef DIRLIGHTMAP_SEPARATE
		#ifdef LIGHTMAP_ON
			UnityLight light2;
		#endif
		#ifdef DYNAMICLIGHTMAP_ON
			UnityLight light3;
		#endif
	#endif
	UnityIndirect indirect;
};

struct UnityGIInput 
{
	UnityLight light; // pixel light, sent from the engine

	float3 worldPos;
	float3 worldViewDir;
	half atten;
	half4 ambientOrLightmapUV;

	float4 boxMax[2];
	float4 boxMin[2];
	float4 probePosition[2];
	float4 probeHDR[2];
};

//-------------------------------------------------------------------------------------
inline half3 DecodeDirectionalLightmap (half3 color, fixed4 dirTex, half3 normalWorld)
{
	// In directional (non-specular) mode Enlighten bakes dominant light direction
	// in a way, that using it for half Lambert and then dividing by a "rebalancing coefficient"
	// gives a result close to plain diffuse response lightmaps, but normalmapped.

	// Note that dir is not unit length on purpose. It's length is "directionality", like
	// for the directional specular lightmaps.
	half3 dir = dirTex.xyz * 2 - 1;

	half4 tau = half4(normalWorld, 1.0) * 0.5;
	half halfLambert = dot(tau, half4(dir, 1.0));

	return color * halfLambert / dirTex.w;
}

inline half3 DecodeDirectionalSpecularLightmap (half3 color, fixed4 dirTex, half3 normalWorld, bool isRealtimeLightmap, fixed4 realtimeNormalTex, out UnityLight o_light)
{
	o_light.color = color;
	o_light.dir = dirTex.xyz * 2 - 1;

	// The length of the direction vector is the light's "directionality", i.e. 1 for all light coming from this direction,
	// lower values for more spread out, ambient light.
	half directionality = length(o_light.dir);
	o_light.dir /= directionality;

	#ifdef DYNAMICLIGHTMAP_ON
	if (isRealtimeLightmap)
	{
		// Realtime directional lightmaps' intensity needs to be divided by N.L
		// to get the incoming light intensity. Baked directional lightmaps are already
		// output like that (including the max() to prevent div by zero).
		half3 realtimeNormal = realtimeNormalTex.zyx * 2 - 1;
		o_light.color /= max(0.125, dot(realtimeNormal, o_light.dir));
	}
	#endif

	o_light.ndotl = LambertTerm(normalWorld, o_light.dir);

	// Split light into the directional and ambient parts, according to the directionality factor.
	half3 ambient = o_light.color * (1 - directionality);
	o_light.color = o_light.color * directionality;

	// Technically this is incorrect, but helps hide jagged light edge at the object silhouettes and
	// makes normalmaps show up.
	ambient *= o_light.ndotl;
	return ambient;
}

inline half3 MixLightmapWithRealtimeAttenuation (half3 lightmapContribution, half attenuation, fixed4 bakedColorTex)
{
	// Let's try to make realtime shadows work on a surface, which already contains
	// baked lighting and shadowing from the current light.
	// Generally do min(lightmap,shadow), with "shadow" taking overall lightmap tint into account.
	half3 shadowLightmapColor = bakedColorTex.rgb * attenuation;
	half3 darkerColor = min(lightmapContribution, shadowLightmapColor);

	// However this can darken overbright lightmaps, since "shadow color" will
	// never be overbright. So take a max of that color with attenuated lightmap color.
	return max(darkerColor, lightmapContribution * attenuation);
}

UNITY_DECLARE_TEXCUBE(unity_SpecCube);
UNITY_DECLARE_TEXCUBE(unity_SpecCube1);

inline UnityGI UnityStandardGlobalIllumination (UnityGIInput data, half occlusion, half oneMinusRoughness, half3 normalWorld)
{
	UnityGI o_gi;
	UNITY_INITIALIZE_OUTPUT(UnityGI, o_gi);
#if defined(SHADER_API_PSP2)
// critical to avoid internal shader compiler errors on PSP2 (unable to use generic UNITY_INITIALIZE_OUTPUT elsewhere though)
	o_gi = (UnityGI)0;
#endif	
	
	o_gi.indirect.diffuse = 0;
	o_gi.indirect.specular = 0;

	#if UNITY_SHOULD_SAMPLE_SH
		#if UNITY_SAMPLE_FULL_SH_PER_PIXEL
			half3 sh = ShadeSH9(half4(normalWorld, 1.0));
		#elif (SHADER_TARGET >= 30)
			half3 sh = data.ambientOrLightmapUV.rgb + ShadeSH12Order(half4(normalWorld, 1.0));
		#else
			half3 sh = data.ambientOrLightmapUV.rgb;
		#endif
	
		o_gi.indirect.diffuse += sh;
	#endif

	#if !defined(LIGHTMAP_ON)
		o_gi.light = data.light;
		o_gi.light.color *= data.atten;

	#else
		// Baked lightmaps
		fixed4 bakedColorTex = UNITY_SAMPLE_TEX2D(unity_Lightmap, data.ambientOrLightmapUV.xy); 
		half3 bakedColor = DecodeLightmap(bakedColorTex);
		
		#ifdef DIRLIGHTMAP_OFF
			o_gi.indirect.diffuse = bakedColor;

			#ifdef SHADOWS_SCREEN
				o_gi.indirect.diffuse = MixLightmapWithRealtimeAttenuation (o_gi.indirect.diffuse, data.atten, bakedColorTex);
			#endif // SHADOWS_SCREEN

		#elif DIRLIGHTMAP_COMBINED
			fixed4 bakedDirTex = UNITY_SAMPLE_TEX2D_SAMPLER (unity_LightmapInd, unity_Lightmap, data.ambientOrLightmapUV.xy);
			o_gi.indirect.diffuse = DecodeDirectionalLightmap (bakedColor, bakedDirTex, normalWorld);

			#ifdef SHADOWS_SCREEN
				o_gi.indirect.diffuse = MixLightmapWithRealtimeAttenuation (o_gi.indirect.diffuse, data.atten, bakedColorTex);
			#endif // SHADOWS_SCREEN

		#elif DIRLIGHTMAP_SEPARATE
			// Left halves of both intensity and direction lightmaps store direct light; right halves - indirect.

			// Direct
			fixed4 bakedDirTex = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, data.ambientOrLightmapUV.xy);
			o_gi.indirect.diffuse += DecodeDirectionalSpecularLightmap (bakedColor, bakedDirTex, normalWorld, false, 0, o_gi.light);

			// Indirect
			half2 uvIndirect = data.ambientOrLightmapUV.xy + half2(0.5, 0);
			bakedColor = DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, uvIndirect));
			bakedDirTex = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd, unity_Lightmap, uvIndirect);
			o_gi.indirect.diffuse += DecodeDirectionalSpecularLightmap (bakedColor, bakedDirTex, normalWorld, false, 0, o_gi.light2);
		#endif
	#endif
	
	#ifdef DYNAMICLIGHTMAP_ON
		// Dynamic lightmaps
		fixed4 realtimeColorTex = UNITY_SAMPLE_TEX2D(unity_DynamicLightmap, data.ambientOrLightmapUV.zw);
		half3 realtimeColor = DecodeRealtimeLightmap (realtimeColorTex) * unity_LightmapIndScale.rgb;

		#ifdef DIRLIGHTMAP_OFF
			o_gi.indirect.diffuse += realtimeColor;

		#elif DIRLIGHTMAP_COMBINED
			half4 realtimeDirTex = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, data.ambientOrLightmapUV.zw);
			o_gi.indirect.diffuse += DecodeDirectionalLightmap (realtimeColor, realtimeDirTex, normalWorld);

		#elif DIRLIGHTMAP_SEPARATE
			half4 realtimeDirTex = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicDirectionality, unity_DynamicLightmap, data.ambientOrLightmapUV.zw);
			half4 realtimeNormalTex = UNITY_SAMPLE_TEX2D_SAMPLER(unity_DynamicNormal, unity_DynamicLightmap, data.ambientOrLightmapUV.zw);
			o_gi.indirect.diffuse += DecodeDirectionalSpecularLightmap (realtimeColor, realtimeDirTex, normalWorld, true, realtimeNormalTex, o_gi.light3);
		#endif
	#endif
	o_gi.indirect.diffuse *= occlusion;

	#ifdef _GLOSSYENV
		half3 worldNormal = reflect(-data.worldViewDir, normalWorld);

		#if UNITY_SPECCUBE_BOX_PROJECTION		
			half3 worldNormal0 = BoxProjectedCubemapDirection (worldNormal, data.worldPos, data.probePosition[0], data.boxMin[0], data.boxMax[0]);
		#else
			half3 worldNormal0 = worldNormal;
		#endif

		half3 env0 = Unity_GlossyEnvironment (UNITY_PASS_TEXCUBE(unity_SpecCube), data.probeHDR[0], worldNormal0, 1-oneMinusRoughness);
		#if UNITY_SPECCUBE_BLENDING
			const float kBlendFactor = 0.99999;
			float blendLerp = data.boxMin[0].w;
			UNITY_BRANCH
			if (blendLerp < kBlendFactor)
			{
				#if UNITY_SPECCUBE_BOX_PROJECTION
					half3 worldNormal1 = BoxProjectedCubemapDirection (worldNormal, data.worldPos, data.probePosition[1], data.boxMin[1], data.boxMax[1]);
				#else
					half3 worldNormal1 = worldNormal;
				#endif

				half3 env1 = Unity_GlossyEnvironment (UNITY_PASS_TEXCUBE(unity_SpecCube1), data.probeHDR[1], worldNormal1, 1-oneMinusRoughness);
				o_gi.indirect.specular = lerp(env1, env0, blendLerp);
			}
			else
			{
				o_gi.indirect.specular = env0;
			}
		#else
			o_gi.indirect.specular = env0;
		#endif
	#endif

	o_gi.indirect.specular *= occlusion;

	return o_gi;
}

inline half3 BRDF_Unity_Indirect (half3 baseColor, half3 specColor, half oneMinusReflectivity, half oneMinusRoughness, half3 normal, half3 viewDir, UnityGI gi)
{
	half3 c = 0;
	#if defined(DIRLIGHTMAP_SEPARATE)
		gi.indirect.diffuse = 0;
		gi.indirect.specular = 0;

		#ifdef LIGHTMAP_ON
			c += UNITY_BRDF_PBS_LIGHTMAP_INDIRECT (baseColor, specColor, oneMinusReflectivity, oneMinusRoughness, normal, viewDir, gi.light2, gi.indirect).rgb;
		#endif
		#ifdef DYNAMICLIGHTMAP_ON
			c += UNITY_BRDF_PBS_LIGHTMAP_INDIRECT (baseColor, specColor, oneMinusReflectivity, oneMinusRoughness, normal, viewDir, gi.light3, gi.indirect).rgb;
		#endif
	#endif
	return c;
}

//-------------------------------------------------------------------------------------



// Surface shader output structure to be used with physically
// based shading model.
struct SurfaceOutputStandard
{
	fixed3 Albedo;		// diffuse color
	fixed3 Specular;	// specular color
	fixed3 Normal;		// tangent space normal, if written
	half3 Emission;
	half Smoothness;	// 0=rough, 1=smooth
	half Occlusion;
	fixed Alpha;
};



inline half4 LightingStandard (SurfaceOutputStandard s, half3 viewDir, UnityGI gi)
{
	s.Normal = normalize(s.Normal);

	// energy conservation
	half oneMinusReflectivity;
	s.Albedo = EnergyConservationBetweenDiffuseAndSpecular (s.Albedo, s.Specular, /*out*/ oneMinusReflectivity);

	half4 c = UNITY_BRDF_PBS (s.Albedo, s.Specular, oneMinusReflectivity, s.Smoothness, s.Normal, viewDir, gi.light, gi.indirect);
	c.rgb += UNITY_BRDF_GI (s.Albedo, s.Specular, oneMinusReflectivity, s.Smoothness, s.Normal, viewDir, gi);
	return c;
}

inline half4 LightingStandard_Deferred (SurfaceOutputStandard s, half3 viewDir, UnityGI gi, out half4 outDiffuse, out half4 outSpecSmoothness, out half4 outNormal)
{
	// energy conservation
	half oneMinusReflectivity;
	s.Albedo = EnergyConservationBetweenDiffuseAndSpecular (s.Albedo, s.Specular, /*out*/ oneMinusReflectivity);

	half4 c = UNITY_BRDF_PBS (s.Albedo, s.Specular, oneMinusReflectivity, s.Smoothness, s.Normal, viewDir, gi.light, gi.indirect);
	c.rgb += UNITY_BRDF_GI (s.Albedo, s.Specular, oneMinusReflectivity, s.Smoothness, s.Normal, viewDir, gi);

	outDiffuse = half4(s.Albedo, s.Alpha);
	outSpecSmoothness = half4(s.Specular, s.Smoothness);
	outNormal = half4(s.Normal * 0.5 + 0.5, 1);
	half4 emission = half4(s.Emission + c.rgb, 1);
	return emission;
}

inline void LightingStandard_GI (
	SurfaceOutputStandard s,
	UnityGIInput data,
	inout UnityGI gi)
{
	gi = UnityStandardGlobalIllumination (data, s.Occlusion, s.Smoothness, s.Normal);
}


#endif // UNITY_PBS_LIGHTING_INCLUDED
