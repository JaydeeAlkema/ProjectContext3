using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField]private float mDelta = 10;
    [SerializeField]private float mSpeedRight = 3.0f;
    [SerializeField] private float mSpeedLeft = 3.0f;

    private Vector3 dir = Vector3.zero;

    private SmoothCam smoothCam = default;

	private void Start()
	{
        Screen.orientation = ScreenOrientation.Landscape;
        smoothCam = GetComponent<SmoothCam>();
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        //dir.x = Input.acceleration.y;
        dir = Input.acceleration.normalized;

        if( Input.mousePosition.x >= Screen.width - mDelta || dir.normalized.x >= 0.2)
        {
            transform.position += Vector3.right * Time.deltaTime * mSpeedRight;
        }
        if( Input.mousePosition.x <= 0f + mDelta || dir.normalized.x <= -0.2)
        {
            transform.position += Vector3.right * Time.deltaTime * mSpeedLeft;
        }
    }
}
