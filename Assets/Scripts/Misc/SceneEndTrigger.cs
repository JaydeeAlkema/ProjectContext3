using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEndTrigger : MonoBehaviour
{
	[SerializeField] private Animator anim;
	private AchievementManager aM;

	private void Start()
	{
		aM = AchievementManager.Instance;
	}

	private void OnTriggerEnter( Collider other )
	{
		if( other.GetComponent<IPlayer>() != null )
		{
			aM.AddAchievementProgress( "ACH_BEATIT", 1 );
			aM.AddAchievementProgress( "ACH_PERFECTIONIST", 115 );
			StartCoroutine( FadeIn() );
		}
	}

	private IEnumerator FadeIn()
	{
		anim.SetTrigger( "Transition" );
		yield return new WaitForSeconds( 2f );
		SceneManager.LoadSceneAsync("EndingShow");
		yield return null;
	}
}
