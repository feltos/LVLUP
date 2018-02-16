using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {

	private LineRenderer line ;
	private float multiplier;
	private float length;
	public float maxLength;


	// Use this for initialization
	void Start () {
		line = gameObject.GetComponent<LineRenderer> ();

		//Multiplicateur de valeurs
		Vector3 vector = transform.localScale;
		multiplier = vector.z;
		//On définit la longueur du rayon de départ
		vector = new Vector3 (0, 0, maxLength/multiplier);
		line.SetPosition (1, vector);


	}
	
	// Update is called once per frame
	void Update () {
		setLength ();

	}

	private void setLength(){
		RaycastHit hit;
		if (Physics.Raycast (line.GetPosition (0), Vector3.forward, out hit)) {
			line.SetPosition (1, new Vector3 (0, 0, hit.distance/multiplier));
			if (hit.collider.tag == "Receiver") {
				print ("true");
			
			}
		} else {
			line.SetPosition(1,new Vector3(0,0,maxLength));

		}
	}
}
