using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
	private static AchievementManager instance;

	[SerializeField] private Achievement_ScriptableObject[] achievements;
	[SerializeField] private Achievement_ScriptableObject latestUnlockedAchievement;

	#region Properties
	public static AchievementManager Instance { get => instance; set => instance = value; }
	public Achievement_ScriptableObject LatestUnlockedAchievement { get => latestUnlockedAchievement; set => latestUnlockedAchievement = value; }
	#endregion

	private void Awake()
	{
		if( instance != null || instance != this )
		{
			instance = null;
			instance = this;
		}
	}

	public Achievement_ScriptableObject GetAchievementByID( string ID )
	{
		foreach( Achievement_ScriptableObject achievement in achievements )
		{
			if( achievement.ID == ID )
			{
				return achievement;
			}
		}
		return null;
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

				latestUnlockedAchievement = achievement;

				Debug.Log( $"Achievement Unlocked: [{achievement.name}]" );
			}
		}
	}
}
