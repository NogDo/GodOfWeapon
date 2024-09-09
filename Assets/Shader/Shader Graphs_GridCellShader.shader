Shader "Shader Graphs/GridCellShader" {
	Properties {
		[NoScaleOffset] _BackgroundTex ("BackgroundTex", 2D) = "white" {}
		[NoScaleOffset] _LinkTex ("LinkTex", 2D) = "white" {}
		[NoScaleOffset] _InnerBackgroundTex ("InnerBackgroundTex", 2D) = "white" {}
		[HDR] _Color ("Color", Vector) = (1,1,1,1)
		_InnerColor ("InnerColor", Vector) = (1,0,0,1)
		_Smoothness ("Smoothness", Float) = 0
		_LinkFlag ("LinkFlag", Vector) = (1,1,1,1)
		_LinkIntensity ("LinkIntensity", Float) = 1
		[ToggleUI] _EnableInnerShape ("EnableInnerShape", Float) = 0
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