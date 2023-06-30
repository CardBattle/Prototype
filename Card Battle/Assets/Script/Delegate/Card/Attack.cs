using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : CardUse
{
    public override void Use()
    {
        base.Use();
        Debug.Log("Attack");
    }

}
