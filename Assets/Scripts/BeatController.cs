using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct beat
{
    public bool play;
    public AudioClip clip;
}

public class BeatController : MonoBehaviour
{

    public float bpm = 60;
    public List<beat> beats;
    float secPerBeat;
    float seconds = 0;
    AudioSource sound;
    int current = 0;
    public float buffer = 0.1f;
    float buffTimer = 0;
    bool hit;


    // Start is called before the first frame update
    void Start()
    {
        secPerBeat = 60/bpm;
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Start timers
        buffTimer -= Time.deltaTime;
        seconds += Time.deltaTime;
        // Set the buffer time to 2* the buffer to allow for space before and after for the user to press the button
        if (seconds >= secPerBeat - buffer && buffTimer <= 0)
        {
            if (beats[current].play == true)
            {
                buffTimer = buffer * 2;
                hit = false;
            }
        }
        //Detects if the player presses the button at the right time
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (buffTimer > 0 && hit == false)
            {
                Debug.Log("Nice!");
                hit = true;
            }
            else
            {
                Debug.Log("Miss :(");
            }
        }
        //Play a sound if the checkmark for the current beat is checked
        if (seconds >= secPerBeat)
        {
            if (beats[current].play == true)
            {
                sound.clip = beats[current].clip;
                sound.Play();
            }
            current++;
            seconds -= secPerBeat;
        }
        //Make sure the current beat is not assigned a higher value than actually exists.
        if (current >= beats.Count)
        {
            current = 0;
        }
    }
}
