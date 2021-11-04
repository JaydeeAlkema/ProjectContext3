using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private int TargetFPS = 60;

	//TODO: Check for device refreshrate instead of setting our own target frame rate.
	void Awake()
	{
		Application.targetFrameRate = TargetFPS;
	}
}
