using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterGameManager : MonoBehaviour
{
	private AchievementManager aM;

	[SerializeField]
	private GameObject objectToShow;
	[SerializeField]
	private Image imageToShow;
	
	private void Awake()
	{
		aM = AchievementManager.Instance;
		imageToShow.sprite = aM.LatestUnlockedAchievement.icon;
		objectToShow.SetActive( true );
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
