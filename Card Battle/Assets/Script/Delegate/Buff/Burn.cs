using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : BuffUse
{
    public override void Use(Character sender, Character receiver)
    {
        receiver.info.Hp -= 1;

        Debug.Log("Burn");
    }
}
