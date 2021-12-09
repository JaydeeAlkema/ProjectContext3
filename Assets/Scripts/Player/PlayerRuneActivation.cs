using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerRuneActivation : MonoBehaviour
{
	[SerializeField] private Transform[] points;
	[SerializeField] private List<RuneOrder> runeOrders;
	public GameObject fingerTrail;
	public GameObject player;
	public bool completed = false;
	public bool failed = false;
	public Runes rune;
	public bool isDrawing = false;
	[SerializeField] private List<Transform> hitPoints;
	[SerializeField] private float timeLeft = 30f;
	[SerializeField] private float drawError = 0.1f;
	[SerializeField] private LineRenderer line;
	[Space]
	[SerializeField] private List<GameObject> animatedRunes = new List<GameObject>();

	private Vector3 touchPos;

	// Update is called once per frame
	void Update()
	{
		if( hitPoints.Count >= 4 ) { CheckforCompletion(); hitPoints.Clear(); }
		DrawRune();
		ShowRuneDrawing();
		timeLeft -= Time.deltaTime;
		if( timeLeft <= 0 && !completed )
		{
			RuneFailed();
		}
	}

	private void OnDisable()
	{
		isDrawing = false;
		fingerTrail.gameObject.SetActive( false );
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		foreach( Transform point in points )
		{
			Gizmos.DrawWireSphere( point.position, drawError );
		}
	}


	void SpawnAnimatedRune( Runes type )
	{
		Vector3 spawnPos = new Vector3( transform.position.x, transform.position.y + 1, transform.position.z + 10 );
		GameObject animatedRuneGo = Instantiate( animatedRunes[( ( byte )rune )], spawnPos, Quaternion.identity, Camera.main.transform );
		Destroy( animatedRuneGo, 4f );
	}

	void DrawRune()
	{
		if( Input.touchCount > 0 )
		{
			Touch touch = Input.GetTouch( 0 );
			touchPos = Camera.main.ScreenToWorldPoint( touch.position );
			touchPos.z = 0;

			if( hitPoints.Count >= 4 ) { CheckforCompletion(); hitPoints.Clear(); }

			//Check to see touch is not canceled
			if( touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled )
			{
				fingerTrail.transform.position = touchPos;
				//on first touchphase check all points to see if close enough
				if( touch.phase == TouchPhase.Began )
				{
					rune = Runes.NULL;
					fingerTrail.gameObject.SetActive( true );
					hitPoints.Clear();
					foreach( Transform point in points )
					{
						if( ( touchPos - point.position ).magnitude <= drawError + 0.5f && !hitPoints.Contains( point ) )
						{
							//add point to list
							hitPoints.Add( point );
							isDrawing = true;
						}
					}
				}

				//for everymove check for touch position is close enough
				if( touch.phase == TouchPhase.Moved && hitPoints.Count >= 1 )
				{
					foreach( Transform point in points )
					{
						if( ( touchPos - point.position ).magnitude <= drawError && !hitPoints.Contains( point ) )
						{
							//add point to list
							hitPoints.Add( point );
						}
						//check to see if 4th hit point is not in list already except when its the first, then register again
						else if( ( touchPos - point.position ).magnitude <= drawError && hitPoints.First() == point && hitPoints.Count > 1 )
						{
							//add point to list
							hitPoints.Add( point );
						}
					}
				}
			}
			//if touchphase has ended clear list for new input
			if( touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled )
			{
				fingerTrail.gameObject.SetActive( false );
				hitPoints.Clear();
				line.positionCount = 0;
				isDrawing = false;
			}
		}
	}

	void ShowRuneDrawing()
	{
		for( int i = 0; i < hitPoints.Count; i++ )
		{
			line.positionCount = i + 1;
			line.SetPosition( i, hitPoints.ElementAt( i).position + new Vector3( 0, 0, -0.1f ) );
			//line.SetPosition( i, touchPos + new Vector3( 0, 0, -0.1f ) );
			//if( i > 0 )
			//{
			//	line.SetPosition( i -1, hitPoints.ElementAt( i -1 ).position + new Vector3( 0, 0, -0.1f ) );
			//}
		}
	}

	bool CheckforCompletion()
	{
		foreach( RuneOrder runeOrder in runeOrders )
		{
			if( new HashSet<Transform>( runeOrder.hitpoints ).SetEquals( hitPoints ) )
			{
				rune = runeOrder.runeType;
				SpawnAnimatedRune( rune );
				isDrawing = false;
			}
		}
		//string order = "";
		//foreach( Transform point in hitPoints )
		//{
		//	order += point.name;
		//}
		//switch( order )
		//{
		//	case "1351":    //Destroy object
		//		rune = Runes.DESTROY;
		//		order = "";
		//		//completed = true;
		//		SpawnAnimatedRune( rune );
		//		isDrawing = false;

		//		break;

		//	case "1634":    //Disable object
		//		rune = Runes.DISABLE;
		//		order = "";
		//		//completed = true;
		//		SpawnAnimatedRune( rune );
		//		isDrawing = false;
		//		break;

		//	case "5623":    //Dodge object
		//		order = "";
		//		//completed = true;
		//		SpawnAnimatedRune( rune );
		//		isDrawing = false;
		//		break;

		//	default:
		//		completed = false;
		//		break;
		//}
		return completed;
	}

	[System.Serializable]
	private struct RuneOrder
	{
		public Runes runeType;
		public List<Transform> hitpoints;
	}

	public void RuneFailed()
	{
		//FAIL
	}

	public enum Runes
	{
		NULL,
		DESTROY,
		DISABLE,
		DODGE
	}
}
