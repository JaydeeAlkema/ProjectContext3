using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCam : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private Vector3 offset;
	[SerializeField] private float smoothing;

	public bool clamp;
	public bool clampY = false;

	public float yPos = default;
	public float xPos = default;
	//Vaste clamp Positie
	public float yClampPos = default;
	public float xClampPos = default;

	private Vector3 desiredPos;
	private Vector3 smoothedPos;
	private Vector3 velocity;

	private void FixedUpdate()
	{
		if( !clampY )
		{
			yPos = target.position.y;
		}
		else
		{
			// in het geval van clamp pas positie aan naar value
			yPos = yClampPos;
		}

		if( !clamp )
		{
			xPos = target.position.x;
		}
		else
		{
			// in het geval van clamp pas positie aan naar value
			xPos = xClampPos;
		}

		if( target )
		{
			desiredPos = new Vector3( offset.x + xPos, offset.y + yPos, offset.z );
			smoothedPos = Vector3.SmoothDamp( transform.position, desiredPos, ref velocity, smoothing );

			transform.position = smoothedPos;
		}
	}
}
