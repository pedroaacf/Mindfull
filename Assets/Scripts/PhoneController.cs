using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneController : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    void Start()
    {
        Invoke("Ring", 5f);
        Invoke("RingStop", 25f);
    }

    void Ring()
    {
        source.clip = clip;
        source.loop = true;
        source.Play();
    }

    public void RingStop()
    {
        source.Stop();
    }
}
