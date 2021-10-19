using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum QTE_KEY
{
	UP = 1,
	DOWN = 2,
	LEFT = 3,
	RIGHT = 4
}

public class PlayerQuickTimeEventBehaviour : MonoBehaviour
{
	[SerializeField] private Transform qteSpriteTransform = default;        // Transform that will hold the QTE sprite.
	[SerializeField] private SpriteRenderer qteSpriteRenderer = default;    // Reference to the QTE sprite renderer.
	[SerializeField] private SpriteRenderer qteHitIndicatorSpriteRenderer;  // Reference to the QTE Hit Indicator sprite renderer.
	[SerializeField] private QTE[] quickTimeEvents = default;               // Array with preset QTE's.
	[SerializeField] private float qteTimeToReact = default;                // How many seconds the player has to react to the QTE before it's too late.
	[SerializeField] private WallJumpZone currentJumpZone = default;        // Current Jump Zone.

	private int QTEIndex = 0;
	private bool QTEActive = false;
	private Vector3 qteHitIndicatorInitialScale = new Vector3();

	public WallJumpZone CurrentJumpZone { get => currentJumpZone; set => currentJumpZone = value; }

	private void Start()
	{
		qteSpriteTransform.gameObject.SetActive( false );
		qteHitIndicatorSpriteRenderer.gameObject.SetActive( false );

		qteHitIndicatorInitialScale = qteHitIndicatorSpriteRenderer.gameObject.transform.localScale;
	}

	private void Update()
	{
		CheckForCorrectInput();
	}

	public void ActivateQTE( QTE_KEY key )
	{
		QTEActive = true;
		QTEIndex = key == QTE_KEY.LEFT ? 0 : 1;

		qteSpriteTransform.gameObject.SetActive( true );
		qteHitIndicatorSpriteRenderer.gameObject.SetActive( true );
		qteSpriteRenderer.sprite = quickTimeEvents[QTEIndex].sprite;

		ActivateHitIndicator();
	}

	public void DeactivateQTE()
	{
		QTEActive = false;

		qteSpriteTransform.gameObject.SetActive( false );
		qteHitIndicatorSpriteRenderer.gameObject.SetActive( false );
		qteHitIndicatorSpriteRenderer.gameObject.transform.localScale = qteHitIndicatorInitialScale;
	}

	private void ActivateHitIndicator()
	{
		StartCoroutine( ScaleOverSeconds( qteHitIndicatorSpriteRenderer.transform.gameObject, Vector3.one, qteTimeToReact ) );
	}

	public IEnumerator ScaleOverSeconds( GameObject objectToScale, Vector3 end, float seconds )
	{
		float elapsedTime = 0;
		Vector3 startingPos = objectToScale.transform.localScale;
		while( elapsedTime < seconds )
		{
			elapsedTime += Time.deltaTime;
			objectToScale.transform.localScale = Vector3.Lerp( startingPos, end, ( elapsedTime / seconds ) );
			yield return new WaitForEndOfFrame();
		}
		objectToScale.transform.localScale = end;
		currentJumpZone.Jump();
	}

	public void CheckForCorrectInput()
	{
		if( !QTEActive )
			return;

		KeyCode keycode = quickTimeEvents[QTEIndex].keyCode;

		if( Input.anyKeyDown && Input.GetKeyDown( keycode ) )
		{
			// Add points
		}
		else if( Input.anyKeyDown && !Input.GetKeyDown( keycode ) )
		{
			// Retract Points
		}
	}
}

[System.Serializable]
public struct QTE
{
	public string name;
	public Sprite sprite;       // Array with all the QTE sprites.
	public KeyCode keyCode;     // Which key to press with the sprite.
}
