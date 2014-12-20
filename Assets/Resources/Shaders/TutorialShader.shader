// Shader created with Shader Forge Beta 0.35 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.35;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32239,y:32981|diff-27-OUT,spec-89-OUT,normal-70-OUT;n:type:ShaderForge.SFN_VertexColor,id:15,x:32943,y:33197;n:type:ShaderForge.SFN_Tex2d,id:16,x:32935,y:32813,ptlb:A,ptin:_A,tex:b66bceaf0cc0ace4e9bdc92f14bba709,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:17,x:32935,y:33034,ptlb:B,ptin:_B,tex:5fb7986dd6d0a8e4093ba82369dd6a4d,ntxv:0,isnm:False|UVIN-19-OUT;n:type:ShaderForge.SFN_TexCoord,id:18,x:33330,y:33148,uv:0;n:type:ShaderForge.SFN_Multiply,id:19,x:33154,y:33245|A-18-UVOUT,B-20-OUT;n:type:ShaderForge.SFN_Vector1,id:20,x:33361,y:33358,v1:8;n:type:ShaderForge.SFN_Lerp,id:27,x:32700,y:32981|A-16-RGB,B-17-RGB,T-15-R;n:type:ShaderForge.SFN_Tex2d,id:56,x:32943,y:33542,ptlb:Normal_B,ptin:_Normal_B,tex:cf20bfced7e912046a9ce991a4d775ec,ntxv:3,isnm:True|UVIN-19-OUT;n:type:ShaderForge.SFN_Tex2d,id:58,x:32943,y:33353,ptlb:Normal_A,ptin:_Normal_A,tex:bbab0a6f7bae9cf42bf057d8ee2755f6,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Lerp,id:59,x:32738,y:33397|A-58-RGB,B-56-RGB,T-15-R;n:type:ShaderForge.SFN_Normalize,id:70,x:32542,y:33397|IN-59-OUT;n:type:ShaderForge.SFN_ConstantLerp,id:89,x:32652,y:33142,a:0.4,b:1|IN-15-R;proporder:17-16-56-58;pass:END;sub:END;*/

Shader "Shader Forge/TutorialShader" {
    Properties {
        _B ("B", 2D) = "white" {}
        _A ("A", 2D) = "white" {}
        _Normal_B ("Normal_B", 2D) = "bump" {}
        _Normal_A ("Normal_A", 2D) = "bump" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _A; uniform float4 _A_ST;
            uniform sampler2D _B; uniform float4 _B_ST;
            uniform sampler2D _Normal_B; uniform float4 _Normal_B_ST;
            uniform sampler2D _Normal_A; uniform float4 _Normal_A_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float2 node_103 = i.uv0;
                float2 node_19 = (i.uv0.rg*8.0);
                float4 node_15 = i.vertexColor;
                float3 normalLocal = normalize(lerp(UnpackNormal(tex2D(_Normal_A,TRANSFORM_TEX(node_103.rg, _Normal_A))).rgb,UnpackNormal(tex2D(_Normal_B,TRANSFORM_TEX(node_19, _Normal_B))).rgb,node_15.r));
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float node_89 = lerp(0.4,1,node_15.r);
                float3 specularColor = float3(node_89,node_89,node_89);
                float3 specular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float4 node_16 = tex2D(_A,TRANSFORM_TEX(node_103.rg, _A));
                float4 node_17 = tex2D(_B,TRANSFORM_TEX(node_19, _B));
                finalColor += diffuseLight * lerp(node_16.rgb,node_17.rgb,node_15.r);
                finalColor += specular;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _A; uniform float4 _A_ST;
            uniform sampler2D _B; uniform float4 _B_ST;
            uniform sampler2D _Normal_B; uniform float4 _Normal_B_ST;
            uniform sampler2D _Normal_A; uniform float4 _Normal_A_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float2 node_104 = i.uv0;
                float2 node_19 = (i.uv0.rg*8.0);
                float4 node_15 = i.vertexColor;
                float3 normalLocal = normalize(lerp(UnpackNormal(tex2D(_Normal_A,TRANSFORM_TEX(node_104.rg, _Normal_A))).rgb,UnpackNormal(tex2D(_Normal_B,TRANSFORM_TEX(node_19, _Normal_B))).rgb,node_15.r));
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float node_89 = lerp(0.4,1,node_15.r);
                float3 specularColor = float3(node_89,node_89,node_89);
                float3 specular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float4 node_16 = tex2D(_A,TRANSFORM_TEX(node_104.rg, _A));
                float4 node_17 = tex2D(_B,TRANSFORM_TEX(node_19, _B));
                finalColor += diffuseLight * lerp(node_16.rgb,node_17.rgb,node_15.r);
                finalColor += specular;
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
