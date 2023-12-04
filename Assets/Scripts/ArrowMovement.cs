using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    double oldTick;
    double newTick;
    double timeBetweenTicks;
    Transform tr;
    public double totalTime = 1;
    double timePassed = 0;
    Vector2 startPos;
    Vector2 targetPos;
    public bool empty = false;


    // Start is called before the first frame update
    void Start()
    {
        oldTick = AudioSettings.dspTime;
        startPos = GetComponent<Transform>().position;
        targetPos = GameObject.FindGameObjectWithTag("Target").GetComponent<Transform>().position;
        tr = GetComponent<Transform>();
        if (empty) Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        newTick = AudioSettings.dspTime;
        timeBetweenTicks = newTick - oldTick;
        oldTick = newTick;
        timePassed += timeBetweenTicks;
        tr.position = new Vector2(position(startPos.x, targetPos.x), position(startPos.y, targetPos.y));
        //if (timePassed > totalTime) Destroy(this.gameObject);
    }

    float position(float startVar,float targetVar)
    {
        float pos;
        float distance = startVar - targetVar;
        pos = startVar - (((float)timePassed)*distance/((float)totalTime));
        return pos;
    }

}
