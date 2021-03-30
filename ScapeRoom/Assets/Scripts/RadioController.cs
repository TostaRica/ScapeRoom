using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour
{
    public GameObject marker;
    public GameObject button;

    public float frequency;
    public float pitch;
    public float markerSpeed;
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        frequency = 93.0f;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active) { 
            if (Input.GetMouseButton(0))
            {
                if (ButtonHasClicked())
                {
                    MoveButton();
                }
            }
        }
    }
    public void SetActivate(bool _activate) {
        active = _activate;
    }
    bool ButtonHasClicked()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                GetComponent<Puzzle_radio_sc>().SelectDial(98);
                return true;
            }
        }
        return false;
    }

    void MoveButton()
    {
        float rotX = Input.GetAxis("Mouse Y") * 200 * Mathf.Deg2Rad;
        button.transform.Rotate(Vector3.right, rotX);
        if ((rotX > 0) && (marker.transform.position.y < 0.525))
        {
            frequency -= 0.015f;
            marker.transform.Translate(-Vector3.down * Time.deltaTime * markerSpeed);
        }
        else if ((rotX < 0) && (marker.transform.position.y > 0.25))
        {
            frequency += 0.015f;
            marker.transform.Translate(Vector3.down * Time.deltaTime * markerSpeed);
        }
        if (((int)frequency % 2) == 0)
        {
            GetComponent<Puzzle_radio_sc>().SelectDial((int)frequency);
            //Debug.Log("Dial Changed");
        }
        else
        {
            //Debug.Log(frequency);
        }
    }
}