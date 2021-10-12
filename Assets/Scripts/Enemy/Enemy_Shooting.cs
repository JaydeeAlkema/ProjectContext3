using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooting : EnemyBehaviour
{
	[SerializeField] private GameObject lazerPrefab = default;
	[SerializeField] private Transform lazerPosition = default;
	[SerializeField] private float laserDuration = 3f;

	private void Shoot()
	{
		GameObject laserGO = Instantiate( lazerPrefab, lazerPosition.position, Quaternion.identity, transform );
		Destroy( laserGO, laserDuration );
	}

}