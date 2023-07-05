using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : CardUse
{
    public override void Use(Character sender, Character receiver)
    {
        base.Use(sender, receiver);

        receiver.info.Hp -= CalculateDmg(sender.info.AttackDmg, card.info.Dice, card.info.EffVal,
        CalculateEffect(card.info.Type, receiver.info.Weapon));

        Debug.Log("Arrow");
    }

}
