using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{

    public CapsuleCollider playerCollider;
    public CapsuleCollider capsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(playerCollider, capsuleCollider, true);
    }
}
