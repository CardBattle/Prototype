using UnityEngine;

public class CardUse : MonoBehaviour
{
    public Card card;
    public void Init()
    {
        card = GetComponent<Card>();
        card.card.use = Use;
    }

    public virtual void Use()
    {

    }

}
