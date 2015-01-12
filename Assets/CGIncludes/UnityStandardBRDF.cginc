#ifndef UNITY_STANDARD_BRDF_INCLUDED
#define UNITY_STANDARD_BRDF_INCLUDED

#include "UnityCG.cginc"
#include "UnityStandardConfig.cginc"

//-------------------------------------------------------------------------------------
struct UnityLight
{
	half3 color;
	half3 dir;
	half  ndotl;
};

struct UnityIndirect
{
	half3 diffuse;
	half3 specular;
};

//-------------------------------------------------------------------------------------
inline half DotClamped (half3 a, half3 b)
{
	#if (SHADER_TARGET < 30)
		return saturate(dot(a, b));
	#else
		return max(0.0f, dot(a, b));
	#endif
}

inline half LambertTerm (half3 normal, half3 lightDir)
{
	return DotClamped (normal, lightDir);
}

inline half BlinnTerm (half3 normal, half3 halfDir)
{
	return DotClamped (normal, halfDir);
}

inline half3 FresnelTerm (half3 F0, half cosA)
{
	half t = pow (abs (1 - cosA), 5);	// ala Schlick interpoliation
	return F0 + (1-F0) * t;
}
inline half3 FresnelLerp (half3 F0, half3 F90, half cosA)
{
	half t = pow (abs (1 - cosA), 5);	// ala Schlick interpoliation
	return lerp (F0, F90, t);
}
inline half3 LazarovFresnelTerm (half3 F0, half roughness, half cosA)
{
	half t = pow (abs (1 - cosA), 5);	// ala Schlick interpoliation
	t /= 4 - 3 * roughness;
	return F0 + (1-F0) * t;
}
inline half3 SebLagardeFresnelTerm (half3 F0, half roughness, half cosA)
{
	half t = pow (abs (1 - cosA), 5);	// ala Schlick interpoliation
	return F0 + (max (F0, roughness) - F0) * t;
}

inline half FresnelTermFast (half F0, half cosA)
{
	half t = 1 - cosA;
	t *= t; // ^2
	t *= t; // ^4
	return F0 + (1-F0) * t;
}

// NOTE: Visibility term here is the full form from Torrance-Sparrow model, it includes Geometric term: V = G / (N.L * N.V)
// This way it is easier to swap Geometric terms and more room for optimizations (except maybe in case of CookTorrance geom term)

// Cook-Torrance visibility term, doesn't take roughness into account
inline half CookTorranceVisibilityTerm (half NdotL, half NdotV,  half NdotH, half VdotH)
{
	VdotH += 1e-5f;
	half G = min (1.0, min (
		(2.0 * NdotH * NdotV) / VdotH,
		(2.0 * NdotH * NdotL) / VdotH));
	return G / (NdotL * NdotV + 1e-5f);
}

// Kelemen is an approximation to Cook-Torrance visibility term
inline half KelemenVisibilityTerm (half NdotL, half LdotH)
{
	return 1.0 / (LdotH * LdotH);
}

// Generic Smith-Schlick visibility term
inline half SmithVisibilityTerm (half NdotL, half NdotV, half k)
{
	half gL = NdotL * (1-k) + k;
	half gV = NdotV * (1-k) + k;
	return 1.0 / (gL * gV);
}

// Smith-Schlick derived for Beckmann
inline half SmithBeckmannVisibilityTerm (half NdotL, half NdotV, half roughness)
{
	half c = 0.797885f; // = (2 / Pi)^0.5
	half k = roughness * roughness * c;
	return SmithVisibilityTerm (NdotL, NdotV, k);
}

// Smith-Schlick derived for GGX 
inline half SmithGGXVisibilityTerm (half NdotL, half NdotV, half roughness)
{
	half k = (roughness * roughness) / 2; // derived by B. Karis, http://graphicrants.blogspot.se/2013/08/specular-brdf-reference.html
	return SmithVisibilityTerm (NdotL, NdotV, k);
}

inline half ImplicitVisibilityTerm ()
{
	return 1;
}

inline half RoughnessToSpecPower (half roughness)
{
#if UNITY_GLOSS_MATCHES_MARMOSET_TOOLBAG2
	// from https://s3.amazonaws.com/docs.knaldtech.com/knald/1.0.0/lys_power_drops.html
	half n = 10.0 / log2((1-roughness)*0.968 + 0.03);
	return n * n;

	// NOTE: another approximate approach to match Marmoset gloss curve is to
	// multiply roughness by 0.7599 in the code below (makes SpecPower range 4..N instead of 1..N)
#else
	half m = roughness * roughness * roughness + 1e-6f;	// follow the same curve as unity_SpecCube
	half n = (2.0 / m) - 2.0;							// http://jbit.net/%7Esparky/academic/mm_brdf.pdf
	n = max(n, 1e-5f);									// prevent possible cases of pow(0,0), which could happen when roughness is 1.0 and NdotH is zero
	return n;
#endif
}

