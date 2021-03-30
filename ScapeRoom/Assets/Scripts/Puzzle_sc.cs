using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle_sc : MonoBehaviour
{
    public abstract void Activate();
    public abstract void OnResolve();
    public abstract void OnFail();
    public abstract void Deactivate();
}
