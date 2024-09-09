Shader "Shader Graphs/CellHighlightShader" {
	Properties {
		[NoScaleOffset] _MainTex ("MainTex", 2D) = "white" {}
		[NoScaleOffset] _BlurTex ("BlurTex", 2D) = "white" {}
		[HDR] _Color ("Color", Vector) = (1,1,1,0)
		[ToggleUI] _EnableFlicker ("EnableFlicker", Float) = 0
		_FlickerSpeed ("FlickerSpeed", Float) = 1
		_FlickerRange ("FlickerRange", Vector) = (0.75,1,0,0)
		_FlickerAlphaRange ("FlickerAlphaRange", Vector) = (0,0.5,0,0)
		[ToggleUI] _IsCombine ("IsCombine", Float) = 0
		[HDR] _ColorCombine ("ColorCombine", Vector) = (0,0,0,0)
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