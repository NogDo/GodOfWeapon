Shader "Ciconia Studio/CS_Glass/URP/Glass" {
	Properties {
		[HideInInspector] _AlphaCutoff ("Alpha Cutoff ", Range(0, 1)) = 0.5
		[Space(15)] [Header(Global Properties )] [Space(10)] _TilingX ("Tiling X", Float) = 1
		_TilingY ("Tiling Y", Float) = 1
		[Space(10)] _OffsetX ("Offset X", Float) = 0
		_OffsetY ("Offset Y", Float) = 0
		[Space(15)] [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull Mode", Float) = 2
		[Enum(Off,0,On,1)] _ZWrite ("ZWrite", Float) = 1
		[Space(10)] [Header(Main Properties)] [Space(15)] _Color ("Color", Vector) = (0,0,0,0)
		[Space(10)] _MainTex ("Albedo -->(Mask A)", 2D) = "white" {}
		_Saturation ("Saturation", Float) = 0
		_Brightness ("Brightness", Range(1, 8)) = 1
		[Space(35)] _MetallicGlossMap ("Metallic(RoughA)", 2D) = "white" {}
		_Metallic ("Metallic", Range(0, 2)) = 0.2
		_Glossiness ("Smoothness", Range(0, 1)) = 0.5
		[Space(35)] _BumpMap ("Normal Map", 2D) = "bump" {}
		_BumpScale ("Scale", Float) = 0.3
		_Refraction ("Refraction", Range(0, 2)) = 1.1
		[Space(35)] _OcclusionMap ("Ambient Occlusion Map", 2D) = "white" {}
		_AoIntensity ("Ao Intensity", Range(0, 2)) = 0
		[Space(45)] [Header(Self Illumination)] [Space(15)] _Intensity ("Intensity", Range(1, 10)) = 1
		[Space(45)] [Header(Reflection Properties) ] [Space(15)] _ColorCubemap ("Color ", Vector) = (1,1,1,1)
		[HDR] _CubeMap ("Cube Map", Cube) = "black" {}
		_ReflectionIntensity ("Reflection Intensity", Float) = 1
		_BlurReflection ("Blur", Range(0, 8)) = 0
		[Space(15)] _ColorFresnel1 ("Color Fresnel", Vector) = (1,1,1,0)
		[ToggleOff(_USECUBEMAP_OFF)] _UseCubemap ("Use Cubemap", Float) = 1
		_FresnelStrength ("Fresnel Strength", Float) = 0
		_PowerFresnel ("Power", Float) = 1
		[Space(45)] [Header(Transparency Properties)] [Space(15)] _Opacity ("Opacity", Range(0, 1)) = 1
		[Space(10)] [Toggle] _UseAlbedoA1 ("Use AlbedoA", Float) = 0
		[Toggle] _InvertAlbedoA1 ("Invert", Float) = 0
		[Space(10)] [Toggle] _UseSmoothness ("Use Smoothness", Float) = 0
		[Space(10)] [Toggle] _FalloffOpacity ("Falloff Opacity", Float) = 0
		[Toggle] _Invert ("Invert", Float) = 0
		[Space(10)] _FalloffOpacityIntensity ("Falloff Intensity", Range(0, 1)) = 1
		_PowerFalloffOpacity ("Power", Float) = 1
		[Space(45)] [Header(Fade Properties)] [Space(15)] _Fade ("Fade", Range(0, 1)) = 0.2
		[Space(10)] [Toggle] _FalloffFade1 ("Exclude Decal", Float) = 0
		[Space(10)] [Toggle] _FalloffFade ("Falloff", Float) = 0
		[Toggle] _InvertFresnelFade ("Invert", Float) = 0
		[Space(10)] _GradientFade ("Falloff Intensity", Range(0, 1)) = 1
		_PowerFalloffFade ("Power", Float) = 1
		[Space(45)] [Header(Decal Properties)] [Space(15)] _ColorDecal ("Color -->(Transparency A)", Vector) = (1,1,1,1)
		[Space(10)] _DetailAlbedoMap ("Decal Map -->(Mask A)", 2D) = "black" {}
		_SaturationDecal ("Saturation", Float) = 0
		[Space(20)] _MetallicDecal ("Metallic", Range(0, 2)) = 0.2
		_GlossinessDecal ("Smoothness", Range(0, 1)) = 0.5
		_ReflectionDecal ("Reflection", Range(0, 1)) = 0
		[Space(35)] _DetailNormalMap ("Normal Map", 2D) = "bump" {}
		_BumpScaleDecal ("Scale", Range(0, 5)) = 0.1
		_BumpScaleDecal1 ("NormalBlend", Range(0, 1)) = 0
		[Space(25)] _Rotation ("Rotation", Float) = 0
		[Space(35)] [HDR] _EmissionColor ("Emission Color", Vector) = (0,0,0,0)
		_EmissiveIntensity ("Emissive Intensity", Range(0, 2)) = 1
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
	Fallback "Hidden/InternalErrorShader"
	//CustomEditor "UnityEditor.ShaderGraph.PBRMasterGUI"
}