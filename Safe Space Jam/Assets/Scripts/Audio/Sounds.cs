using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    // Made using the help of Brackeys Tutorials - https://www.youtube.com/user/Brackeys
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.5f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    [Range(0.1f, 0.3f)]
    public float fadeSpeed = 0.1f;

    public bool loop;
    public bool fadeIn;
    public bool fadeOut;

    [HideInInspector]
    public AudioSource source;
}