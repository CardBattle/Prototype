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
    //���õ� ī���(��ο�� ī���)
    /*public List<GameObject> playerSelectedCards;
    public List<GameObject> enemySelectedCards;*/

    public void Init()
    {
        playerCardPrefabs = player.GetComponent<Character>().cards;
        enemyCardPrefabs = enemy.GetComponent<Character>().cards;

        Load(playerCardPrefabs, playerCardObjs);
        Load(enemyCardPrefabs, enemyCardObjs);
    }

    void Load(List<GameObject> cardPrefabs, List<GameObject> cardObjs)
    {
        foreach (var cardPrefab in cardPrefabs)
        {
            cardObjs.Add(cardPrefab);        
        }
    }

    /*void Test()
    {
        foreach (var card in playerCards)
            card.info.use(player.GetComponent<Character>(), enemy.GetComponent<Character>());
    }*/
}
