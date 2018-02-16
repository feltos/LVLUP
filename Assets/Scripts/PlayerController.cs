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

    ParticleSystem shockParticle;
    bool particlePlayed  = false;

    public bool objectInHand = false;

    bool textShownOnce = false;
    bool textHideOnce = false;

	void Start ()
    {
        Transform tmp = transform.Find("StartShock");
        if(tmp != null) {
            shockParticle = tmp.GetComponent<ParticleSystem>();
        }

        body = GetComponent<Rigidbody>();
        spring = GetComponent<ConfigurableJoint>();
	}
	
	void Update ()
    {
        horizontal = ControllersManager.Instance.GetAxis("Horizontal", playerIndex);
        vertical = ControllersManager.Instance.GetAxis("Vertical", playerIndex);
        movement = new Vector3(-horizontal * speed, 0, -vertical * speed);

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
            FindObjectOfType<CameraController>().AddShakeDuration(0.1f);
        }

        if(springed)
        {
            springTimer += Time.deltaTime;

            if(Vector3.Distance(transform.position, otherPlayer.position) < 1 && shockParticle != null && !particlePlayed) {
                shockParticle.Play();
                particlePlayed = true;
            }

            if (springTimer >= 0.2f)
            {
                particlePlayed = false;
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
