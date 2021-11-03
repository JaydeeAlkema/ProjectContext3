using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRuneActivation : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private bool[] hits;
    public bool completed = false;
    [SerializeField]private int index = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        DrawRune();
        CheckforCompletion();
    }

    void DrawRune()
    {
        foreach( Touch touch in Input.touches )
        {
            if( touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled )
            {
                if(touch.phase == TouchPhase.Began)
                {
                    Debug.Log( "Touch began" );
                    if((touch.position.normalized - (Vector2)points[index].position.normalized).sqrMagnitude <= 0.1f && index == 0)
                    {
                        Debug.Log( "Touch hit" );
                        hits[index] = true;
                        index++;
					}
				}

                if(touch.phase == TouchPhase.Moved)
                {
                    Debug.Log( "Touch moved" );
                    if((touch.position.normalized - (Vector2)points[index].position.normalized).sqrMagnitude <= 0.1f && index != 0)
                    {
                        Debug.Log( "Touch hit" );
                        hits[index] = true;
                        if( index < hits.Length - 1 ) { index++; }
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
}
