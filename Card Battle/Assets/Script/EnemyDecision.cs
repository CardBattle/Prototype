using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EnemyDecision : MonoBehaviour
{
    Card card;
    public Button button;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        card = collision.gameObject.GetComponent<Card>();
        BattleManager.Bm.enemyCards.Remove(card);
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

        BattleManager.Bm.state = BattleManager.State.WaitState;

        Destroy(card.gameObject);

        // button.interactable = false;
    }
}

