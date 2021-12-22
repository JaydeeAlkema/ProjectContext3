using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedUp : MonoBehaviour
{
    public GameObject sceneManager;
    AudioSource song;

    void Start()
    {
       Time.timeScale = 5f; 
       Time.fixedDeltaTime= Time.fixedDeltaTime * Time.timeScale;
       song = sceneManager.GetComponent<AudioSource>();
       song.pitch = Time.timeScale;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime= Time.fixedDeltaTime * Time.timeScale;
            song.pitch = Time.timeScale;
        }
    }

}
