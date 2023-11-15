using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct beat2
{
    public float time;
    public int sound;
}

public class BeatControllerv2 : MonoBehaviour
{

    public List<AudioClip> sounds;
    public List<beat> beats;
    public int currentBeat = 0;
    float time;
    public float delay = 3;
    AudioSource instrument;
    public float buffer = .1f;
    bool started = false;
    bool end = false;
    public bool testing = true;
    public float start = 0;

    // Start is called before the first frame update
    void Start()
    {
        instrument = GetComponent<AudioSource>();
        time = -1 * delay;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //Debug.Log(time);
        if (time >= 0)
        {
            if (!started)
            {
                started = true;
            }
            if (!end && beats.Count > 0)
            {
                //if (time >= beats[currentBeat].time)
                {
                    //instrument.clip = sounds[beats[currentBeat].sound];
                    instrument.Play();
                    currentBeat++;
                    if (currentBeat == beats.Count)
                    {
                        end = true;
                    }
                }
            }
        }
    }
}
