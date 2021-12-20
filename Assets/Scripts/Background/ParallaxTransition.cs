using UnityEngine;

[RequireComponent( typeof( BoxCollider ) )]
public class ParallaxTransition : MonoBehaviour
{
	[SerializeField] private int layerIndex;

	private ParallaxTransitionBehaviour parallaxTransitionBehaviour;

	private void Awake()
	{
		GetComponent<BoxCollider>().isTrigger = true;

		parallaxTransitionBehaviour = FindObjectOfType<ParallaxTransitionBehaviour>();
	}

	private void OnTriggerEnter( Collider other )
	{
		if( other.GetComponent<IPlayer>() != null )
		{
			parallaxTransitionBehaviour.ToggleLayer( layerIndex );
		}
	}
}
