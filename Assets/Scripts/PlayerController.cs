using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontal;
    float vertical;
    Vector3 movement;
    Rigidbody body;
    [SerializeField]
    float speed;
    SpringJoint spring;
    bool springed;
    public bool objectInHand = false;

    [SerializeField]
    int playerIndex;

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
    }

    void FixedUpdate()
    {
        body.velocity = movement;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    public int GetPlayerIndex() {
        return playerIndex;
    }
}
