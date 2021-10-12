using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMover : MonoBehaviour
{
	[SerializeField] private Vector2 moveVector;

	private void FixedUpdate()
	{
		Vector2 newPos = new Vector2( transform.position.x + moveVector.x, transform.position.y + moveVector.y );
		transform.position = newPos;
	}
}
