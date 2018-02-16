using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    bool open = false;
    float doorCloseTimer;

    void Update()
    {
        Debug.Log(doorCloseTimer);
        if (open)
        {
            doorCloseTimer += Time.deltaTime;
            if(doorCloseTimer >= 1f)
            {
                transform.eulerAngles = new Vector3(0, 0);
                this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                doorCloseTimer = 0.0f;
                open = false;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            transform.eulerAngles = new Vector3(0, -90);
            this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            open = true;
        }
    }
}
