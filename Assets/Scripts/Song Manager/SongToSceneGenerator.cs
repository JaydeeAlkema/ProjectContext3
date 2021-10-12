// Code Snippets used from https://answers.unity.com/questions/1138633/how-to-sync-gameobject-creation-to-the-beat-of-a-s.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( AudioSource ) )]
public class SongToSceneGenerator : MonoBehaviour
{
	[SerializeField] private float FPS = 60f;
	[SerializeField] private int BPM = 128;
	[SerializeField] private AudioSource source = default;
	[SerializeField] private AudioClip clip = default;
	[SerializeField] private float volume = 0.1f;
	[SerializeField] private float objectDespawnTimer = 6f;
	[Space]
	[SerializeField] private GameObject obstaclePrefab;
	[SerializeField] private Transform obstacleParent;
	[SerializeField] private Transform obstacleSpawnPoint;
	[SerializeField] private List<int> obstacleSpawnIndeces = new List<int>();
	[SerializeField] private int currentObstacleIndex = 0;
	[Space]
	[SerializeField] private GameObject platformPrefab;
	[SerializeField] private Transform platformParent;
	[SerializeField] private Transform platformSpawnPoint;
	[SerializeField] private int currentPlatformIndex = 0;
	[Space]
	[SerializeField] private float spectrumHeight = 32;
	[SerializeField] private bool debugSpectrum = false;
	[SerializeField] private bool recordTimeStamps = false;

	private float lastTime = 0f;
	private float deltaTime = 0f;
	private float timer = 0f;
	private float totalTime = 0f;
	private bool canSpawnObstacle = true;

	private void Start()
	{
		Initialize();
	}

	private void Update()
	{
		SpawnObjectsOnBPM();
		DrawDebugSpectrum();
		RecordTimestamps();
	}
	public void Initialize()
	{
		source.clip = clip;
		source.volume = volume;

		source.Play();
	}

	private void DrawDebugSpectrum()
	{
		if( !debugSpectrum ) return;

		float[] spectrum = new float[1024];

		AudioListener.GetSpectrumData( spectrum, 0, FFTWindow.BlackmanHarris );

		for( int i = 1; i < spectrum.Length - 1; i++ )
		{
			Debug.DrawLine( new Vector3( i - 1, ( spectrum[i] * spectrumHeight ) + 10, 0 ), new Vector3( i, ( spectrum[i] * spectrumHeight ) + 10, 0 ), Color.red );
			Debug.DrawLine( new Vector3( i - 1, Mathf.Log( ( spectrum[i - 1] * spectrumHeight ) ) + 10, 2 ), new Vector3( i, Mathf.Log( ( spectrum[i - 1] * spectrumHeight ) ) + 10, 2 ), Color.cyan );
			Debug.DrawLine( new Vector3( Mathf.Log( i - 1 ), spectrum[i - 1] - 10, 1 ), new Vector3( Mathf.Log( i ), spectrum[i] - 10, 1 ), Color.green );
			Debug.DrawLine( new Vector3( Mathf.Log( i - 1 ), Mathf.Log( spectrum[i - 1] ), 3 ), new Vector3( Mathf.Log( i ), Mathf.Log( spectrum[i] ), 3 ), Color.blue );
		}
	}

	private void SpawnObjectsOnBPM()
	{
		// Calculate our own deltatime using the time elapsed from the start of the audioclip until now.
		deltaTime = GetComponent<AudioSource>().time - lastTime;
		timer += deltaTime;
		totalTime += deltaTime;

		// Spawn an objects exactly on the Beat.
		// Where "timer >= ( FPS / BPM )" is equal to 1 "Beat".
		if( timer >= ( FPS / BPM ) )
		{
			GameObject newPlatform = Instantiate( platformPrefab, platformSpawnPoint.transform.position, Quaternion.identity, platformParent );
			currentPlatformIndex++;

			foreach( int index in obstacleSpawnIndeces )
			{
				if( index - 6 == currentPlatformIndex )
				{
					SpawnObstacle();
				}
			}
			Destroy( newPlatform, objectDespawnTimer );
			timer -= ( FPS / BPM );
		}
		lastTime = GetComponent<AudioSource>().time;
	}

	private void RecordTimestamps()
	{
		if( !recordTimeStamps )
			return;

		if( Input.GetKeyDown( KeyCode.Space ) )
		{
			obstacleSpawnIndeces.Add( currentPlatformIndex );
		}
	}

	private void SpawnObstacle()
	{
		if( !recordTimeStamps )
		{
			GameObject newObstacle = Instantiate( obstaclePrefab, obstacleSpawnPoint.transform.position, Quaternion.identity, obstacleParent );
			currentObstacleIndex++;

			Destroy( newObstacle, objectDespawnTimer );
		}
	}

	public void SpawnObject( GameObject _object )
	{
		GameObject newObj = Instantiate( obstaclePrefab, obstacleSpawnPoint.position, Quaternion.identity, obstacleParent );
		Destroy( newObj, objectDespawnTimer );
	}
}
