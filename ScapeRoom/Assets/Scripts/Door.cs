using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isOpening = false;
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
        float rotX = Input.GetAxis("Mouse X") * 10 * Mathf.Deg2Rad;
        this.transform.Rotate(Vector3.up, rotX);
    }
}