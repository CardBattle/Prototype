using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EnemyDecision : MonoBehaviour
{
    // �ݶ��̴��� �ε��� ī�� ����
    private Card Importedcard;
    // ��Ʋ�Ŵ������� �ѱ������ ī�� ����
    public Card card;
    // ���� �� ī�带 �����ϱ� ���� �ִ� ����
    public bool cardCheck;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Importedcard = collision.gameObject.GetComponent<Card>();

        DecisionButtom();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Importedcard = null;
    }

    public void DecisionButtom()
    {
        if (Importedcard == null)
            return;

        card = Importedcard;
        card.defenseCheck = false;
        cardCheck = true;

        BattleManager.Bm.enemyCards.Remove(Importedcard);
        BattleManager.Bm.enemyDeleteCards.Add(Importedcard);

        BattleManager.Bm.CardAlignment(false);

        BattleManager.Bm.enemyDice = card.info.RandomDice;
        Importedcard.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
    }
}

