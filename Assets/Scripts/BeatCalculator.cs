using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCalculator : MonoBehaviour
{

    bool started = false;
    float timer = 0;
    int beats = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !started)
        {
            started = true;
            beats++;
        }
        if (started)
        {
            timer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print(timer);
                print(beats);
                beats++;
            }
        }

    }
}
