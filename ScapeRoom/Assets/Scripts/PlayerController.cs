using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject gameManager;

    public GameObject camera;
    private Rigidbody m_rigidbody;

    public float yaw;
    public float pitch;

    public float speedH;
    public float speedV;

    public float SpeedWalk;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        Vector3 playerRotation = new Vector3(pitch, yaw, 0.0f);
        if (playerRotation.x < -90.0f)
        {
            playerRotation = new Vector3(-90.0f, yaw, 0.0f);
        }
        else if (playerRotation.x > 90.0f)
        {
            playerRotation = new Vector3(90.0f, yaw, 0.0f);
        }

        transform.eulerAngles = playerRotation;

        if (Input.GetMouseButtonDown(0))
        {
            if (ObjectHasClicked())
            {
                //gameManager.GetComponent<GameManager>().GoToInspector();
                return;
            }
            else
            {
                transform.Translate(Vector3.forward * Time.deltaTime * SpeedWalk);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = Camera.main.transform.forward;
            direction.y = 0;
            transform.position += direction * SpeedWalk * Time.deltaTime;
        }
    }

    bool ObjectHasClicked()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                hit.collider.enabled = false;
                //gameManager.GetComponent<GameManager>().GetObjectToInspect(hit.collider.gameObject);
                return true;
            }
        }
        return false;
    }
}