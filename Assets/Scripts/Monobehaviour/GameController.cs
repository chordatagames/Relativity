using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public static Mode mode;
	public RectTransform timePanel;
	public Text worldTimeUI;
	public Text localTimeUI;
	public Button startingModeButton;
	public GameObject blackHolePrefab;
	public Level level;
	
	public bool InPlayMode { get { return (mode == Mode.PLAY); } }
	
	// Singleton Pattern
	public static GameController control;
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
	
	public enum Mode
	{
		SPAWNING = 0,
		REMOVING = 1,
		POSITIONING = 2,
        SCALING = 3,
        PLAY = 4
    }

	void Start()
	{
		WorldScene.Instance.SetValues();
        startingModeButton.onClick.Invoke();
		worldTimeUI = timePanel.transform.FindChild ("World Time").GetComponent<Text>();
		localTimeUI = timePanel.transform.FindChild ("Local Time").GetComponent<Text>();
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
				break;

			case Mode.SPAWNING:
				Vector3 spawnPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				spawnPoint = Grid.Gridify3(spawnPoint);

				GameObject blackHole = Instantiate<GameObject>(blackHolePrefab);
				blackHole.transform.position = spawnPoint;
				break;
			}
		}
	}
	
	public void PauseAndReset ()
	{
		PauseGame ();
		WorldScene.Instance.ResetValues ();
	}
	
	public void PauseGame()
	{
		Time.timeScale = 0;
	}
	
	public void UnpauseGame()
    {
        Time.timeScale = 1;
	}
}