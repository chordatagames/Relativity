using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
	public static Mode mode;
	public RectTransform timePanel;
	public Text worldTimeUI;
	public Text localTimeUI;
	public Text blackHoleCountUI;
	public Button startingModeButton;

	public GameObject blackHolePrefab;

	[HideInInspector()]
	public List<GameObject> blackholeList;
	public Level level;
	
	public bool InPlayMode { get { return (mode == Mode.PLAY); } }
	
	public enum Mode
	{
		SPAWNING = 0,
		REMOVING = 1,
		POSITIONING = 2,
		SCALING = 3,
        PLAY = 4
    }
    
    public static GameController control;
	void Awake()
	{
		control = this;
		PauseGame ();
		WorldScene.Instance.ClearDataEntries();
	}

	void Start()
	{
		WorldScene.Instance.SetValues();
        startingModeButton.onClick.Invoke();

		blackholeList = new List<GameObject>();
	}

	void Update ()
	{
		worldTimeUI.text = World.time.ToString();
		localTimeUI.text = GameObject.FindGameObjectWithTag("Ship").GetComponent<PlayerBehaviour>().time.ToString();
		if (mode == Mode.PLAY)
		{
			World.PassTime();
		}

		if(Input.GetMouseButtonDown(0))
		{
			switch(mode)
			{
			case Mode.REMOVING:
				Vector3 removalPoint = Grid.Gridify3(Camera.main.ScreenToWorldPoint(Input.mousePosition));

				foreach (GameObject bh in blackholeList)
				{
					if (bh.transform.position == removalPoint)
					{
						blackholeList.Remove(bh);
						WorldScene.Instance.dataEntries.Remove((IGameEditable)bh.GetComponent<BlackHoleBehaviour>());
						Destroy(bh);
						blackHoleCountUI.text = blackholeList.Count.ToString();
					}
				}
				break;

			case Mode.SPAWNING:
				if (blackholeList.Count >= level.blackholeLimit)
				{
					break;
				}
				PointerEventData pe = new PointerEventData(EventSystem.current);
				pe.position =  Input.mousePosition;
				
				List<RaycastResult> hits = new List<RaycastResult>();
				EventSystem.current.RaycastAll( pe, hits );
				bool notUI = true;
				foreach (RaycastResult h in hits)
				{
					notUI &= (h.gameObject.layer != 5);//if all pass as other layers, notUI will remain true;
				}
				if (notUI)
				{
					Vector3 spawnPoint = Grid.Gridify3(Camera.main.ScreenToWorldPoint(Input.mousePosition));

					GameObject blackHole = Instantiate<GameObject>(blackHolePrefab);
					blackHole.transform.position = spawnPoint;
					blackholeList.Add(blackHole);
					blackHoleCountUI.text = blackholeList.Count.ToString();
				}
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
