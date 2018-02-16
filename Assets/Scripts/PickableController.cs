using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableController : MonoBehaviour
{
    Transform player;
    bool grabbed;

	void Update ()
    {
		if(grabbed)
        {
            Debug.Log("gbfardgd");
            transform.position = player.position + Vector3.forward;
        }
        /*if(grabbed && Input.GetButtonDown("Fire1"))
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            grabbed = false;
        }*/
        
	}

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player") && Input.GetButtonDown("Fire1"))
        {
            player = collision.gameObject.transform;
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            grabbed = true;
        }
    }
}
