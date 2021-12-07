using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShowWarning : MonoBehaviour
{
    public GameObject showWarning;
    [SerializeField] private PlayerRuneActivation runes = default;
    [SerializeField] private float minDistance = 35f;
    [SerializeField] private GameObject obstacles = default;
    public List<GameObject> individualObstacles = new List<GameObject>();
    public int obstacleIndex = 0;

	private void Start()
	{
        runes = GetComponent<PlayerRuneActivation>();
		foreach( Transform obstacle in obstacles.GetComponentsInChildren<Transform>() )
		{
            individualObstacles.Add( obstacle.gameObject );
            individualObstacles.Sort(delegate(GameObject a, GameObject b) { return Vector2.Distance( this.transform.position, a.transform.position ).CompareTo( Vector2.Distance( this.transform.position, b.transform.position ) ); } );
		}
        individualObstacles.Remove( individualObstacles.First<GameObject>() );
	}

	void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if( individualObstacles.Any<GameObject>() == true)
        {
			if( ( individualObstacles[0].transform.position - this.transform.position ).magnitude <= minDistance)
			{
				showWarning.SetActive( true );
			}
			else
			{
				showWarning.SetActive( false );
			}
        }
        else
        {
            showWarning.SetActive( false );
		}
    }
}
