using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IEnemy
{
	public float destroyTime = 5f;

	public virtual void Init() { }

	public void OnHit() { }
}
