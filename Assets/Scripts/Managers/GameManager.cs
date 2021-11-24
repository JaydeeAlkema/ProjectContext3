using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject tutorialObject;

	void Awake()
	{
		if( PlayerPrefs.GetInt( "firstTime", 0 ) == 0 )
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
