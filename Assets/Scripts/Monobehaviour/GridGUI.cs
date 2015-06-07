using UnityEngine;
using System.Collections;

public class GridGUI : MonoBehaviour {
	public Vector2 origin;
	public Vector2 gridSize = new Vector2(20f,20f);
	public float pass { get { return Grid.gridSize; } set { pass = value; } }

	private Material lineMaterial;
	private Camera cam;
	private Vector2 start = new Vector2(0,0);

	private void CreateLineMaterial() {
		if (!lineMaterial) {
			lineMaterial = new Material("Shader \"Lines/Colored Blended\" {" + "SubShader { Pass { " + "    Blend SrcAlpha OneMinusSrcAlpha " + "    ZWrite Off Cull Off Fog { Mode Off } " + "    BindChannels {" + "      Bind \"vertex\", vertex Bind \"color\", color }" + "} } }");
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
		}
	}

	void Start() {
		cam = GetComponent<Camera>();
	}

	void OnPostRender() {
		CreateLineMaterial();
		GL.PushMatrix();
		lineMaterial.SetPass(0);
		GL.LoadOrtho();
		GL.LoadProjectionMatrix(cam.projectionMatrix);
		GL.modelview = cam.worldToCameraMatrix;
		GL.Begin(GL.LINES);
		GL.Color(Color.gray);

		// X axis lines
		for(float i = origin.x-(gridSize.x/2+pass); i <= origin.x+(gridSize.x/2-pass); i += pass)
		{
			GL.Vertex(new Vector3(i,cam.transform.position.y-Screen.height/2,0));
			GL.Vertex(new Vector3(i,cam.transform.position.y+Screen.height/2,0));
		}
		
		// Y axis lines
		for(float i = origin.y-(gridSize.y/2+pass); i <= origin.y+(gridSize.y/2-pass); i += pass)
		{
			GL.Vertex(new Vector3(cam.transform.position.x-Screen.width/2,i,0));
			GL.Vertex(new Vector3(cam.transform.position.x+Screen.width/2,i,0));
		}

		GL.End();
		GL.PopMatrix();
	}
}