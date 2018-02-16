﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableController : MonoBehaviour
{
    Transform player;
    bool grabbed;
    float pickTimer;
    PlayerController playerController;

	void Update ()
    {
		if(grabbed)
        {
            if(playerController.vertical < 0)
            transform.position = player.transform.position + Vector3.forward;

            if (playerController.vertical > 0)
            transform.position = player.transform.position + Vector3.back;

            if (playerController.horizontal < 0)
            transform.position = player.transform.position + Vector3.right;

            if (playerController.horizontal > 0)
            transform.position = player.transform.position + Vector3.left;

            gameObject.GetComponent<Rigidbody>().useGravity = false;
            pickTimer += Time.deltaTime;
        }
        if(grabbed && ControllersManager.Instance.GetButtonDown("Fire1", playerController.GetPlayerIndex()) && pickTimer >= 0.1f)
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
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player") && 
            ControllersManager.Instance.GetButtonDown("Fire1", collision.gameObject.GetComponent<PlayerController>().GetPlayerIndex()) && 
            collision.gameObject.GetComponent<PlayerController>().objectInHand == false)
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            player = collision.gameObject.transform;
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            collision.gameObject.GetComponent<PlayerController>().objectInHand = true;
            grabbed = true;
        }
    }
}
