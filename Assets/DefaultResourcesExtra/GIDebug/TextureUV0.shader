Shader "Hidden/GIDebug/TextureUV0" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Pass {
			Tags { "RenderType"="Opaque" }
			LOD 200

			CGPROGRAM
			#pragma vertex vert_surf
			#pragma fragment frag_surf
			#include "UnityCG.cginc"
			
			struct v2f_surf
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 unity_LightmapST;
			float _DecodeRGBM;
			
			v2f_surf vert_surf (appdata_full v)
			{
				v2f_surf o;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				o.uv.xy = v.texcoord.xy;
				return o;
			}

			float4 frag_surf (v2f_surf IN) : COLOR
			{
				float4 result = tex2D (_MainTex, IN.uv.xy);
				if (_DecodeRGBM)
					result = float4 (DecodeLightmap(result), 1);
				return result;
			}
			ENDCG
		}
	}
}
