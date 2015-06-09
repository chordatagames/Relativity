using UnityEngine;
using System.Collections;
using System;

public class GridGUI : MonoBehaviour {
	public Vector2 origin;
	public Vector2 gridSize = new Vector2(20f,20f);
	public float pass { get { return Grid.gridSize; } set { pass = value; } }
	public bool grid;

	private Material lineMaterial;
	private Camera cam;
	private Vector2 start = new Vector2(0,0);

	void Start() {
		cam = GetComponent<Camera>();
	}

	void OnPostRender() {
		GLFunctionality.CreateDefaultLineMaterial(ref lineMaterial);
		GL.PushMatrix();
		lineMaterial.SetPass(0);
		GL.LoadOrtho();
		GL.LoadProjectionMatrix(cam.projectionMatrix);
		GL.modelview = cam.worldToCameraMatrix;
		GL.Begin(GL.LINES);

		// begin grid
		if (grid)
		{
			GL.Color(new Color(0.5f, 0.5f, 0.5f, 1f));

			// X axis lines
			for (float i = origin.x - (gridSize.x / 2 + pass); i <= origin.x + (gridSize.x / 2 - pass); i += pass)
			{
				GL.Vertex(new Vector3(i, cam.transform.position.y - Screen.height / 2, 0));
				GL.Vertex(new Vector3(i, cam.transform.position.y + Screen.height / 2, 0));
			}

			// Y axis lines
			for (float i = origin.y - (gridSize.y / 2 + pass); i <= origin.y + (gridSize.y / 2 - pass); i += pass)
			{
				GL.Vertex(new Vector3(cam.transform.position.x - Screen.width / 2, i, 0));
				GL.Vertex(new Vector3(cam.transform.position.x + Screen.width / 2, i, 0));
			}
			
		}
		// end grid

		// begin border - i wish there was a better way to do this.
		Rect bounds = GameController.control.level.levelBounds;
		GL.Color(Color.red);
		
		// they have to be done like this because they need to be above the grid.
		GL.Vertex(new Vector3(bounds.position.x, bounds.position.y, -1));
		GL.Vertex(new Vector3(bounds.position.x, bounds.position.y + bounds.size.y, -1));

		GL.Vertex(new Vector3(bounds.position.x, bounds.position.y + bounds.size.y, -1));
		GL.Vertex(new Vector3(bounds.position.x + bounds.size.x, bounds.position.y + bounds.size.y, -1));

		GL.Vertex(new Vector3(bounds.position.x + bounds.size.x, bounds.position.y + bounds.size.y, -1));
		GL.Vertex(new Vector3(bounds.position.x + bounds.size.x, bounds.position.y, -1));

		GL.Vertex(new Vector3(bounds.position.x + bounds.size.x, bounds.position.y, -1));
		GL.Vertex(new Vector3(bounds.position.x, bounds.position.y, -1));
		// end border

		GL.End();
		GL.PopMatrix();
	}
}