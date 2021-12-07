using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
	private void Awake()
	{
		Time.timeScale = 1;
	}

	public void GoToMenu()
	{
		SceneManager.LoadSceneAsync( "Menu" );
	}

	public void GoToGallery()
	{
		SceneManager.LoadSceneAsync( "Gallery" );
	}

	public void GoToGame()
	{
		SceneManager.LoadSceneAsync( "Game" );
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
