using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : CardUse
{
    public override void Use(Character sender, Character receiver)
    {
        base.Use(sender, receiver);

        Debug.Log("Defense");
    }

}
