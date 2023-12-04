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
    bool played = false;
    double timePlayed;
    bool leave = false;

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
        if (leave & !played)
        {
            audioSource.Play();
            played = true;
            timePlayed = timePassed;
        }
        if(timePassed >= timePlayed + audioSource.clip.length && played) Destroy(this.gameObject);
    }

    public void GetGone()
    {
        leave = true;
    }

}
