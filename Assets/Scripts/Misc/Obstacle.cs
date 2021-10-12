using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IObstacle
{
	[SerializeField] private GameObject onHitGO;

	public void OnHit()
	{
		GameObject onHitGO_ = Instantiate( onHitGO, transform.position, Quaternion.identity );

		Destroy( onHitGO_, 2f );
		Destroy( gameObject );
	}
}
