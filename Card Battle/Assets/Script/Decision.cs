using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Decision : MonoBehaviour
{
    // �ݶ��̴��� �ε��� ī�� ����
    public Card Importedcard;
    // ��Ʋ�Ŵ������� �ѱ������ ī�� ����
    public Card card;
    // ���� �� ī�带 �����ϱ� ���� �ִ� ����
    public bool cardCheck;
    // ���� ī�带 �´��� �ȳ´��� üũ �ϴ� ����
    public bool cardPresence;

    // ������ �ٷ� Ÿ�̸Ӱ� 0�ʰ� �Ǳ����� �ִ� Ŭ����
    [SerializeField]
    private EnemyDecision enemyDecision;
    
    // ���� ��ư
    [SerializeField]
    private Button button; 

 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Importedcard = collision.gameObject.GetComponent<Card>();    
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
        cardPresence = true;
        cardCheck = true;

        BattleManager.Bm.playerCards.Remove(Importedcard);
        BattleManager.Bm.playerDeleteCards.Add(Importedcard);


        BattleManager.Bm.CardAlignment(true);

        BattleManager.Bm.playerDice = card.info.RandomDice;
        BattleManager.Bm.state = BattleManager.State.CardDecision;

        if ((BattleManager.Bm.enemyCards.Count == 0) || Importedcard != null 
            && enemyDecision.card != null)  { BattleManager.Bm.timer = 0; }
    
        Importedcard.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
    }
}

