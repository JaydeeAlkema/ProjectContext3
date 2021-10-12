using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IEnemy
{
	[SerializeField] private float speed = 6f;  // Speed is in Unity units per second.

	private Rigidbody2D rb2d = default;

	private void Start()
	{
		Init();
	}

	/// <summary>
	/// Initialize the enemy properties.
	/// </summary>
	private void Init()
	{
		rb2d = GetComponent<Rigidbody2D>();

		Vector2 vel = rb2d.velocity;
		vel.x = speed;
		rb2d.velocity = vel;
	}

	public void OnHit() { }
}
