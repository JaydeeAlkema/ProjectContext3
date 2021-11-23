using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerRuneActivation : MonoBehaviour
{
	[SerializeField] private Transform[] points;
	public GameObject player;
	public bool completed = false;
	public bool failed = false;
	public Runes rune;
	[SerializeField] private List<Transform> hitPoints;
	[SerializeField] private float timeLeft = 30f;
	[SerializeField] private float drawError = 0.1f;
	[SerializeField] private LineRenderer line;

	// Update is called once per frame
	void FixedUpdate()
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

	void DrawRune()
	{
		if( Input.touchCount > 0 )
		{
			Touch touch = Input.GetTouch( 0 );
			Vector3 touchPos = Camera.main.ScreenToWorldPoint( touch.position );
			touchPos.z = 0;

			if( hitPoints.Count >= 4 ) { CheckforCompletion(); hitPoints.Clear(); }

			//Check to see touch is not canceled
			if( touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled )
			{
				//on first touchphase check all points to see if close enough
				if( touch.phase == TouchPhase.Began )
				{
					hitPoints.Clear();
					foreach( Transform point in points )
					{
						if( ( touchPos - point.position ).magnitude <= drawError + 0.5f && !hitPoints.Contains( point ) )
						{
							//add point to list
							hitPoints.Add( point );
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
				hitPoints.Clear();
				line.positionCount = 0;
			}
		}
	}

	void ShowRuneDrawing()
	{
		for( int i = 0; i < hitPoints.Count; i++ )
		{
			line.positionCount = i + 1;
			line.SetPosition( i, hitPoints.ElementAt( i ).position + new Vector3( 0, 0, -0.1f ) );
		}
	}

	bool CheckforCompletion()
	{
		string order = "";
		foreach( Transform point in hitPoints )
		{
			order += point.name;
		}
		switch( order )
		{
			case "1351":
				//Destroy object
				rune = Runes.DESTROY;
				order = "";
				//completed = true;
				Debug.Log( "Destroyed" );
				break;

			case "1634":
				//Disable object
				rune = Runes.DISABLE;
				order = "";
				//completed = true;
				Debug.Log( "Disable" );
				break;

			case "5623":
				//Dodge object
				order = "";
				//completed = true;
				Debug.Log( "Dodge" );
				break;

			default:
				completed = false;
				break;
		}
		return completed;
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
