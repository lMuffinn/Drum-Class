using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BeatController : MonoBehaviour
{

    public float bpm = 60;
    public List<GameObject> beats;
    float secPerBeat;
    float seconds = 0;
    AudioSource sound;
    int current = 0;
    public float buffer = 0.1f;
    float buffTimer = 0;
    bool hit;
    int lastCur =0;


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
            if (beats[current] != null)
            {
                buffTimer = buffer * 2;
                hit = false;
            }
        }
        //Detects if the player presses the button at the right time
        if (beats[lastCur] != null)
        {
            if (Input.GetKeyDown(beats[lastCur].GetComponent<Instrument>().key))
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
        }
        //Play a sound if the checkmark for the current beat is checked
        if (seconds >= secPerBeat)
        {
            if (beats[current] != null)
            {
                sound.clip = beats[current].GetComponent<Instrument>().sound;
                sound.Play();
            }
            lastCur = current;
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
