Shader "Custom/OceanWave"
{
	
	Properties
	{
		_Color ("Color", Color) = (1, 1, 1, 1)
	    _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	    _Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	    //Ocean Wave Control
		_PhaseOffset ("PhaseOffset", Range(0,1)) = 0
		_Speed ("Speed", Range(0.0,10)) = 1.0
		_Depth ("Depth", Range(0.0,1)) = 0.2
		_Smoothing ("Smoothing", Range(0,1)) = 0.0
		_XDrift ("X Drift", Range(0.0,2.0)) = 0.05
		_ZDrift ("Z Drift", Range(0.0,2.0)) = 0.12
		_Scale ("Scale", Range(0.1,10)) = 1.0
	}
	
	SubShader
	{
		Tags
		{
			"Queue" = "Geometry"
			"RenderType" = "Opaque"
			"IgnoreProjector" = "True"
		}
		
		CGPROGRAM
		#pragma surface surf BlinnPhong vertex:vert
		//#pragma surface surf Lambert finalcolor:showNormals vertex:vert noforwardadd
		#pragma target 3.0
		
		
        fixed4 _Color;
        half _Shininess;
		float _PhaseOffset;
		float _Speed;
		float _Depth;
		float _Smoothing;
		float _XDrift;
		float _ZDrift;
		float _Scale;
		
		struct Input
		{
		    float2 uv_MainTex;
		};
		
		// I ran into an enormous, annoying error where the Cg sin() function was causing some geometry mayhem,
		//   altering properties it shouldn't, if it was given a value of exactly 1.
		// Any modulation fixed it.  No other sin() implementation has the same problem.
		// No idea what the problem is!  Using this as a workaround, may go to the forums to see what's up later.
		float sine( float x )
		{
			float b = (1-x)<0.001 ? 1.0001 : 1.0;
			return sin(x) * b;
		}
		
		void vert( inout appdata_full v, out Input o )
		{
			// Note that, to start off, all work is in object (local) space.
			// We will eventually move normals to world space to handle arbitrary object orientation.
			// There is no real need for tangent space in this case.
			UNITY_INITIALIZE_OUTPUT( Input, o );
			// Do all work in world space
			float3 v0 = mul( _Object2World, v.vertex ).xyz;
			
			// Create two fake neighbor vertices.
			// The important thing is that they be distorted in the same way that a real vertex in their location would.
			// This is pretty easy since we're just going to do some trig based on position, so really any samples will do.
			float3 v1 = v0 + float3( 0.05, 0, 0 ); // +X
			float3 v2 = v0 + float3( 0, 0, 0.05 ); // +Z
			
			
			// Some animation values
			float phase = _PhaseOffset * (3.14 * 2);
			float phase2 = _PhaseOffset * (3.14 * 1.123);
			float speed = _Time.y * _Speed;
			float speed2 = _Time.y * (_Speed * 0.33 );
			float _Depth2 = _Depth * 1.0;
			float v0alt = v0.x * _XDrift + v0.z * _ZDrift;
			float v1alt = v1.x * _XDrift + v1.z * _ZDrift;
			float v2alt = v2.x * _XDrift + v2.z * _ZDrift;
			
			// Modify the real vertex and two theoretical samples by the distortion algorithm (here a simple sine wave on Y, driven by local X pos)
			v0.y += sin( phase  + speed  + ( v0.x  * _Scale ) ) * _Depth;
			v0.y += sin( phase2 + speed2 + ( v0alt * _Scale ) ) * _Depth2; // This is just another wave being applied for a bit more complexity.
			
			v1.y += sin( phase  + speed  + ( v1.x  * _Scale ) ) * _Depth;
			v1.y += sin( phase2 + speed2 + ( v1alt * _Scale ) ) * _Depth2;
			
			v2.y += sin( phase  + speed  + ( v2.x  * _Scale ) ) * _Depth;
			v2.y += sin( phase2 + speed2 + ( v2alt * _Scale ) ) * _Depth2;
			
			// By reducing the delta on Y, we effectively restrict the amout of variation the normals will exhibit.
			// This appears like a smoothing effect, separate from the actual displacement depth.
			// It's basically undoing the change to the normals, leaving them straight on Y.
//			v1.y -= (v1.y - v0.y) * _Smoothing;
//			v2.y -= (v2.y - v0.y) * _Smoothing;
			
			// Solve worldspace normal
			float3 vna = cross( v2-v0, v1-v0 );
			
			// Put normals back in object space
			float3 vn = mul( (_World2Object), vna );
			
			// Normalize
//			v.normal = normalize( vn );
			
			// Put vertex back in object space, Unity will automatically do the MVP projection
			v.vertex.xyz = mul( (_World2Object), v0 );
		}
		
		void surf (Input IN, inout SurfaceOutput o)
		{
			fixed4 c = _Color;
			o.Albedo = _Color.rgb;
			o.Gloss = _Color.a;
			o.Alpha = _Color.a;
			o.Specular = _Shininess;
		}
		
		ENDCG
		
	}
	
}