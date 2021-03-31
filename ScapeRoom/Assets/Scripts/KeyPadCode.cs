using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadCode : MonoBehaviour
{
    [SerializeField] private readonly string botonTag = "KeyButton";
    [SerializeField] private float maxDistance = 5;

    bool collectable = false;

    public int[] password = { 2, 5, 5, 7 };
    public int[] codeEnter = new int[4];

    public AudioClip[] clips;
    public AudioSource audio;
    public int numeroAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ButtonHasClicked();
        }
    }

    bool ButtonHasClicked()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            collectable = hit.collider.gameObject.CompareTag(botonTag);
            if (collectable)
            {
                AddNumber(hit.collider.gameObject.GetComponent<BottonKeyPad>().ClickButton());
                return true;
            }
        }
        return false;
    }

    void AddNumber(int n)
    {
        if (numeroAmount < 4)
        {
            audio.clip = clips[2];
            codeEnter[numeroAmount] = n;
            audio.Play();
            numeroAmount++;
            if (numeroAmount == 4)
            {
                if (CheckSolution())
                {
                    audio.clip = clips[1];
                }
                else
                {
                    audio.clip = clips[0];
                    numeroAmount = 0;
                }
                audio.Play();
            }
        }
    }

    bool CheckSolution()
    {
        for (int i = 0; i < password.Length; i++)
        {
            if (password[i] != codeEnter[i])
            {
                return false;
            }
        }
        return true;
    }
}