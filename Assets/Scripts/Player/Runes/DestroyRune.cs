using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRune : PlayerRuneActivation
{
    public Transform objectToDestroy;

	private void Update()
	{
        DestroyObject();
	}

	void DestroyObject(){
        if(completed){
			if(objectToDestroy != null) { Destroy( objectToDestroy.gameObject ); }
			this.gameObject.SetActive( false );
		}

        if(failed){
			if( objectToDestroy != null ) { Destroy( objectToDestroy.gameObject ); }
			RuneFailed();
			this.gameObject.SetActive( false );
		}
	}


}
