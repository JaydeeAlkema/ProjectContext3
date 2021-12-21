using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GalleryManager : MonoBehaviour
{
    private AchievementManager aM;
    public GameObject objectToShow;
    public Image imageToShow;
    
    // Start is called before the first frame update
    void Start()
    {
        aM = AchievementManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenEnding(string AchID)
    {
        if(aM.GetAchievementByID( AchID ).unlocked)
        {
            imageToShow.sprite = aM.GetAchievementByID( AchID ).icon;
            objectToShow.SetActive( true );
            Debug.Log( "Should show image" );
		}
	}
}
