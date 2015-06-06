﻿using UnityEngine;
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
	public Material lineMaterial;

	private List<LineProp> lines = new List<LineProp>();
	private float count = 0;
	Camera cam;

	void Start()
	{
		cam = GetComponent<Camera>();
	}
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
		else
		{
			lines.Clear();
		}
	}

	// lmao
	private void CreateLineMaterial() {
		if (!lineMaterial) {
			lineMaterial = new Material("Shader \"Lines/Colored Blended\" {" + "SubShader { Pass { " + "    Blend SrcAlpha OneMinusSrcAlpha " + "    ZWrite Off Cull Off Fog { Mode Off } " + "    BindChannels {" + "      Bind \"vertex\", vertex Bind \"color\", color }" + "} } }");
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
		}
	}

	void OnPostRender() 
	{
		//if (GameController.mode == GameController.Mode.SPAWNING) 
		//{
			CreateLineMaterial();
			GL.PushMatrix();
			lineMaterial.SetPass(0);
			GL.LoadOrtho();
			GL.LoadProjectionMatrix(cam.projectionMatrix);
			GL.modelview = cam.worldToCameraMatrix;
			GL.Begin(GL.LINES);
			GL.Color(Color.blue);

			for (int i = 1; i < lines.Count-1; i++)
			{
				GL.Color(new Color((1f/10f)*lines[i].timeDilation,1-(1f/10f)*lines[i].timeDilation,0));

				GL.Vertex(lines[i-1].position);
				GL.Vertex(lines[i].position);
			}
			
			GL.End();
			GL.PopMatrix();
		//}
	}
}