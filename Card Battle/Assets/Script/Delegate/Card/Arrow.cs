using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : CardUse
{
    public override void Use()
    {
        base.Use();
        Debug.Log("Arrow");
    }

}
