using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public int currentNumber = 0;

    public GameObject rightSize;
    public GameObject leftSize;

    public float timeToChange = 1.0f;
    public float currentTime;
    public bool isChanging;
    public bool isRight;
    public float speedRotation;

    public AudioSource m_audio;

    public
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isChanging)
            {
                ButtonHasClicked();
            }
        }
        if (isChanging)
        {
            DoTransition();
        }
    }

    bool ButtonHasClicked()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                if (hit.transform.gameObject == rightSize)
                {
                    m_audio.Play();
                    RotateSpin(true);
                }
                if (hit.transform.gameObject == leftSize)
                {
                    m_audio.Play();
                    RotateSpin(false);
                }
                return true;
            }
        }
        return false;
    }

    public void RotateSpin(bool toRight)
    {
        isChanging = true;
        currentTime = timeToChange;
        if (!toRight)
        {
            isRight = false;
            if (currentNumber == 9)
            {
                currentNumber = 0;
            }
            else
            {
                currentNumber--;
            }
        }
        else
        {
            isRight = true;
            if (currentNumber == 0)
            {
                currentNumber = 9;
            }
            else
            {
                currentNumber++;
            }
        }
    }

    void DoTransition()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (isRight)
            {
                this.transform.Rotate(Vector3.forward * speedRotation * Time.deltaTime);
            }
            else
            {
                this.transform.Rotate(Vector3.forward * -speedRotation * Time.deltaTime);
            }
        }
        else
        {
            isChanging = false;
        }
    }
}