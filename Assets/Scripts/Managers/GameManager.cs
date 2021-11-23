using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private int TargetFPS = 60;
	[SerializeField] private GameObject tutorialObject;

	//TODO: Check for device refreshrate instead of setting our own target frame rate.
	void Awake()
	{
		Application.targetFrameRate = TargetFPS;
		if(PlayerPrefs.GetInt( "firstTime", 0) == 0)
		{
			PlayerPrefs.SetInt( "firstTime", 1 );
			//show tutorial
			tutorialObject.SetActive( true );
			Time.timeScale = 0;
		}
	}

	public void ContinueGame()
	{
		Time.timeScale = 1;
		tutorialObject.SetActive( false );
	}
}
