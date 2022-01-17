using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEndTrigger : MonoBehaviour
{
	[SerializeField] private Animator anim;

	private void OnTriggerEnter( Collider other )
	{
		if( other.GetComponent<IPlayer>() != null )
		{
			StartCoroutine( FadeIn() );
		}
	}

	private IEnumerator FadeIn()
	{
		anim.SetTrigger( "Transition" );
		yield return new WaitForSeconds( 2f );
		SceneManager.LoadSceneAsync( SceneManager.GetActiveScene().buildIndex + 1 );
		yield return null;
	}
}
