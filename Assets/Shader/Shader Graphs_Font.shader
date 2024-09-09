Shader "Shader Graphs/Font" {
	Properties {
		[NoScaleOffset] _FontTex ("FontTex", 2DArray) = "" {}
		_NumberValue ("NumberValue", Float) = 123
		[HDR] _ColorValue ("ColorValue", Vector) = (0.7735849,0.5745933,0.3466536,1)
		_Size ("Size", Range(1, 100)) = 20
		_OffsetX ("OffsetX", Float) = 1.1
		[Toggle(_FOURDIGITS)] _FOURDIGITS ("_FOURDIGITS", Float) = 0
		[Toggle(_FIVEDIGITS)] _FIVEDIGITS ("_FIVEDIGITS", Float) = 0
		_ShowSign ("ShowSign", Float) = 0
		[HDR] _InnerColor ("InnerColor", Vector) = (0,0,0,1)
		[HDR] _OutlineColor ("OutlineColor", Vector) = (0,0,0,1)
		_Outline ("Outline", Range(0, 1)) = 1
		[HideInInspector] _QueueOffset ("_QueueOffset", Float) = 0
		[HideInInspector] _QueueControl ("_QueueControl", Float) = -1
		[HideInInspector] [NoScaleOffset] unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Hidden/Shader Graph/FallbackError"
	//CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
}