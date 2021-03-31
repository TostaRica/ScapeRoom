using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : Puzzle_sc
{
    public int[] lockedCode = { 2, 4, 7 };
    public GameObject[] spins;
    public bool hasSolved = false;
    public Animator solveAnimation;
    public Animator openBox;
    public AudioSource audioSolved;
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    public override void Activate() { }
    public override void OnFail() { }
    public override void Deactivate()
    {
    }

    public override void OnResolve()
    {
        solveAnimation.SetBool("Solved", true);
        openBox.SetBool("isSolved", true);
        hasSolved = true;
        audioSolved.Play();
        //solveAnimation.Play();
    }

    void Update()
    {
        if ((isSolved()) && (!hasSolved))
        {
            Debug.Log(isSolved());
            OnResolve();
            Deactivate();
        }
    }

    bool isSolved()
    {
        for (int i = 0; i < spins.Length; i++)
        {
            if (spins[i].GetComponent<Spin>().currentNumber != lockedCode[i])
            {
                return false;
            }
        }
        return true;
    }
}