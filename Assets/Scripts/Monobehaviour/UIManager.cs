using UnityEngine;
using System;
using System.Collections;

public class UIManager : MonoBehaviour
{
	public void EnterMode (string _mode)
	{
		GameController.Mode prevMode = GameController.mode;
		GameController.mode = (GameController.Mode) Enum.Parse(typeof(GameController.Mode), _mode);
		if (GameController.mode != GameController.Mode.PLAY)
		{
			if (prevMode == GameController.Mode.PLAY)
			{
				GameController.control.PauseAndReset ();
			}
		}
		else
		{
			GameController.control.UnpauseGame ();
		}
	}
	
	public void TogglePlayMode()
	{
		if (GameController.control.InPlayMode)
		{
			WorldScene.Instance.ResetValues();
			EnterMode ("POSITIONING");
		}
		else
		{
			WorldScene.Instance.SetValues();
			EnterMode("PLAY");
		}
	}
}