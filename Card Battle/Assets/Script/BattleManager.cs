using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class BattleManager : MonoBehaviour
{
    public static BattleManager Bm;
    public enum State
    {
        //대기 상태
        WaitState,

        //플레이어가 주사위가 더 높았을때
        PlayerTurn,
        //적에 주사위가 더 높았을때
        EnemyTurn
    }

    public GameObject player;
    public GameObject enemy;
    public List<GameObject> playerdDeck;
    public List<GameObject> enemyDeck;
    public List<Card> playerCards;
    public List<Card> enemyCards;

    public CardManager cardManager;
   
    
    public bool myCard;
    public bool cardSelect;
     
    [SerializeField] Transform myDeckPosition;
    [SerializeField] Transform enemyDeckPosition;
    [SerializeField] Transform myCardUp;
    [SerializeField] Transform myCardDown;
    [SerializeField] Transform enemyCardUp;
    [SerializeField] Transform enemyCardDown;


    public Card selectCard;

    int order = 0;
    int enemyOrder = 0;


    /*public State state; //지금 턴 상태
    public float timer; //현재 남은 시간
    public Slider playerHp; // 플레이어 hp
    public Slider enemyHp; // 적 hp
    public Slider timerSlider; //타이머 슬라이더
    public TextMeshPro timerText; //타이머 시간*/

    private void Awake()
    {
        if (Bm == null)
        {
            Bm = this;
        }

        foreach (var character in GameObject.FindGameObjectsWithTag("Player"))
            character.GetComponent<Character>().Init();

        cardManager.Init();
    }
    private void Start()
    {
        myCard = true;
        CardCombine();
        myCard = false;
        CardCombine();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            myCard = true;
            Test();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            myCard = false;
            Test();
        }
    }

    private void Test()
    {
        if (playerdDeck.Count <= 0)
        {
            CardCombine();
        }
        if (enemyDeck.Count <= 0)
        {
            CardCombine();
        }

        Add();
    }

    void CardCombine()
    {
        if (myCard && playerdDeck.Count == 0)
        {
            for (int i = 0; i < cardManager.playerCardObjs.Count; i++)
            {
                playerdDeck.Add(cardManager.playerCardObjs[i]);
            }

            for (int i = 0; i < playerdDeck.Count; i++)
            {
                int x = Random.Range(0, playerdDeck.Count);
                var temp = playerdDeck[i];
                playerdDeck[i] = playerdDeck[x];
                playerdDeck[x] = temp;
            }

            if (playerCards.Count == 0)
            {

            }
        }

        if (!myCard && enemyDeck.Count == 0)
        {
            for (int i = 0; i < cardManager.enemyCardObjs.Count; i++)
            {
                enemyDeck.Add(cardManager.enemyCardObjs[i]);
            }

            enemyOrder = 0;
        }

    }

    public void StateTurn()
    {
        //턴이 시작될때 여기서 플레이어 버프 디버프 피관리 체크
        //플레이어나 적이 죽으면 BattleResult() 넘어감
    }
    public void DeckCheck()
    {
        //플레이어나 적 덱에 카드가 있는지 체크
        //없으면 DeckPull()로 이동
    }
    public void CardDraw()
    {
        //카드 드로우 턴
        //드로우 할 카드 없으면 대기
    }
    public void CardSelectionTurn()
    {
        //여기서 타이머 체크
        //카드 선택
    }
    public void DiceTurn()
    {
        //카드 선택을 안했거나 덱이 없을땐 0으로 초기값변경
        //플레이어 적 주사위 체크
    }
    public void DiceResultTurn()
    {
        //주사위 높은사람이 카드 효과 발동
        //다시 StateTurn()으로 돌아감
    }
    //여기는 다른 분기점
    public void DeckPull()
    {
        //카드 없을때 채워주는 메서드
        //여기서 카드 선택 메서드까지 기다렸다가 같이 DiceTurn() 으로 이동
    }
    public void BattleResult()
    {
        //전투 결과
    }
    public void CardMouseOver(Card card)
    {
        selectCard = card;
        cardSelect = true;

        card.GetComponent<Order>().DragOrder(cardSelect);
        
    }
    public void CardMouseExit(Card card)
    {
        selectCard = null;
        cardSelect = false;

        card.GetComponent<Order>().DragOrder(cardSelect);
    }


    void Add()
    {
        if (myCard)
        {
            var cardObject = Instantiate(playerdDeck[0], new Vector2(myDeckPosition.transform.position.x, myDeckPosition.transform.position.y), Utlis.Qi);
            var card = cardObject.GetComponent<Card>();
            card.Init();

            playerCards.Add(card);
            playerdDeck!.RemoveAt(0);
            
            card.GetComponent<Order>().SetOriginOrder(order++);
            CardAlignment(myCard);

        }

        if (!myCard)
        {
            var cardObject = Instantiate(enemyDeck[0], new Vector2(enemyDeckPosition.transform.position.x, enemyDeckPosition.transform.position.y), Utlis.Qi);
            var card = cardObject.GetComponent<Card>();
            card.Init();

            enemyCards.Add(card);
            enemyDeck!.RemoveAt(0);

            card.GetComponent<Order>().SetOriginOrder(enemyOrder++);
            CardAlignment(myCard);          
        }


    }

    void CardAlignment(bool isMine)
    {
        List<PRS> originCardPRSs = new List<PRS>();

        if (isMine)
            originCardPRSs = RoundAlignment(myCardUp, myCardDown, playerCards.Count, -6.74f, Vector3.one * 2.5f); ;
        if (!isMine)
            originCardPRSs = RoundAlignment(enemyCardUp, enemyCardDown, enemyCards.Count, 6.74f, Vector3.one * 2.5f);

        var targetCards = isMine ? playerCards : enemyCards;

        for (int i = 0; i < targetCards.Count; i++)
        {
            var targetCard = targetCards[i];

            targetCard.originPRS = originCardPRSs[i];

           // while (Vector2.Distance(targetCard.transform.position, targetCard.originPRS.pos) >= 0.1f)
           // {
                targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);
                
          //  }
        }
        //yield return null;
    }

    List<PRS> RoundAlignment(Transform upTR, Transform downTR, int objcount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objcount];
        List<PRS> results = new List<PRS>(objcount);

        switch (objcount)
        {
            case 1:
                objLerps = new float[] { 0 };
                break;
            case 2:
                objLerps = new float[] { 0, 0.2f };
                break;
            case 3:
                objLerps = new float[] { 0, 0.2f, 0.4f };
                break;
            case 4:
                objLerps = new float[] { 0, 0.2f, 0.4f, 0.6f };
                break;
            case 5:
                objLerps = new float[] { 0, 0.2f, 0.4f, 0.6f, 0.8f };
                break;
            case 6:
                objLerps = new float[] { 0, 0.2f, 0.4f, 0.6f, 0.8f, 1 };
                break;
            default:
                float interval = 1f / (objcount - 1);
                for (int i = 0; i < objcount; i++)
                {
                    objLerps[i] = interval * i;
                }
                break;
        }

        for (int i = 0; i < objcount; i++)
        {
            var targetPos = Vector2.Lerp(upTR.position, downTR.position, objLerps[i]);
            var targetRot = Utlis.Qi;

            if (objcount >= 7)
            {
                targetPos.x = height;
            }

            results.Add(new PRS(targetPos, targetRot, scale));
        }

        return results;
    }
}
