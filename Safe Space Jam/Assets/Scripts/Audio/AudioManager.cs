using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    // Made using the help of Brackeys Tutorials - https://www.youtube.com/user/Brackeys

    public Sounds[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (s.fadeIn)
            StartCoroutine(FadeIn(s));
        else s.source.Play();
        return;
    }

    public void StopPlay(string name)
    {

        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (s.fadeOut)
            StartCoroutine(FadeOut(s));
        else s.source.Stop();
        return;
    }

    IEnumerator FadeOut(Sounds s)
    {
        for (float i = s.source.volume; i > 0.0f; i -= (s.fadeSpeed * Time.deltaTime))
        {
            s.source.volume -= s.fadeSpeed * Time.deltaTime;
            yield return null;
        }
        s.source.volume = 0.0f;
        s.source.Stop();
    }

    IEnumerator FadeIn(Sounds s)
    {
        s.source.volume = 0.0f;
        s.source.Play();
        for (float i = s.source.volume; i < s.volume; i += (s.fadeSpeed * Time.deltaTime))
        {
            s.source.volume += s.fadeSpeed * Time.deltaTime;
            yield return null;
        }
        s.source.volume = s.volume;
    }
}