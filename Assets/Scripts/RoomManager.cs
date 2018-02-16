using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    [SerializeField]
    GameObject door;
    [SerializeField]Dalle dalle;

    [SerializeField]
    Button button;

    public enum State
    {
        NO_ONE,
        KEY,
        BUTTON,
        DALLE
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
                if(button.GetClicked())
                {
                    door.transform.eulerAngles = new Vector3(0, -90);
                }
                break;

            case State.DALLE: 
                if(dalle.GetPressed())
                {
                    door.transform.eulerAngles = new Vector3(0, -90);
                }
                if(!dalle.GetPressed())
                {
                    door.transform.eulerAngles = new Vector3(0, 0);
                }
                break;
        }
	}
}
