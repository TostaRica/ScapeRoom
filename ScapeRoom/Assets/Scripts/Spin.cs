using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public int currentNumber = 0;

    [SerializeField] private GameObject rightSize;
    [SerializeField] private GameObject leftSize;

    [SerializeField] private Camera lockerCamera;

    [SerializeField] private float timeToChange = 1.0f;
    [SerializeField] private float currentTime;
    [SerializeField] private bool isChanging;
    [SerializeField] private bool isRight;
    [SerializeField] private float speedRotation;

    public AudioSource m_audio;

    private void Start()
    {
        DisableColliders();
    }

    // Update is called once per frame
    private void Update()
    {
        if (lockerCamera.isActiveAndEnabled)
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
    }

    private bool ButtonHasClicked()
    {
        RaycastHit hit;
        Ray ray = lockerCamera.ScreenPointToRay(Input.mousePosition);
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
            StartCoroutine(Sleep());
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
            if (currentNumber == 0)
            {
                currentNumber = 9;
            }
            else
            {
                currentNumber--;
            }
        }
        else
        {
            isRight = true;
            if (currentNumber == 9)
            {
                currentNumber = 0;
            }
            else
            {
                currentNumber++;
            }
        }
    }

    private void DoTransition()
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

    public void EnableColliders()
    {
        rightSize.SetActive(true);
        leftSize.SetActive(true);
    }

    public void DisableColliders()
    {
        rightSize.SetActive(false);
        leftSize.SetActive(false);
    }

    private IEnumerator Sleep()
    {
        yield return new WaitForSeconds(0.05f);
    }

}