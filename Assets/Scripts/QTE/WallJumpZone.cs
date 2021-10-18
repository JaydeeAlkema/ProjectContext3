using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpZone : MonoBehaviour
{
	[SerializeField] private Transform[] jumpPoints;        // Array with all the points the player jumps to.
	[SerializeField] private Transform initialJumpPoint;    // First jump point.
	[SerializeField] private float timeToReact = 2f;        // How much time the player has to react to complete a QTE.

	private Camera cam;
	private SmoothCam smoothCam;
	private RotateCamera rotateCamera;
	private GameObject player;
	private PlayerMovementBehaviour playerMovement;

	private int jumpPointIndex = 0;

	private bool initialJumpDone = false;
	private bool canCheckForInitialJumpDistance = false;
	private bool canJumpToNextPoint = false;

	private void Start()
	{
		Init();
	}

	private void Update()
	{
		JumpInput();
	}

	private void Init()
	{
		cam = Camera.main;
		smoothCam = cam.GetComponent<SmoothCam>();
		rotateCamera = cam.GetComponent<RotateCamera>();
		playerMovement = FindObjectOfType<PlayerMovementBehaviour>();
		player = playerMovement.gameObject;
	}

	/// <summary>
	/// Checks for Input and calls functions and events accordingly.
	/// </summary>
	private void JumpInput()
	{
		//Debug.Log( Mathf.Abs( playerMovement.transform.position.x - initialJumpPoint.position.x ) );
		if( canCheckForInitialJumpDistance && !initialJumpDone && Mathf.Abs( playerMovement.transform.position.x - initialJumpPoint.position.x ) < 0.1f )
		{
			//Debug.Log( "jump" );
			initialJumpDone = true;
			StartCoroutine( JumpTowardsPoint( jumpPoints[jumpPointIndex] ) );

			playerMovement.State = PlayerState.WALLJUMPING;
			playerMovement.ResetVelocity();
			playerMovement.Constrain( true );
		}

		if( canJumpToNextPoint )
		{
			if( Input.GetKeyDown( KeyCode.A ) )
			{
				StartCoroutine( JumpTowardsPoint( jumpPoints[jumpPointIndex] ) );
			}
		}
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if( collision.GetComponent<IPlayer>() != null )
		{
			Debug.Log( "Player Entered Jumpzone" );
			canCheckForInitialJumpDistance = true;

			rotateCamera.enabled = false;

			smoothCam.clamp = true;
			smoothCam.clampY = false;
			smoothCam.xClampPos = transform.position.x;
		}
	}

	private void OnTriggerExit2D( Collider2D collision )
	{
		Debug.Log( "Player Exited Jumpzone" );
		rotateCamera.enabled = true;
		smoothCam.clamp = false;
		smoothCam.xClampPos = 0;

		playerMovement.State = PlayerState.MOVING;
		playerMovement.Constrain( false );
	}

	/// <summary>
	/// Moves the player character to the point at a constant speed.
	/// </summary>
	/// <param name="point"> Move towards this point. </param>
	/// <returns></returns>
	private IEnumerator JumpTowardsPoint( Transform point )
	{
		while( Vector2.Distance( playerMovement.transform.position, point.position ) > 0.01f )
		{
			canJumpToNextPoint = false;
			float step = 7f * Time.deltaTime;
			playerMovement.gameObject.transform.position = Vector2.MoveTowards( playerMovement.gameObject.transform.position, point.position, step );
			yield return null;
		}

		jumpPointIndex++;
		canJumpToNextPoint = true;
		smoothCam.clampY = false;
		yield return null;
	}
}
