using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooting : EnemyBehaviour
{
	[SerializeField] private LineRenderer lazerLine = default;
	[SerializeField] private Transform shootTo = default;
	[SerializeField] private float laserDuration = 3f;
	[SerializeField] private Transform attackFromPosition = default;
	[SerializeField] private Transform firePoint = default;
	[SerializeField] private GameObject player = default;

	private bool inPosition = false;
	private bool hasShot = false;

	private float dist = default;
	private float counter = default;
	[SerializeField]private float lineDrawSpeed = 6f;

	

	private void Start()
	{
		lazerLine.enabled = false;
		
	}

	private void FixedUpdate()
	{
		if( inPosition )
		{
			Shoot();
		}
		
	}

	private void Update()
	{
		MoveToPosition();


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
		else if(!inPosition)
		{
			shootTo.position = player.transform.position;
			shootTo.position += new Vector3( 1f, 0f, 0f );
			dist = Vector3.Distance( firePoint.position, shootTo.position );
			inPosition = true;
		}
		
	}

	private void Shoot()
	{
		if( counter < dist )
		{
			counter += 0.1f / lineDrawSpeed;
			float x = Mathf.Lerp( 0, dist, counter );

			Vector3 pointA = firePoint.position;
			Vector3 pointB = shootTo.position;

			Vector3 pointAlongLine = x * Vector3.Normalize( pointB - pointA ) + pointA;

			lazerLine.SetPosition( 0, firePoint.position );
			lazerLine.SetPosition( 1, pointAlongLine );
			lazerLine.enabled = true;
			StartCoroutine( WaitForShot());
		}
	}

	IEnumerator WaitForShot()
	{
		yield return new WaitForSeconds(laserDuration);
		hasShot = true;
	}

	void MoveAway()
	{
		lazerLine.enabled = false;
		transform.position += transform.position * -10f * Time.deltaTime;
	}

}