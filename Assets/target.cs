using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public Queue<Instrument> beatsInQueue = new Queue<Instrument>();
    public float radius = 1;
    Transform myTr;
    AudioSource audioSource;
    public GameObject[] instruments;
    // Start is called before the first frame update
    void Start()
    {
        myTr = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        instruments = GameObject.FindGameObjectsWithTag("Beat");
        //Debug.Log(GameObject.FindGameObjectsWithTag("Beat"));
        Instrument[] beatsOnScreen = GameObject.FindObjectsByType<Instrument>(FindObjectsSortMode.None);
        foreach (Instrument beat in beatsOnScreen)
        {
            Transform transform = beat.gameObject.GetComponent<Transform>();
            if (Distance(myTr.position,transform.position) < radius && !beatsInQueue.Contains(beat))
            {
                beatsInQueue.Enqueue(beat);
            }
            if(transform.position.y < myTr.position.y - radius)
            {
                Debug.Log("missed");
                Destroy(beatsInQueue.Dequeue().gameObject);
            }
        }
        if (Input.anyKeyDown)
        {
            if (beatsInQueue.Count == 0) Debug.Log("missed");
            else if (beatsInQueue.Peek().play && Input.GetKeyDown(beatsInQueue.Peek().key))
            {
                audioSource.clip = beatsInQueue.Peek().sound;
                Destroy(beatsInQueue.Dequeue().gameObject);
                audioSource.Play();
                Debug.Log("Hit");
            }
            else Debug.Log("Missed");
        }
    }

    float Distance(Vector2 myPos, Vector2 beatPos)
    {
        Vector2 distanceVect = new Vector2(beatPos.x - myPos.x, beatPos.y - myPos.y);
        float distance = Mathf.Sqrt(Mathf.Pow(distanceVect.x,2) + Mathf.Pow(distanceVect.y,2));
        return distance;
    }

}
