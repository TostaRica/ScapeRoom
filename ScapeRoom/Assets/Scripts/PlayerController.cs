using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_rigidbody;

    public float yaw;
    public float pitch;

    public float speedH;
    public float speedV;

    public float SpeedWalk;
    private bool isPlayerActive;

    // Start is called before the first frame update
    private void Start()
    {
        isPlayerActive = true;
        m_rigidbody = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isPlayerActive)
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
            Vector3 playerRotation = new Vector3(pitch, yaw, 0.0f);
            if (playerRotation.x < -90.0f)
            {
                playerRotation = new Vector3(-90.0f, yaw, 0.0f);
            }
            else if (playerRotation.x > 90.0f)
            {
                playerRotation = new Vector3(90.0f, yaw, 0.0f);
            }

            transform.eulerAngles = playerRotation;

            if (Input.GetMouseButton(0))
            {
                Vector3 direction = Camera.main.transform.forward;
                direction.y = 0;
                transform.position += direction * SpeedWalk * Time.deltaTime;
            }
        }
    }

    public void StopPlayerController()
    {
        isPlayerActive = false;
        //transform.GetChild(0).GetComponent<Camera>().enabled = false;
    }

    public void ResumePlayerController()
    {
        isPlayerActive = true;
        //transform.GetChild(0).GetComponent<Camera>().enabled = true;
    }
}