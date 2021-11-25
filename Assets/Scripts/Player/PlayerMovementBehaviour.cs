using System.Collections;
using UnityEngine;
using NaughtyAttributes;

public enum PlayerState
{
	MOVING = 1,
	JUMPING = 2,
	SLIDING = 3,
	WALLJUMPING = 4
}

public class PlayerMovementBehaviour : MonoBehaviour, IPlayer
{
	[SerializeField] private PlayerState state = PlayerState.MOVING;
	[SerializeField] private Rigidbody2D rb2d = default;
	[SerializeField] private Transform spriteTransform = default;

	[SerializeField] private float baseMovementSpeed = 5f;

	[SerializeField] private KeyCode jumpKeyCode = KeyCode.Space;
	[SerializeField] private KeyCode[] slideKeyCode = { KeyCode.S, KeyCode.DownArrow };

	[SerializeField] private Transform jumpCheckTransform = default;
	[SerializeField] private float jumpForce = 100f;
	[SerializeField] private float jumpCheckDistance = 1f;
	[SerializeField] private float immediateJumpCooldown = 0.1f;

	[SerializeField] private LayerMask hitMask = default;

	[SerializeField] private PlayerRuneActivation runeActivation;
	private PlayerAnimationBehaviour playerAnimationBehaviour = default;

	private SmoothCam smoothCam = default;

	private RaycastHit2D hit = default;
	private Quaternion fromRotation = default;
	private Vector3 targetNormal = default;
	private Quaternion toRotation = default;

	[SerializeField] [ReadOnly] private bool jumpOnCooldown = false;
	[SerializeField] [ReadOnly] private bool canJump = false;
	[SerializeField] [ReadOnly] private bool jumping = false;
	[SerializeField] [ReadOnly] private bool canSlide = true;
	[SerializeField] [ReadOnly] private bool isSliding = false;
	[SerializeField] private int maxSpriteBlinkCount = 6;
	[SerializeField] private float spriteBlinkInterval = 0.25f;

	private float hitboxY = default;
	private float hitboxYPos = default;
	private int spriteBlinkCount = 0;

	private CapsuleCollider2D capsuleCollider;
	private SpriteRenderer spriteRenderer;

	public PlayerState State { get => state; set => state = value; }

	private void Start()
	{
		playerAnimationBehaviour = GetComponent<PlayerAnimationBehaviour>();
		smoothCam = Camera.main.GetComponent<SmoothCam>();
		capsuleCollider = GetComponent<CapsuleCollider2D>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		hitboxY = capsuleCollider.size.y;
		hitboxYPos = capsuleCollider.offset.y;

		if( !rb2d ) { rb2d = GetComponentInChildren<Rigidbody2D>(); }

		canSlide = true;
	}

	private void Update()
	{
		if( state != PlayerState.WALLJUMPING )
		{
			Move();
			GetJumpInput();
			GetSlideInput();
		}

		UpdateSpriteRotation();
		UpdateAnimator();
	}

	private void Move()
	{
		hit = Physics2D.Raycast( jumpCheckTransform.position, Vector2.down, jumpCheckDistance, hitMask );

		Vector2 vel = rb2d.velocity;
		vel.x = baseMovementSpeed;
		rb2d.velocity = vel;

		if( !jumping )
		{
			if( toRotation.z > 0 ) { vel.y = 1; }
			else if( toRotation.z < 0 ) { vel.y = -1; }
		}

		if( !canJump && jumping && hit.collider != null )
		{
			jumping = false;
		}

		//Debug.Log( string.Format( "Velocity [{0}][{1}]", vel.x, vel.y ) )
	}

	private void GetJumpInput()
	{
		if( jumpOnCooldown ) return;

		canJump = hit.collider != null && !jumpOnCooldown;

#if UNITY_ANDROID || PLATFORM_ANDROID
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
					if( touch.deltaPosition.y > beginPos.y + 50f && canJump && !runeActivation.isDrawing)
					{
						Jump();
					}
				}
			}
		}
#endif

#if UNITY_EDITOR
		if( canJump && Input.GetKeyDown( jumpKeyCode ) )
		{
			Jump();
		}
#endif
	}

	private void GetSlideInput()
	{
		//add animation
		//shorten hitbox?
#if UNITY_ANDROID || PLATFORM_ANDROID
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
#endif

#if UNITY_EDITOR
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
#endif
	}

	public void Jump()
	{
		jumping = true;
		rb2d.AddForce( transform.up * jumpForce );
		StartCoroutine( StartImmediateJumpCooldown() );
	}

	private void Slide()
	{
		canSlide = false;
		isSliding = true;
		if( isSliding )
		{
			capsuleCollider.size = new Vector2( capsuleCollider.size.x, 0f );
			capsuleCollider.offset = new Vector2( capsuleCollider.offset.x, -0.25f );
			spriteRenderer.gameObject.transform.localScale = new Vector3( 0.025f, 0.025f, 1f );
			spriteRenderer.gameObject.transform.localPosition = new Vector3( 0f, 0.185f, 1f );
			StartCoroutine( SlideCooldown() );
		}
	}

	public void ResetVelocity()
	{
		Vector2 vel = rb2d.velocity;
		vel.x = 0;
		vel.y = 0;
		rb2d.velocity = vel;
	}

	public void Constrain( bool constrain )
	{
		rb2d.constraints = constrain ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;
	}

	private IEnumerator StartImmediateJumpCooldown()
	{
		jumpOnCooldown = true;
		yield return new WaitForSeconds( immediateJumpCooldown );
		jumpOnCooldown = false;
	}

	private IEnumerator SlideCooldown()
	{
		yield return new WaitForSeconds( 1f );
		capsuleCollider.size = new Vector2( capsuleCollider.size.x, hitboxY );
		capsuleCollider.offset = new Vector2( capsuleCollider.offset.x, hitboxYPos );
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
		playerAnimationBehaviour.SetBool( "Jumping", jumping );
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

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawRay( jumpCheckTransform.position, Vector2.down * jumpCheckDistance );
	}
}
