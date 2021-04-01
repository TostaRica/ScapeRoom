using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] private readonly string doorTag = "Door";
    [SerializeField] private float maxDistance = 5;
    [SerializeField] private PlayerController playerScript;

    private bool opened = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!opened && Main_sc.GetInventoryItem("DoorKey"))
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
                        opened = true;
                    }
                }
            }
        }
    }
}