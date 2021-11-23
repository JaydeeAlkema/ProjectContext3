using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class StartAudioFromTimestamp : MonoBehaviour
{
	[SerializeField] private float startTime = 0f;  // Timestamp in seconds.
	[SerializeField] private float stopTime = 0f;   // Timestamp in seconds.
	[Space]
	[SerializeField] [ReadOnly] private float currentTime = 0f;    // Current Timestamp of the audio clip.

	private AudioSource audioSource;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();

		audioSource.time = startTime;
		audioSource.Play();
	}

	private void Update()
	{
		currentTime = audioSource.time;

		if( currentTime >= stopTime )
		{
			audioSource.Stop();
		}
	}
}
