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

    public float horizontal;
    public float vertical;
    Vector3 movement;
    Rigidbody body;
    ConfigurableJoint spring;
    bool springed;
    float springTimer;
    [SerializeField]
    Transform otherPlayer;

    public bool objectInHand = false;

    bool textShownOnce = false;
    bool textHideOnce = false;

	void Start ()
    {
        body = GetComponent<Rigidbody>();
        spring = GetComponent<ConfigurableJoint>();
	}
	
	void Update ()
    {
        horizontal = ControllersManager.Instance.GetAxis("Horizontal", playerIndex);
        vertical = ControllersManager.Instance.GetAxis("Vertical", playerIndex);
        movement = new Vector3(-horizontal * speed, 0, vertical * speed);

        if(textShownOnce && !textHideOnce)
        {
            informationnalText.text = "You can interact with this object";
        }
        else if(textHideOnce && !textShownOnce)
        {
            informationnalText.text = "";
        }

        textShownOnce = false;
        textHideOnce = false;

        if(Vector3.Distance(transform.position,otherPlayer.position) >= 3.3)
        {
            springed = true;
        }

        if(springed)
        {
            springTimer += Time.deltaTime;
            if (springTimer >= 0.2f)
            {
                springed = false;
            }
        }
    }

    void FixedUpdate()
    {
        if(!springed)
        {
            body.velocity = movement;
            springTimer = 0.0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Interactive")
        {
            textShownOnce = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Interactive")
        {
            textHideOnce = true;
        }
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }
}
