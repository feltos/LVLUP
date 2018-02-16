using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dalle : MonoBehaviour
{
    public bool pressed = false;
    bool pressedByPickable = false;

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pickable")) {
            pressed = true;
            pressedByPickable = true;
            AudioManager.Instance.DallePresses();
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            pressed = true;
            AudioManager.Instance.DallePresses();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pickable")) {
            pressed = false;
            pressedByPickable = false;
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Player") && !pressedByPickable) {
            pressed = false;
        }
    }

    public bool GetPressed()
    {
        return pressed;
    }
}
