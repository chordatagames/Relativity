using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineDrawer : MonoBehaviour 
{
	private class LineProp 
	{
		public Vector3 position;
		public float timeDilation;

		public LineProp(Vector3 position) {
			this.position = position;
			this.timeDilation = World.timeDilationFactor;
		}
	}

	// object to track
	public GameObject track;
	public float waitTime;

	private List<LineProp> lines = new List<LineProp>();
	private float count = 0;

	void Update() 
	{
		if (GameController.mode == GameController.Mode.PLAY) 
		{
			count -= Time.deltaTime;
			if (count <= 0)
			{
				lines.Add(new LineProp(track.transform.position));
				count = waitTime;
			}
		}
	}

	void OnPostRender() 
	{
		if (GameController.mode == GameController.Mode.SPAWNING) 
		{
			GL.PushMatrix();
			GL.LoadOrtho();
			GL.Begin(GL.LINES);
			//loop through all the vertices in the vertexArr array.
			for (int i = 0; i < lines.Count-1; i++)
			{
				if (lines[i].timeDilation > 1.0f) GL.Color(Color.red);
				else GL.Color(Color.green); 

				GL.Vertex(lines[i].position);
			}
			
			GL.End();
			GL.PopMatrix();
		}
	}
}
