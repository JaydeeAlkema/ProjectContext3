using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectClickable : MonoBehaviour
{

	private Vector3 startingScale = new Vector3();

	private void Awake()
	{
		startingScale = transform.localScale;
	}

	private void OnMouseDown()
	{
		transform.localScale = startingScale * 1.05f;
	}

	private void OnMouseUp()
	{
		transform.localScale = startingScale;

	}
}
