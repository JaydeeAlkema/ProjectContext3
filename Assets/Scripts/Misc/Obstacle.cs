using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField]private GameObject destroyRune = default;
	[SerializeField] private float minDistance = 25f;

	private void FixedUpdate()
	{
		CheckDistance();
	}

	void CheckDistance(){
		if(Vector3.Distance(destroyRune.transform.position, this.transform.position) <= minDistance){
			destroyRune.gameObject.SetActive( true );
			destroyRune.GetComponent<DestroyRune>().objectToDestroy = this.transform;
		}
	}
}
