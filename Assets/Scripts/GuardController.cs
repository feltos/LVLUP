using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour {

    [SerializeField]
    float speed = 2;
    [SerializeField]
    List<Transform> waypoints;
    [SerializeField]
    GameObject sprite;

    Animator animatorController;

    int indexOfTarget;

	// Use this for initialization
	void Start () {
        float minDistance = Mathf.Infinity;

		foreach(Transform point in waypoints) {
            if(Vector3.Distance(transform.position, point.position) < minDistance) {
                indexOfTarget = waypoints.IndexOf(point);
                minDistance = Vector3.Distance(transform.position, point.position);
            }
        }

        animatorController = sprite.GetComponent<Animator>();
    }

    void FixedUpdate() {
        //Look at
        var targetRotation = Quaternion.LookRotation(waypoints[indexOfTarget].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

        //Goes to
        Vector3 lastPos = transform.position;
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z),
             new Vector3(waypoints[indexOfTarget].position.x, transform.position.y, waypoints[indexOfTarget].position.z), speed * Time.deltaTime);

        
        sprite.transform.eulerAngles = new Vector3(-45, 0, 0);

        //Animation
        animatorController.SetBool("LookUp", false);
        animatorController.SetBool("LookDown", false);
        animatorController.SetBool("LookLeft", false);
        animatorController.SetBool("LookRight", false);

        if(Mathf.Abs(lastPos.x - transform.position.x) < Mathf.Abs(lastPos.z - transform.position.z)) {
            if(lastPos.z - transform.position.z > 0) {
                animatorController.SetBool("LookUp", true);
            } else {
                animatorController.SetBool("LookDown", true);
            }
        } else {
            if(lastPos.x - transform.position.x < 0) {
                animatorController.SetBool("LookLeft", true);
            } else {
                animatorController.SetBool("LookRight", true);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		if(Vector3.Distance(transform.position, waypoints[indexOfTarget].position) <= 1) {
            indexOfTarget++;
            if(indexOfTarget >= waypoints.Count) {
                indexOfTarget = 0;
            }
        }
	}
}
