using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerInteract : MonoBehaviour
{
    [SerializeField] private float maxDistance = 2;
    [SerializeField] private PlayerController playerScript;
    [SerializeField] private AudioSource m_audio;

    bool open = false;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, forward, out hit, maxDistance))
        {
            if (hit.collider.gameObject.CompareTag("Drawer"))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    open = !open;
                    hit.collider.gameObject.GetComponent<Animator>().SetBool("Solved", open);
                    m_audio.Play();
                }
            }
        }
    }
}
