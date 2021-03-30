using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_radio_sc : Puzzle_sc
{
    [SerializeField] private AudioClip[] clipList;
    AudioSource audioSource;
    int currentClip;
    bool playCode;
    bool batteries;
    float dial;
    private int[] code;
    private char[] iCode;

    public AudioSource rFrequency;
    public AudioClip[] audios;

    private void Start()
    {
        code = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        iCode = new char[code.Length];
        audioSource = GetComponent<AudioSource>();
        currentClip = 0;
        playCode = false;
        batteries = false;
        dial = 98;
    }

    // Update is called once per frame
    private void Update()
    {
        if (batteries && playCode && !audioSource.isPlaying)
        {
            audioSource.clip = clipList[currentClip];
            audioSource.Play();
            ++currentClip;
            if (currentClip == code.Length)
            {
                // audioSource.clip = clipList[currentClip]; sonido blanco
                // audioSource.Play();
                currentClip = 0;
            }
        }
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
            //clipList[i] = audio de sonido distorsionado
        }

        iCode[Random.Range(0, iCode.Length - 1)] = 'X';
        OnResolve();
    }

    public char[] GetInterfaceCode()
    {
        return iCode;
    }

    public void SelectDial(int currentDial)
    {
        Debug.Log(currentDial);
        if (currentDial != dial)
        {
            int i = (int)Random.Range(0.0f, 5.0f);
            rFrequency.clip = audios[i];
            rFrequency.Play();
            // play random sound
            playCode = false;
        }
        else
        {
            playCode = true;
        }
    }

    public override void Activate()
    {
        //activar audio
        batteries = true;
        GenerateCode();
    }
    public override void OnFail()
    {
        GenerateCode();
    }

    public override void OnResolve()
    {
        Main_sc.SetKey("code1", string.Join(string.Empty, code));
    }
    public override void Deactivate()
    {
        playCode = false;
    }
}