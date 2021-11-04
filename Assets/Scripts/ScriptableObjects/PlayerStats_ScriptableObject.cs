using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats_ScriptableObject : ScriptableObject
{
	public int playerCharacterIndex = 0;	// Reference to the player character select. 0 - 3 indicates which sprites we have to load.

	//TODO: Lijst maken van alle mogelijke endings die de speler kan behalen. Maybe zo simpel als een paar booleans.
}
