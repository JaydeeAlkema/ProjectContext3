using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField]private float mDelta = 10;
    [SerializeField]private float mSpeedRight = 3.0f;
    [SerializeField] private float mSpeedLeft = 3.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x >= Screen.width - mDelta)
        {
            transform.position += Vector3.right * Time.deltaTime * mSpeedRight;
        }
        if(Input.mousePosition.x <= 0f + mDelta)
        {
            transform.position += Vector3.left * Time.deltaTime * mSpeedLeft;
        }
    }
}
