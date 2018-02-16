using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dalle : MonoBehaviour
{
    public bool pressed = false;

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pickable") || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            pressed = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pickable") || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            pressed = false;
        }
    }

    public bool GetPressed()
    {
        return pressed;
    }
}
