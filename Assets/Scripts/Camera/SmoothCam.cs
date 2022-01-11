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

	public Transform Target { get => target; set => target =  value ; }
	public Vector3 Offset { get => offset; set => offset =  value ; }
	public float Smoothing { get => smoothing; set => smoothing =  value ; }
	public Vector2 MinClampVector { get => minClampVector; set => minClampVector =  value ; }
	public Vector2 MaxClampVector { get => maxClampVector; set => maxClampVector =  value ; }
	public bool ClampX { get => clampX; set => clampX =  value ; }
	public bool ClampY { get => clampY; set => clampY =  value ; }

	private void Update()
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
