Shader "Shader Graphs/AdvancedLightningShader 2" {
	Properties {
		[HDR] _Color ("Color", Vector) = (1,1,0,1)
		_LightningJitter ("LightningJitter", Float) = 4
		_NoiseOffset ("NoiseOffset", Float) = 0
		_ThicknessScale ("ThicknessScale", Range(0, 1)) = 0
		_StartValue ("StartValue", Range(0, 1)) = 0
		_MinValue ("MinValue", Range(0, 1)) = 0
		_AlphaPower ("AlphaPower", Range(0, 5)) = 1
		_Age ("Age", Range(0, 1)) = 0
		_VectorScale ("VectorScale", Vector) = (1,1,1,1)
		_Retract ("DissapearValue", Range(0, 1)) = 0
		_Throw ("ThrowValue", Range(0, 1)) = 1
		_Random ("Random", Range(-0.2, 0.2)) = 0.1
		_WidthLength ("WidthLength", Float) = 0
		_NoiseScaleVertical ("NoiseScaleVertical", Float) = 0
		_NoiseScaleVertical2 ("NoiseScaleVertical2", Float) = 0
		_NoiseScaleHorizontal ("NoiseScaleHorizontal", Float) = 0
		_NoiseScaleHorizontal2 ("NoiseScaleHorizontal2", Float) = 0
		_VerticalSpeed ("VerticalSpeed", Float) = 2.5
		_HorizontalSpeed ("HorizontalSpeed", Float) = 0.5
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