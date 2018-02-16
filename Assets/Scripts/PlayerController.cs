using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Text informationnalText;
    [SerializeField]
    int playerIndex;
    [SerializeField]
    float speed;

    float horizontal;
    float vertical;
    Vector3 movement;
    Rigidbody body;
    SpringJoint spring;
    bool springed;

    public bool objectInHand = false;

    bool textShownOnce = false;
    bool textHideOnce = false;

	void Start ()
    {
        body = GetComponent<Rigidbody>();
        spring = GetComponent<SpringJoint>();
	}
	
	void Update ()
    {
        horizontal = ControllersManager.Instance.GetAxis("Horizontal", playerIndex);
        vertical = ControllersManager.Instance.GetAxis("Vertical", playerIndex);
        movement = new Vector3(-horizontal * speed, 0, -vertical * speed);

        if(textShownOnce && !textHideOnce) {
            informationnalText.text = "You can interact with this object";
        }else if(textHideOnce && !textShownOnce) {
            informationnalText.text = "";
        }

        textShownOnce = false;
        textHideOnce = false;
    }

    void FixedUpdate()
    {
        body.velocity = movement;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Interactive") {
            textShownOnce = true;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if(collision.gameObject.tag == "Interactive") {
            textHideOnce = true;
        }
    }

    public int GetPlayerIndex() {
        return playerIndex;
    }
}
