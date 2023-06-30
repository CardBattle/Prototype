using UnityEngine;

public class Cut : MonoBehaviour
{
    Card card;
    public void Init()
    {
        card = GetComponent<Card>();
        card.card.use = Use;
    }

    private void Use()
    {
        Debug.Log("Cut");
    }

}
