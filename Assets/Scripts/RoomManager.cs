using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    
    List<DoorController> doors;
    [SerializeField]
    Dalle dalle;
    [SerializeField]
    Button button;

    bool enterRoom = false;
    bool isFinished = false;
    int playerInRoom = 0;

    bool doorClosed = false;

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
        doors = new List<DoorController>();

        foreach(Transform tmp in transform) {
            if(tmp.name.Contains("MurPorte")) {
                foreach(Transform tmp2 in tmp.transform) {
                    if(tmp2.GetComponent<DoorController>() != null) {
                        doors.Add(tmp2.GetComponent<DoorController>());
                    }
                }
            }
        }

        foreach(DoorController door in doors) {
            door.SetRoomManager(this);
        }
	}
	
	void Update ()
    {
        if(!enterRoom && PlayerAreReallyInside()) {
            enterRoom = true;
            AudioManager.Instance.CloseDoor();
            CloseDoor();
        }

		switch(state)
        {
            case State.NO_ONE:
                if(enterRoom && !isFinished) {
                    foreach(DoorController door in doors) {
                        Collider[] targetsInViewRadius = Physics.OverlapSphere(door.transform.position, 1);

                        foreach(Collider other in targetsInViewRadius) {
                            if(other.gameObject.layer == LayerMask.NameToLayer("Player")) {
                                OpenDoor();
                                isFinished = true;
                            }
                        }
                    }
                }
                break;

            case State.KEY:
                if(enterRoom && !isFinished) {
                    foreach(DoorController door in doors) {
                        Collider[] targetsInViewRadius = Physics.OverlapSphere(door.transform.position, 1);

                        foreach(Collider other in targetsInViewRadius) {
                            if(other.gameObject.name.Contains("Key")){
                                Destroy(other.gameObject);
                                OpenDoor();
                                isFinished = true;
                            }
                        }
                    }
                }
                break;

            case State.BUTTON:
                if(button.GetClicked() && enterRoom)
                {
                    OpenDoor();
                }
                break;

            case State.DALLE:
                if(enterRoom) {
                    if(dalle.GetPressed() && doorClosed) {
                        OpenDoor();
                        isFinished = true;
                    }else if(!dalle.GetPressed() && !doorClosed) {
                        CloseDoor();
                        isFinished = false;
                    }
                }
                break;
        }
	}

    bool PlayerAreReallyInside() {
        if(playerInRoom != 2) {
            return false;
        }

        foreach(DoorController door in doors) {
            Collider[] targetsInViewRadius = Physics.OverlapSphere(door.transform.position, 1);
            
            foreach(Collider other in targetsInViewRadius) {
                if(other.gameObject.layer == LayerMask.NameToLayer("Player")) {
                    return false;
                }
            }
        }

        return true;
    }

    void OpenDoor() {
        foreach(DoorController door in doors) {
            door.OpenDoor();
        }
        doorClosed = false;
    }

    void CloseDoor() {
        foreach(DoorController door in doors) {
            door.CloseDoor();
        }
        doorClosed = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            playerInRoom++;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            playerInRoom--;
        }
    }
}
