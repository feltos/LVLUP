using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllersManager : MonoBehaviour {

    #region Private Variable
    int numberOfPlayer = 2;
    int numberOfGamepadConnected;

    bool forcedToUseKeyboardAlone;
    #endregion

    #region Singleton

    static ControllersManager instance;
    public static ControllersManager Instance {
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

    #endregion

    #region Unity Methods
    void Start() {
        LookNumberOfGamepadConnected();
    }

    // Update is called once per frame
    void Update () {
        //Check number of player
        if(numberOfPlayer == 0) {
            Debug.LogWarning("Controllers Manager: Is there realy 0 player ?");
        }

        //Check number of gamepad pluged in
        LookNumberOfGamepadConnected();
    }

    #endregion

    #region Private Methods

    void LookNumberOfGamepadConnected() {
        int previousNumberOfGamepadConnected = numberOfGamepadConnected;

        numberOfGamepadConnected = Input.GetJoystickNames().Length;

        foreach(string str in Input.GetJoystickNames()) {
            if(str == "") {
                numberOfGamepadConnected--;
            }
        }

        if(previousNumberOfGamepadConnected < numberOfGamepadConnected) {
            OnGamePadConnected();
        }

        if(previousNumberOfGamepadConnected > numberOfGamepadConnected) {
            OnGamePadDisconnected();
        }

        previousNumberOfGamepadConnected = numberOfGamepadConnected;
    }

    void SetNumberOfPlayer(int player) {
        numberOfPlayer = player;
    }

    void OnGamePadConnected() {
        CheckIfEnoughGamepad();
        Debug.LogWarning("Controllers Manager: A Gamepad has been connected");
    }

    void OnGamePadDisconnected() {
        CheckIfEnoughGamepad();
        Debug.LogWarning("Controllers Manager: A Gamepad has been connected");
    }

    void CheckIfEnoughGamepad() {
        int offsetGamepad = numberOfPlayer - numberOfGamepadConnected;
        switch(offsetGamepad) {
            case 2:
                forcedToUseKeyboardAlone = false;
                break;

            case 1:
                forcedToUseKeyboardAlone = true;
                break;

            case 0:
                forcedToUseKeyboardAlone = false;
                break;

            case -1:
                forcedToUseKeyboardAlone = false;
                break;

            case -2:
                forcedToUseKeyboardAlone = false;
                break;
        }
    }

    string GetEndOfInputName(int indexPlayer) {
        string endOfNameInput;

        if(forcedToUseKeyboardAlone && indexPlayer == 0) {
            endOfNameInput = "_0";
        } else if(indexPlayer == 0) {
            endOfNameInput = "";
        } else if(forcedToUseKeyboardAlone) {
            endOfNameInput = "_" + indexPlayer.ToString();
        } else {
            endOfNameInput = "_" + (indexPlayer + 1).ToString();
        }
        
        return endOfNameInput;
    }

    #endregion

    #region Public Methods

    public int GetNumberOfGamePadConnected() {
        return numberOfGamepadConnected;
    }

    #endregion

    #region Input Methods

    public bool GetButton(string buttonName, int indexPlayer = 0) {
        return Input.GetButton(buttonName + GetEndOfInputName(indexPlayer));
    }

    public bool GetButtonDown(string buttonName, int indexPlayer = 0) {
        return Input.GetButtonDown(buttonName + GetEndOfInputName(indexPlayer));
    }

    public bool GetButtonUp(string buttonName, int indexPlayer = 0) {
        return Input.GetButtonUp(buttonName + GetEndOfInputName(indexPlayer));
    }

    public float GetAxis(string axisName, int indexPlayer = 0) {
        if(Input.GetJoystickNames()[indexPlayer] == "Wireless Controller") {
            if(axisName == "Horizontal") {
                return Input.GetAxis(axisName + GetEndOfInputName(indexPlayer));
            } else {
                return (-1) * Input.GetAxis(axisName + GetEndOfInputName(indexPlayer));
            }
        } else {
            return Input.GetAxis(axisName + GetEndOfInputName(indexPlayer));
        }
    }

    public float GetAxisRaw(string axisName, int indexPlayer = 0) {
        return Input.GetAxisRaw(axisName + GetEndOfInputName(indexPlayer));
    }

    public bool GetKey(string name) {
        return Input.GetKey(name);
    }

    public bool GetKeyDown(string name) {
        return Input.GetKeyDown(name);
    }

    public bool GetKeyUp(string name) {
        return Input.GetKeyUp(name);
    }

    public bool GetMouseButton(int button) {
        return Input.GetMouseButton(button);
    }

    public bool GetMouseButtonDown(int button) {
        return Input.GetMouseButtonDown(button);
    }

    public bool GetMouseButtonUp(int button) {
        return Input.GetMouseButtonUp(button);
    }

    #endregion
}
