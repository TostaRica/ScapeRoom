using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public Slider sliderY;
    public Slider sliderH;

    public GameObject settingCanvas;
    public GameObject playerCanvas;

    // Start is called before the first frame update
    void Start()
    {
        sliderY.value = player.GetComponent<PlayerController>().speedV;
        sliderH.value = player.GetComponent<PlayerController>().speedH;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GotoSettings()
    {
        settingCanvas.SetActive(true);
        playerCanvas.SetActive(false);
    }
    public void ReturnToGame()
    {
        settingCanvas.SetActive(false);
        playerCanvas.SetActive(true);
    }
    public void SetVerticalSensivity()
    {
        player.GetComponent<PlayerController>().speedV = sliderY.value;
    }

    public void SetHorizontalSensivity()
    {
        player.GetComponent<PlayerController>().speedH = sliderH.value;
    }
}