Shader "Shader Graphs/AnimeProp_Rim" {
	Properties {
		[NoScaleOffset] _MainTex ("MainTex", 2D) = "white" {}
		_Color ("Color", Vector) = (1,1,1,0)
		[NoScaleOffset] _MetallicTex ("MetallicTex", 2D) = "linearGrey" {}
		_MetallicStrength ("MetallicStrength", Range(0, 1)) = 1
		[NoScaleOffset] _RoughnessTex ("RoughnessTex", 2D) = "white" {}
		_RoughnessStrength ("RoughnessStrength", Range(0, 1)) = 1
		_GlossStrength ("GlossStrength", Range(0, 1)) = 1
		[NoScaleOffset] [Normal] _NormalTex ("NormalTex", 2D) = "bump" {}
		_NormalStrength ("NormalStrength", Float) = 1
		[ToggleUI] _EnableRim ("EnableRim", Float) = 0
		[HDR] _RimColor ("RimColor", Vector) = (1,0.7254902,0,0)
		_RimStrenght ("RimStrenght", Float) = 1
		_RimFrequency ("RimFrequency", Float) = 0
		_RimMinPower ("RimMinPower", Float) = 1
		[ToggleUI] _EnableEmission ("EnableEmission", Float) = 0
		[ToggleUI] _UseEmissionGradient ("UseEmissionGradient", Float) = 0
		[NoScaleOffset] _EmissionTex ("EmissionTex", 2D) = "white" {}
		[NoScaleOffset] _EmissionGradientTex ("EmissionGradientTex", 2D) = "white" {}
		[HDR] _EmissionColor ("EmissionColor", Vector) = (0,0,0,0)
		_EmissionIntensity ("EmissionIntensity", Float) = 1
		_EmissionSpeed ("EmissionSpeed", Float) = 1
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