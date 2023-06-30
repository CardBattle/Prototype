using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Card card;
    public void Init()
    {
        card = GetComponent<Card>();
        card.card.use = Use;
    }

    private void Use()
    {
        Debug.Log("Fireball");
    }
}