inline half BlinnPhongNormalizedTerm (half NdotH, half n)
{
	half normTerm = (n + 1.0) / (2.0 * UNITY_PI);
	half specTerm = pow (NdotH, n);
	return specTerm * normTerm;
}

inline half GGXTerm (half NdotH, half roughness)
{
	half a = roughness * roughness;
	half a2 = a * a;
	half d = NdotH * NdotH * (a2 - 1.f) + 1.f;
	return a2 / (UNITY_PI * d * d);
}

//-------------------------------------------------------------------------------------
/*
// https://s3.amazonaws.com/docs.knaldtech.com/knald/1.0.0/lys_power_drops.html

const float k0 = 0.00098, k1 = 0.9921;
// pass this as a constant for optimization
const float fUserMaxSPow = 100000; // sqrt(12M)
const float g_fMaxT = ( exp2(-10.0/fUserMaxSPow) - k0)/k1;
float GetSpecPowToMip(float fSpecPow, int nMips)
{
   // Default curve - Inverse of TB2 curve with adjusted constants
   float fSmulMaxT = ( exp2(-10.0/sqrt( fSpecPow )) - k0)/k1;
   return float(nMips-1)*(1.0 - clamp( fSmulMaxT/g_fMaxT, 0.0, 1.0 ));
}

	//float specPower = RoughnessToSpecPower (roughness);
	//float mip = GetSpecPowToMip (specPower, 7);
*/

// Decodes HDR textures
// handles dLDR, RGBM formats
// Modified version of DecodeHDR from UnityCG.cginc
inline half3 DecodeHDR_NoLinearSupportInSM2 (half4 data, half4 decodeInstructions)
{
	// If Linear mode is not supported we can skip exponent part

	// In Standard SM2.0 and SM3.0 are always using different shader variations
	// Since SM2.0only hardware does not support Linear and shader its own variation - we can skip exponent part for SM2.0 too
	#if defined(UNITY_NO_LINEAR_COLORSPACE) || (SHADER_TARGET < 30)
		return (data.a * decodeInstructions.x) * data.rgb;
	#else
		return DecodeHDR(data, decodeInstructions);
	#endif
}

half3 Unity_GlossyEnvironment (UNITY_ARGS_TEXCUBE(tex), half4 hdr, half3 worldNormal, half roughness)
{
#if !UNITY_GLOSS_MATCHES_MARMOSET_TOOLBAG2 || (SHADER_TARGET < 30)
	float mip = roughness * UNITY_SPECCUBE_LOD_STEPS;
#else
	// TODO: remove pow, store cubemap mips differently
	float mip = pow(roughness,3.0/4.0) * UNITY_SPECCUBE_LOD_STEPS;
#endif

	half4 rgbm = SampleCubeReflection(tex, worldNormal.xyz, mip);
	return DecodeHDR_NoLinearSupportInSM2 (rgbm, hdr);
}

//-------------------------------------------------------------------------------------

// Note: BRDF entry points use oneMinusRoughness (aka "smoothness") and oneMinusReflectivity for optimization
// purposes, mostly for DX9 SM2.0 level. Most of the math is being done on these (1-x) values, and that saves
// a few precious ALU slots.


