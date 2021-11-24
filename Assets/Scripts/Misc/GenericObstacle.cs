using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObstacle : MonoBehaviour, IObstacle
{
	[SerializeField] private LayerMask interactionLayer = default;

	public void OnHit()
	{

	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if( collision.GetComponent<PlayerMovementBehaviour>() != null )
		{
			collision.GetComponent<PlayerMovementBehaviour>()?.BlinkSprite();
			OnHit();
		}
	}
}
