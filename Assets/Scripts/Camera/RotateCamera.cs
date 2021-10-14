using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
	[SerializeField] private float mDelta = 10;
	[SerializeField] private float mSpeedRight = 3.0f;
	[SerializeField] private float mSpeedLeft = 3.0f;
	[SerializeField] private bool lockLeft = default;
	[SerializeField] private bool lockRight = default;

	// Update is called once per frame
	void Update()
	{
		//Max rotate value
		if( transform.rotation.y >= 0.065f )
		{
			lockRight = true;
		}
		else
		{
			lockRight = false;
		}
		if( transform.rotation.y <= -0.20f )
		{
			lockLeft = true;
		}
		else
		{
			lockLeft = false;
		}

		if( Input.mousePosition.x >= Screen.width - mDelta && !lockRight )
		{
			transform.Rotate( 0f, mSpeedRight * Time.deltaTime, 0f );
		}
		if( Input.mousePosition.x <= 0f + mDelta && !lockLeft )
		{
			transform.Rotate( 0f, mSpeedLeft * Time.deltaTime, 0f );
		}
	}

	public void ResetRotation()
	{
		transform.Rotate( 0, 0, 0 );
	}
}
