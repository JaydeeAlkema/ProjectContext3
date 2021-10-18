using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWarning : MonoBehaviour
{

    [SerializeField] private GameObject enemies = default;
    [SerializeField] private GameObject enemyWarning = default;
    [SerializeField] private GameObject enemy = default;
    [SerializeField] private GameObject obstacles = default;
    [SerializeField] private GameObject obstacleWarning = default;
    [SerializeField] private GameObject obstacle = default;

    void Update()
    {
        ShowEnemyWarning();
        ShowObstacleWarning();
    }

    private void ShowEnemyWarning()
    {
        if( enemyWarning != null )
        {
            if( enemies.GetComponentInChildren<EnemyBehaviour>() != null )
            {
                enemy = enemies.GetComponentInChildren<EnemyBehaviour>().gameObject;
            }

            if( enemy != null )
            {
                if( transform.position.x - enemy.transform.position.x <= 10f )
                {
                    enemyWarning.SetActive( true );
                }

                if( enemy.transform.position.x - transform.position.x >= 2f )
                {
                    enemyWarning.SetActive( false );
                    enemy = null;
                }
            }
        }
    }

    private void ShowObstacleWarning()
    {
        if( obstacleWarning != null )
        {
            if( obstacles.GetComponentInChildren<EnemyBehaviour>() != null )
            {
                obstacle = obstacles.GetComponentInChildren<EnemyBehaviour>().gameObject;
            }

            if( obstacle != null )
            {
                if( transform.position.x - obstacle.transform.position.x >= 10f )
                {
                    obstacleWarning.SetActive( true );
                }

                if( obstacle.transform.position.x - transform.position.x <= -2f )
                {
                    obstacleWarning.SetActive( false );
                    obstacle = null;
                }
            }
        }
    }
}
