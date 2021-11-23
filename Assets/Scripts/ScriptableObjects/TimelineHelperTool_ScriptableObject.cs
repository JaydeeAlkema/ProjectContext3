using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Timeline Helper Tool Events", menuName = "ScriptableObjects/New Timeline Helper Tool Events" )]
public class TimelineHelperTool_ScriptableObject : ScriptableObject
{
	public List<TimelineHelperEvent> Events = new List<TimelineHelperEvent>();

	public void Add( TimelineHelperEvent _event )
	{
		Events.Add( _event );
	}

	public void Remove( TimelineHelperEvent _event )
	{
		Events.Remove( _event );
	}

	public void Clear()
	{
		Events.Clear();
	}
}
