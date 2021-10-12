using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCam : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private Vector3 offset;
	[SerializeField] private float smoothing;

	[SerializeField] private bool clamp;
	public bool clampY = true;

	public float yPos = default;

	private Vector3 desiredPos;
	private Vector3 smoothedPos;
	private Vector3 velocity;

	private void FixedUpdate()
	{
		if (!clampY)
		{
			yPos = target.position.y;
		}

		if( target )
		{
			desiredPos = new Vector3( offset.x + target.position.x, offset.y + yPos, offset.z );
			smoothedPos = Vector3.SmoothDamp( transform.position, desiredPos, ref velocity, smoothing );

			transform.position = smoothedPos;
		}
	}
}
