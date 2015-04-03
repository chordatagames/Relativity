using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public RectTransform timePanel;

	public Mode mode;
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
	
	public void EnterSpawnMode()
	{
		mode = Mode.SPAWNING;
		Time.timeScale = 0;
	}
	public void EnterPlayMode()
	{
		mode = Mode.PLAY;
		Time.timeScale = 1;
	}
	public void EnterBulldozerMode()
	{
		mode = Mode.REMOVING;
		Time.timeScale = 0;
	}
	public void EnterTransformMode()
	{
		mode = Mode.TRANSFORMING;
		Time.timeScale = 0;
	}

	void Update ()
	{
		timePanel.transform.FindChild ("World Time").GetComponent<Text> ().text = World.time.ToString();
		timePanel.transform.FindChild ("Local Time").GetComponent<Text> ().text = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().time.ToString();
		World.PassTime();
	}
}
