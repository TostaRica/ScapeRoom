using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_sc : MonoBehaviour
{
    private static Dictionary<string, bool> inventory;
    private static Dictionary<string, string> keys;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new Dictionary<string, bool>();
        keys = new Dictionary<string, string>();
       //room1
        inventory["batteries"] = true;
        inventory["blueliquid"] = false;
        inventory["key1"] = false;
        keys["code1"] = "root";
       //room2
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("hola");
    }
    public static void SetKey(string name, string value) {
        keys[name] = value;
    }
    public static void SetInventoryItem(string name, bool value)
    {
        inventory[name] = value;
    }
    public static string GetKey(string name)
    {
        return keys[name];
    }
    public static bool GetInventoryItem(string name)
    {
       return inventory[name];
    }

}
