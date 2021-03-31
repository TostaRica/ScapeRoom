using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_radio_sc : Puzzle_sc
{
    [SerializeField] private AudioClip[] clipList;
    [SerializeField] private int[] code;
    [SerializeField] private char[] iCode;

    AudioSource audioSource;
    int currentClip;
    bool playCode;
    bool active;
    float dial;
    

    public AudioSource rFrequency;
    public AudioClip[] audios;

    private void Awake()
    {
        code = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        iCode = new char[code.Length];
        GenerateCode();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentClip = 0;
        playCode = false;
        active = false;
        dial = 98;
    }

    // Update is called once per frame
    private void Update()
    {
        if (active && playCode && !audioSource.isPlaying)
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
        //for (int i = code.Length - 1; i > 0; i--)
        //{
        //    int randomIndex = Random.Range(0, i + 1);
        //    int temp = code[i];
        //    code[i] = code[randomIndex];
        //    code[randomIndex] = temp;

        //    AudioClip tempClip = clipList[i];
        //    clipList[i] = clipList[randomIndex];
        //    clipList[randomIndex] = tempClip;
        //}
        code = new int[] { 9, 4, 2, 3, 1, 5, 8, 7, 6, 0 };
        //for (int i = 0; i < iCode.Length; ++i)
        //{
        //    iCode[i] = (char)(code[i] + '0');
        //    //clipList[i] = audio de sonido distorsionado
        //}


        //iCode[Random.Range(0, iCode.Length - 1)] = 'X';
        OnResolve();
    }

    public char[] GetInterfaceCode()
    {
        return iCode;
    }

    public void SelectDial(int currentDial)
    {
        if (active) { 
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
    }

    public override void Activate()
    {
        //activar audio
        active = true;
        GetComponent<RadioController>().SetActivate(true);
    }
    
    public override void OnFail()
    {
        GenerateCode();
    }

    public override void OnResolve()
    {
        Main_sc.SetKey(codeRadio, string.Join(string.Empty, code));
    }
    
    public override void Deactivate()
    {
        playCode = false;
    }

    public int[] GetCode()
    {
        return code;
    }


}