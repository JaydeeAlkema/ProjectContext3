using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{

	[SerializeField] private Animator sceneFadeAnimator;

	private void Awake()
	{
		Application.targetFrameRate = 120;
	}

	public async void LoadNextScene()
	{
		sceneFadeAnimator.SetTrigger( "Transition" );
		await Task.Delay( 2500 );
		SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 );
	}
}
