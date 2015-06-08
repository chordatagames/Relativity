using UnityEngine;
using System.Collections;
using System;

public class PostLevelMenuManager : MonoBehaviour
{
	public GameObject NextLevelButton;
	public GameObject ReplayLevelButton;
	public GameObject MainMenuButton;

	public Level level;

	public void Update ()
	{
		if (level.completed)
		{
			NextLevelButton.SetActive(true);
			ReplayLevelButton.SetActive(true);
			MainMenuButton.SetActive(true);
		}
		
		if (!level.completed)
		{
			NextLevelButton.SetActive(false);
			ReplayLevelButton.SetActive(false);
			MainMenuButton.SetActive(false);
		}
	}

	public void ReplayLevelButtonClicked ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void NextLevelButtonClicked ()
	{
		int currentLevelNumber = Convert.ToInt32(Application.loadedLevelName.Remove(0, 6));
		int nextLevelNumber = currentLevelNumber + 1;
		Application.LoadLevel ("Level " + nextLevelNumber.ToString());
	}
	
	public void MainMenuButtonClicked ()
	{
		Application.LoadLevel ("Menu");
	}
}