using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

    [SerializeField]
    Text textMinutes;
    [SerializeField]
    Text textSeconds;

    float timeInSeconds = 0;
    
    bool isRunning = false;
    bool hasFinished = false;

    static TimerController instance;
    public static TimerController Instance {
        get {
            return instance;
        }
    }

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetTimeForLevel(float time) {
        timeInSeconds = time;
        StartCoroutine(ClockAnimation());
    }

    public bool HasFinished() {
        return hasFinished;
    }

    public bool IsRunning() {
        return isRunning;
    }

    public void ResetTimer() {
        if(isRunning) {
            StopCoroutine(ClockAnimation());
        }
        textSeconds.text = "";
        textMinutes.text = "";
        isRunning = false;
        hasFinished = false;
    }

    IEnumerator ClockAnimation() {
        isRunning = true; 

        while(timeInSeconds > 0) {
            timeInSeconds -= 1;
            DisplayTime();

            yield return new WaitForSeconds(1);
        }
        hasFinished = true;
        isRunning = false;
    }

    void DisplayTime() {
        textMinutes.text = ((int)timeInSeconds / 60).ToString();
        if((timeInSeconds % 60) < 10) {
            textSeconds.text = "0" + (timeInSeconds % 60).ToString();
        } else {
            textSeconds.text = (timeInSeconds % 60).ToString();
        }
    }
}
