Shader "Shader Graphs/Button3DV2" {
	Properties {
		[NoScaleOffset] _MainTex ("MainTex", 2D) = "white" {}
		[ToggleUI] _UseOldMetallicTex ("UseOldMetallicTex", Float) = 0
		_Color ("Color", Vector) = (1,1,1,0)
		[NoScaleOffset] _MetallicTex ("MetallicTex", 2D) = "linearGrey" {}
		_MetallicStrength ("MetallicStrength", Range(0, 1)) = 1
		_Smoothness ("Smoothness", Range(0, 1)) = 1
		[NoScaleOffset] [Normal] _NormalTex ("NormalTex", 2D) = "bump" {}
		_NormalStrength ("NormalStrength", Float) = 1
		[Toggle(_ENABLEEMISSION)] _ENABLEEMISSION ("EnableEmission", Float) = 0
		[NoScaleOffset] _EmissionTex ("EmissionTex", 2D) = "white" {}
		[HDR] _EmissionColor ("EmissionColor", Vector) = (0,0,0,0)
		_EmissionIntensity ("EmissionIntensity", Float) = 1
		[Toggle(_USECOLORMASKING)] _USECOLORMASKING ("UseColorMasking", Float) = 0
		_Color_1 ("Color_1", Vector) = (0,0,0,0)
		[ToggleUI] _EmissionUV2 ("EmissionUV2", Float) = 0
		[ToggleUI] _EmissionGradient ("EmissionGradient", Float) = 0
		[NoScaleOffset] _EmissionGradientTex ("EmissionGradientTex", 2D) = "white" {}
		_EmissionSpeed ("EmissionSpeed", Float) = 1
		[ToggleUI] _UseColorDisable ("UseColorDisable", Float) = 0
		_ColorDisable ("ColorDisable", Vector) = (1,1,1,1)
		_MetallicStrengthDisable ("MetallicStrengthDisable", Range(0, 1)) = 0.5
		_lerpTime ("lerpTime", Range(0, 1)) = 0
		_SmoothnessDisable ("SmoothnessDisable", Range(0, 1)) = 0.2
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

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Hidden/Shader Graph/FallbackError"
	//CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
}