using System.Collections;
using UnityEngine;
using NaughtyAttributes;

public class PlayerMovementBehaviour : MonoBehaviour, IPlayer
{
	[SerializeField] private Transform spriteTransform = default;
	[Space]
	[SerializeField] private float baseMovementSpeed = 5f;
	[SerializeField] private float slopeForce = 100f;
	[SerializeField] private float slopeForceRayLength = 2f;
	[SerializeField] private float jumpMultiplier = 6;
	[SerializeField] private AnimationCurve jumpfallOff = default;
	[SerializeField] private float slideTime = 0.75f;
	[Space]
	[SerializeField] private KeyCode jumpKeyCode = KeyCode.Space;
	[SerializeField] private KeyCode[] slideKeyCode = { KeyCode.S, KeyCode.DownArrow };
	[Space]
	[SerializeField] private LayerMask hitMask = default;
	[Space]
	[SerializeField] private PlayerRuneActivation runeActivation;
	[Space]
	[SerializeField] [ReadOnly] private bool isJumping = false;
	[SerializeField] [ReadOnly] private bool canSlide = true;
	[SerializeField] [ReadOnly] private bool isSliding = false;
	[Space]
	[SerializeField] private int maxSpriteBlinkCount = 6;
	[SerializeField] private float spriteBlinkInterval = 0.25f;

	private PlayerAnimationBehaviour playerAnimationBehaviour = null;
	private SmoothCam smoothCam = null;
	private SpriteRenderer spriteRenderer = null;
	private CharacterController charController = null;

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
	}

	private void Update()
	{
		Move();
		GetJumpInput();
		GetSlideInput();

		UpdateSpriteRotation();
		UpdateAnimator();
	}

	private void Move()
	{
		charController.SimpleMove( Vector3.right * baseMovementSpeed );

		if( OnSlope() )
			charController.Move( Vector3.down * charController.height / 2f * slopeForce * Time.deltaTime );

		GetJumpInput();

		//Debug.Log( string.Format( "Velocity [{0}][{1}]", vel.x, vel.y ) )
	}

	private void GetJumpInput()
	{
		if( !charController.isGrounded ) return;

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
					if( touch.deltaPosition.y > beginPos.y + 50f && !runeActivation.isDrawing )
					{
						isJumping = true;
						StartCoroutine( JumpEvent() );
					}
				}
			}
		}

		if( Input.GetKeyDown( jumpKeyCode ) )
		{
			isJumping = true;
			StartCoroutine( JumpEvent() );
		}
	}

	private void GetSlideInput()
	{
		//add animation
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
					if( touch.deltaPosition.y < beginPos.y - 50f && canSlide && !runeActivation.isDrawing )
					{
						Slide();
					}
				}
			}
		}

		if( canSlide )
		{
			foreach( KeyCode key in slideKeyCode )
			{
				if( Input.GetKeyDown( key ) )
				{
					Slide();
				}
			}
		}
	}

	private IEnumerator JumpEvent()
	{
		charController.slopeLimit = 90f;
		float timeInAir = 0f;
		do
		{
			float jumpForce = jumpfallOff.Evaluate( timeInAir );
			charController.Move( Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime );
			timeInAir += Time.deltaTime;
			yield return null;
		} while( !charController.isGrounded && charController.collisionFlags != CollisionFlags.Above );
		charController.slopeLimit = 45f;
		isJumping = false;
	}

	private void Slide()
	{
		canSlide = false;
		isSliding = true;
		if( isSliding )
		{
			charController.height = 0f;
			charController.center = new Vector2( charController.center.x, -0.25f );
			spriteRenderer.gameObject.transform.localScale = new Vector3( 0.025f, 0.025f, 1f );
			spriteRenderer.gameObject.transform.localPosition = new Vector3( 0f, 0.185f, 1f );
			StartCoroutine( SlideCooldown() );
		}
	}

	private bool OnSlope()
	{
		if( isJumping )
			return false;

		if( Physics.Raycast( transform.position, Vector3.down, out hit, charController.height / 2f * slopeForceRayLength ) )
			if( hit.normal != Vector3.up )
				return true;
		return false;
	}

	private IEnumerator SlideCooldown()
	{
		yield return new WaitForSeconds( slideTime );
		charController.height = 0.7f;
		charController.center = new Vector2( charController.center.x, 0f );
		spriteRenderer.gameObject.transform.localScale = new Vector3( 0.05f, 0.05f, 1f );
		spriteRenderer.gameObject.transform.localPosition = new Vector3( 0f, 0.37f, 1f );
		isSliding = false;
		canSlide = true;
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
		playerAnimationBehaviour.SetBool( "Jumping", isJumping );
	}

	public void BlinkSprite()
	{
		StartCoroutine( BlinkSpriteIE() );
	}

	private IEnumerator BlinkSpriteIE()
	{
		while( spriteBlinkCount < maxSpriteBlinkCount )
		{
			spriteRenderer.enabled = false;
			yield return new WaitForSeconds( spriteBlinkInterval );
			spriteRenderer.enabled = true;
			yield return new WaitForSeconds( spriteBlinkInterval );
			spriteBlinkCount++;
		}
		spriteBlinkCount = 0;
		yield return null;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawRay( transform.position, Vector3.down * slopeForceRayLength );
	}
}
