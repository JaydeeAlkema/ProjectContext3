using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooting : EnemyBehaviour
{
	[SerializeField] private LineRenderer lazerLine = default;
	[SerializeField] private Transform lazerPosition = default;
	[SerializeField] private float laserDuration = 3f;
	[SerializeField] private Transform attackFromPosition = default;
	[SerializeField] private Transform firePoint = default;
	[SerializeField] private GameObject player = default;

	private bool inPosition = false;
	private bool hasShot = false;

	private void Start()
	{
		lazerLine.enabled = false;
	}

	private void Update()
	{
		MoveToPosition();
		if(inPosition)
		{
			Shoot();
		}

		if(hasShot)
		{
			MoveAway();
		}
	}

	private void MoveToPosition()
	{
		if( transform.position.x <= attackFromPosition.position.x && !inPosition)
		{
			transform.position += Vector3.right * 5.0f * Time.deltaTime;
		}
		else
		{
			inPosition = true;
		}
		
	}

	private void Shoot()
	{
		lazerLine.SetPosition( 0, firePoint.position );
		lazerLine.SetPosition( 1, player.transform.position );
		lazerLine.enabled = true;
		hasShot = true;
	}

	void MoveAway()
	{
		transform.position += transform.position * -1f * Time.deltaTime;
	}

}