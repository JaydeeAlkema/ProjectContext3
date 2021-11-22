using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{

    public void GoToMenu()
    {
        SceneManager.LoadScene( "Menu");
    }

    public void GoToGallery()
    {
        SceneManager.LoadScene( "Gallery");
    }

    public void GoToGame()
    {
        SceneManager.LoadScene( "Game");
    }
}
