using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
	private static AchievementManager instance;

	[SerializeField] private Achievement_ScriptableObject[] achievements;

	#region Properties
	public static AchievementManager Instance { get => instance; set => instance = value; }
	#endregion

	private void Awake()
	{
		if( instance != null || instance != this )
		{
			instance = null;
			instance = this;
		}
	}

	public void AddAchievementProgress( string ID, int value )
	{
		Achievement_ScriptableObject achievement = achievements.FirstOrDefault( x => x.ID == ID );

		if( !achievement.unlocked )
		{
			achievement.current += value;

			if( achievement.current >= achievement.goal )
			{
				achievement.current = achievement.goal;
				achievement.unlocked = true;

				Debug.Log( $"Achievement Unlocked: [{achievement.name}]" );
			}
		}
	}
}
