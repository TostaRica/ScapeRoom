using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Puzzle_radio_sc : Puzzle_sc
{
    int result;
    int[] code;
    char[] iCode;
    [SerializeField] private AudioClip a0;
    [SerializeField] private AudioClip a1;
    [SerializeField] private AudioClip a2;
    [SerializeField] private AudioClip a3;
    [SerializeField] private AudioClip a4;
    [SerializeField] private AudioClip a5;
    [SerializeField] private AudioClip a6;
    [SerializeField] private AudioClip a7;
    [SerializeField] private AudioClip a8;
    [SerializeField] private AudioClip a9;

    // Start is called before the first frame update
    void Start()
    {
        code = new int[]{ 0,1,2,3,4,5,6,7,8,9 };
        iCode = new char[code.Length];
        GenerateCode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerateCode() {
        for (int i = code.Length - 1; i > 0; i--) { 
            int randomIndex = Random.Range(0, i + 1); 
            int temp = code[i];
            code[i] = code[randomIndex];
            code[randomIndex] = temp; 
        }
       
        for (int i = 0; i < iCode.Length; ++i)
        {
            iCode[i] = (char)(code[i] + '0');
        }

        iCode[Random.Range(0, iCode.Length - 1)] = 'X';
    }

    public char[] GetInterfaceCode() {

        return iCode;
    }

    public override void Activate() 
    {
        //activar audio
        //generar el codigo
    }

    public override void OnResolve()
    {

    }
}
