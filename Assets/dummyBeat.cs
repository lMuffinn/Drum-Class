using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyBeat : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip audioClip;
    double oldTick;
    double newTick;
    double timeBetweenTicks;
    public double totalTime = 1;
    double timePassed = 0;
    public bool empty = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        oldTick = AudioSettings.dspTime;
        if (empty) Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        newTick = AudioSettings.dspTime;
        timeBetweenTicks = newTick - oldTick;
        oldTick = newTick;
        timePassed += timeBetweenTicks;
        if(timePassed >= totalTime)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            Debug.Log("played" + audioSource.clip);
            Destroy(this.gameObject);
        }
    }
}
