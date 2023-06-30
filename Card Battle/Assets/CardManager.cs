using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    //인스펙터창에서 넣어야하는 카드 프리팹들
    public List<GameObject> cardPrefabs;
    //Instantiate 된 카드 게임오브젝트 리스트
    public List<GameObject> cardObjs;
    //Instantiate된 카드 게임오브젝트들의 카드 스크립트
    public List<Card> cards;
    //선택된 카드들(드로우된 카드들)
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
