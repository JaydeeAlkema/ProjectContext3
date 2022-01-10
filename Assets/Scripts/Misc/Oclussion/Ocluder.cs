using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Ocluder class is responsible for ocluding all relevant objects in the scene. With some exceptions like the player.
/// </summary>
public class Ocluder : MonoBehaviour
{
	[SerializeField] private List<GameObject> objectsToBeOcluded = new List<GameObject>();

	private void Update()
	{
		foreach( GameObject gameObject in objectsToBeOcluded )
		{
			if( gameObject.GetComponent<SpriteRenderer>() != null )
			{
				if( !ICanSee( gameObject ) )
				{
					gameObject.SetActive( false );
				}
				else if( ICanSee( gameObject ) )
				{
					gameObject.SetActive( true );
				}
			}
		}
	}

	private bool ICanSee( GameObject _object )
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes( Camera.main );
		return GeometryUtility.TestPlanesAABB( planes, _object.GetComponent<SpriteRenderer>().bounds );
	}
}
