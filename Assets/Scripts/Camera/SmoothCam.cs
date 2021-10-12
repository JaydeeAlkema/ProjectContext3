using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCam : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private Vector3 offset;
	[SerializeField] private float smoothing;

	public bool clamp;
	public bool clampY = true;

	public float yPos = default;
	public float xPos = default;

	private Vector3 desiredPos;
	private Vector3 smoothedPos;
	private Vector3 velocity;

	private void FixedUpdate()
	{
		if (!clampY)
		{
			yPos = target.position.y;
		}

		if(!clamp)
		{ 
			xPos = target.position.x;
		}

		if( target )
		{
			desiredPos = new Vector3( offset.x + xPos, offset.y + yPos, offset.z );
			smoothedPos = Vector3.SmoothDamp( transform.position, desiredPos, ref velocity, smoothing );

			transform.position = smoothedPos;
		}
	}
}
