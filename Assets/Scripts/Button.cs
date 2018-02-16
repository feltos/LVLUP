using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool clicked = false;

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player") && Input.GetButtonDown("Fire1"))
        {
            clicked = true;
        }
    }

    public bool GetClicked()
    {
        return clicked;
    }
}
