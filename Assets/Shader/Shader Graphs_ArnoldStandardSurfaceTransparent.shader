Shader "Shader Graphs/ArnoldStandardSurfaceTransparent" {
	Properties {
		_BASE_COLOR ("BaseColor", Vector) = (0,0,0,0)
		[NoScaleOffset] _BASE_COLOR_MAP ("BaseColorMap", 2D) = "white" {}
		_METALNESS ("Metalness", Float) = 0
		[NoScaleOffset] _METALNESS_MAP ("MetalnessMap", 2D) = "white" {}
		_SPECULAR_COLOR ("SpecularColor", Vector) = (1,1,1,0)
		[NoScaleOffset] _SPECULAR_COLOR_MAP ("SpecularColorMap", 2D) = "white" {}
		_SPECULAR_ROUGHNESS ("SpecularRoughness", Float) = 0
		[NoScaleOffset] _SPECULAR_ROUGHNESS_MAP ("SpecularRoughnessMap", 2D) = "white" {}
		_SPECULAR_IOR ("SpecularIOR", Float) = 1.5
		[NoScaleOffset] _SPECULAR_IOR_MAP ("SpecularIORMap", 2D) = "white" {}
		_EMISSION_COLOR ("EmissionColor", Vector) = (0,0,0,0)
		[NoScaleOffset] _EMISSION_COLOR_MAP ("EmissionColorMap", 2D) = "white" {}
		[NoScaleOffset] [Normal] _NORMAL_MAP ("NormalMap", 2D) = "bump" {}
		_OPACITY ("Opacity", Range(0, 1)) = 1
		[NoScaleOffset] _OPACITY_MAP ("OpacityMap", 2D) = "white" {}
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