using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public PlayerData playerData;

	private void Start()
	{
		playerData = new PlayerData( new float[] { 0, 0, 0 } );
	}

	private void Update()
	{
		if( Input.GetKey( KeyCode.LeftArrow ) )
		{
			playerData.position[0] -= 1 * Time.deltaTime;
		}
		if( Input.GetKey( KeyCode.RightArrow ) )
		{
			playerData.position[0] += 1 * Time.deltaTime;
		}

		transform.position = new Vector3( playerData.position[0], playerData.position[1], playerData.position[2] );
	}
}
