using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    bool open = false;
    float doorCloseTimer;
    RoomManager roomManager;

    private void Start() {
        roomManager = transform.parent.gameObject.GetComponent<RoomManager>();
    }

    void Update()
    {
        if (open)
        {
            doorCloseTimer += Time.deltaTime;
            if(doorCloseTimer >= 1f)
            {
                transform.eulerAngles = new Vector3(0, 0);
                doorCloseTimer = 0.0f;
                open = false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && roomManager.state == RoomManager.State.NO_ONE)
        {
            transform.eulerAngles = new Vector3(0, -90);
            open = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Key") && roomManager.state == RoomManager.State.KEY)
        {
            transform.eulerAngles = new Vector3(0, -90);
            Destroy(other.gameObject);
        }
    }


}
