using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObstacle : MonoBehaviour, IObstacle
{
	[SerializeField] private LayerMask interactionLayer = default;

	public void OnHit()
	{

	}

	private void OnTriggerEnter( Collider other )
	{
		if( other.GetComponent<PlayerMovementBehaviour>() != null )
		{
			other.GetComponent<PlayerMovementBehaviour>()?.BlinkSprite();
			OnHit();
		}
	}
}
