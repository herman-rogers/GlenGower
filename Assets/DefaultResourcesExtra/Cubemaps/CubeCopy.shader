Shader "Hidden/CubeCopy" {
	Properties {
		_MainTex ("Main", CUBE) = "" {}
		_Level ("Level", Float) = 0.
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		Pass {
			ZTest Always
			Blend Off
			AlphaTest off
			Cull Off
			ZWrite Off
			Fog { Mode off }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"

			float _Level;

			struct v2f {
				float4 pos : SV_POSITION;
				float4 uvw : TEXCOORD0;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uvw = v.texcoord;
				return o;
			}

#ifdef SHADER_API_D3D11
			TextureCube _MainTex;
			SamplerState _MainTexSampler;
			#define sampleCUBE(tex, dir, lod) tex.SampleLevel(tex##Sampler, dir, lod)
#else
			samplerCUBE _MainTex;
			#define sampleCUBE(tex, dir,  lod) texCUBElod(tex, half4(dir, lod))
#endif

			float4 frag(v2f  i) : SV_Target
			{
				return sampleCUBE(_MainTex, i.uvw.xyz, _Level);
			}
			ENDCG
		}
	}
}
