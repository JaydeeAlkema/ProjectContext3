using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTransitionBehaviour : MonoBehaviour
{
	[SerializeField] GameObject[] layers;

	private void Start()
	{
		ToggleLayer( 0 );
	}

	public void ToggleLayer( int index )
	{
		for( int i = 0; i < layers.Length; i++ )
		{
			if( i != index )
			{
				layers[i].SetActive( false );
			}
			else
			{
				layers[i].SetActive( true );
			}
		}
	}
}
