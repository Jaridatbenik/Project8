﻿Shader "Outlined/Custom" {
	Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_OutlineColor ("Outline Color", Color) = (0,0,0,0)
		_Outline ("Outline width", Range (0, 1)) = .1
		_MainTex ("Base (RGB)", 2D) = "white" { }
	}

 
CGINCLUDE
#include "UnityCG.cginc"
 
struct appdata {
	float4 vertex : POSITION;
	float3 normal : NORMAL;
};
 
struct v2f {
	float4 pos : POSITION;
	float4 color : COLOR;
};
 
uniform float _Outline;
uniform float4 _OutlineColor;
 
v2f vert(appdata v) {
	// just make a copy of incoming vertex data but scaled according to normal direction
	v2f o;

	v.vertex *= -( 1 + _Outline);

	o.pos = UnityObjectToClipPos(v.vertex);

	o.color = _OutlineColor;
	
	return o;
}
ENDCG
 
	SubShader {
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True"}
		Cull Back
		CGPROGRAM
		#pragma surface surf Lambert
		 

	#pragma target 3.0

	UNITY_INSTANCING_BUFFER_START(Props)
	// put more per-instance properties here
	UNITY_INSTANCING_BUFFER_END(Props)

		sampler2D _MainTex;
		fixed4 _Color;
		 
		struct Input {
			float2 uv_MainTex;
		};
		 
		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG

		Pass {
			Name "OUTLINE"
			Tags { "Queue" = "Transparent" "IgnoreProjector" = "True"}
			Cull Back
			ZWrite Off
			//ZTest Less
			Offset 1, 1
 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			half4 frag(v2f i) :COLOR { return i.color; }
			ENDCG
		}
	}
 
	Fallback "Transparent"
}