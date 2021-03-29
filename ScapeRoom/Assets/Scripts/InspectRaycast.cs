using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectRaycast : MonoBehaviour
{
    [SerializeField] private readonly string selectableTag = "Selectable";
    [SerializeField] private GameObject hittedObject;
    [SerializeField] private float maxDistance = 5;
    [SerializeField] private Material material;
    [SerializeField] private Color outlineColor = Color.red;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, forward, out hit, maxDistance))
        {
            hittedObject = hit.collider.gameObject;
            if (hittedObject.CompareTag(selectableTag))
            {
                Debug.Log("Hitted");
                material = hittedObject.GetComponent<Renderer>().material;
                material.SetFloat("_OutlineThickness", 0.03f);
                material.SetColor("_OutlineColor", outlineColor);
            }
            else
            {
                hittedObject = null;
            }
        }
        else
        {
            if (hittedObject != null)
            {
                material.SetFloat("_OutlineThickness", 0.0f);
                hittedObject = null;
            }
        }

    }

}
