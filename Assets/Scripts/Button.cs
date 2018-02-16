using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool clicked = false;

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player") && 
            ControllersManager.Instance.GetButtonDown("Fire1", collision.gameObject.GetComponent<PlayerController>().GetPlayerIndex()))
        {
            clicked = true;
        }
    }

    public bool GetClicked()
    {
        return clicked;
    }
}
