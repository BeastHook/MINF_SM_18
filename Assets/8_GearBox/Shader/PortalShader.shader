Shader "Custom/PortalShader" {
	
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		Zwrite off

		// transparent
		ColorMask 0

		// biderictional behaviour
		Cull off

		Stencil {
			Ref 1

			// set all pixels in the portal to 1
			Pass replace
		}
		Pass {
		}
	}
}

