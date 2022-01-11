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
			if( ICanSee( gameObject ) )
			{
				gameObject.SetActive( true );
				Debug.Log( $"Can See {gameObject.name}" );
			}
			if( !ICanSee( gameObject ) )
			{
				gameObject.SetActive( false );
				Debug.Log( $"Can NOT See {gameObject.name}" );
			}
		}
	}

	private bool ICanSee( GameObject _object )
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes( Camera.main );
		if( gameObject.GetComponent<SpriteRenderer>() != null )
			return GeometryUtility.TestPlanesAABB( planes, _object.GetComponent<SpriteRenderer>().bounds );
		else if( gameObject.GetComponentInChildren<SpriteRenderer>() != null )
			return GeometryUtility.TestPlanesAABB( planes, _object.GetComponentInChildren<SpriteRenderer>().bounds );
		else
			return false;
	}
}
