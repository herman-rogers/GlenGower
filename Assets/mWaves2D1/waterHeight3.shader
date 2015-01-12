// just a test shader.. - mgear - http://unitycoder.com/blog/
Shader "mShaders/WaterBumpShader3" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _ExtrudeTex ("Extrusion Texture", 2D) = "white" {}
      _Amount ("Extrusion Amount", Range(-1,1)) = 0.5
    }
    SubShader { 
//	Pass {
		//Cull Off
//		ZWrite On
		//ZTest Always
		
	
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
	  #pragma target 3.0 
      #pragma surface surf SimpleSpecular alpha vertex:vert
//      #pragma surface surf Lambert alpha vertex:vert
	  
//      #pragma surface surf Lambert vertex:vert



      half4 LightingSimpleSpecular (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
          half3 h = normalize (lightDir + viewDir);

          half diff = max (0, dot (s.Normal, lightDir));

          float nh = max (0, dot (s.Normal, h));
			
          float spec = s.Specular; //pow (nh, 48.0);

          half4 c;
          c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * (atten * 2);
          c.a = s.Alpha;
          return c;
      }

	  
      struct Input {
//          float2 _ExtrudeTex;
//		  float3 customColor;
//		float2 uv_MainTex;
//		float2 uv_ExtrudeTex : TEXCOORD0;
//		float4 pos;
		float2 uv_MainTex;
		float2 uv_ExtrudeTex;
 };


      float _Amount;
      sampler2D _ExtrudeTex;
//	  float4 _MainTex_ST;
//	  float4  _ExtrudeTex_ST;
	  

//      void vert (inout appdata_full v, out Input o) {
      void vert (inout appdata_full v) {
//          v.vertex.xyz += v.normal * TRANSFORM_TEX(v.texcoord, _ExtrudeTex).r * _Amount;
			#if !defined(SHADER_API_OPENGL)
			float4 tex = tex2Dlod (_ExtrudeTex, float4(v.texcoord.xy,0,0));
			if (v.normal.y>0)
			{
				v.vertex.y += tex.rgb* _Amount;
			}
			#endif
//float4 texval = tex2Dlod (_MainTex, float4(v.texcoord.xy,0,0));
//          v.vertex.xyz += v.normal * texval.r * _Amount;
//          v.vertex.xyz += v.normal * _Amount;
//		  v.color = float4(1,0,0,1);
//		  v.color = tex2D (_MainTex, float4(1,0,0,1)).rgb;
//		 o.uv_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);
//		 o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord);
//		 o.uv_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);
//		  v.color = v.texcoord;
//		  v.color = v.texcoord;
		  
//		  v.color = tex2D (_MainTex, v.texcoord).rgb;
		  
//		  o.customColor = abs(v.normal);
//		  o.customColor = v.color;
      }
	  
      sampler2D _MainTex;
	  
      void surf (Input IN, inout SurfaceOutput o) {
		  //half4 col = tex2D (_MainTex, IN.uv_MainTex+(_Time.x*0.1));
		  half4 col= tex2D (_MainTex, IN.uv_MainTex+(_Time.x*0.1))*0.5;
			col+=tex2D (_MainTex, IN.uv_MainTex*0.95+(_Time.x*0.1))*0.25;
			col+=tex2D (_MainTex, IN.uv_MainTex*0.90+(_Time.x*0.1))*0.25;
		  half4 wave = tex2D (_ExtrudeTex, IN.uv_ExtrudeTex);
//          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
          o.Albedo = col.rgb;
		  half blurSizeY = 8;
		  half blurSizeX = 8;
		  half foams = wave.rgb;
		  //if (foams>0.8)  o.Emission = foams*0.5;
//		  if (foams>0.8)  o.Specular = foams*0.8;

		   half4 sum = half4(0.0);
		   sum += tex2D(_ExtrudeTex, float2(IN.uv_ExtrudeTex.x - 4.0 * blurSizeX, IN.uv_ExtrudeTex.y - 4.0 * blurSizeY)) * 0.05;
		   sum += tex2D(_ExtrudeTex, float2(IN.uv_ExtrudeTex.x - 3.0 * blurSizeX, IN.uv_ExtrudeTex.y - 3.0 * blurSizeY)) * 0.09;
		   sum += tex2D(_ExtrudeTex, float2(IN.uv_ExtrudeTex.x - 2.0 * blurSizeX, IN.uv_ExtrudeTex.y - 2.0 * blurSizeY)) * 0.12;
		   sum += tex2D(_ExtrudeTex, float2(IN.uv_ExtrudeTex.x - 1.0 * blurSizeX, IN.uv_ExtrudeTex.y - 1.0 * blurSizeY)) * 0.15;
		   sum += tex2D(_ExtrudeTex, float2(IN.uv_ExtrudeTex.x,IN.uv_ExtrudeTex.y)) * 0.16;
		   sum += tex2D(_ExtrudeTex, float2(IN.uv_ExtrudeTex.x + 1.0 * blurSizeX, IN.uv_ExtrudeTex.y + 1.0 * blurSizeY)) * 0.15;
		   sum += tex2D(_ExtrudeTex, float2(IN.uv_ExtrudeTex.x + 2.0 * blurSizeX, IN.uv_ExtrudeTex.y + 2.0 * blurSizeY)) * 0.12;
		   sum += tex2D(_ExtrudeTex, float2(IN.uv_ExtrudeTex.x + 3.0 * blurSizeX, IN.uv_ExtrudeTex.y + 3.0 * blurSizeY)) * 0.09;
		   sum += tex2D(_ExtrudeTex, float2(IN.uv_ExtrudeTex.x + 4.0 * blurSizeX, IN.uv_ExtrudeTex.y + 4.0 * blurSizeY)) * 0.05;

			//if (foams>0.8)  
//			{
				// blur
//				o.Specular = 0.5-sum;
//				o.Specular = lerp(-0.5,0.5-sum*5,0.5-foams);
				o.Specular = lerp(-0.3,0.5-sum*4,0.5-foams);
//				if (sum>
//				o.Emission = sum;
	//}


		  o.Alpha = col.a;
      }
	  
      ENDCG
    } 
    Fallback "Diffuse"
  }