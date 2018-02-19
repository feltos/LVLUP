using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableController : MonoBehaviour
{
    Transform player;
    bool grabbed;
    float pickTimer;
    PlayerController playerController;

    Vector3 lastPositionPlayer;

    void Update() {
        if(grabbed) {
            bool hasMoved = false; ;
            if(playerController.vertical < 0) {
                transform.position = player.transform.position + Vector3.forward;
                hasMoved = true;
            }

            if(playerController.vertical > 0) {
                transform.position = player.transform.position + Vector3.back;
                hasMoved = true;
            }

            if(playerController.horizontal < 0) {
                transform.position = player.transform.position + Vector3.right;
                hasMoved = true;
            }

            if(playerController.horizontal > 0) {
                transform.position = player.transform.position + Vector3.left;
                hasMoved = true;
            }

            if(!gameObject.name.Contains("Key")) {
                gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
            pickTimer += Time.deltaTime;

            if(!hasMoved) {
                transform.position = player.transform.position + Vector3.forward;
            }
        }

        if(grabbed && ControllersManager.Instance.GetButtonDown("Fire1", playerController.GetPlayerIndex()) && pickTimer >= 0.1f) {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            pickTimer = 0;
            if(!gameObject.name.Contains("Key")) {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
            playerController.objectInHand = false;
            grabbed = false;
        }
    }

    private void OnDestroy() {
        if(playerController != null) {
            playerController.objectInHand = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {

        if(collision.gameObject.layer == LayerMask.NameToLayer("Player") && 
            ControllersManager.Instance.GetButtonDown("Fire1", collision.gameObject.GetComponent<PlayerController>().GetPlayerIndex()) && 
            collision.gameObject.GetComponent<PlayerController>().objectInHand == false)
        {
            if(this.gameObject.layer == LayerMask.NameToLayer("Key"))
            {
                AudioManager.Instance.PickKey();
            }

            playerController = collision.gameObject.GetComponent<PlayerController>();
            player = collision.gameObject.transform;
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            collision.gameObject.GetComponent<PlayerController>().objectInHand = true;
            AudioManager.Instance.PickObject();
            
            grabbed = true;
        }
    }
}
