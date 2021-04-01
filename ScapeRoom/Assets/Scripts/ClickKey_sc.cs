using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickKey_sc : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private InputField textField;
    [SerializeField] private Puzzle_computer_sc computer;

    public AudioSource keyAudio;
    // Update is called once per frame
    public string PressKey()
    {
        if (Input.GetMouseButton(0))
        {
            keyAudio.Play();
            if (key == "Enter")
            {
                computer.CheckDecision(textField.text);
            }
            else if (key != "Esc")
            {
                textField.text = textField.text + key;
            }
        }
        return key;
    }
}