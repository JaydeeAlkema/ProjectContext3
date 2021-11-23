using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField] private PlayerRuneActivation runeActivation = default;
	[SerializeField] private PlayerRuneActivation.Runes defaultBehaviour;
	[SerializeField] private GameObject player = default;
	[Space]
	[SerializeField] private float activationRange = 25f;   // If player gets within this range, the rune will activate.

	private void Start()
	{
		runeActivation.gameObject.SetActive( false );
	}

	private void Update()
	{
		CheckDistance();
		CheckForDestroyComplete();
	}

	void CheckDistance()
	{
		if( ( player.transform.position - this.transform.position ).magnitude <= activationRange )
		{
			runeActivation.gameObject.SetActive( true );
		}
	}

	void CheckForDestroyComplete()
	{
		if( runeActivation.rune == PlayerRuneActivation.Runes.DESTROY )
		{
			runeActivation.gameObject.SetActive( false );
			Destroy( this.gameObject );
		}

		if( runeActivation.rune == PlayerRuneActivation.Runes.DISABLE )
		{
			this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
			runeActivation.gameObject.SetActive( false );
		}
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		runeActivation.rune = defaultBehaviour;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube( new Vector3( transform.position.x - activationRange, transform.position.y, transform.position.z ), new Vector3( 0.25f, 10, 1 ) );
	}
}
