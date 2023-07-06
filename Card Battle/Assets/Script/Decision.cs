using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Decision : MonoBehaviour
{
    Card card;
    public Button button;
    public delegate void CardUse(Character sender, Character receiver);
    public CardUse cardUse;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        card = collision.gameObject.GetComponent<Card>();
        print("카드가 존재함");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        card = null;
    }

    public void DecisionButtom()
    {
        if (card == null)
            return;

        card.info.use(BattleManager.Bm.player.GetComponent<Character>(), BattleManager.Bm.enemy.GetComponent<Character>());

        BattleManager.Bm.playerCards.Remove(card);
        BattleManager.Bm.CardAlignment(true);

        BattleManager.Bm.state = BattleManager.State.CardDecision;
        
        
        card.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        // button.interactable = false;
    }
}

