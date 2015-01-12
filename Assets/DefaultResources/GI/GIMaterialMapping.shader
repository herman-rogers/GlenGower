Shader "Hidden/GIMaterialMapping" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Texture0 ("Texture0", 2D) = "white" {}
		_Texture1 ("Texture1", 2D) = "white" {}
		_OneOverOutputBoost ("OneOverOutputBoost", Float) = 1
		_MaxOutputValue ("MaxOutputValue", Float) = 1
	}
	SubShader {
		Pass{
			Tags { "RenderType"="Opaque" }
			LOD 200
			Cull Off
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile POSITION_UV1 POSITION_UV2
			#pragma multi_compile TEXTURE1_ALPHA TEXTURE1_RGB
			#pragma multi_compile RGBM_ON RGBM_OFF
			#include "UnityCG.cginc"
			
			struct appdata_vert
			{
				float4 vertex		: POSITION;
				float2 texcoord0	: TEXCOORD0;
				float2 texcoord1	: TEXCOORD1;
				float2 texcoord2	: TEXCOORD2;
			};
			
			struct v2f
			{
				float4 pos		: SV_POSITION;
				float4 uv		: TEXCOORD0;
				float4 dummy	: TEXCOORD1;
			};
			
			float4 unity_LightmapST;
			
			float4 _Texture0_ST;
			float4 _Texture1_ST;

			half4 EncodeRGBM (half3 rgb)
			{
				const float kRGBMMaxRange = 8.0;
				const float kOneOverRGBMMaxRange = 1.0 / (float)kRGBMMaxRange;
				const float kMinMultiplier = 2.0 * 1e-2;

				float4 rgbm = float4(rgb * kOneOverRGBMMaxRange, 1.0f);
				rgbm.a = max(max(rgbm.r, rgbm.g), max(rgbm.b, kMinMultiplier));
				rgbm.a = ceil(rgbm.a * 255.0) / 255.0;
				rgbm.rgb /= rgbm.a;
				return rgbm;
			}
			
			v2f vert (appdata_vert v)
			{
				float2 positionUV;
				#if POSITION_UV1
				positionUV = v.texcoord1.xy;
				#else
				positionUV = v.texcoord2.xy;
				#endif
				positionUV = positionUV * unity_LightmapST.xy + unity_LightmapST.zw;
				
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, float4 (positionUV, 0.f, 1.f));
				o.uv.xy = TRANSFORM_TEX(v.texcoord0, _Texture0);
				o.uv.zw = TRANSFORM_TEX(v.texcoord0, _Texture1);
				o.dummy = v.vertex; // make OpenGL happy
				return o;
			}
			
			float4 _Color;
			sampler2D _Texture0;
			sampler2D _Texture1;
			float _OneOverOutputBoost;
			float _MaxOutputValue;
			
			float4 frag (v2f i) : COLOR
			{
				float4 tex0 = tex2D (_Texture0, i.uv.xy);
				float4 tex1 = tex2D (_Texture1, i.uv.zw);
				
				#if TEXTURE1_ALPHA
				tex1 = tex1.aaaa;
				#endif
				
				float4 outColor = _Color * tex0 * tex1;
				
				#if RGBM_ON
				outColor = EncodeRGBM (outColor.rgb);
				#else
				outColor = float4(clamp(pow(outColor.rgb, _OneOverOutputBoost), 0, _MaxOutputValue), 1.0f);
				#endif
				
				return outColor;
			}
			
			ENDCG
		}
	}
}
