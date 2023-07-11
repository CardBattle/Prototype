using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;
public class BattleManager : MonoBehaviour
{
    public static BattleManager Bm;
    public enum State
    {
        //대기 상태
        WaitState,

        //카드를 냈는지 안냈는지 체크
        SelectCard,

        //카드 결정후
        CardDecision,

        //카드 정리
        DestroyCard,     
    }

    //플레이어, 적 정보와 덱 체크
    [SerializeField]
    private Character player;
    [SerializeField]
    private Character enemy;

    //플레이어, 적 카드 정보 체크
    [SerializeField]  
    public Decision playerDecision;
    public EnemyDecision enemyDecision;

    //플레이어, 적 덱이랑 드로우 리스트
    [SerializeField]
    private List<GameObject> playerDeck;
    [SerializeField]
    private List<GameObject> enemyDeck;
    public List<Card> playerCards;
    public List<Card> enemyCards;

    // 전에 쓴 카드들 삭제하기 위한 리스트
    public List<Card> playerDeleteCards;
    public List<Card> enemyDeleteCards;

    //카드매니저
    [SerializeField]
    private CardManager cardManager;


    //카드 매니저

    [SerializeField]
    private TextMeshPro Selet;
    //원래 있었던 내 카드 위치
    [SerializeField]
    private PRS originalPlace;

    //드로우 카드 위치 오브젝트
    [SerializeField] Transform myDeckPosition;
    [SerializeField] Transform enemyDeckPosition;
    [SerializeField] Transform myCardUp;
    [SerializeField] Transform myCardDown;
    [SerializeField] Transform enemyCardUp;
    [SerializeField] Transform enemyCardDown;

    //Add메서드 하기 편하게 넣어본 이벤트
    private static Action<bool> OnAddCard;
    
    //Decision 클래스에서 넘겨받은 카드 정보를 저장하는 클래스
    private Card selectCard;



    //카드 이미지들 순서
    private int order = 0;
    private int enemyOrder = 0;
   
    // 전에 쓴 카드들 이미지들 순서
    private int sortingCard = 0;
    private int enemySortingCard = 0;

    // 전전에 쓴 카드 오브젝트 삭제하기 위한 변수
    private int turnCount = 0;

    // 다이스 체크
    public int playerDice = 0;
    public int enemyDice = 0;

    // 타이머
    public float timer;
    
    // 플레이어 동작
    public State state;
    [SerializeField]
    private Slider playerHpSlider; // 플레이어 hp 슬라이더
    [SerializeField]
    private Slider enemyHpSlider; // 적 hp 슬라이더
    [SerializeField]
    private  TextMeshPro timerText; //타이머 텍스트

    private void Awake()
    {
        if (Bm == null)
        {
            Bm = this;
        }

        player.Init();  
        enemy.Init();
          
        playerHpSlider.maxValue = player.info.MaxHp;    
        enemyHpSlider.maxValue = enemy.info.MaxHp;

        playerHpSlider.value = player.info.Hp;
        enemyHpSlider.value = enemy.info.Hp;

 
        cardManager.Init();       
        OnAddCard = Add;

        state = State.CardDecision;

        timerText.text = "10.00";

    }
    private void Start()
    {     
        StartCoroutine(StartGame(5));        
        StateTurn();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnAddCard?.Invoke(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            enemyCards[0].GetComponent<Card>().EnemyCardFront();
        }
    }
    private void StateTurn()
    {
        playerDecision.cardPresence = false;
        BuffCheck();
        StartCoroutine(WaitTimer());
    }
    /*public void DeckCheck()
    {
        //플레이어나 적 덱에 카드가 있는지 체크
        //없으면 DeckPull()로 이동
    }
    public void CardDraw()
    {
        //카드 드로우 턴
        //드로우 할 카드 없으면 대기
    }*/
    private void CardSelectionTurn()
    {
        state = State.WaitState;
        StartCoroutine(Timer());

        if (enemyCards.Count != 0)
        {
            enemyCards[0]?.MoveTransform(enemyCards[0].originPRS, true, 0.7f);
        }    
    }
    private void DiceTurn()
    {
        //존재하는 버프 사용
        if(player.info.buffs.Count > 0)
        {
            foreach(var buff in player.info.buffs)
            {
                buff.buffUse.Use(player);
            }
        }
        if(enemy.info.buffs.Count > 0)
        {
            foreach(var buff in enemy.info.buffs)
            {
                buff.buffUse.Use(enemy);
            }
        }

        if (playerDecision.card == null && playerDice == 0) { playerDice = 0; }
        if (enemyDecision.card == null && enemyDice == 0) { enemyDice = 0; }

        print($"아군 다이스{playerDice}");
        print($"적군 다이스{enemyDice}");

        if (enemyDecision.card != null)     
            enemyDecision.card.EnemyCardFront();

        if (playerDice > enemyDice)
        {
            playerDecision.card.info.use(player, enemy);
        }
        else if (playerDice == enemyDice)
        {

            playerDice = 0;
            enemyDice = 0;

            StartCoroutine(CardSorting());
            return;
        }
        else
        {
            enemyDecision.card.info.use(enemy, player);
        }

        playerHpSlider.value = player.info.Hp;
        enemyHpSlider.value = enemy.info.Hp;

        Debug.Log($"CardUse 결과: 플레이어Hp:{player.info.Hp}\n에너미Hp:{enemy.info.Hp}");

        playerDice = 0;
        enemyDice = 0;

        StartCoroutine(CardSorting());       
    }
    public void DiceResultTurn()
    {
        //주사위 높은사람이 카드 효과 발동
        //다시 StateTurn()으로 돌아감
    }
      
