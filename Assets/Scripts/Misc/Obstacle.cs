using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField] private PlayerRuneActivation runeActivation = default;
	[SerializeField] private float minDistance = 25f;
	[SerializeField] private GameObject player = default;

	private void FixedUpdate()
	{
		CheckDistance();
		CheckForDestroyComplete();
	}

	void CheckDistance(){
		if((player.transform.position - this.transform.position).magnitude <= minDistance){
			runeActivation.gameObject.SetActive( true );
		}
	}

	void CheckForDestroyComplete(){
		if(runeActivation.rune == PlayerRuneActivation.Runes.DESTROY){
			Destroy( this.gameObject );
			runeActivation.gameObject.SetActive( false );
		}

		if(runeActivation.rune == PlayerRuneActivation.Runes.DISABLE){
			this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
			runeActivation.gameObject.SetActive( false );
		}
	}
}
