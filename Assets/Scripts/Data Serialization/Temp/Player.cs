using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public PlayerData playerData;

	private float width;
	private float height;

	private void Start()
	{
		playerData = new PlayerData( new float[] { 0, 0, 0 } );

		width = Screen.width * 0.5f;
		height = Screen.height * 0.5f;
	}

	private void Update()
	{

		if( Input.touchCount > 0 )
		{
			Touch touch = Input.GetTouch( 0 );

			if( touch.phase == TouchPhase.Moved && !Helpers.IsOverUI() )
			{
				Vector2 pos = touch.position;
				pos.x = ( pos.x - width ) / width;
				pos.y = ( pos.y - height ) / height;

				playerData.position[0] = pos.x;
				playerData.position[1] = pos.y;
			}
		}

		//if( Input.GetKey( KeyCode.LeftArrow ) )
		//{
		//	playerData.position[0] -= 1 * Time.deltaTime;
		//}
		//if( Input.GetKey( KeyCode.RightArrow ) )
		//{
		//	playerData.position[0] += 1 * Time.deltaTime;
		//}

		transform.position = new Vector3( playerData.position[0], playerData.position[1], playerData.position[2] );
	}
}
