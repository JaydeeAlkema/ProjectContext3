using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObstacle : MonoBehaviour, IObstacle
{
	[SerializeField] private LayerMask interactionLayer = default;

	public void OnHit()
	{
		throw new System.NotImplementedException();
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		OnHit();
	}
}
