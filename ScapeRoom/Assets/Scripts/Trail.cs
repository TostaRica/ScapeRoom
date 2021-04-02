using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public Animation animator;
    public float trailSpeed;
    public float speed;
    private float timeToReturn = 0.5f;
    public float currentTime;
    public GameObject trail;
    // Start is called before the first frame update
    public bool beatDone;

    void Start()
    {
        //this.transform.position = GameObject.FindWithTag("LeftWall").transform.position;
    }

    void WaitForReturn()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            trail.GetComponent<TrailRenderer>().Clear();
            //trail.GetComponent<TrailRenderer>().enabled = true;
            beatDone = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (beatDone)
        {
            WaitForReturn();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RightWall"))
        {
            Debug.Log("Dreta");
            trail.GetComponent<TrailRenderer>().enabled = false;
            beatDone = true;
            currentTime = timeToReturn;
        }
        if (other.gameObject.CompareTag("LeftWall"))
        {
            Debug.Log("esquerra");
            trail.GetComponent<TrailRenderer>().Clear();
            trail.GetComponent<TrailRenderer>().enabled = true;
        }
    }
}