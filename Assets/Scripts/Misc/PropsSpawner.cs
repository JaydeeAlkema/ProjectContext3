using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsSpawner : MonoBehaviour
{
	[SerializeField] private List<SpawnGroup> spawnGroups = new List<SpawnGroup>();

	private int previousPropIndex;

	private void Start()
	{
		StartCoroutine( SpawnProps() );
	}

	private IEnumerator SpawnProps()
	{
		while( true )
		{
			foreach( SpawnGroup spawnGroup in spawnGroups )
			{
				int propIndex = Random.Range( 0, spawnGroup.propsToSpawn.Count );

				if( !spawnGroup.allowDuplicateSpawns )
				{
					while( propIndex == previousPropIndex )
					{
						propIndex = Random.Range( 0, spawnGroup.propsToSpawn.Count );
					}
				}

				GameObject propToSpawn = Instantiate( spawnGroup.propsToSpawn[propIndex],
				new Vector2( spawnGroup.spawnLocation.position.x - Random.Range( -spawnGroup.spawnOffset.x, spawnGroup.spawnOffset.x ),
							 spawnGroup.spawnLocation.position.y - Random.Range( -spawnGroup.spawnOffset.y, spawnGroup.spawnOffset.y ) ),
				Quaternion.identity,
				this.transform );

				previousPropIndex = propIndex;
				Destroy( propToSpawn, spawnGroup.propLifeTime );

				yield return new WaitForSeconds( Random.Range( spawnGroup.minTimeBetweenSpawns, spawnGroup.maxTimeBetweenSpawns ) );
			}
			yield return null;
		}
	}
}

[System.Serializable]
public struct SpawnGroup
{
	public string name;
	public Transform spawnLocation;
	public List<GameObject> propsToSpawn;
	public float minTimeBetweenSpawns, maxTimeBetweenSpawns;
	public Vector2 spawnOffset;
	public float propLifeTime;
	public bool allowDuplicateSpawns;
}
