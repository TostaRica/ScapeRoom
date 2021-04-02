using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCodeLight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject light1;
    [SerializeField] private GameObject light2;
    [SerializeField] private GameObject light3;
    [SerializeField] private GameObject light4;
    [SerializeField] private GameObject codeLight;
    // Update is called once per frame
    void Update()
    {
        bool solved = light1.GetComponent<Animator>().GetBool("Solved") && light2.GetComponent<Animator>().GetBool("Solved") && light3.GetComponent<Animator>().GetBool("Solved") && light4.GetComponent<Animator>().GetBool("Solved");
        if (solved) codeLight.GetComponent<Light>().enabled = solved;
    }
}
