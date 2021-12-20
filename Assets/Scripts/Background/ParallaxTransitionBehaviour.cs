using UnityEngine;

public class ParallaxTransitionBehaviour : MonoBehaviour
{
	[SerializeField] private GameObject[] layers;

	public void ToggleLayer( int index )
	{
		foreach( GameObject layer in layers )
		{
			layer.SetActive( false );
		}
		layers[index].SetActive( true );
	}
}
