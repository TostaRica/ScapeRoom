using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] private readonly string doorTag = "Door";
    [SerializeField] private float maxDistance = 5;
    [SerializeField] private PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, forward, out hit, maxDistance))
        {
            if (hit.collider.gameObject.CompareTag(doorTag))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //if i have the key()
                    hit.collider.gameObject.GetComponent<Door>().isOpening = true;
                    playerScript.enabled = false;
                }
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    playerScript.enabled = true;
                    hit.collider.gameObject.GetComponent<Door>().isOpening = false;
                }
            }
        }
    }
}