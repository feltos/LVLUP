using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    bool open = false;

    float doorCloseTimer;
    RoomManager roomManager;

    private void Start() {
        roomManager = transform.parent.transform.parent.gameObject.GetComponent<RoomManager>();

        if(open) {
            OpenDoor();
        }
    }

    void Update()
    {
        
    }

    public void SetRoomManager(RoomManager tmp) {
        roomManager = tmp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && roomManager.state == RoomManager.State.NO_ONE)
        {
            //AudioManager.Instance.OpenDoor();
            //OpenDoor();
            //open = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Key") && roomManager.state == RoomManager.State.KEY)
        {
            AudioManager.Instance.OpenDoor();
            OpenDoor(); 
            Destroy(other.gameObject);
        }
    }

    public void OpenDoor() {      
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 90, 0);
    }

    public void CloseDoor() {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 90, 0);
    }
}
