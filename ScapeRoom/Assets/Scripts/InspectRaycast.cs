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
        Null, Selectable, Collectable, Interactive, InteractiveLock
    }

    private GoTag goTag;

    private void Start()
    {
        volume.profile.TryGetSettings<DepthOfField>(out depthOfField);
        depthOfField.active = false;
        goTag = GoTag.Null;
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        bool hitObj = Physics.Raycast(transform.position, forward, out hit, maxDistance);
        if (!onInspect && !onRelease && hitObj)
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
                case "InteractiveLock":
                    goTag = GoTag.InteractiveLock;
                    break;
            }

            if (goTag == GoTag.Selectable || goTag == GoTag.Collectable || goTag == GoTag.Interactive || goTag == GoTag.InteractiveLock)
            {
                //material = hittedObject.GetComponent<Renderer>().material;
                //material.SetFloat("_OutlineThickness", 0.03f);
                //material.SetColor("_OutlineColor", outlineColor);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    onInspect = true;
                    inspected = hit.collider.gameObject;
                    if (goTag != GoTag.Interactive && goTag != GoTag.InteractiveLock)
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

            if (onInspect)
            {
                if (goTag == GoTag.InteractiveLock)
                {
                    Ray ray = secondCamera.ScreenPointToRay(Input.mousePosition);
                    bool hitInteractive = Physics.Raycast(ray, out hit, 1.5f);
                    Cursor.lockState = CursorLockMode.None;
                    Interactive_keyobject interactive = inspected.GetComponent<Interactive_keyobject>();
                    if (interactive != null)
                    {
                        interactive.tryInteract();
                    }

                    if (Input.GetKeyDown(KeyCode.Mouse0) && !hitInteractive ) {
                        ReleaseInteractive();
                    }

                }
                else if (goTag == GoTag.Selectable || goTag == GoTag.Collectable)
                {
                    inspected.transform.position = Vector3.Lerp(inspected.transform.position, playerSocket.position, 0.2f);
                    Vector3 rotation = new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * Time.deltaTime * 125f;
                    playerSocket.Rotate(rotation);
                }
                else if(goTag == GoTag.Interactive)
                {

                    KeyboardRay keyboard = inspected.GetComponent<KeyboardRay>();

                    if (keyboard != null) keyboard.onInspect = true;

                    Locker locker = inspected.GetComponentInChildren<Locker>();

                    if (locker != null)
                    {
                        if (!locker.hasSolved)
                        {
                            locker.onInspect = true;
                            locker.EnableSpins();
                            Cursor.lockState = CursorLockMode.None;
                        }
                    }
                    Ray ray = secondCamera.ScreenPointToRay(Input.mousePosition);
                    bool hitInteractive = Physics.Raycast(ray, out hit, 1.5f);
                    if (Input.GetKeyDown(KeyCode.Mouse0) && !hitInteractive)
                    {
                        if (keyboard != null) keyboard.onInspect = true;
                        if (!locker.hasSolved)
                        {
                            locker.onInspect = true;
                            locker.EnableSpins();
                            Cursor.lockState = CursorLockMode.None;
                        }
                        ReleaseInteractive();
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
                    Cursor.lockState = CursorLockMode.Locked;
                    onInspect = false;
                }
                else if (goTag == GoTag.Selectable)
                {
                    StartCoroutine(dropItem());
                    Cursor.lockState = CursorLockMode.Locked;
                    onInspect = false;
                }

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
    public void setOnInspect(bool inspect)
    {
        onInspect = inspect;
    }
}