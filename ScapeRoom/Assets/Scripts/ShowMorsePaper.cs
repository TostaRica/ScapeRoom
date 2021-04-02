using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMorsePaper : MonoBehaviour
{
    [SerializeField] private GameObject morsePaper;
    // Start is called before the first frame update
    void Start()
    {
        morsePaper.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Main_sc.GetInventoryItem("MorsePaper")) {
            morsePaper.SetActive(true);
        }
    }
}
