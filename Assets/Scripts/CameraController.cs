using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    public float shakeDuration = 0f;
    
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    List<PlayerController> players;

    Vector3 offsetShaking;

    // Use this for initialization
    void Start () {
        players = new List<PlayerController>();
        players.AddRange(FindObjectsOfType<PlayerController>());
    }

    private void FixedUpdate() {
        Vector3 target = Vector3.Lerp(players[0].transform.position, players[1].transform.position, 0.5f);

        transform.position = target + new Vector3(2.2f, 20, 10);

        if(shakeDuration <= 0) {
            originalPos = transform.localPosition;
        }
    }

    // Update is called once per frame
    void Update () {
        if(shakeDuration > 0) {
            transform.position = transform.position + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        } else {
            shakeDuration = 0f;
        }
    }

    public void AddShakeDuration(float amount) {
        shakeDuration += amount;
    }
}
