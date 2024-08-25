Shader "SideFX/VAT_RigidBodyDynamicsAlbedo" {
	Properties {
		[NoScaleOffset] _Albedo ("Albedo", 2D) = "white" {}
		_Smoothness ("Smoothness", Float) = 0.5
		[Toggle(_ENABLEEMISSION)] _ENABLEEMISSION ("EnableEmission", Float) = 0
		_EmissionIntensity ("EmissionIntensity", Float) = 0
		[NoScaleOffset] _EmissionTex ("EmissionTex", 2D) = "white" {}
		[HDR] _EmissionColor ("EmissionColor", Vector) = (0,0,0,0)
		[ToggleUI] _UseFrameSegment ("UseFrameSegment", Float) = 0
		_SegmentSize ("SegmentSize", Float) = 60
		[ToggleUI] _B_autoPlayback ("Auto Playback", Float) = 1
		_gameTimeAtFirstFrame ("Game Time at First Frame", Float) = 0
		_displayFrame ("Display Frame", Float) = 22.86
		_playbackSpeed ("Playback Speed", Float) = 1
		_houdiniFPS ("Houdini FPS", Float) = 60
		[ToggleUI] _B_interpolate ("Interframe Interpolation", Float) = 0
		[ToggleUI] _B_interpolateCol ("Interpolate Color", Float) = 0
		[ToggleUI] _B_interpolateSpareCol ("Interpolate Spare Color", Float) = 0
		[ToggleUI] _B_surfaceNormals ("Support Surface Normal Maps", Float) = 1
		[ToggleUI] _B_twoSidedNorms ("Two Sided Normals", Float) = 0
		[NoScaleOffset] _posTexture ("Position Texture", 2D) = "white" {}
		[NoScaleOffset] _posTexture2 ("Position Texture 2", 2D) = "white" {}
		[NoScaleOffset] _rotTexture ("Rotation Texture", 2D) = "white" {}
		[NoScaleOffset] _colTexture ("Color Texture", 2D) = "white" {}
		[NoScaleOffset] _spareColTexture ("Spare Color Texture", 2D) = "white" {}
		[ToggleUI] _B_pscaleAreInPosA ("Piece Scales Are in Position Alpha", Float) = 1
		_globalPscaleMul ("Global Piece Scale Multiplier", Float) = 1
		[ToggleUI] _B_stretchByVel ("Stretch by Velocity", Float) = 0
		_stretchByVelAmount ("Stretch by Velocity Amount", Float) = 0
		[ToggleUI] _B_animateFirstFrame ("Animate First Frame", Float) = 0
		[Toggle(_B_LOAD_COL_TEX)] _B_LOAD_COL_TEX ("Load Color Texture", Float) = 1
		[Toggle(_B_SMOOTH_TRAJECTORIES)] _B_SMOOTH_TRAJECTORIES ("Smoothly Interpolated Trajectories", Float) = 0
		[Toggle(_B_LOAD_NORM_TEX)] _B_LOAD_NORM_TEX ("Load Surface Normal Map", Float) = 0
		_frameCount ("Frame Count", Float) = 60
		_boundMaxX ("Bound Max X", Float) = 0
		_boundMaxY ("Bound Max Y", Float) = 0
		_boundMaxZ ("Bound Max Z", Float) = 0
		_boundMinX ("Bound Min X", Float) = 0
		_boundMinY ("Bound Min Y", Float) = 0
		_boundMinZ ("Bound Min Z", Float) = 0
		[Toggle(_USEDISSOLVE)] _USEDISSOLVE ("UseDissolve", Float) = 1
		_DissolveNoiseScale ("DissolveNoiseScale", Float) = 35.9
		[HDR] _DissolveColor ("DissolveColor", Vector) = (16,11.73333,0,0)
		_DissolveStep ("DissolveStep", Float) = 0.16
		_DissolveStartFrame ("DissolveStartFrame", Float) = 1
		[Toggle(_ENABLEALBEDOMULTIPLY)] _ENABLEALBEDOMULTIPLY ("EnableAlbedoMultiply", Float) = 0
		[ToggleUI] _NotUseMaxFrame ("NotUseMaxFrame", Float) = 0
		[HDR] _AlbedoColor ("AlbedoColor", Vector) = (0,0,0,0)
		_AlbedoMaxFrame ("AlbedoMaxFrame", Float) = 60
		[NoScaleOffset] [Normal] _SampleTexture2D_72ab43ee7e5b4ff3acd5a9fb57f150dc_Texture_1 ("Texture2D", 2D) = "bump" {}
		[HideInInspector] _WorkflowMode ("_WorkflowMode", Float) = 1
		[HideInInspector] _CastShadows ("_CastShadows", Float) = 1
		[HideInInspector] _ReceiveShadows ("_ReceiveShadows", Float) = 0
		[HideInInspector] _Surface ("_Surface", Float) = 0
		[HideInInspector] _Blend ("_Blend", Float) = 0
		[HideInInspector] _AlphaClip ("_AlphaClip", Float) = 1
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