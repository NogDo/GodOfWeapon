Shader "Shader Graphs/XRayWall_URP_Lit_Shadergraph" {
	Properties {
		_BaseColor ("BaseColor", Vector) = (0,0,0,0)
		[NoScaleOffset] _BaseMap ("BaseMap", 2D) = "white" {}
		_UVShift ("UVShift", Vector) = (0,0,0,0)
		[NoScaleOffset] _MetallicGlossMap ("MetallicGlossMap", 2D) = "white" {}
		_Metallic ("Metallic", Range(0, 1)) = 0
		_Smoothness ("Smoothness", Range(0, 1)) = 0
		_Rotation ("Rotation", Float) = 0
		_uvIdx ("uvIdx", Float) = 0
		[ToggleUI] _UseEmission ("UseEmission", Float) = 1
		[HDR] _EmissionColor ("EmissionColor", Vector) = (1,1,1,0)
		_EmissionMap ("EmissionMap", 2D) = "white" {}
		[ToggleUI] _useNormal ("useNormal", Float) = 0
		[Normal] _NormalMap ("NormalMap", 2D) = "bump" {}
		_NormalBlend ("NormalBlend", Float) = 0
		[ToggleUI] _useXRay ("useXRay", Float) = 1
		[ToggleUI] _DirectionalXray ("DirectionalXray", Float) = 1
		_XrayPos ("XrayPos", Vector) = (0.5,0.5,0,0)
		_XrayRadius ("XrayRadius", Float) = 1
		_XraySmoothness ("XraySmoothness", Float) = 0.5
		_XrayOpacity ("XrayOpacity", Float) = 1
		[ToggleUI] _useNoise ("useNoise", Float) = 0
		_NoiseTex ("NoiseTex", 2D) = "white" {}
		_XrayNoise ("XrayNoise", Float) = 20
		_NoisePanning ("NoisePanning", Vector) = (0,0,0,0)
		[ToggleUI] _useDither ("useDither", Float) = 0
		_AlphaClipValue ("AlphaClipValue", Float) = 0
		[HideInInspector] _WorkflowMode ("_WorkflowMode", Float) = 1
		[HideInInspector] _CastShadows ("_CastShadows", Float) = 1
		[HideInInspector] _ReceiveShadows ("_ReceiveShadows", Float) = 1
		[HideInInspector] _Surface ("_Surface", Float) = 0
		[HideInInspector] _Blend ("_Blend", Float) = 0
		[HideInInspector] _AlphaClip ("_AlphaClip", Float) = 0
		[HideInInspector] _SrcBlend ("_SrcBlend", Float) = 1
		[HideInInspector] _DstBlend ("_DstBlend", Float) = 0
		[ToggleUI] [HideInInspector] _ZWrite ("_ZWrite", Float) = 1
		[HideInInspector] _ZWriteControl ("_ZWriteControl", Float) = 0
		[HideInInspector] _ZTest ("_ZTest", Float) = 4
		[HideInInspector] _Cull ("_Cull", Float) = 0
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