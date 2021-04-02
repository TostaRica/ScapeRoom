using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    [SerializeField] private readonly string infoTag = "infoObject";
    [SerializeField] private float maxDistance = 5;

    public Text infoText;
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
            if (hit.collider.gameObject.CompareTag(infoTag))
            {
                infoText.text = hit.collider.gameObject.GetComponent<ObjectInfo>().nameObject;
            }
        }
        else
        {
            infoText.text = "";
        }
    }
}