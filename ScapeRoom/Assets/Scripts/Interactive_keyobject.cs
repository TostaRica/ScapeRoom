using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Interactive_keyobject : MonoBehaviour
{
    [SerializeField] private string requiredObject;
    [SerializeField] private bool isPuzzle = false;
    private bool isUnlocked;
    
    // Start is called before the first frame update
    void Start()
    {
        isUnlocked = false;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    bool Interact() 
    {
        isUnlocked = Main_sc.GetInventoryItem(requiredObject);
        if (isUnlocked) {
            if (isPuzzle)
            {
                GetComponent<Puzzle_sc>().Activate();
            }
            else 
            {
                GetComponent<Open_object_sc>().Open();
            }
           return true;
        }
        return false;
    }
    //borrar en cuanto este hecha la parte de la interactuar
    public bool tryInteract() 
    {
        if (!isUnlocked && Input.GetMouseButtonDown(0)) {
            return Interact();
        }

        return false;

    }
}
