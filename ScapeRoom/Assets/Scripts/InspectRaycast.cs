using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class InspectRaycast : MonoBehaviour
{
    [SerializeField] private bool onInspect;
    [SerializeField] private float transitionSpeed;
    [SerializeField] private float maxDistance = 5;

    [SerializeField] private GameObject selected;
    [SerializeField] private GameObject inspected;

    [SerializeField] private PlayerController playerScript;
    [SerializeField] private Transform playerSocket;

    [SerializeField] private Color outlineColor = Color.red;
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private Quaternion originalRotation;

    [SerializeField] private Material material;
    [SerializeField] private PostProcessVolume volume;
    [SerializeField] private DepthOfField depthOfField;

    private Camera mainCamera;
    private Camera secondCamera;
    private bool onRelease = false;

    private enum GoTag
    {
        Null, Selectable, Collectable, Interactive
    }

    private GoTag goTag;

    private void Start()
    {
        volume.profile.TryGetSettings<DepthOfField>(out depthOfField);
        goTag = GoTag.Null;
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        if (!onInspect && !onRelease && Physics.Raycast(transform.position, forward, out hit, maxDistance))
        {
            goTag = GoTag.Null;
            switch (hit.collider.gameObject.tag)
            {
                case "Selectable":
                    goTag = GoTag.Selectable;
                    break;

                case "Collectable":
                    goTag = GoTag.Collectable;
                    break;

                case "Interactive":
                    goTag = GoTag.Interactive;
                    break;
            }

            if (goTag == GoTag.Selectable || goTag == GoTag.Collectable || goTag == GoTag.Interactive)
            {
                selected = hit.collider.gameObject;
                //material = hittedObject.GetComponent<Renderer>().material;
                //material.SetFloat("_OutlineThickness", 0.03f);
                //material.SetColor("_OutlineColor", outlineColor);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    onInspect = true;
                    inspected = selected;
                    if (goTag != GoTag.Interactive)
                    {
                        inspected.GetComponent<Collider>().isTrigger = true;
                        originalPosition = hit.transform.position;
                        originalRotation = hit.transform.rotation;
                        depthOfField.active = true;
                        StartCoroutine(pickupItem());
                    }
                    else
                    {
                        secondCamera = inspected.GetComponent<Camera>();
                        secondCamera.enabled = true;
                        mainCamera.enabled = false;
                        playerScript.StopPlayerController();
                    }
                }
            }
        }
        else
        {
            if (selected != null)
            {
                //material.SetFloat("_OutlineThickness", 0.0f);
                selected = null;
            }

            if (onInspect)
            {
                if (goTag == GoTag.Interactive && inspected.name != "Computer")
                {
                    Cursor.lockState = CursorLockMode.None;
                    if (inspected.GetComponent<Interactive_keyobject>() != null)
                    {
                        inspected.GetComponent<Interactive_keyobject>().tryInteract();
                    }
                }
                else if(goTag == GoTag.Selectable || goTag == GoTag.Collectable)
                {
                    inspected.transform.position = Vector3.Lerp(inspected.transform.position, playerSocket.position, 0.2f);
                    Vector3 rotation = new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * Time.deltaTime * 125f;
                    playerSocket.Rotate(rotation);
                }
                else
                {
                    KeyboardRay keyboard = inspected.GetComponent<KeyboardRay>();

                    if(keyboard != null)
                    {
                        keyboard.onInspect = true;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && onInspect)
            {
                if (goTag == GoTag.Collectable)
                {
                    Main_sc.SetInventoryItem(inspected.name, true);
                    inspected.SetActive(false);
                    depthOfField.active = false;
                    playerScript.ResumePlayerController();
                }
                else if (goTag == GoTag.Selectable)
                {
                    StartCoroutine(dropItem());
                }
                Cursor.lockState = CursorLockMode.Locked;
                onInspect = false;
            }
        }
    }

    public void ReleaseInteractive()
    {
        mainCamera.enabled = true;
        secondCamera.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        onInspect = false;
        onRelease = true;
        StartCoroutine(ReleaseRay());
    }

    private IEnumerator ReleaseRay()
    {
        yield return new WaitForSeconds(0.5f);
        playerScript.ResumePlayerController();
        onRelease = false;

    }

    private IEnumerator pickupItem()
    {
        playerScript.StopPlayerController();
        yield return new WaitForSeconds(0.2f);
        inspected.transform.SetParent(playerSocket);
    }

    private IEnumerator dropItem()
    {
        inspected.transform.rotation = originalRotation;
        inspected.transform.position = originalPosition;
        yield return new WaitForSeconds(0.2f);
        inspected.transform.SetParent(null);
        inspected.GetComponent<Collider>().isTrigger = false;
        playerScript.ResumePlayerController();
        depthOfField.active = false;
    }
    public void setOnInspect(bool inspect) {
        onInspect = inspect;
    }
}