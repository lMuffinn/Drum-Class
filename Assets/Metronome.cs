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
    public double bpm = 80;
    public List<Instrument> beats;
    Queue<Instrument> beatQueue = new Queue<Instrument>();
    Instrument currentBeat;
    //Tolerance
    public double tolerance = 0.1;
    public float timeBeforeHit = 1;
    public bool gameLeader = false;
    public double timeFromStart = 0;
    public AudioSource audioSource;
    public double StartDelay = 2;
    public GameObject dummyBeat;

    // Start is called before the first frame update
    void Start()
    {
        oldTick = AudioSettings.dspTime;
        timePerBeat = 60 / bpm;
        audioSource = GetComponent<AudioSource>();
        foreach (Instrument beat in beats)
        {
            //Debug.Log(beat);
            beatQueue.Enqueue(beat);
        }
        //NextBeat();
    }

    // Update is called once per frame
    void Update()
    {
        //Keep time. -----------------------------
        newTick = AudioSettings.dspTime;
        timeBetweenTicks = newTick - oldTick;
        oldTick = newTick;
        timeFromLastTick += timeBetweenTicks;
        timeFromStart += timeBetweenTicks;
        //Debug.Log("Time from start: " + timeFromStart);

        //Cycle through beats. -------------------------
        if (timeFromLastTick >= timePerBeat)
        {
            //Reset Tick Timer.
            timeFromLastTick -= timePerBeat;
            //Shift to next beat.
            NextBeat();
        }
    }

    void NextBeat()
    {
        if (gameLeader)
        {
            if (timeFromStart >= StartDelay)
            {
                currentBeat = beatQueue.Dequeue();
                beatQueue.Enqueue(currentBeat);
                GameObject imageClone;
                imageClone = Instantiate(beatQueue.Peek().gameObject);
                imageClone.GetComponent<ArrowMovement>().totalTime = timeBeforeHit;
            }
        }
        if (!gameLeader)
        {
            if (timeFromStart >= StartDelay)
            {
                currentBeat = beatQueue.Dequeue();
                beatQueue.Enqueue(currentBeat);
                GameObject imageClone;
                imageClone = Instantiate(dummyBeat);
                imageClone.GetComponent<dummyBeat>().totalTime = timeBeforeHit;
                imageClone.GetComponent<dummyBeat>().audioClip = currentBeat.sound;
                imageClone.GetComponent<dummyBeat>().empty = currentBeat.GetComponent<ArrowMovement>().empty;
            }
        }
    }
}
