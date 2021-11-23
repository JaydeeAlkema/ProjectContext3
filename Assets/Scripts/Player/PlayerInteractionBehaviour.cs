using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionBehaviour : MonoBehaviour
{
	[SerializeField] private GameObject hitTrigger;
	[SerializeField] private List<KeyCode> inputs = new List<KeyCode>();

	private PlayerAnimationBehaviour playerAnimationBehaviour;

	private void Start()
	{
		playerAnimationBehaviour = GetComponentInChildren<PlayerAnimationBehaviour>();
	}

	private void Update()
	{
		CheckForCorrectInputs();
	}

	private void CheckForCorrectInputs()
	{
		foreach( KeyCode keyCode in inputs )
		{
			if( Input.GetKeyDown( keyCode ) )
			{
				playerAnimationBehaviour.SetTrigger( "Shoot" );
				StartCoroutine( TriggerHitArea() );
			}
		}
	}

	public IEnumerator TriggerHitArea()
	{
		hitTrigger.SetActive( true );
		yield return new WaitForSeconds( 0.1f );
		hitTrigger.SetActive( false );
	}
}
