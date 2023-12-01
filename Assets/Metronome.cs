using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    double oldTick;
    double newTick;
    double timeBetweenTicks;
    double timeFromLastTick = 0;
    double timePerBeat;
    bool soundPlayed = false;
    public double bpm = 80;
    public List<Instrument> beats;
    Queue<Instrument> beatQueue = new Queue<Instrument>();
    Instrument currentBeat;
    AudioSource audioSource;
    //Tolerance
    public double tolerance = 0.1;
    bool canHit = false;
    bool hit = false;
    double hitTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        oldTick = AudioSettings.dspTime;
        timePerBeat = 60 / bpm;
        foreach (Instrument beat in beats)
        {
            //Debug.Log(beat);
            beatQueue.Enqueue(beat);
        }
        NextBeat();
    }

    // Update is called once per frame
    void Update()
    {
        //Keep time. -----------------------------
        newTick = AudioSettings.dspTime;
        timeBetweenTicks = newTick - oldTick;
        oldTick = newTick;
        timeFromLastTick += timeBetweenTicks;
        hitTimer -= timeBetweenTicks;

        //Tolerance.
        if (timeFromLastTick >= timePerBeat - tolerance * 2 && !canHit) hitTimer = tolerance * 2;
        canHit = (hitTimer > 0);
        if (!canHit) hit = false;

        //Button Presses.
        if (Input.anyKeyDown)
        {
            if (!currentBeat.play) Debug.Log("missed");
            else
            {
                if (canHit && Input.GetKeyDown(currentBeat.key) && !hit)
                {
                    Debug.Log("hit!");
                    hit = true;
                }
                else Debug.Log("missed");
            }
        }

        //Play sound.
        if (timeFromLastTick >= timePerBeat - tolerance && !soundPlayed)
        {
            //Play Beat.
            audioSource.clip = currentBeat.sound;
            audioSource.Play();
            soundPlayed = true;
        }

        //Cycle through beats. -------------------------
        if (timeFromLastTick >= timePerBeat)
        {
            //Reset Tick Timer. 
            timeFromLastTick -= timePerBeat;
            //Shift to next beat.
            NextBeat();
            soundPlayed = false;
        }
    }

    void NextBeat()
    {
        currentBeat = beatQueue.Dequeue();
        beatQueue.Enqueue(currentBeat);
        GameObject imageClone;
        try
        {
            imageClone = Instantiate(currentBeat.image);
            imageClone.GetComponent<ArrowMovement>().totalTime = timePerBeat - tolerance;
        }
        finally { }
    }
}
