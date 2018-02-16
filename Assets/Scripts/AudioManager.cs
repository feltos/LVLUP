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
        MakeSound(openDoor);
    }

    public void CloseDoor()
    {
        MakeSound(closeDoor);
    }

    public void DallePresses()
    {
        MakeSound(dallePressed);
    }

    public void PickObject()
    {
        MakeSound(pickObject);
    }

    public void PickKey()
    {
        MakeSound(pickKey);
    }

    public void ButtonPress()
    {
        MakeSound(buttonPress);
    }
}
