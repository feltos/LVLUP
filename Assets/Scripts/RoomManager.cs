using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    [SerializeField]
    GameObject door;
    [SerializeField]Dalle dalle;
    [SerializeField]int dalleCount;
    [SerializeField]int nmbDalle;

    [SerializeField]
    GameObject button;

    public enum State
    {
        NO_ONE,
        KEY,
        BUTTON,
        DALLE,
        SYMBOLS
    }
    public State state = State.NO_ONE;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		switch(state)
        {
            case State.NO_ONE:
                break;

            case State.KEY:
                break;

            case State.BUTTON:   
                break;

            case State.DALLE: 
                if(dalle.GetPressed())
                {
                    dalleCount++;
                }
                if(!dalle.GetPressed())
                {
                    
                }
                if(dalleCount == nmbDalle)
                {
                    door.transform.eulerAngles = new Vector3(0, -90);
                }
                else
                {
                    door.transform.eulerAngles = new Vector3(0, 0);
                }
                break;

            case State.SYMBOLS:
                break;
        }
	}
}
