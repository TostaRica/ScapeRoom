using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardRay : MonoBehaviour
{
    [SerializeField] private float maxDistance = 2f;
    [SerializeField] private InspectRaycast InspectRaycast;

    public bool onInspect = false;

    private void Start()
    {
        onInspect = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (onInspect)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.gameObject.tag == "KeyBoard")
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (hit.collider.gameObject.GetComponent<ClickKey_sc>().PressKey() == "Esc") {
                        InspectRaycast.ReleaseInteractive();
                        onInspect = false;
                    }
                }
            }
        }
    }
}
