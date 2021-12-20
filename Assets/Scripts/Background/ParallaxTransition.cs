using UnityEngine;

[RequireComponent( typeof( BoxCollider ) )]
public class ParallaxTransition : MonoBehaviour
{
	[SerializeField] private int layerIndex = 0;

	ParallaxTransitionBehaviour parallaxTransitionBehaviour;

	private void Awake()
	{
		transform.position = new Vector3( transform.position.x, transform.position.y, 0f );
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
