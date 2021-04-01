using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator doorAnimator;
    public bool isOpening = false;
    public AudioSource m_audio;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpening)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        m_audio.Play();
        doorAnimator.SetBool("Solved", true);
        float rotX = Input.GetAxis("Mouse X") * 10 * Mathf.Deg2Rad;
        this.transform.Rotate(Vector3.up, rotX);
    }
}