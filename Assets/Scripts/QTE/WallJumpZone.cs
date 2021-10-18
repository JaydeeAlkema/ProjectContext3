using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpZone : MonoBehaviour
{
	[SerializeField] private Transform[] jumpPoints = default;      // Array with all the points the player jumps to.
	[SerializeField] private Transform initialJumpPoint = default;  // First jump point.
	[SerializeField] private float timeToReact = 2f;                // How much time the player has to react to complete a QTE.

	private Camera cam = default;
	private SmoothCam smoothCam = default;
	private RotateCamera rotateCamera = default;
	private GameObject player = default;
	private PlayerMovementBehaviour playerMovement = default;
	private PlayerQuickTimeEventBehaviour playerQuickTimeEventBehaviour = default;
	private SpriteRenderer playerSprite = default;

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
		playerSprite = player.GetComponentInChildren<SpriteRenderer>();
		playerQuickTimeEventBehaviour = FindObjectOfType<PlayerQuickTimeEventBehaviour>();
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
			Jump();

			playerMovement.State = PlayerState.WALLJUMPING;
			playerMovement.ResetVelocity();
			playerMovement.Constrain( true );
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

			collision.GetComponent<PlayerQuickTimeEventBehaviour>().CurrentJumpZone = this;
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

		playerQuickTimeEventBehaviour.DeactivateQTE();
		FlipPlayerSprite( 1 );
	}

	public void Jump()
	{
		StartCoroutine( JumpTowardsNextPoint() );
	}

	/// <summary>
	/// Moves the player character to the point at a constant speed.
	/// </summary>
	/// <param name="point"> Move towards this point. </param>
	/// <returns></returns>
	private IEnumerator JumpTowardsNextPoint()
	{
		// Reset QTE and activate it again.
		playerQuickTimeEventBehaviour.DeactivateQTE();

		while( Vector2.Distance( playerMovement.transform.position, jumpPoints[jumpPointIndex].position ) > 0.01f )
		{
			canJumpToNextPoint = false;
			float step = 7f * Time.deltaTime;
			playerMovement.gameObject.transform.position = Vector2.MoveTowards( playerMovement.gameObject.transform.position, jumpPoints[jumpPointIndex].position, step );
			yield return null;
		}

		// Call function within player behaviour that changes to the walljumping animation.

		if( jumpPointIndex < jumpPoints.Length - 1 )
		{
			switch( jumpPointIndex % 2 )
			{
				case 0:
					playerQuickTimeEventBehaviour.ActivateQTE( QTE_KEY.LEFT );
					break;

				case 1:
					playerQuickTimeEventBehaviour.ActivateQTE( QTE_KEY.RIGHT );
					break;
			}

			FlipPlayerSprite( jumpPointIndex );
		}

		jumpPointIndex++;
		canJumpToNextPoint = true;
		smoothCam.clampY = false;
		yield return null;
	}

	/// <summary>
	/// Flips the player sprite to make it so it always faces the wall.
	/// Makes sense for walljumping and gives the player a visual queue as to what to do.
	/// </summary>
	private void FlipPlayerSprite( int dir )
	{
		switch( dir % 2 )
		{
			case 0:
				playerSprite.flipX = true;
				break;

			case 1:
				playerSprite.flipX = false;
				break;
		}
	}
}
