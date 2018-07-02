Shader "Custom/TestShaderPortal" {
	
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		Zwrite off

		ColorMask 0

		Cull off

		Stencil{
			Ref 1
			Pass replace
		}	
		Pass{
		}
	}
}
