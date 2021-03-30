using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class InspectRaycast : MonoBehaviour
{
    [SerializeField] private readonly string selectableTag = "Selectable";
    [SerializeField] private readonly string collectableTag = "Collectable";
    [SerializeField] private readonly string interactiveTag = "Interactive";
    [SerializeField] private GameObject hittedObject;
    [SerializeField] private GameObject inspected;
    [SerializeField] private float maxDistance = 5;
    [SerializeField] private Material material;
    [SerializeField] private Color outlineColor = Color.red;
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private Quaternion originalRotation;
    [SerializeField] private Transform playerSocket;
    [SerializeField] private PlayerController playerScript;
    [SerializeField] private bool onInspect;
    [SerializeField] private PostProcessVolume volume;

    [SerializeField] private DepthOfField depthOfField;
    enum GoTag {
        Null, Selectable, Collectable, Interactive
    }
    GoTag goTag;
    private void Start()
    {
        volume.profile.TryGetSettings<DepthOfField>(out depthOfField);
        goTag = GoTag.Null;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, forward, out hit, maxDistance) && !onInspect)
        {
            goTag = GoTag.Null;
            if (hit.collider.gameObject.CompareTag(collectableTag)) goTag = GoTag.Collectable;
            if (hit.collider.gameObject.CompareTag(selectableTag)) goTag = GoTag.Selectable;
            if (hit.collider.gameObject.CompareTag(interactiveTag)) goTag = GoTag.Interactive;

            if (goTag == GoTag.Selectable || goTag == GoTag.Collectable || goTag == GoTag.Interactive)
            {
                hittedObject = hit.collider.gameObject;
                //material = hittedObject.GetComponent<Renderer>().material;
                //material.SetFloat("_OutlineThickness", 0.03f);
                //material.SetColor("_OutlineColor", outlineColor);

                if (Input.GetKeyDown(KeyCode.Mouse0)) {
                    inspected = hit.collider.gameObject;
                    inspected.GetComponent<Collider>().isTrigger = true;
                    originalPosition = hit.transform.position;
                    originalRotation = hit.transform.rotation;
                    onInspect = true;
                    depthOfField.active = true;
                    StartCoroutine(pickupItem());

                }

            }
        }
        else
        {
            if (hittedObject != null)
            {
                //material.SetFloat("_OutlineThickness", 0.0f);
                hittedObject = null;
            }
        }

        if (onInspect)
        {
            inspected.transform.position = Vector3.Lerp(inspected.transform.position, playerSocket.position, 0.2f);
           
            if (goTag == GoTag.Interactive)
            {
                Cursor.lockState = CursorLockMode.None;
                //gameObject.GetComponent<Interactive_keyobject>().tryInteract();
            }
            else 
            {
                Vector3 rotation = new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * Time.deltaTime * 125f;
                playerSocket.Rotate(rotation);
            }
                
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && onInspect)
        {
           
            if (goTag == GoTag.Collectable)
            {
                Main_sc.SetInventoryItem(inspected.name, true);
                inspected.SetActive(false);
            }
            Cursor.lockState = CursorLockMode.Locked;
            StartCoroutine(dropItem());
            onInspect = false;
        }

    }

    IEnumerator pickupItem()
    {
        playerScript.enabled = false;
        yield return new WaitForSeconds(0.2f);
        inspected.transform.SetParent(playerSocket);
    }

    IEnumerator dropItem()
    {
        inspected.transform.rotation = originalRotation;
        inspected.transform.position = originalPosition;
        inspected.transform.SetParent(null);
        inspected.GetComponent<Collider>().isTrigger = false;
        playerScript.enabled = true;
        depthOfField.active = false;
        yield return new WaitForSeconds(0.2f);
    }
}
