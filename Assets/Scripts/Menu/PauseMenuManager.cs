using UnityEngine;
using System.Collections;

public class PauseMenuManager : MonoBehaviour
{
	public GameObject PauseMenuButton;
	public GameObject ContinueButton;
	public GameObject MainMenuButton;

	private UIScreen currentScreen;

	void Update ()
	{
		if (currentScreen == UIScreen.Menu)
		{
			ContinueButton.SetActive (true);
			MainMenuButton.SetActive (true);
		}
		else
		{
			ContinueButton.SetActive (false);
			MainMenuButton.SetActive (false);
		}
	}

	public void PauseMenuButtonClicked ()
	{
		currentScreen = (currentScreen == UIScreen.Play ? UIScreen.Menu : UIScreen.Play);
	}

	public void ContinueButtonClicked ()
	{
		currentScreen = UIScreen.Play;
	}

	public void MainMenuButtonClicked ()
	{
		Application.LoadLevel ("Menu");
	}

	private enum UIScreen
	{
		Play,
		Menu
	}
}