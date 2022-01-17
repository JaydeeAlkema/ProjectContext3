using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;

public class RuneObstacle : MonoBehaviour
{
	//TODO: Add 2 seperate sprite variables for each interaction type except dodge.

	[SerializeField] private PlayerRuneActivation runeActivation = default;
	[SerializeField] private PlayerRuneActivation.Runes defaultBehaviour;
	[SerializeField] private GameObject player = default;
	[Space]
	[SerializeField] private float activationRange = 25f;   // If player gets within this range, the rune will activate.
	[SerializeField] private bool isBarrier = default;

	private SpriteRenderer spriteRenderer;
	private Animator animator;
	private GameManager gm;
	private bool runeDone = false;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		gm = FindObjectOfType<GameManager>();

		runeActivation.gameObject.SetActive( false );
	}

	private void Update()
	{
		CheckDistance();
	}

	void CheckDistance()
	{
		if( !runeDone && ( player.transform.position - this.transform.position ).magnitude <= activationRange )
		{
			runeActivation.gameObject.SetActive( true );
			gm.slowDown = true;
		}
		if( !runeDone && player.transform.position.x >= transform.position.x )
		{
			runeActivation.rune = defaultBehaviour;
			CheckForDestroyComplete();
		}
	}

	void CheckForDestroyComplete()
	{
		switch( runeActivation.rune )
		{
			case PlayerRuneActivation.Runes.DESTROY:
				{
					animator.SetBool( "Destroyed", true );
					runeDone = true;
					runeActivation.gameObject.SetActive( false );
					runeActivation.rune = PlayerRuneActivation.Runes.NULL;
					AchievementManager.Instance.AddAchievementProgress( "ACH_AGGRESSIVE", 1 );
					//StartCoroutine( DestroyAfterAnim() );
					break;
				}

			case PlayerRuneActivation.Runes.DISABLE:
				{
					animator.SetBool( "Disabled", true );
					runeDone = true;
					runeActivation.gameObject.SetActive( false );
					runeActivation.rune = PlayerRuneActivation.Runes.NULL;
					AchievementManager.Instance.AddAchievementProgress( "ACH_PACIFIST", 1 );
					break;
				}

			case PlayerRuneActivation.Runes.DODGE:
				{
					runeDone = true;
					this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
					runeActivation.gameObject.SetActive( false );
					runeActivation.rune = PlayerRuneActivation.Runes.NULL;
					break;
				}

			default:
				break;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube( new Vector3( transform.position.x - activationRange, transform.position.y, transform.position.z ), new Vector3( 0.25f, 10, 1 ) );
	}

	IEnumerator DestroyAfterAnim()
	{

		yield return new WaitForSeconds( 2f );
		Destroy( this.gameObject );
	}
}
