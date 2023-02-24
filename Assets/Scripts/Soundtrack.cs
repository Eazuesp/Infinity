using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    public AudioClip[] soundtrack;
    public new AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio.clip = soundtrack[Random.Range(0, soundtrack.Length)];
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
        {
            audio.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            audio.Play();
        }
    }
}
