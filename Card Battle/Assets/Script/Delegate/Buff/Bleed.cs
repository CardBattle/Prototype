using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : BuffUse
{

    public override void Use(Character sender, Character receiver)
    {
        Debug.Log("Bleed");
    }
}
