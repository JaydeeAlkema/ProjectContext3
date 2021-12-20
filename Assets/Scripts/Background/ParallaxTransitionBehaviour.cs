using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTransitionBehaviour : MonoBehaviour
{
	[SerializeField] GameObject[] layers;

	public void ToggleLayer( int index )
	{
		for( int i = 0; i < layers.Length; i++ )
		{
			if( i != index )
			{
				layers[i].SetActive( false );
			}
		}
	}
}
