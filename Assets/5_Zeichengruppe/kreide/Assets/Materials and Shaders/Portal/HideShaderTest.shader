﻿Shader "Custom/HideShaderTest" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		Color[_Color]

		Stencil{
			Ref 1
			Comp Equal
		}
		Pass{
		}
	}
}
