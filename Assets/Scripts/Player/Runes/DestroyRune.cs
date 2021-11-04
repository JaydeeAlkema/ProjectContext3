using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRune : PlayerRuneActivation
{
    public Transform objectToDestroy;

	private void FixedUpdate()
	{
        DestroyObject();
	}

	void DestroyObject(){
        if(completed){
			if(objectToDestroy != null) { Destroy( objectToDestroy.gameObject ); }   
		}

        if(failed){
			if( objectToDestroy != null ) { Destroy( objectToDestroy.gameObject ); }
			RuneFailed();
		}
	}


}
