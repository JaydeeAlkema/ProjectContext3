using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject tutorialObject;
	[SerializeField] private float amountToSlowDown = 1;
	[SerializeField] private float slowDownSpeed = 1;
	[SerializeField] private float timeToSlowDown = 1;

	public bool slowDown = false;

	void Awake()
	{
		Application.targetFrameRate = 60;
		if( tutorialObject != null )
		{
			if( PlayerPrefs.GetInt( "firstTime") == 0 )
			{
				PlayerPrefs.SetInt( "firstTime", 1 );
				//show tutorial
				tutorialObject.SetActive( true );
				Time.timeScale = 0;
			}
		}
	}

	private void Update()
	{
		SlowDown();
	}

	public void PauseGame()
	{
		Time.timeScale = 0;
		GetComponent<AudioSource>().Pause();
	}

	public void ContinueGame()
	{
		Time.timeScale = 1;
		GetComponent<AudioSource>().Play();
	}

	public void TutorialOff()
	{
		tutorialObject.SetActive( false );
		Time.timeScale = 1;
	}

	public void SlowDown()
	{
		if( Time.timeScale >= amountToSlowDown && slowDown)
		{
			Time.timeScale -= slowDownSpeed * Time.deltaTime;
			GetComponent<AudioSource>().pitch -= slowDownSpeed * Time.deltaTime;
		}
		if(slowDown){ StartCoroutine( ResetSlowdown() ); }
	}

	public IEnumerator ResetSlowdown()
	{
		yield return new WaitForSeconds(timeToSlowDown);
		slowDown = false;
		Time.timeScale = 1f;
		GetComponent<AudioSource>().pitch = 1f;
	}
}
