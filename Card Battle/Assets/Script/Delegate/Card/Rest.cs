using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : CardUse
{
    public override void Use()
    {
        base.Use();
        Debug.Log("Rest");
    }

}
