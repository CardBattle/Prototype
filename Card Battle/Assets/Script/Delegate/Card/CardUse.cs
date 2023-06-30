using UnityEngine;

public class CardUse : MonoBehaviour
{
    public Card card;
    public void Init()
    {
        card = GetComponent<Card>();
        card.card.use = Use;
    }

    public virtual void Use(Character sender, Character receiver)
    {
        if(card.card.buffs.Count > 0 )
        {
            foreach(var buff in card.card.buffs)
            {
                buff.buff.use(sender, receiver);
            }
        }
    }

}
