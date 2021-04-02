using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadCode : MonoBehaviour
{
    [SerializeField] private readonly string botonTag = "KeyButton";
    [SerializeField] private float maxDistance = 1.2f;

    bool collectable = false;

    public int[] password;
    public int[] codeEnter = new int[4];

    public AudioClip[] clips;
    public AudioSource m_audio;
    public int numeroAmount = 0;

    public Camera main;

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
        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, maxDistance))
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
            m_audio.clip = clips[2];
            codeEnter[numeroAmount] = n;
            m_audio.Play();
            numeroAmount++;
            if (numeroAmount == 4)
            {
                if (CheckSolution())
                {
                    m_audio.clip = clips[1];
                }
                else
                {
                    m_audio.clip = clips[0];
                    numeroAmount = 0;
                }
                m_audio.Play();
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