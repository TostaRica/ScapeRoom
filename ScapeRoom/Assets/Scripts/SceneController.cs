using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
