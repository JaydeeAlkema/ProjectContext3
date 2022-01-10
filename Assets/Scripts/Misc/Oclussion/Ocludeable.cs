using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Ocludeable class checks if the boundingbox of the collider and/or sprite is NOT within view of the player and subsequently turns off the entire object.
/// Unity has occlsion culling, but this only refers to rendering the objects out of view, any and all behaviour on the object still persists to update.
/// With this class we can be certain the ENTIRE object is effectivly non-existant when it is not relevant.
/// </summary>
public class Ocludeable : MonoBehaviour
{
	private SpriteRenderer renderer;
	private List<Component> components = new List<Component>();

	private void Start()
	{
		renderer = GetComponent<SpriteRenderer>();
		components.AddRange( GetComponents( typeof( Component ) ) );
	}

	// Update is called once per frame
	void Update()
	{
		if( renderer.isVisible )
		{

		}
	}

	private void DisableAllComponents()
	{

	}

	private void EnableAllComponents()
	{

	}

}