    //여기는 다른 분기점
    /*public void DeckPull()
    {
        //카드 없을때 채워주는 메서드
        //여기서 카드 선택 메서드까지 기다렸다가 같이 DiceTurn() 으로 이동
    }*/
    
    private void BattleResult(int result)
    {
        if (result == 0) { timerText.text = "패배"; }
        if (result == 1) { timerText.text = "승리"; }
        if (result == 2) { timerText.text = "무승부"; }

        StopAllCoroutines();     
    }

    void CardCombine(bool myCard)
    {
        if (myCard && playerDeck.Count == 0)
        {
            for (int i = 0; i < cardManager.playerCardObjs.Count; i++)
            {
                playerDeck.Add(cardManager.playerCardObjs[i]);
            }

            for (int i = 0; i < playerDeck.Count; i++)
            {
                int x = Random.Range(0, playerDeck.Count);
                var temp = playerDeck[i];
                playerDeck[i] = playerDeck[x];
                playerDeck[x] = temp;
            }
        }
        if (!myCard && enemyDeck.Count == 0)
        {
            for (int i = 0; i < cardManager.enemyCardObjs.Count; i++)
            {
                enemyDeck.Add(cardManager.enemyCardObjs[i]);
            }
        }
    }

    public void CardMoustDown(Card card)
    {
        if (state == State.CardDecision)
            return;

        if (state == State.WaitState)
        {
            selectCard = card;

            originalPlace = card.originPRS;

            card.originPRS = new PRS(new Vector2(-1.8f, 2.7f), Utlis.Qi, Vector3.one * 4.2f);
            card.MoveTransform(card.originPRS, false);

            card.cardSelect = true;

            state = State.SelectCard;
        }
        else if (state == State.SelectCard)
        {
            selectCard.cardSelect = false;
            selectCard.originPRS = originalPlace;
            selectCard.MoveTransform(selectCard.originPRS, false);

            originalPlace = card.originPRS;
            selectCard = card;

            card.originPRS = new PRS(new Vector2(-1.8f, 2.7f), Utlis.Qi, Vector3.one * 4.2f);
            card.MoveTransform(card.originPRS, false);

            card.cardSelect = true;        
        }

    }

    public void CardSelectDown(Card card)
    {
        selectCard.originPRS = originalPlace;
        card.MoveTransform(card.originPRS, false);

        card.cardSelect = false;
    }
    public void CardMouseOver(Card card)
    {
        card.GetComponent<Order>().DragOrder(true);

    }
    public void CardMouseExit(Card card)
    {
        card.GetComponent<Order>().DragOrder(false);
    }

