using UnityEngine;

public class Cut : CardUse
{
    public override void Use(Character sender, Character receiver)
    {
        base.Use(sender, receiver); 
        Debug.Log("Cut");
    }

}
