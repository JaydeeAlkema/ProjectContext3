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

	private SpriteRenderer spriteRenderer;
	private ShowWarning warning;
	private Animator anim;
	private bool runeDone = false;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		warning = player.GetComponent<ShowWarning>();

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
			CheckForDestroyComplete();
		}
		if( !runeDone && player.transform.position.x >= transform.position.x )
		{
			runeActivation.rune = defaultBehaviour;
		}
	}

	void CheckForDestroyComplete()
	{
		if( runeActivation.rune == PlayerRuneActivation.Runes.DESTROY )
		{
			runeDone = true;
			anim.SetBool( "Destroyed", true );
			runeActivation.gameObject.SetActive( false );
			runeActivation.rune = PlayerRuneActivation.Runes.NULL;
			StartCoroutine( DestroyAfterAnim() );
		}

		if( runeActivation.rune == PlayerRuneActivation.Runes.DISABLE )
		{
			runeDone = true;
			anim.SetBool( "Disabled", true );
			warning.individualObstacles.Remove( warning.individualObstacles.First<GameObject>() );
			runeActivation.gameObject.SetActive( false );
			runeActivation.rune = PlayerRuneActivation.Runes.NULL;
		}

		if(runeActivation.rune == PlayerRuneActivation.Runes.DODGE)
		{
			runeDone = true;
			this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
			warning.individualObstacles.Remove( warning.individualObstacles.First<GameObject>() );
			runeActivation.gameObject.SetActive( false );
			runeActivation.rune = PlayerRuneActivation.Runes.NULL;
		}
	}

	private void OnDestroy()
	{
		warning.individualObstacles.Remove( warning.individualObstacles.First<GameObject>() );
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube( new Vector3( transform.position.x - activationRange, transform.position.y, transform.position.z ), new Vector3( 0.25f, 10, 1 ) );
	}

	IEnumerator DestroyAfterAnim(){

		yield return new WaitForSeconds( 2f );
		Destroy( this.gameObject );
	}
}
