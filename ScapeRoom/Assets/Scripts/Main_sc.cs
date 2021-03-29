using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_sc : MonoBehaviour
{
    public static Dictionary<string, bool> inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = new Dictionary<string, bool>();
       //room1
        inventory["batteries"] = true;
        inventory["blueliquid"] = false;
        inventory["key1"] = false;
       //room2
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
