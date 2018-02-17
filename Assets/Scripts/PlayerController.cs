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

    Animator animatorController;

    void Start ()
    {
        Transform tmp = transform.Find("StartShock");
        if(tmp != null) {
            shockParticle = tmp.GetComponent<ParticleSystem>();
        }

        body = GetComponent<Rigidbody>();
        spring = GetComponent<ConfigurableJoint>();

        animatorController = GetComponent<Animator>();
        if(animatorController == null) {
            Debug.LogError("A Animatore is missing");
        }
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

        //Animation
        float hor = (-1)*body.velocity.x;
        float ver = (-1)*body.velocity.z;
        animatorController.SetBool("LookUp", false);
        animatorController.SetBool("LookDown", false);
        animatorController.SetBool("LookLeft", false);
        animatorController.SetBool("LookRight", false);

        if(hor == 0 && ver == 0) {
            animatorController.SetBool("Idle", true);
        } else {
            animatorController.SetBool("Idle", false);
            if(Mathf.Abs(hor) < Mathf.Abs(ver)) {
                if(ver > 0) {
                    animatorController.SetBool("LookUp", true);
                } else {
                    animatorController.SetBool("LookDown", true);
                }
            } else {
                if(hor < 0) {
                    animatorController.SetBool("LookLeft", true);
                } else {
                    animatorController.SetBool("LookRight", true);
                }
            }
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
