using System.Collections;
using UnityEngine;

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

	private PlayerAnimationBehaviour playerAnimationBehaviour = default;

	private SmoothCam smoothCam = default;

	private RaycastHit2D hit = default;
	private Quaternion fromRotation = default;
	private Vector3 targetNormal = default;
	private Quaternion toRotation = default;

	private bool jumpOnCooldown = false;
	private bool canJump = false;
	private bool jumping = false;
	private bool canSlide = false;
	private bool falling = false;

	public PlayerState State { get => state; set => state = value; }

	private void Start()
	{
		playerAnimationBehaviour = GetComponent<PlayerAnimationBehaviour>();
		smoothCam = Camera.main.GetComponent<SmoothCam>();

		if( !rb2d ) { rb2d = GetComponentInChildren<Rigidbody2D>(); }
	}

	private void Update()
	{
		if( state != PlayerState.WALLJUMPING )
		{
			Move();
			GetJumpInput();
			Slide();
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

		falling = !hit;

		//Debug.Log( string.Format( "Velocity [{0}][{1}]", vel.x, vel.y ) )
	}

	private void GetJumpInput()
	{
		if( jumpOnCooldown ) return;

		canJump = hit.collider != null && !jumpOnCooldown;

		if( Input.touchCount > 0 )
		{
			Touch touch = Input.GetTouch( 0 );
			Vector3 touchPos = Camera.main.ScreenToWorldPoint( touch.position );
			touchPos.z = 0;
			Vector2 beginPos = default;
			if(touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
			{
				if(touch.phase == TouchPhase.Began)
				{
					beginPos = touch.position;
				}

				if( touch.phase == TouchPhase.Moved)
				{
					if( touch.deltaPosition.y > beginPos.y + 50f && canJump )
					{
						Jump();
					}
				}
			}
		}

		if( canJump && Input.GetKeyDown( jumpKeyCode ) )
		{
			Jump();
		}
		else if( jumping && hit.collider != null )
		{
			jumping = false;
		}
	}

	public void Jump()
	{
		jumping = true;
		rb2d.AddForce( transform.up * jumpForce );
		StartCoroutine( StartImmediateJumpCooldown() );
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

	private void Slide()
	{

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

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawRay( jumpCheckTransform.position, Vector2.down * jumpCheckDistance );
	}
}
