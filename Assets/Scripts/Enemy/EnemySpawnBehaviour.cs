using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBehaviour : MonoBehaviour
{
	[SerializeField] private Transform enemyTransformParent = default;
	[SerializeField] private GameObject enemyPrefab = default;

	public void SpawnEnemy( Transform spawnTransform )
	{
		GameObject newEnemyGO = Instantiate( enemyPrefab, spawnTransform.position, Quaternion.identity, enemyTransformParent );
	}
}
