using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public Animation animator;
    public float trailSpeed;
    public float speed;
    private float timeToReturn = 0.70f;
    public float currentTime;
    public GameObject[] shortBeatsPoints;
    public int wayPoint = 0;
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
            Debug.Log(currentTime);
            currentTime -= Time.deltaTime;
        }
        else
        {
            trail.GetComponent<TrailRenderer>().Clear();
            trail.GetComponent<TrailRenderer>().enabled = true;
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
        else
        {
            float step = speed * Time.deltaTime;
            // calculate distance to move
            //transform.position = Vector3.MoveTowards(transform.position, shortBeatsPoints[wayPoint].transform.position, step);
            //transform.Translate(Vector3.right * Time.deltaTime * trailSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RightWall"))
        {
            trail.GetComponent<TrailRenderer>().enabled = false;
            this.transform.position = GameObject.FindWithTag("LeftWall").transform.position;
            wayPoint = 0;
            beatDone = true;
            Debug.Log(timeToReturn + "fFff");
            currentTime = timeToReturn;
        }
        if (other.gameObject.CompareTag("LeftWall"))
        {
            //trail.GetComponent<TrailRenderer>().Clear();
            //trail.GetComponent<TrailRenderer>().enabled = true;
        }
        if (other.gameObject.CompareTag("Point"))
        {
            wayPoint++;
        }
    }
}