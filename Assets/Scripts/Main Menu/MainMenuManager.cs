using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
	public GameObject LevelSelectButton;
	public GameObject QuitGameButton;
	public GameObject MainMenuButton;

	private UIScreen currentScreen;

	void Start ()
	{
		currentScreen = UIScreen.MainMenu;
	}

	void Update ()
	{
		if (currentScreen == UIScreen.MainMenu)
		{
			LevelSelectButton.SetActive(true);
			QuitGameButton.SetActive(true);
			MainMenuButton.SetActive(false);
		}

		else if (currentScreen == UIScreen.LevelSelect)
		{
			LevelSelectButton.SetActive(false);
			QuitGameButton.SetActive(false);
			MainMenuButton.SetActive(true);
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
		Application.Quit ();
	}

	public enum UIScreen
	{
		MainMenu,
		LevelSelect
	}
}