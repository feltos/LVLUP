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
        roomManager = transform.parent.gameObject.GetComponent<RoomManager>();

        if(open) {
            OpenDoor();
        }
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && roomManager.state == RoomManager.State.NO_ONE)
        {
            OpenDoor();
            open = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Key") && roomManager.state == RoomManager.State.KEY)
        {
            OpenDoor(); 
            Destroy(other.gameObject);
        }
    }

    public void OpenDoor() {
        transform.eulerAngles = new Vector3(0, -90);
    }

    public void CloseDoor() { 
        transform.eulerAngles = new Vector3(0, 0);
    }
}
