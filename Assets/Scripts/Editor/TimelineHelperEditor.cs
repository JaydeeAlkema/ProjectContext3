using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor( typeof( TimelineHelperTool ) )]
public class TimelineHelperEditor : Editor
{
	TimelineHelperTool timelineHelperTool;

	private void OnSceneGUI()
	{
		if( !timelineHelperTool )
			timelineHelperTool = FindObjectOfType<TimelineHelperTool>();

		if( !timelineHelperTool.showGizmos )
			return;

		timelineHelperTool.ShowHandles();

		Debug.Log( "Drawing Handles" );
		Debug.Log( timelineHelperTool.scriptableObject.Events.Count );
	}
}
