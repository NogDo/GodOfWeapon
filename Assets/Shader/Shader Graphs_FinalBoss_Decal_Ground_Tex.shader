Shader "Shader Graphs/FinalBoss_Decal_Ground_Tex" {
	Properties {
		[NoScaleOffset] _MainTex ("MainTex", 2D) = "white" {}
		_Tilling ("Tilling", Vector) = (1,1,0,0)
		_Offset ("Offset", Vector) = (0,0,0,0)
		_MainTexRotate ("MainTexRotate", Float) = 0
		_BaseColor ("BaseColor", Vector) = (0,0,0,0)
		_Opacity ("Opacity", Float) = 0
		_Smoothness ("Smoothness", Float) = 0
		_Metallic ("Metallic", Float) = 0
		[ToggleUI] _EnableEmission ("EnableEmission", Float) = 0
		_EmissTex ("EmissTex", 2D) = "white" {}
		_EmissionIntensity ("EmissionIntensity", Float) = 0
		[HDR] _EmissColor ("EmissColor", Vector) = (0,0,0,0)
		[ToggleUI] _EnableRim ("EnableRim", Float) = 0
		[HDR] _RimColor ("RimColor", Vector) = (1,0.7254902,0,0)
		_RimPower ("RimPower", Float) = 1
		[ToggleUI] _EnableHybridVector ("EnableHybridVector", Float) = 0
		_HybridVector4 ("HybridVector4", Vector) = (0,0,0,0)
		[HideInInspector] _DrawOrder ("Draw Order", Range(-50, 50)) = 0
		[Enum(Depth Bias, 0, View Bias, 1)] [HideInInspector] _DecalMeshBiasType ("DecalMesh BiasType", Float) = 0
		[HideInInspector] _DecalMeshDepthBias ("DecalMesh DepthBias", Float) = 0
		[HideInInspector] _DecalMeshViewBias ("DecalMesh ViewBias", Float) = 0
		[HideInInspector] _DecalAngleFadeSupported ("Decal Angle Fade Supported", Float) = 1
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
	Fallback "Hidden/Shader Graph/FallbackError"
	//CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
}