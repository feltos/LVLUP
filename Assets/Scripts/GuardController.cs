using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour {

    [SerializeField]
    float speed = 2;
    [SerializeField]
    List<Transform> waypoints;

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
	}

    void FixedUpdate() {
        //Look at
        var targetRotation = Quaternion.LookRotation(waypoints[indexOfTarget].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

        //Goes to
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z),
             new Vector3(waypoints[indexOfTarget].position.x, transform.position.y, waypoints[indexOfTarget].position.z), speed * Time.deltaTime);
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
