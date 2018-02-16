﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableController : MonoBehaviour
{
    Transform player;
    bool grabbed;
    float pickTimer;
    [SerializeField]PlayerController playerController;

	void Update ()
    {
		if(grabbed)
        {
            Debug.Log("gbfardgd");
            transform.position = player.position + Vector3.forward;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            pickTimer += Time.deltaTime;
        }
        if(grabbed && Input.GetButtonDown("Fire1") && pickTimer >= 1f)
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            pickTimer = 0;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            playerController.objectInHand = false;
            grabbed = false;
        }
        
	}

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player") && Input.GetButtonDown("Fire1") && playerController.objectInHand == false)
        {
            player = collision.gameObject.transform;
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            playerController.objectInHand = true;
            grabbed = true;
        }
    }
}
