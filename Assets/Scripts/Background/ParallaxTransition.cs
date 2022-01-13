using UnityEngine;

[RequireComponent( typeof( BoxCollider ) )]
public class ParallaxTransition : MonoBehaviour
{
	[SerializeField] private int layerIndex = 0;
	[SerializeField] private Transform spriteTransform;
	[SerializeField] private Animator anim;

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
			anim.SetTrigger( "Transition" );
		}
	}

	public void TriggerTransitionBehaviour()
	{
		parallaxTransitionBehaviour.ToggleLayer( layerIndex );
	}

}
