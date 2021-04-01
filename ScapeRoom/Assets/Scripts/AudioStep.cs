using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStep : MonoBehaviour
{
    public AudioClip[] audioSteps;
    public AudioSource stepAudioSource;
    public float timeStep;
    public float currentTime;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !player.GetComponent<InspectRaycast>().GetOnInspect())
        {
            if (currentTime < 0)
            {
                currentTime = timeStep;
                stepAudioSource.clip = audioSteps[Random.Range(0, audioSteps.Length)];
                stepAudioSource.Play();
            }
            else
            {
                currentTime -= Time.deltaTime;
            }
        }
    }
}