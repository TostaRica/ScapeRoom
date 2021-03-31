using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardRay : MonoBehaviour
{
    [SerializeField] private float maxDistance = 2f;
    [SerializeField] private Camera main;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private InspectRaycast InspectRaycast;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.gameObject.tag == "KeyBoard")
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (hit.collider.gameObject.GetComponent<ClickKey_sc>().PressKey() == "Esc") {
                        main.enabled = true;
                        GetComponent<Camera>().enabled = false;
                        playerController.ResumePlayerController();
                        //InspectRaycast.setOnInspect(false);
                    }
                }
            }
        }
    }
}
