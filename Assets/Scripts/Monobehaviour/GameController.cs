using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
	public static GameController control;

	public RectTransform timePanel;
	public Text worldTimeUI;
	public Text localTimeUI;
	public Text blackHoleCountUI;
	public Button startingModeButton;
	public List<GameObject> blackholeList;

	public static Mode mode;
	public bool InPlayMode { get { return (mode == Mode.PLAY); } }

	public GameObject blackHolePrefab;
	public Level level;

	public enum Mode
	{
		SPAWNING,
		REMOVING,
		TRANSFORMING,
		PLAY
	}

	/// <summary>
	/// Awake calls for this instance. 
	/// MonoBehaviour singleton pattern
	/// </summary>
	void Awake()
	{
		if(control == null)
		{
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if(control != this)
		{
			Destroy(gameObject);
		}
		PauseGame();
	
	}

	void Start()
	{
		startingModeButton.onClick.Invoke();
		blackholeList = new List<GameObject>();
	}

	public void PauseGame()
	{
		Time.timeScale = 0;
	}
	public void UnpauseGame()
	{
		Time.timeScale = 1;
	}

	public void TogglePlayMode()
	{
		if (InPlayMode)
		{
			WorldScene.Instance.ResetValues();
			GizmoModePosition();
		}
		else
		{
			WorldScene.Instance.SetValues();
			EnterPlayMode();
		}
	}

	public void EnterPlayMode()
	{
		mode = Mode.PLAY;
		GizmoModeNone();
		UnpauseGame();
		//SAVE POSITIONS OF BLACK HOLES
	}

	public void EnterSpawnMode()
	{
		mode = Mode.SPAWNING;
		GizmoModeNone();
		PauseGame();
	}
	public void EnterBulldozerMode()
	{
		mode = Mode.REMOVING;
		GizmoModeNone();
		PauseGame();
	}
	public void EnterTransformMode()
	{
		mode = Mode.TRANSFORMING;
		PauseGame();
	}
	
	public void GizmoModeNone()
	{
		GizmoHandle.type = GizmoHandle.GizmoType.NONE;
	}
	public void GizmoModeScale()
	{
		GizmoHandle.type = GizmoHandle.GizmoType.SCALE;
		EnterTransformMode();
	}
	public void GizmoModePosition()
	{
		GizmoHandle.type = GizmoHandle.GizmoType.POSITION;
		EnterTransformMode();
	}

	void Update ()
	{
		worldTimeUI.text = World.time.ToString();
		localTimeUI.text = GameObject.FindGameObjectWithTag("Ship").GetComponent<PlayerBehaviour>().time.ToString();
		World.PassTime();

		if(Input.GetMouseButtonDown(0))
		{
			switch(mode)
			{
			case Mode.REMOVING:
				Vector3 removalPoint = Grid.Gridify3(Camera.main.ScreenToWorldPoint(Input.mousePosition));

				// it stands for black hole ya pervs
				foreach (GameObject bh in blackholeList)
					if (bh.transform.position == removalPoint)
					{
						blackholeList.Remove(bh);
						Destroy(bh);
						blackHoleCountUI.text = blackholeList.Count.ToString();
					}
				break;

			case Mode.SPAWNING:
				if (blackholeList.Count >= level.blackholeLimit) break;
				Vector3 spawnPoint = Grid.Gridify3(Camera.main.ScreenToWorldPoint(Input.mousePosition));

				GameObject blackHole = Instantiate<GameObject>(blackHolePrefab);
				blackHole.transform.position = spawnPoint;
				blackholeList.Add(blackHole);
				blackHoleCountUI.text = blackholeList.Count.ToString();
				break;
			}
		}
	}
}
