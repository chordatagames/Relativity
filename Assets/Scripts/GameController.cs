using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
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

}
