using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRuneActivation : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private bool[] hits;
    public bool completed = false;
    public bool failed = false;
    [SerializeField]private int index = 0;
    [SerializeField] private float timeLeft = 30f;
    [SerializeField] private float drawError = 0.1f;

    // Update is called once per frame
    void FixedUpdate()
    {
        DrawRune();
        CheckforCompletion();
        timeLeft -= Time.deltaTime;
        if( timeLeft <= 0 )
        {
            failed = true;
        }
    }

    void DrawRune()
    {
        //foreach( Touch touch in Input.touches )
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch( 0 );
            Vector3 touchPos = Camera.main.ScreenToWorldPoint( touch.position );
            touchPos.z = 0;
            if( touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled )
            {
                if(touch.phase == TouchPhase.Began)
                {
                    Debug.Log( "Touch began" );
                    if((touchPos - points[index].position).magnitude <= drawError && index == 0)
                    {
                        Debug.Log( "Touch hit" );
                        hits[index] = true;
                        if(hits[index]){ index++; }
					}
				}

                if(touch.phase == TouchPhase.Moved)
                {
                    Debug.Log( "Touch moved" );
                    if((touchPos - points[index].position).magnitude <= drawError && index != 0)
                    {
                        Debug.Log( "Touch hit" );
                        hits[index] = true;
                        if( hits[index] && index < hits.Length - 1 ) { index++; }
					}
					
				}

                if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    if(!completed)
                    {
                        hits.Equals(false);
                        index = 0;
					}
				}
            }
        }
	}

    bool CheckforCompletion()
    {
        if(hits[hits.Length-1] == true)
        {
            completed = true;
		}
        return completed;
	}

    public void RuneFailed()
    {

    }
}