    IEnumerator WaitTimer()
    {      
        yield return new WaitForSeconds(0.5f);
     
        if (playerCards.Count == 0)
        {
            StartCoroutine(DrawFullCard(true));
        }

        if (enemyCards.Count == 0)
        {
            StartCoroutine(DrawFullCard(false));       
        }

        if (turnCount >= 2 && playerDecision.cardCheck && playerDeleteCards.Count >= 2)
        {
            Destroy(playerDeleteCards[0].gameObject);
            playerDeleteCards.RemoveAt(0);
            playerDeleteCards[0].GetComponent<Order>().SettingOrder(0);
            sortingCard = 1;
        }

        if (turnCount >= 2 && enemyDecision.cardCheck && enemyDeleteCards.Count >= 2)
        {
            Destroy(enemyDeleteCards[0].gameObject);
            enemyDeleteCards.RemoveAt(0);
            enemyDeleteCards[0].GetComponent<Order>().SettingOrder(0);
            enemySortingCard = 1;
        }

        yield return new WaitForSeconds(2);
    
        if (player.info.Hp == 0 && enemy.info.Hp != 0)
        {
            BattleResult(0);
        }
        else if (enemy.info.Hp == 0 && player.info.Hp != 0)
        {
            BattleResult(1);
        }
        else if (enemy.info.Hp == 0 && player.info.Hp == 0)
        {
            BattleResult(2);
        }
      
        OnAddCard?.Invoke(true);
        OnAddCard?.Invoke(false);

        playerDecision.cardCheck = false;
        enemyDecision.cardCheck = false;


        yield return new WaitForSeconds(0.5f);

        ++turnCount;

        timer = 10;

        if(enemyCards.Count != 0)
        {
            enemyCards[0].originPRS = new PRS(new Vector2(1.8f, 2.7f), Utlis.Qi, Vector3.one * 4.2f);
        }   
        
        CardSelectionTurn();
    }
    IEnumerator Timer()
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = string.Format("{0:N2}", timer);
            yield return new WaitForSeconds(0.001f);
        }

        if (timer <= 0)
        {
            timerText.text = "0.00";
           
            if (playerDecision.cardPresence == false && playerDecision.Importedcard != null)
            {
                playerDecision.DecisionButtom();
            }
        }
  
        state = State.CardDecision;
        DiceTurn();
    }

    void BuffCheck()
    {
        if (enemy.info.buffs.Count > 0)
        {
            foreach (var buff in enemy.info.buffs)
            {
                Debug.Log($"enemy buff:{buff.info.Name}, remain: {buff.info.CurrentTurn}");
                buff.BuffCheck(enemy);
                buff.info.CurrentTurn--;
            }
        }
        if (player.info.buffs.Count > 0)
        {
            foreach (var buff in player.info.buffs)
            {
                Debug.Log($"player buff:{buff.info.Name}, remain: {buff.info.CurrentTurn}");
                buff.BuffCheck(player);
                buff.info.CurrentTurn--;
            }
        }
    }

    IEnumerator StartGame(int start)
    {
        CardCombine(true);
        CardCombine(false);

        for (int i = 0; i < start; i++)
        {
            yield return new WaitForSeconds(0.1f);
            OnAddCard?.Invoke(true);
            yield return new WaitForSeconds(0.1f);
            OnAddCard?.Invoke(false);
        }
    }

    IEnumerator DrawFullCard(bool myCard)
    {  
        for (int i = 0; i < 5; i++)
        {                  
            OnAddCard?.Invoke(myCard);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator CardSorting()
    {
        if (playerCards.Count == 0 && playerDeck.Count == 0)
        {
            CardCombine(true);
        }
        if (enemyCards.Count == 0 && enemyDeck.Count == 0)
        {
            CardCombine(false);
        }

        if (playerDecision.card != null)        
            playerDecision.card.originPRS = new PRS(new Vector2(-4.3f, 2.7f), Utlis.Qi, Vector3.one * 4.2f);
        if (enemyDecision.card != null)
            enemyDecision.card.originPRS = new PRS(new Vector2(4.3f, 2.7f), Utlis.Qi, Vector3.one * 4.2f);

        if (playerDecision.card != null)
            playerDecision.card.GetComponent<Order>().SettingOrder(sortingCard++);
        if (enemyDecision.card != null)
            enemyDecision.card.GetComponent<Order>().SettingOrder(enemySortingCard++);

        yield return new WaitForSeconds(0.3f);

        if (playerDecision.card != null)
            playerDecision.card.MoveTransform(playerDecision.card.originPRS, true, 0.7f);
        if(enemyDecision.card != null)     
            enemyDecision.card.MoveTransform(enemyDecision.card.originPRS, true, 0.7f);
        
        yield return new WaitForSeconds(0.1f);     

        playerDecision.card = null;
        enemyDecision.card = null;

        
        StateTurn();
    }

    void Add(bool myCard)
    {
        if (myCard && playerDeck.Count != 0 && playerCards.Count < 9)
        {
            var cardObject = Instantiate(playerDeck[0], new Vector2(myDeckPosition.transform.position.x, myDeckPosition.transform.position.y), Utlis.Qi);
            var card = cardObject.GetComponent<Card>();
            card.Init();

            card.CardFront(myCard);
            playerCards.Add(card);
            playerDeck!.RemoveAt(0);

            card.GetComponent<Order>().SetOriginOrder(order++);
            CardAlignment(myCard);
        }

        if (!myCard && enemyDeck.Count != 0 && enemyCards.Count < 11)
        {
            var cardObject = Instantiate(enemyDeck[0], new Vector2(enemyDeckPosition.transform.position.x, enemyDeckPosition.transform.position.y), Utlis.Qi);
            var card = cardObject.GetComponent<Card>();
            card.Init();

            card.CardFront(myCard);
            enemyCards.Add(card);
            enemyDeck!.RemoveAt(0);

            card.GetComponent<Order>().SetOriginOrder(enemyOrder++);
            CardAlignment(myCard);
        }


    }

    public void CardAlignment(bool isMine)
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
            targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);          
        }      
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
