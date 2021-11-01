using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

	[SerializeField] private Vector2 parallaxEffectMultiplier;
	[SerializeField] private bool infiniteHorizontal;
	[SerializeField] private bool infiniteVertical;

	private Transform cameraTransform;
	private Vector3 lastCameraPosition;
	private float textureUnitSizeX;
	private float textureUnitSizeY;

	private void Start()
	{
		cameraTransform = Camera.main.transform;
		lastCameraPosition = cameraTransform.position;
		Sprite sprite = GetComponent<SpriteRenderer>().sprite;
		Texture2D texture = sprite.texture;
		textureUnitSizeX = ( texture.width / sprite.pixelsPerUnit ) * transform.localScale.x;
		textureUnitSizeY = ( texture.height / sprite.pixelsPerUnit ) * transform.localScale.y;
	}

	private void LateUpdate()
	{
		Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
		transform.position += new Vector3( deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y, deltaMovement.z );
		lastCameraPosition = cameraTransform.position;

		if( infiniteHorizontal )
		{
			if( Mathf.Abs( cameraTransform.position.x - transform.position.x ) >= textureUnitSizeX )
			{
				float offsetPositionX = ( cameraTransform.position.x - transform.position.x ) % textureUnitSizeX;
				transform.position = new Vector3( cameraTransform.position.x + offsetPositionX, transform.position.y );
			}
		}

		if( infiniteVertical )
		{
			if( Mathf.Abs( cameraTransform.position.y - transform.position.y ) >= textureUnitSizeY )
			{
				float offsetPositionY = ( cameraTransform.position.y - transform.position.y ) % textureUnitSizeY;
				transform.position = new Vector3( transform.position.x, cameraTransform.position.y + offsetPositionY );
			}
		}
	}

	//[SerializeField] private float parallaxEffect;

	//private GameObject cam;
	//private float length;
	//private float startpos;

	//private void Start()
	//{
	//	cam = Camera.main.gameObject;
	//	startpos = transform.position.x;
	//	length = GetComponent<SpriteRenderer>().bounds.size.x;
	//}

	//private void Update()
	//{
	//	float temp = cam.transform.position.x * ( 1 - parallaxEffect );
	//	float dist = cam.transform.position.x * parallaxEffect;

	//	transform.position = new Vector3( startpos + dist, transform.position.y, transform.position.z );

	//	if( temp > startpos + length ) startpos += length;
	//	else if( temp < startpos - length ) startpos -= length;
	//}
}
