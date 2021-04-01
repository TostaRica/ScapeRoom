using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_sc : MonoBehaviour
{

    public const string codeRadio = "Code1";

    private static Dictionary<string, bool> inventory = new Dictionary<string, bool>();
    private static Dictionary<string, string> keys = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Start()
    {
        //room1
        inventory["Hands"] = true;
        inventory["Battery"] = false;
        inventory["DoorKey"] = false;
        keys[codeRadio] = "root";
        //room2
        inventory["MorsePaper"] = false;
        inventory["Scissors"] = false;
        inventory["Thumb"] = false;
        inventory["KeySilver"] = true;
    }

    public static void SetKey(string name, string value)
    {
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
