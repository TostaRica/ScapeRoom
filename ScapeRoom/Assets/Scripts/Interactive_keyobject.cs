using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Interactive_keyobject : MonoBehaviour
{
    [SerializeField] private string requiredObject;
    [SerializeField] private GameObject eventScript;
    [SerializeField] private bool isUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        isUnlocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }
    void Unlock() 
    {
        isUnlocked = Main_sc.inventory[requiredObject];
        if (isUnlocked) { 
            //TODO: llamar event 
        }
    }
    void Interact() 
    {
        if (Input.GetMouseButtonDown(0)) {
            Unlock();
        }    
    }
}
