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

	void Start ()
    {
        body = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        movement = new Vector3(-horizontal * speed, 0, -vertical * speed);
    }

    void FixedUpdate()
    {
        body.velocity = movement;
    }

    private void OnTriggerEnter(Collider other)
    {
   
    }
}
