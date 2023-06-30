using UnityEngine;

public class Cut : CardUse
{
    public override void Use()
    {
        base.Use();
        Debug.Log("Cut");
    }

}
