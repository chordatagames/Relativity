using UnityEngine;
using System.Collections;

public static class GLFunctionality {

	public static void CreateDefaultLineMaterial(ref Material lineMaterial)
	{
		lineMaterial = new Material(
			@"Shader ""Lines/Colored Blended"" {
				SubShader {
					Tags { ""RenderType""=""Opaque"" }
					Pass {
						ZWrite On
						ZTest LEqual
						Cull Off
						Fog { Mode Off }
						BindChannels {
							Bind ""vertex"", vertex Bind ""color"", color
						}
					}
				}
			}"
		);
		lineMaterial.hideFlags = HideFlags.HideAndDontSave;
		lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
	}

}
