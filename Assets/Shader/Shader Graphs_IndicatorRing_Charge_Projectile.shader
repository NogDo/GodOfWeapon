Shader "Shader Graphs/IndicatorRing_Charge_Projectile" {
	Properties {
		[NoScaleOffset] _OuterRingMap ("OuterRingMap", 2D) = "white" {}
		[NoScaleOffset] _InnerRingMap ("InnerRingMap", 2D) = "white" {}
		[HDR] _OuterRingColor ("OuterRingColor", Vector) = (1,1,1,0)
		[HDR] _InnerRingColor ("InnerRingColor", Vector) = (1,1,1,0)
		_Opacity ("Opacity", Range(0, 1)) = 1
		_Scale ("Scale", Range(0, 1)) = 1
		_InnerScale ("InnerScale", Range(0, 1)) = 1
		_HybridScaleVector ("_HybridScaleVector", Vector) = (1,1,1,1)
		[ToggleUI] _UseHybridVector ("UseHybridVector", Float) = 1
		_RotationSpeed ("RotationSpeed", Float) = 0
		_InnerRotationSpeed ("InnerRotationSpeed", Float) = 0
		_Alpha ("Alpha", Float) = 1
		[HideInInspector] _CastShadows ("_CastShadows", Float) = 0
		[HideInInspector] _Surface ("_Surface", Float) = 1
		[HideInInspector] _Blend ("_Blend", Float) = 0
		[HideInInspector] _AlphaClip ("_AlphaClip", Float) = 1
		[HideInInspector] _SrcBlend ("_SrcBlend", Float) = 1
		[HideInInspector] _DstBlend ("_DstBlend", Float) = 0
		[ToggleUI] [HideInInspector] _ZWrite ("_ZWrite", Float) = 0
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