/**
* Main entry point to Physically Based BRDF.
* Derived from Disney work and based on modified Cook-Torrance with BlinnPhong as NDF and Schlick approximation for Fresnel.
*
*/
half4 BRDF1_Unity_PBS (half3 diffColor, half3 specColor, half oneMinusReflectivity, half oneMinusRoughness,
	half3 normal, half3 viewDir,
	UnityLight light, UnityIndirect gi/*, half alpha*/)
{
	half roughness = 1-oneMinusRoughness;
	half3 halfDir = normalize (light.dir + viewDir);

	half nl = light.ndotl;
	half nh = BlinnTerm (normal, halfDir);
	half nv = DotClamped (normal, viewDir);
	half vh = DotClamped (viewDir, halfDir);
	half lv = DotClamped (light.dir, viewDir);
	half lh = DotClamped (light.dir, halfDir);

#if UNITY_BRDF_GGX
	half V = SmithGGXVisibilityTerm (nl, nv, roughness);
	half D = GGXTerm (nh, roughness);
#else
	half V = SmithBeckmannVisibilityTerm (nl, nv, roughness);
	half D = BlinnPhongNormalizedTerm (nh, RoughnessToSpecPower (roughness));
#endif

	// 1.00001 here to prevent from argument going just slightly below
	// zero due to floating point. Having NaNs here is not nice.
	half nlPow5 = pow((1.00001-nl), 5);
	half nvPow5 = pow((1.00001-nv), 5);
	half Fd90 = 0.5 + 2 * lh * lh * roughness;
	half disneyDiffuse = (1 + (Fd90-1) * nlPow5) * (1 + (Fd90-1) * nvPow5);
	
	half specularTerm = max(0, (V * D * nl) / 4); // Torrance-Sparrow model, Fresnel is applied later (for optimization reasons)
	half diffuseTerm = disneyDiffuse * nl;
	
	// HACK: theoretically we should divide by Pi diffuseTerm!
	// BUT 1) that will make shader look significantly darker than Legacy ones
	// and 2) on engine side "Non-important" lights have to be divided by Pi to, if are injected into ambient SH
	specularTerm *= UNITY_PI; // Diffuse energy conservation

	// TODO: once shader is switched to alpha-blend pre-multiply (_SrcBlend = One)
	#if UNITY_PROPER_PBS_TRANSPARENCY
 		diffuseTerm *= alpha;
 		alpha = saturate(alpha + (1-oneMinusReflectivity));
 	#else
		half alpha = FresnelTerm(0, nv) * oneMinusRoughness; // TODO: this is just plain wrong!
	#endif

	half grazingTerm = saturate(oneMinusRoughness + (1-oneMinusReflectivity));
    half3 color =	diffColor * (gi.diffuse + light.color * diffuseTerm)
                    + specularTerm * light.color * FresnelTerm (specColor, vh)
					+ gi.specular * FresnelLerp (specColor, grazingTerm, nv);

	return half4(color, alpha);
}

// TBD: take a look at visibility term optimizations: http://www.filmicworlds.com/2014/04/21/optimizing-ggx-shaders-with-dotlh/ 
half4 BRDF2_Unity_PBS (half3 diffColor, half3 specColor, half oneMinusReflectivity, half oneMinusRoughness,
	half3 normal, half3 viewDir,
	UnityLight light, UnityIndirect gi/*, half alpha*/)
{
	half roughness = 1-oneMinusRoughness;
	half3 halfDir = normalize (light.dir + viewDir);

	half nl = light.ndotl;
	half nh = BlinnTerm (normal, halfDir);
	half nv = DotClamped (normal, viewDir);
	half lh = DotClamped (light.dir, halfDir);

	half specularPower = RoughnessToSpecPower (roughness);
	half Pi30 = (UNITY_PI * 30);
	half specularTerm = ((specularPower + 1) * pow (nh, specularPower)) / (Pi30 * lh + 1e-5f); // slightly different from original
	half diffuseTerm = nl;
	half fresnelTerm = FresnelTermFast(0, nv);
	half grazingTerm = oneMinusReflectivity * oneMinusRoughness * fresnelTerm;

	half3 color =	diffColor * (gi.diffuse + light.color * diffuseTerm)
					+ specColor * (gi.specular + (light.color * specularTerm))
					+ gi.specular * grazingTerm;
	return half4(color, fresnelTerm * oneMinusRoughness);
}

sampler2D unity_NHxRoughness;
half4 BRDF3_Unity_PBS (half3 diffColor, half3 specColor, half oneMinusReflectivity, half oneMinusRoughness,
	half3 normal, half3 viewDir,
	UnityLight light, UnityIndirect gi/*, half alpha*/)
{
	half roughness = 1-oneMinusRoughness;
	half3 halfDir = normalize (light.dir + viewDir);

	half nl = light.ndotl;
	half nh = BlinnTerm (normal, halfDir);
	half nv = DotClamped (normal, viewDir);

//			half specularTerm = BlinnPhongNormalizedTerm (nh, RoughnessToSpecPower (roughness));
	half specularTerm = tex2D(unity_NHxRoughness, half2(dot(normal, halfDir), roughness)).UNITY_ATTEN_CHANNEL * 16 * nl;
	half diffuseTerm = nl;
	half fresnelTerm = FresnelTermFast(0, nv);
	half grazingTerm = oneMinusReflectivity * oneMinusRoughness * fresnelTerm;

	half3 color =	diffColor * (gi.diffuse + light.color * diffuseTerm)
					+ specColor * (gi.specular + light.color * specularTerm)
					+ gi.specular * grazingTerm;
	return half4(color, fresnelTerm * oneMinusRoughness);
}


#endif // UNITY_STANDARD_BRDF_INCLUDED
