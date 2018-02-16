using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    [SerializeField]
    List<DoorController> doors;
    [SerializeField]
    Dalle dalle;
    [SerializeField]
    float timeForFinishingRoom;
    [SerializeField]
    Button button;

    bool enterRoom = false;
    bool isFinished = false;
    int playerInRoom = 0;

    public enum State
    {
        NO_ONE,
        KEY,
        BUTTON,
        DALLE,
		LAZER
    }

    public State state = State.NO_ONE;

	void Start ()
    {

	}
	
	void Update ()
    {
        if(playerInRoom == 2 && !enterRoom) {
            enterRoom = true;
            CloseDoor();
        }

        if(enterRoom && !TimerController.Instance.IsRunning()) {
            TimerController.Instance.SetTimeForLevel(timeForFinishingRoom); 
        }

        if(TimerController.Instance.IsRunning()) {
            if(TimerController.Instance.HasFinished())
            {
                GameManager.Instance.Lose();
            }

            if(enterRoom && playerInRoom == 0) {
                TimerController.Instance.ResetTimer();
            }
        }

		switch(state)
        {
            case State.NO_ONE:
                break;

            case State.KEY:
                break;

            case State.BUTTON:
                if(button.GetClicked() && enterRoom)
                {
                    OpenDoor();
                }
                break;

            case State.DALLE: 
                if(dalle.GetPressed() && enterRoom)
                {
                    OpenDoor();
                }
                if(!dalle.GetPressed() && enterRoom)
                {
                    CloseDoor();
                }
                break;
        }
	}

    void OpenDoor() {
        foreach(DoorController door in doors) {
            door.OpenDoor();
        }
    }

    void CloseDoor() {
        foreach(DoorController door in doors) {
            door.CloseDoor();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            playerInRoom++;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player") {
            playerInRoom--;
        }
    }
}
