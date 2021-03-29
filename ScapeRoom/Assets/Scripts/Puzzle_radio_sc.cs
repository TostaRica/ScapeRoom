using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_radio_sc : Puzzle_sc
{
    private int result;
    private int[] code;
    private char[] iCode;
    //[SerializeField] private AudioClip a0;
    //[SerializeField] private AudioClip a1;
    //[SerializeField] private AudioClip a2;
    //[SerializeField] private AudioClip a3;
    //[SerializeField] private AudioClip a4;
    //[SerializeField] private AudioClip a5;
    //[SerializeField] private AudioClip a6;
    //[SerializeField] private AudioClip a7;
    //[SerializeField] private AudioClip a8;
    //[SerializeField] private AudioClip a9;

    [SerializeField] private AudioClip[] clipList;

    // Start is called before the first frame update
    private void Start()
    {
        code = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        iCode = new char[code.Length];
        GenerateCode();
        Activate();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void GenerateCode()
    {
        for (int i = code.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = code[i];
            code[i] = code[randomIndex];
            code[randomIndex] = temp;

            AudioClip tempClip = clipList[i];
            clipList[i] = clipList[randomIndex];
            clipList[randomIndex] = tempClip;
        }

        for (int i = 0; i < iCode.Length; ++i)
        {
            iCode[i] = (char)(code[i] + '0');
        }

        iCode[Random.Range(0, iCode.Length - 1)] = 'X';
    }

    public char[] GetInterfaceCode()
    {
        return iCode;
    }

    public override void Activate()
    {
        //activar audio
        AudioSource audioSource = GetComponent<AudioSource>();
        float currentClipLength, timer;
        timer = 0.0f;
        currentClipLength = 0.0f;

        for (int i = 0; i < clipList.Length;)
        {
            timer += Time.deltaTime;

            if (timer > currentClipLength)
            {
                audioSource.Stop();

                audioSource.clip = clipList[i];
                currentClipLength = clipList[i].length;

                timer = 0;

                audioSource.Play();

                i++;
            }
        }
    }

    public override void OnResolve()
    {
    }
}