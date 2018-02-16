using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    AudioClip openDoor;
    [SerializeField]
    AudioClip closeDoor;
    [SerializeField]
    AudioClip dallePressed;
    [SerializeField]
    AudioClip pickObject;
    [SerializeField]
    AudioClip pickKey;
    [SerializeField]
    AudioClip buttonPress;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Multiple instances of SoundEffects!");
        }
        Instance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void MakeSound(AudioClip OriginalClip)
    {
        AudioSource.PlayClipAtPoint(OriginalClip, transform.position);
    }

    public void OpenDoor()
    {
        Debug.Log("openDoor");
        MakeSound(openDoor);
    }

    public void CloseDoor()
    {
        Debug.Log("closeDoor");
        MakeSound(closeDoor);
    }

    public void DallePresses()
    {
        Debug.Log("DallePressed");
        MakeSound(dallePressed);
    }

    public void PickObject()
    {
        Debug.Log("PickObject");
        MakeSound(pickObject);
    }

    public void PickKey()
    {
        Debug.Log("PickKey");
        MakeSound(pickKey);
    }

    public void ButtonPress()
    {
        Debug.Log("ButtonPress");
        MakeSound(buttonPress);
    }
}
