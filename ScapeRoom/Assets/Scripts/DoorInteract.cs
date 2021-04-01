using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] private readonly string doorTag = "Door";
    [SerializeField] private float maxDistance = 5;
    [SerializeField] private PlayerController playerScript;
    [SerializeField] private string msg;
    public Text infoText;

    private bool opened = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        if (!opened && Physics.Raycast(transform.position, forward, out hit, maxDistance))
        {
            if (hit.collider.gameObject.CompareTag(doorTag))
            {
                if (Main_sc.GetInventoryItem("DoorKey"))
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        //if i have the key()
                        hit.collider.gameObject.GetComponent<Door>().isOpening = true;
                        opened = true;
                    }
                }
                else
                {
                    infoText.text = msg;
                }
            }
            else
            {
                if (hit.collider.gameObject.GetComponent<ObjectInfo>() != null)
                {
                    infoText.text = hit.collider.gameObject.GetComponent<ObjectInfo>().description;
                }
                else
                {
                    infoText.text = "";
                }
            }
        }
    }
}