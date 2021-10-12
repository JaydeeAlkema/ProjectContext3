using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Moving : EnemyBehaviour
{
	[SerializeField] private float speed = 6f;  // Speed is in Unity units per second.

	private Rigidbody2D rb2d = default;

	private void Start()
	{
		Init();
	}

	public override void Init()
	{
		rb2d = GetComponent<Rigidbody2D>();

		Vector2 vel = rb2d.velocity;
		vel.x = speed;
		rb2d.velocity = vel;

		Destroy( this.gameObject, destroyTime );
	}
}
