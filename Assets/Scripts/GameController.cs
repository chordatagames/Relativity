using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public RectTransform timePanel;

	public static Mode mode;
	public bool InPlayMode { get { return (mode == Mode.PLAY); } }
	
	public enum Mode
	{
		SPAWNING,
		REMOVING,
		TRANSFORMING,
		PLAY
	}
	
	void Start()
	{
		EnterSpawnMode();
	}

	public void PauseGame()
	{
		Time.timeScale = 0;
	}
	public void UnpauseGame()
	{
		Time.timeScale = 1;
	}

	public void EnterSpawnMode()
	{
		mode = Mode.SPAWNING;
		PauseGame();
	}
	public void EnterPlayMode()
	{
		mode = Mode.PLAY;
		UnpauseGame();
	}
	public void EnterBulldozerMode()
	{
		mode = Mode.REMOVING;
		PauseGame();
	}
	public void EnterTransformMode()
	{
		mode = Mode.TRANSFORMING;
		PauseGame();
	}
	public void GizmoModeScale()
	{
		GizmoHandle.type = GizmoHandle.GizmoType.Scale;
		EnterTransformMode();
	}
	public void GizmoModePosition()
	{
		GizmoHandle.type = GizmoHandle.GizmoType.Position;
		EnterTransformMode();
	}

	void Update ()
	{
		timePanel.transform.FindChild ("World Time").GetComponent<Text> ().text = World.time.ToString();
		timePanel.transform.FindChild ("Local Time").GetComponent<Text> ().text = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().time.ToString();
		World.PassTime();
	}
}
