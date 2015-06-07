using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
	public GameObject levelSelectButton;
	public GameObject quitGameButton;
	public GameObject mainMenuButton;
	public GameObject[] levelButtons;

	private UIScreen currentScreen;

	void Start ()
	{
		currentScreen = UIScreen.MainMenu;
	}

	void Update ()
	{
		if (currentScreen == UIScreen.MainMenu)
		{
			levelSelectButton.SetActive(true);
			quitGameButton.SetActive(true);
			mainMenuButton.SetActive(false);
			foreach (GameObject levelButton in levelButtons)
			{
				levelButton.SetActive(false);
			}
		}

		else if (currentScreen == UIScreen.LevelSelect)
		{
			levelSelectButton.SetActive(false);
			quitGameButton.SetActive(false);
			mainMenuButton.SetActive(true);
			foreach (GameObject levelButton in levelButtons)
			{
				levelButton.SetActive(true);
			}
		}
	}

	public void LevelSelectButtonClicked ()
	{
		currentScreen = UIScreen.LevelSelect;
	}
	
	public void MainMenuButtonClicked ()
	{
		currentScreen = UIScreen.MainMenu;
	}

	public void QuitButtonClicked ()
	{
		Application.Quit();
	}

	public void LevelButtonClicked (int level)
	{
		string levelNameString = "Level " + level.ToString ();
		Debug.Log ("Loading " + levelNameString);
		Application.LoadLevel (levelNameString);
	}

	private enum UIScreen
	{
		MainMenu,
		LevelSelect
	}
}