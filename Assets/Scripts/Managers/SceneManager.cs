using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private int TargetFPS = 60;    
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = TargetFPS;
    }
}
