Shader "Shader Graphs/UberCharacter" {
	Properties {
		[NoScaleOffset] _MainTex ("MainTex", 2D) = "white" {}
		_Color ("Color", Vector) = (1,1,1,0)
		_Smoothness ("Smoothness", Float) = 0.15
		_Flash ("Flash", Float) = 0
		_MainColor ("MainColor", Vector) = (0.75,0.7323113,0.7323113,0)
		[NoScaleOffset] [Normal] _NormalMap ("NormalMap", 2D) = "bump" {}
		[NoScaleOffset] _MetallicTex ("MetallicTex", 2D) = "white" {}
		_MetallicStrength ("MetallicStrength", Range(0, 10)) = 0
		[ToggleUI] _EnableRoughnessTex ("EnableRoughnessTex", Float) = 0
		[NoScaleOffset] _RoughnessTex ("RoughnessTex", 2D) = "white" {}
		[NoScaleOffset] _EmissionTex ("EmissionTex", 2D) = "black" {}
		_EmissionIntensity ("EmissionIntensity", Float) = 0
		[Toggle(_ENABLEEMISSIONNOISE)] _ENABLEEMISSIONNOISE ("EnableEmissionNoise", Float) = 0
		[NoScaleOffset] _NoiseTex ("NoiseTex", 2D) = "white" {}
		[ToggleUI] _IsEmissionNoiseEnding ("IsEmissionNoiseEnding", Float) = 0
		_FlickerValue ("FlickerValue", Float) = 1
		_NoiseEmissionPower ("NoiseEmissionPower", Float) = 1
		[HDR] _EmissionNoiseColor ("EmissionNoiseColor", Vector) = (1,1,1,0)
		_NoiseSpeed ("NoiseSpeed", Float) = 0.5
		_Brightness ("Brightness", Range(0, 2)) = 1
		_Alpha ("Alpha", Float) = 1
		[NoScaleOffset] _Noise2Texture ("Noise2Texture", 2D) = "white" {}
		[Toggle(_ENABLEEMISSIONNOISE2)] _ENABLEEMISSIONNOISE2 ("EnableEmissionNoise2", Float) = 0
		[HDR] _EmissionNoise2Color ("EmissionNoise2Color", Vector) = (0,0,0,0)
		_Noise2Threshold ("Noise2Threshold", Float) = 0
		_Noise2Distortion ("Noise2Distortion", Float) = 50
		_EmissionNoise2Power ("EmissionNoise2Power", Float) = 0
		_EmissionNoise2Speed ("EmissionNoise2Speed", Float) = 0
		[ToggleUI] _EnableSmoothControl ("EnableSmoothControl", Float) = 0
		_FresnelStrength ("FresnelStrength", Float) = 5
		_SmoothContorlIntensity ("SmoothContorlIntensity", Float) = 1
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