using UnityEngine;

[CreateAssetMenu( fileName = "New Achievement", menuName = "ScriptableObjects/New Achievement" )]
public class Achievement_ScriptableObject : ScriptableObject
{
	public Sprite icon;

	public string name;
	public string description;
	public string ID;

	public int current;
	public int goal;

	public bool unlocked;
	public bool hidden;
}
