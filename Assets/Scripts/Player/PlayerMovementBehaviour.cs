using System.Collections;
using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour, IPlayer
{
	[SerializeField] private Player_ScriptableObject playerData;
	[SerializeField] private Transform spriteTransform;

	private PlayerAnimationBehaviour playerAnimationBehaviour = null;
	private SmoothCam smoothCam = null;
	private SpriteRenderer spriteRenderer = null;
	private CharacterController charController = null;
	private PlayerRuneActivation playerRuneActivation = null;

	private RaycastHit hit = default;
	private Quaternion fromRotation = default;
	private Vector3 targetNormal = default;
	private Quaternion toRotation = default;
	private int spriteBlinkCount = 0;

	private void Awake()
	{
		if( !playerAnimationBehaviour ) playerAnimationBehaviour = GetComponent<PlayerAnimationBehaviour>();
		if( !smoothCam ) smoothCam = Camera.main.GetComponent<SmoothCam>();
		if( !spriteRenderer ) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		if( !charController ) charController = GetComponent<CharacterController>();
		if( !playerRuneActivation ) playerRuneActivation = FindObjectOfType<PlayerRuneActivation>();
	}

	private void Update()
	{
		Move();
		if( !playerData.isSliding && !playerData.isJumping ) GetJumpInput();
		if( !playerData.isSliding && !playerData.isJumping ) GetSlideInput();

		UpdateSpriteRotation();
		UpdateAnimator();
	}

	private void Move()
	{
		playerData.grounded = charController.isGrounded;

		charController.SimpleMove( Vector3.right * playerData.baseMovementSpeed );

		if( OnSlope() )
			charController.Move( Vector3.down * charController.height / 2f * playerData.slopeForce * Time.deltaTime );

		//Debug.Log( string.Format( "Velocity [{0}][{1}]", vel.x, vel.y ) )
	}

	private void GetJumpInput()
	{
		if( !playerData.grounded ) return;

		if( Input.touchCount > 0 )
		{
			Touch touch = Input.GetTouch( 0 );
			Vector3 touchPos = Camera.main.ScreenToWorldPoint( touch.position );
			touchPos.z = 0;
			Vector2 beginPos = default;
			if( touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled )
			{
				if( touch.phase == TouchPhase.Began )
				{
					beginPos = touch.position;
				}

				if( touch.phase == TouchPhase.Moved )
				{
					if( touch.deltaPosition.y > beginPos.y + 50f && !playerRuneActivation.isDrawing )
					{
						StartCoroutine( JumpEvent() );
					}
				}
			}
		}

		foreach( KeyCode key in playerData.jumpKeyCodes )
		{
			if( Input.GetKeyDown( key ) )
			{
				StartCoroutine( JumpEvent() );
			}
		}
	}

	private void GetSlideInput()
	{
		if( !playerData.grounded ) return;

		if( Input.touchCount > 0 )
		{
			Touch touch = Input.GetTouch( 0 );
			Vector3 touchPos = Camera.main.ScreenToWorldPoint( touch.position );
			touchPos.z = 0;
			Vector2 beginPos = default;
			if( touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled )
			{
				if( touch.phase == TouchPhase.Began )
				{
					beginPos = touch.position;
				}

				if( touch.phase == TouchPhase.Moved )
				{
					if( touch.deltaPosition.y < beginPos.y - 50f && !playerRuneActivation.isDrawing )
					{
						Slide();
					}
				}
			}
		}

		foreach( KeyCode key in playerData.slideKeyCodes )
		{
			if( Input.GetKeyDown( key ) )
			{
				Slide();
			}
		}
	}

	private IEnumerator JumpEvent()
	{
		playerData.isJumping = true;
		charController.slopeLimit = 90f;

		float timeInAir = 0f;
		do
		{
			float jumpForce = playerData.jumpfallOff.Evaluate( timeInAir );
			playerAnimationBehaviour.SetFloat( "Velocity_Y", Mathf.Clamp01( jumpForce ) );
			charController.Move( Vector3.up * jumpForce * playerData.jumpMultiplier * Time.deltaTime );
			timeInAir += Time.deltaTime;
			yield return null;
		} while( !charController.isGrounded && charController.collisionFlags != CollisionFlags.Above );

		charController.slopeLimit = 45f;
		playerData.isJumping = false;
	}

	private void Slide()
	{
		playerData.isSliding = true;
		if( playerData.isSliding )
		{
			charController.height = 0f;
			charController.center = new Vector2( charController.center.x, -0.25f );
			spriteRenderer.gameObject.transform.localPosition = new Vector3( 0f, 0.185f, 1f );
			StartCoroutine( SlideCooldown() );
		}
	}

	private bool OnSlope()
	{
		if( playerData.isJumping )
			return false;

		if( Physics.Raycast( transform.position, Vector3.down, out hit, charController.height / 2f * playerData.slopeForceRayLength ) )
			if( hit.normal != Vector3.up )
				return true;
		return false;
	}

	private IEnumerator SlideCooldown()
	{
		yield return new WaitForSeconds( playerData.slideTime );
		charController.height = 0.7f;
		charController.center = new Vector2( charController.center.x, 0f );
		spriteRenderer.gameObject.transform.localPosition = new Vector3( 0f, 0.37f, 0f );

		yield return new WaitForEndOfFrame();
		playerData.isSliding = false;
	}

	private void UpdateSpriteRotation()
	{
		fromRotation = transform.rotation;
		targetNormal = hit.normal;
		toRotation = Quaternion.FromToRotation( Vector3.up, hit.normal );

		spriteTransform.rotation = Quaternion.Slerp( fromRotation, toRotation, 1f );

		//Debug.Log( toRotation );
	}

	private void UpdateAnimator()
	{
		playerAnimationBehaviour.SetBool( "Jumping", playerData.isJumping );
		playerAnimationBehaviour.SetBool( "Sliding", playerData.isSliding );
	}

	public void BlinkSprite()
	{
		StartCoroutine( BlinkSpriteIE() );
	}

	private IEnumerator BlinkSpriteIE()
	{
		while( spriteBlinkCount < playerData.maxSpriteBlinkCount )
		{
			spriteRenderer.enabled = false;

			yield return new WaitForSeconds( playerData.spriteBlinkInterval );
			spriteRenderer.enabled = true;

			yield return new WaitForSeconds( playerData.spriteBlinkInterval );
			spriteBlinkCount++;
		}
		spriteBlinkCount = 0;
		yield return null;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawRay( transform.position, Vector3.down * playerData.slopeForceRayLength );
	}
}
