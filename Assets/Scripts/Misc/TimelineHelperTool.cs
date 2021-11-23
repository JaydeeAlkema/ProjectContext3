#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimelineHelperTool : MonoBehaviour
{
	public bool listenForInput = true; // If Enabled, it will listen to all keycode inputs. Set these keycodes to something niot present in the game, like F1
	public bool showGizmos = true;    // If Enabled, draw all gizmos of all the events.
	public bool clearOnExit = false;  // If Enabled, clears the scriptableObject data on exit.
	public TimelineHelperTool_ScriptableObject scriptableObject;
	public List<TimelineHelperEvent> helperEvents = new List<TimelineHelperEvent>();

	private PlayerMovementBehaviour player;

	private void Start()
	{
		player = FindObjectOfType<PlayerMovementBehaviour>();
	}

	private void Update()
	{
		if( !player )
			player = FindObjectOfType<PlayerMovementBehaviour>();

		if( !listenForInput )
			return;

		foreach( TimelineHelperEvent _event in helperEvents )
		{
			if( Input.GetKeyDown( _event.key ) )
			{
				TimelineHelperEvent newEvent = _event;
				newEvent.position = player.transform.position;
				newEvent.timeStamp = Time.time;

				scriptableObject.Add( newEvent );
			}
		}
	}

	private void OnDisable()
	{
		if( clearOnExit )
		{
			scriptableObject.Clear();
		}
	}

	public void ShowHandles()
	{
		List<TimelineHelperEvent> _events = scriptableObject.Events;

		foreach( TimelineHelperEvent _event in _events )
		{
			Handles.color = new Color( _event.eventColor.r, _event.eventColor.g, _event.eventColor.b );
			Handles.DrawWireCube( new Vector3( _event.position.x, _event.position.y ), new Vector3( 1, 2, 1 ) );
		}
	}
}

[System.Serializable]
public struct TimelineHelperEvent
{
	public new string name;
	public KeyCode key;
	public EventType eventType;
	public Color eventColor;
	[Space]
	public Vector3 position;
	public float timeStamp;
}

public enum EventType
{
	RUNE,
	TRANSITION,
	OBSTACLE
}
#endif