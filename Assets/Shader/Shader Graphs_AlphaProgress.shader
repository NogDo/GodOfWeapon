Shader "Shader Graphs/AlphaProgress" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		[NoScaleOffset] _Texture ("Texture", 2D) = "white" {}
		[ToggleUI] _UseSlide ("UseSlide", Float) = 0
		_FillRate ("FillRate", Range(0, 1)) = 1
		[ToggleUI] _InverseSide ("InverseSide", Float) = 1
		[ToggleUI] _UseScale ("UseScale", Float) = 0
		_Progress ("Progress", Range(0, 1)) = 0
		[ToggleUI] _UseFade ("UseFade", Float) = 0
		_Visibility ("Visibility", Range(0, 1)) = 1
		[ToggleUI] _LeftFade ("LeftFade", Float) = 0
		[ToggleUI] _RightFade ("RightFade", Float) = 0
		[ToggleUI] _TopFade ("TopFade", Float) = 0
		[ToggleUI] _BottomFade ("BottomFade", Float) = 0
		[HideInInspector] _CastShadows ("_CastShadows", Float) = 1
		[HideInInspector] _Surface ("_Surface", Float) = 1
		[HideInInspector] _Blend ("_Blend", Float) = 0
		[HideInInspector] _AlphaClip ("_AlphaClip", Float) = 0
		[HideInInspector] _SrcBlend ("_SrcBlend", Float) = 1
		[HideInInspector] _DstBlend ("_DstBlend", Float) = 0
		[ToggleUI] [HideInInspector] _ZWrite ("_ZWrite", Float) = 0
		[HideInInspector] _ZWriteControl ("_ZWriteControl", Float) = 0
		[HideInInspector] _ZTest ("_ZTest", Float) = 4
		[HideInInspector] _Cull ("_Cull", Float) = 2
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