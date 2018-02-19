using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFinale : MonoBehaviour {

    [SerializeField]
    GameObject prefabKey0;
    [SerializeField]
    List<GameObject> activeKey;

	// Use this for initialization
	void Start () {
        activeKey = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, 1);

        int keyBrought = 0;

        foreach(Collider other in targetsInViewRadius) {
            
            if(other.gameObject.name.Contains("KeyFinale")) {
               
                keyBrought++;
            }
            
        }

        if(keyBrought == 2) {
            OpenDoor();
        }
    }

    public void OpenDoor() {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 90, 0);

        GameManager.Instance.LoadScene("Win");
    }
}
