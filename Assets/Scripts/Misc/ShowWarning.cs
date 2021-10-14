using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWarning : MonoBehaviour
{

    [SerializeField] private GameObject enemies = default;
    [SerializeField] private GameObject warning = default;
    [SerializeField] private GameObject enemy = default;

    void Update()
    {
        if( warning != null )
        {
            if( enemies.GetComponentInChildren<EnemyBehaviour>() != null )
            {
                enemy = enemies.GetComponentInChildren<EnemyBehaviour>().gameObject;
            }

            if( enemy != null )
            {
                if( transform.position.x - enemy.transform.position.x <= 10f )
                {
                    warning.SetActive( true );
                }

                if( enemy.transform.position.x - transform.position.x >= 2f )
                {
                    warning.SetActive( false );
                    enemy = null;
                }
            }
        }
    }
}
