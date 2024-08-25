Shader "Nokdef/UberFX" {
	Properties {
		[HideInInspector] _EmissionColor ("Emission Color", Vector) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff ("Alpha Cutoff ", Range(0, 1)) = 0.5
		[ASEBegin] [Enum(UnityEngine.Rendering.BlendMode)] _SourceBlendRGB ("Blend Mode", Float) = 10
		_MainTex ("Main Texture", 2D) = "white" {}
		_MainTextureChannel ("Main Texture Channel", Vector) = (1,1,1,0)
		_MainAlphaChannel ("Main Alpha Channel", Vector) = (0,0,0,1)
		_MainTexturePanning ("Main Texture Panning ", Vector) = (0.2522222,0,0,0)
		_Desaturate ("Desaturate? ", Range(0, 1)) = 0
		[Toggle(_USESOFTALPHA_ON)] _UseSoftAlpha ("Use Soft Particles?", Float) = 0
		_SoftFadeFactor ("Soft Fade Factor", Range(0.1, 1)) = 0.1
		[Toggle(_USEALPHAOVERRIDE_ON)] _UseAlphaOverride ("Use Alpha Override", Float) = 0
		_AlphaOverride ("Alpha Override", 2D) = "white" {}
		_AlphaOverrideChannel ("Alpha Override Channel", Vector) = (0,0,0,1)
		_AlphaOverridePanning ("Alpha Override Panning", Vector) = (0,0,0,0)
		_DetailNoise ("Detail Noise", 2D) = "white" {}
		_DetailNoisePanning ("Detail Noise Panning", Vector) = (0.2,0,0,0)
		_DetailDistortionChannel ("Detail Distortion Channel", Vector) = (0,0,0,1)
		_DistortionIntensity ("Distortion Intensity", Range(0, 3)) = 2
		_DetailMultiplyChannel ("Detail Multiply Channel", Vector) = (1,1,1,0)
		_MultiplyNoiseDesaturation ("Multiply Noise Desaturation", Range(0, 1)) = 1
		_DetailAdditiveChannel ("Detail Additive Channel", Vector) = (0,0,0,1)
		_DetailDisolveChannel ("Detail Disolve Channel", Vector) = (0,0,0,1)
		[Toggle(_USERAMP_ON)] _UseRamp ("Use Color Ramping?", Float) = 0
		_MiddlePointPos ("Middle Point Position", Range(0, 1)) = 0.5
		_WhiteColor ("Highs", Vector) = (1,0.8950032,0,0)
		_MidColor ("Middles", Vector) = (1,0.4447915,0,0)
		_LastColor ("Lows", Vector) = (1,0,0,0)
		[Toggle(_USEUVOFFSET_ON)] _UseUVOffset ("Use UV Offset", Float) = 0
		[Toggle(_FRESNEL_ON)] _Fresnel ("Fresnel", Float) = 0
		_FresnelPower ("Fresnel Power", Float) = 1
		_FresnelScale ("Fresnel Scale", Float) = 1
		[HDR] _FresnelColor ("Fresnel Color", Vector) = (1,1,1,1)
		[ASEEnd] [Toggle(_DISABLEEROSION_ON)] _DisableErosion ("Disable Erosion", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Hidden/InternalErrorShader"
	//CustomEditor "UnityEditor.ShaderGraph.PBRMasterGUI"
}