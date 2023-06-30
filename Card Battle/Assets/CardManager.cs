using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    //�ν�����â���� �־���ϴ� ī�� �����յ�
    public List<GameObject> cardPrefabs;
    //Instantiate �� ī�� ���ӿ�����Ʈ ����Ʈ
    public List<GameObject> cardObjs;
    //Instantiate�� ī�� ���ӿ�����Ʈ���� ī�� ��ũ��Ʈ
    public List<Card> cards;
    //���õ� ī���(��ο�� ī���)
    public List<GameObject> selectedCards;

    private void Awake()
    {
        foreach(var cardPrefab in cardPrefabs)
        {
            var cardObj = Instantiate(cardPrefab);
            var card = cardObj.GetComponent<Card>();

            card.Init();

            cardObjs.Add(cardObj);
            cards.Add(card);
        }

        Test();
    }

    void Test()
    {
        foreach (var card in cards)
            card.card.use();
            
    }
}
