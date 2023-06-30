using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : CardUse
{
    public override void Use()
    {
        base.Use();
        Debug.Log("Defense");
    }

}
