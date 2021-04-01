using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectWithAnimation : Open_object_sc
{
    [SerializeField] private bool getObject = false;
    public override void Open()
    {
        //throw new System.NotImplementedException();
        // activar animacion
        GetComponent<Animation>().Play();
        if (getObject) Main_sc.SetInventoryItem(name, true);

    }

}
