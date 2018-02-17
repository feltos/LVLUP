using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    static GameManager instance;
    public static GameManager Instance {
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

    void Start() {
        TimerController.Instance.SetTimeForLevel(300);
    }

    // Update is called once per frame
    void Update () {
        if(TimerController.Instance.HasFinished()) {
            Lose();
        }

        if(SceneManager.GetActiveScene().name == "Menu" && ControllersManager.Instance.GetKeyDown("escape")) {
            Debug.Log("Quitte");
            Application.Quit();
        }

        if(SceneManager.GetActiveScene().name == "Credits" && ControllersManager.Instance.GetKeyDown("escape")) {
            LoadScene("Menu");
        }

        if(SceneManager.GetActiveScene().name == "Lose" && ControllersManager.Instance.GetKeyDown("escape")) {
            LoadScene("Menu");
        }
        if(SceneManager.GetActiveScene().name == "Win" && ControllersManager.Instance.GetKeyDown("escape")) {
            LoadScene("Menu");
        }

    }

    public void Lose() {
        SceneManager.LoadScene("Lose");
    }

    public void LoadScene(string name) {
        SceneManager.LoadScene(name);
    }
}
