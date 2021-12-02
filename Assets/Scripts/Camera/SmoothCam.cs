using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCam : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private Vector3 offset;
	[Space]
	[SerializeField] private float smoothing;
	[Space]
	[SerializeField] private Vector2 minClampVector;
	[SerializeField] private Vector2 maxClampVector;
	[SerializeField] private bool clampX;
	[SerializeField] private bool clampY;

	private Vector3 desiredPos;
	private Vector3 smoothedPos;
	private Vector3 velocity;

	float clampedPosX;
	float clampedPosY;

	private void FixedUpdate()
	{
		if( target )
		{
			desiredPos = new Vector3( target.position.x + offset.x, target.position.y + offset.y, offset.z );
			smoothedPos = Vector3.SmoothDamp( transform.position, desiredPos, ref velocity, smoothing );

			if( clampX ) { clampedPosX = Mathf.Clamp( smoothedPos.x, minClampVector.x, maxClampVector.x ); smoothedPos.x = clampedPosX; }
			if( clampY ) { clampedPosY = Mathf.Clamp( smoothedPos.y, minClampVector.y, maxClampVector.y ); smoothedPos.y = clampedPosY; }

			transform.position = smoothedPos;
		}
	}
}
