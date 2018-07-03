Shader "Custom/HologramShader"
{
	Properties
	{
		_Texture0("Texture 0", 2D) = "white" {}
	_BaseColor("BaseColor", Color) = (0,0,0,0)
		_HologramTextureTiling("Hologram Texture Tiling", Vector) = (1,1,0,0)
		_ScrollSpeed("ScrollSpeed", Float) = 0
		_FresnelIntensity("FresnelIntensity", Float) = 0
		[HDR]_FresnelColor("FresnelColor", Color) = (0.9411765,0.6448276,0.3897059,1)
		[HideInInspector] __dirty("", Int) = 1
	}

		SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true" }
		Cull Back
		Blend One One
		CGINCLUDE
#include "UnityShaderVariables.cginc"
#include "UnityPBSLighting.cginc"
#include "Lighting.cginc"
#pragma target 3.0
		struct Input
	{
		float3 worldPos;
		float3 worldNormal;
		float4 screenPos;
	};

	uniform float4 _BaseColor;
	uniform float4 _FresnelColor;
	uniform float _FresnelIntensity;
	uniform sampler2D _Texture0;
	uniform float2 _HologramTextureTiling;
	uniform float _ScrollSpeed;

	void surf(Input i , inout SurfaceOutputStandard o)
	{
		float4 temp_output_6_0 = _BaseColor;
		o.Albedo = temp_output_6_0.rgb;
		float3 ase_worldPos = i.worldPos;
		float3 ase_worldViewDir = normalize(UnityWorldSpaceViewDir(ase_worldPos));
		float3 ase_worldNormal = i.worldNormal;
		float fresnelNDotV19 = dot(normalize(ase_worldNormal), ase_worldViewDir);
		float fresnelNode19 = (0.0 + 1.0 * pow(1.0 - fresnelNDotV19, _FresnelIntensity));
		float4 ase_screenPos = float4(i.screenPos.xyz , i.screenPos.w + 0.00000000001);
		float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
		ase_screenPosNorm.z = (UNITY_NEAR_CLIP_VALUE >= 0) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
		float mulTime15 = _Time.y * _ScrollSpeed;
		float4 tex2DNode5 = tex2D(_Texture0, (ase_screenPosNorm*float4(_HologramTextureTiling, 0.0 , 0.0) + mulTime15).xy);
		o.Emission = ((_FresnelColor * fresnelNode19) + (_BaseColor * (1.0 - tex2DNode5))).rgb;
		o.Alpha = tex2DNode5.r;
	}

	ENDCG
		CGPROGRAM
#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
	{
		Name "ShadowCaster"
		Tags{ "LightMode" = "ShadowCaster" }
		ZWrite On
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 3.0
#pragma multi_compile_shadowcaster
#pragma multi_compile UNITY_PASS_SHADOWCASTER
#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
#include "HLSLSupport.cginc"
#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
#define CAN_SKIP_VPOS
#endif
#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "UnityPBSLighting.cginc"
		sampler3D _DitherMaskLOD;
	struct v2f
	{
		V2F_SHADOW_CASTER;
		float3 worldPos : TEXCOORD1;
		float4 screenPos : TEXCOORD2;
		float3 worldNormal : TEXCOORD3;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};
	v2f vert(appdata_full v)
	{
		v2f o;
		UNITY_SETUP_INSTANCE_ID(v);
		UNITY_INITIALIZE_OUTPUT(v2f, o);
		UNITY_TRANSFER_INSTANCE_ID(v, o);
		float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
		fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
		o.worldNormal = worldNormal;
		o.worldPos = worldPos;
		TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
			o.screenPos = ComputeScreenPos(o.pos);
		return o;
	}
	fixed4 frag(v2f IN
#if !defined( CAN_SKIP_VPOS )
		, UNITY_VPOS_TYPE vpos : VPOS
#endif
	) : SV_Target
	{
		UNITY_SETUP_INSTANCE_ID(IN);
	Input surfIN;
	UNITY_INITIALIZE_OUTPUT(Input, surfIN);
	float3 worldPos = IN.worldPos;
	fixed3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
	surfIN.worldPos = worldPos;
	surfIN.worldNormal = IN.worldNormal;
	surfIN.screenPos = IN.screenPos;
	SurfaceOutputStandard o;
	UNITY_INITIALIZE_OUTPUT(SurfaceOutputStandard, o)
		surf(surfIN, o);
#if defined( CAN_SKIP_VPOS )
	float2 vpos = IN.pos;
#endif
	half alphaRef = tex3D(_DitherMaskLOD, float3(vpos.xy * 0.25, o.Alpha * 0.9375)).a;
	clip(alphaRef - 0.01);
	SHADOW_CASTER_FRAGMENT(IN)
	}
		ENDCG
	}
	}
		Fallback "Diffuse"
		CustomEditor "ASEMaterialInspector"
}