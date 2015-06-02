using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public static GameController control;

	public RectTransform timePanel;
	public Button startingModeButton;

	public static Mode mode;
	public bool InPlayMode { get { return (mode == Mode.PLAY); } }

	public GameObject blackHolePrefab;
	
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
		timePanel.transform.FindChild ("World Time").GetComponent<Text> ().text = World.time.ToString();
		timePanel.transform.FindChild ("Local Time").GetComponent<Text> ().text = GameObject.FindGameObjectWithTag("Ship").GetComponent<PlayerBehaviour>().time.ToString();
		World.PassTime();

		if(Input.GetMouseButtonDown(0))
		{
			switch(mode)
			{
			case Mode.REMOVING:
				
				break;
			case Mode.SPAWNING:
				Vector3 spawnPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				spawnPoint.z = 0;
				GameObject blackHole = Instantiate<GameObject>(blackHolePrefab);
				blackHole.transform.position = spawnPoint;
				break;
			}
		}

	}
}
