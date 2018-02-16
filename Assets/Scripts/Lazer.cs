using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {

	private LineRenderer line ;
	private float length;
	public float maxLength;


	// Use this for initialization
	void Start () {
		line = gameObject.GetComponent<LineRenderer> ();

		//On définit la longueur du rayon de départ
		Vector3 vector = new Vector3 (0, 0, maxLength);
		line.SetPosition (1, vector);


	}
	
	// Update is called once per frame
	void Update () {
		setLength ();

	}

	private void setLength(){
		RaycastHit hit;
		Debug.DrawRay (transform.position, transform.forward, Color.green, 120);
		if (Physics.Raycast (transform.position, transform.forward, out hit)) {
			line.SetPosition (1, new Vector3 (0, 0, hit.distance));
			if (hit.collider.tag == "Receiver") {
				print ("true");
			
			}
		} else {
			line.SetPosition(1,new Vector3(0,0,maxLength));

		}
	}
}
