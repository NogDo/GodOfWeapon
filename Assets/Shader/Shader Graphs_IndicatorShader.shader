Shader "Shader Graphs/IndicatorShader" {
	Properties {
		[NoScaleOffset] _Ring1Tex ("Ring1Tex", 2D) = "white" {}
		[NoScaleOffset] _Ring2Tex ("Ring2Tex", 2D) = "white" {}
		[NoScaleOffset] _Ring3Tex ("Ring3Tex", 2D) = "white" {}
		[HDR] _Color ("Color", Vector) = (1,0,0,1)
		_Scale ("Scale", Float) = 1
		_SpeedBase ("SpeedBase", Float) = 0
		_Ring1Speed ("Ring1Speed", Float) = 1
		_Ring2Speed ("Ring2Speed", Float) = 1
		_Ring3Speed ("Ring3Speed", Float) = 1
		[HideInInspector] _QueueOffset ("_QueueOffset", Float) = 0
		[HideInInspector] _QueueControl ("_QueueControl", Float) = -1
		[HideInInspector] [NoScaleOffset] unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
	Fallback "Hidden/Shader Graph/FallbackError"
	//CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
}