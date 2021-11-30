using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWarning : MonoBehaviour
{
    public GameObject showWarning;
    [SerializeField] private PlayerRuneActivation runes = default;
    [SerializeField] private float minDistance = 35f;
    [SerializeField] private GameObject obstacle = default;

	private void Start()
	{
        runes = GetComponent<PlayerRuneActivation>();
	}

	void Update()
    {
     //   ShowObstacleWarning();
        CheckDistance();
    }

    private void ShowObstacleWarning()
    {
        if(runes.completed)
        {
            showWarning.SetActive( true );
		}
    }

    void CheckDistance()
    {
        if( ( this.transform.position - obstacle.transform.position ).magnitude <= minDistance )
        {
            showWarning.SetActive( true );
        }
    }
}
