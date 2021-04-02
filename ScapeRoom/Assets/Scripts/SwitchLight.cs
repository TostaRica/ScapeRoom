using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLight : Open_object_sc
{
    [SerializeField] private GameObject lightObject;
    public override void Open()
    {
        bool solved = GetComponent<Animator>().GetBool("Solved");
        GetComponent<Animator>().SetBool("Solved", !solved);
        lightObject.GetComponent<Animator>().SetBool("Solved", !solved);
    }
}
