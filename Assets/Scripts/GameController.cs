using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public RectTransform timePanel;

	void Update ()
	{
		timePanel.transform.FindChild ("World Time").GetComponent<Text> ().text = World.time.ToString();
		timePanel.transform.FindChild ("Local Time").GetComponent<Text> ().text = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().time.ToString();
	}
}
