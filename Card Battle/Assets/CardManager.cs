using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    //���� �÷��̾��� ī�� ������
    List<GameObject> playerCardPrefabs;
    List<GameObject> enemyCardPrefabs;
    //Instantiate �� ī�� ���ӿ�����Ʈ ����Ʈ
    public List<GameObject> playerCardObjs;
    public List<GameObject> enemyCardObjs;
    //Instantiate�� ī�� ���ӿ�����Ʈ���� ī�� ��ũ��Ʈ
    public List<Card> playerCards;
    public List<Card> enemyCards;
    //���õ� ī���(��ο�� ī���)
    public List<GameObject> playerSelectedCards;
    public List<GameObject> enemySelectedCards;

    private void Awake()
    {
        playerCardPrefabs = player.GetComponent<Character>().cards;
        enemyCardPrefabs = enemy.GetComponent<Character>().cards;

        Init(playerCardPrefabs, playerCardObjs, playerCards);
        Init(enemyCardPrefabs, enemyCardObjs, enemyCards);

        Test();
    }

    void Init(List<GameObject> cardPrefabs, List<GameObject> cardObjs, List<Card> cards)
    {
        foreach (var cardPrefab in cardPrefabs)
        {
            var cardObj = Instantiate(cardPrefab);
            var card = cardObj.GetComponent<Card>();

            card.Init();

            cardObjs.Add(cardObj);
            cards.Add(card);
        }
    }

    void Test()
    {
        foreach (var card in playerCards)
            card.card.use(null, null);

        foreach (var card in enemyCards)
            card.card.use(null, null);
    }
}
