using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb2d;
	[SerializeField] private float speed;
	[SerializeField] private GameObject onHitGO;

	private void OnEnable()
	{
		rb2d.velocity = new Vector2( speed, 0f );
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		collision.GetComponent<IObstacle>()?.OnHit();

		GameObject onHitGO_ = Instantiate( onHitGO, transform.position, Quaternion.identity );

		Destroy( onHitGO_, 2f );
		Destroy( gameObject );
	}
}
