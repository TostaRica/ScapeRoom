using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour
{
    public AudioSource audio;

    public float shortBeatTime = 0.5f;
    public float longShortTime = 1;
    public float restTime = 0.5f;
    public float currentRestTime;

    public bool isBeating;
    public bool iChangeToWaiting;
    public bool iChangeToBeating;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeating)
        {
            DoShortBeat();
        }
        else
        {
            WaitRestTime();
        }
    }

    void WaitRestTime()
    {
        if (currentRestTime > 0)
        {
            currentRestTime -= Time.deltaTime;
        }
        else
        {
            currentRestTime = shortBeatTime;
            iChangeToWaiting = false;
            isBeating = true;
        }
    }
    void DoShortBeat()
    {
        if (currentRestTime >= 0)
        {
            if (!iChangeToBeating)
            {
                audio.Play();
                iChangeToBeating = true;
            }
            currentRestTime -= Time.deltaTime;
            isBeating = true;
        }
        else
        {
            audio.Stop();
            iChangeToBeating = false;
            isBeating = false;
            currentRestTime = restTime;
        }
    }
}