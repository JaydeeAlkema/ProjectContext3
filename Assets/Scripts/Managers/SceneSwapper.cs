using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{

    public void GoToMenu()
    {
        SceneManager.LoadScene( "Menu");
        //if( SceneManager.GetActiveScene().name == "Game" ) { SceneManager.UnloadSceneAsync( "Game" ); }
        //else { SceneManager.UnloadSceneAsync( "Gallery" ); }
    }

    public void GoToGallery()
    {
        SceneManager.LoadScene( "Gallery");
        //if( SceneManager.GetActiveScene().name == "Game" ) { SceneManager.UnloadSceneAsync( "Game" ); }
        //else { SceneManager.UnloadSceneAsync( "Menu" ); }
    }

    public void GoToGame()
    {
        SceneManager.LoadScene( "Game");
        //if( SceneManager.GetActiveScene().name == "Menu" ) { SceneManager.UnloadSceneAsync( "Menu" ); }
        //else { SceneManager.UnloadSceneAsync( "Gallery" ); }
    }
}
