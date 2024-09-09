Shader "Shader Graphs/Hovl_UberShader" {
	Properties {
		[NoScaleOffset] _MainTexture ("Main Texture", 2D) = "white" {}
		_MainTexTiling ("MainTex Tiling", Vector) = (1,1,0,0)
		[NoScaleOffset] _Noise ("Noise", 2D) = "white" {}
		_NoiseTiling ("Noise Tiling", Vector) = (1,1,0,0)
		[NoScaleOffset] _Flow ("Flow", 2D) = "white" {}
		_FlowTiling ("Flow Tiling", Vector) = (1,1,0,0)
		[NoScaleOffset] _Mask ("Mask", 2D) = "white" {}
		_MaskTiling ("Mask Tiling", Vector) = (1,1,0,0)
		_MainTexSpeedXY ("Main Tex Speed XY", Vector) = (0,0,0,0)
		_DistortionSpeedXYPowerZ ("Distortion Speed XY Power Z", Vector) = (0,0,0,0)
		_NoiseSpeedXYPowerZ ("Noise Speed XY Power Z", Vector) = (0,0,1,0)
		_NoiseOpacityLerp ("Noise Opacity Lerp", Range(0, 1)) = 0
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