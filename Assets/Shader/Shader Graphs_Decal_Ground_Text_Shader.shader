Shader "Shader Graphs/Decal_Ground_Text_Shader" {
	Properties {
		[NoScaleOffset] _Base_Map_Shape_1 ("Base Map Shape 1", 2D) = "white" {}
		[ToggleUI] _Use_Shape_2 ("Use Shape 2", Float) = 0
		[NoScaleOffset] _Base_Map_Shape_2 ("Base Map Shape 2", 2D) = "white" {}
		_Scale ("Scale", Float) = 1
		_Tilling ("Tilling", Vector) = (1,1,0,0)
		_Offset ("Offset", Vector) = (0,0,0,0)
		[ToggleUI] _UseWorldPos ("UseWorldPos", Float) = 0
		_Follow_Offset ("Follow Offset", Float) = 1
		_NoisePower ("NoisePower", Float) = 10
		_BaseColor ("BaseColor", Vector) = (1,1,1,0)
		_Opacity ("Opacity", Float) = 1
		_Alpha_Multi ("Alpha Multi", Float) = 1
		_EdgeFadeOff ("EdgeFadeOff", Range(0, 1)) = 0
		[ToggleUI] _Use_Parralax ("Use Parralax", Float) = 0
		[NoScaleOffset] _HeightMap ("HeightMap", 2D) = "white" {}
		_ParralaxStep ("ParralaxStep", Float) = 32
		_Height ("Height", Float) = 1
		[ToggleUI] _useEmission ("useEmission", Float) = 0
		_Emission_Intensity ("Emission Intensity", Float) = 0
		_EmissionClamp ("EmissionClamp", Range(0, 2)) = 1
		[HDR] _EmissionColor ("EmissionColor", Vector) = (0,0,0,0)
		[ToggleUI] _useHybrid ("useHybrid", Float) = 0
		_HybridScaleVector ("_HybridScaleVector", Vector) = (1,0,0,0)
		[ToggleUI] _isRotation ("isRotation", Float) = 0
		_Shape_1_Rotate ("Shape 1 Rotate", Float) = 0
		_Shape_2_Rotate ("Shape 2 Rotate", Float) = 0
		[HideInInspector] _DrawOrder ("Draw Order", Range(-50, 50)) = 0
		[Enum(Depth Bias, 0, View Bias, 1)] [HideInInspector] _DecalMeshBiasType ("DecalMesh BiasType", Float) = 0
		[HideInInspector] _DecalMeshDepthBias ("DecalMesh DepthBias", Float) = 0
		[HideInInspector] _DecalMeshViewBias ("DecalMesh ViewBias", Float) = 0
		[HideInInspector] _DecalAngleFadeSupported ("Decal Angle Fade Supported", Float) = 1
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