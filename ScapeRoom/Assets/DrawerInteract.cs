using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerInteract : MonoBehaviour
{
    [SerializeField] private float maxDistance = 5;
    [SerializeField] private PlayerController playerScript;
    [SerializeField] private AudioSource m_audio;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, forward, out hit, maxDistance))
        {
            if (hit.collider.gameObject.CompareTag("Animated"))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    hit.collider.gameObject.GetComponent<Interactive_keyobject>().tryInteract();
                    if (m_audio != null) m_audio.Play();
                }
            }
        }

    } 
}

