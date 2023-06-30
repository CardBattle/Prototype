using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : CardUse
{
    public override void Use()
    {
        base.Use();
        Debug.Log("Fireball");
    }

}
