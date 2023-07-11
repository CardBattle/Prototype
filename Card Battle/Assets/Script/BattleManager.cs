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
        //��� ����
        WaitState,

        //ī�带 �´��� �ȳ´��� üũ
        SelectCard,

        //ī�� ������
        CardDecision,

        //ī�� ����
        DestroyCard,     
    }

    //�÷��̾�, �� ������ �� üũ
    [SerializeField]
    private Character player;
    [SerializeField]
    private Character enemy;

    //�÷��̾�, �� ī�� ���� üũ
    [SerializeField]  
    public Decision playerDecision;
    public EnemyDecision enemyDecision;

    //�÷��̾�, �� ���̶� ��ο� ����Ʈ
    [SerializeField]
    private List<GameObject> playerDeck;
    [SerializeField]
    private List<GameObject> enemyDeck;
    public List<Card> playerCards;
    public List<Card> enemyCards;

    // ���� �� ī��� �����ϱ� ���� ����Ʈ
    public List<Card> playerDeleteCards;
    public List<Card> enemyDeleteCards;

    //ī��Ŵ���
    [SerializeField]
    private CardManager cardManager;


    //ī�� �Ŵ���

    [SerializeField]
    private TextMeshPro Selet;
    //���� �־��� �� ī�� ��ġ
    [SerializeField]
    private PRS originalPlace;

    //��ο� ī�� ��ġ ������Ʈ
    [SerializeField] Transform myDeckPosition;
    [SerializeField] Transform enemyDeckPosition;
    [SerializeField] Transform myCardUp;
    [SerializeField] Transform myCardDown;
    [SerializeField] Transform enemyCardUp;
    [SerializeField] Transform enemyCardDown;

    //Add�޼��� �ϱ� ���ϰ� �־ �̺�Ʈ
    private static Action<bool> OnAddCard;
    
    //Decision Ŭ�������� �Ѱܹ��� ī�� ������ �����ϴ� Ŭ����
    private Card selectCard;



    //ī�� �̹����� ����
    private int order = 0;
    private int enemyOrder = 0;
   
    // ���� �� ī��� �̹����� ����
    private int sortingCard = 0;
    private int enemySortingCard = 0;

    // ������ �� ī�� ������Ʈ �����ϱ� ���� ����
    private int turnCount = 0;

    // ���̽� üũ
    public int playerDice = 0;
    public int enemyDice = 0;

    // Ÿ�̸�
    public float timer;
    
    // �÷��̾� ����
    public State state;
    [SerializeField]
    private Slider playerHpSlider; // �÷��̾� hp �����̴�
    [SerializeField]
    private Slider enemyHpSlider; // �� hp �����̴�
    [SerializeField]
    private  TextMeshPro timerText; //Ÿ�̸� �ؽ�Ʈ

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
        //�÷��̾ �� ���� ī�尡 �ִ��� üũ
        //������ DeckPull()�� �̵�
    }
    public void CardDraw()
    {
        //ī�� ��ο� ��
        //��ο� �� ī�� ������ ���
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
        //�����ϴ� ���� ���
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

        print($"�Ʊ� ���̽�{playerDice}");
        print($"���� ���̽�{enemyDice}");

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

        Debug.Log($"CardUse ���: �÷��̾�Hp:{player.info.Hp}\n���ʹ�Hp:{enemy.info.Hp}");

        playerDice = 0;
        enemyDice = 0;

        StartCoroutine(CardSorting());       
    }
    public void DiceResultTurn()
    {
        //�ֻ��� ��������� ī�� ȿ�� �ߵ�
        //�ٽ� StateTurn()���� ���ư�
    }
      
    //����� �ٸ� �б���
    /*public void DeckPull()
    {
        //ī�� ������ ä���ִ� �޼���
        //���⼭ ī�� ���� �޼������ ��ٷȴٰ� ���� DiceTurn() ���� �̵�
    }*/
    
    private void BattleResult(int result)
    {
        if (result == 0) { timerText.text = "�й�"; }
        if (result == 1) { timerText.text = "�¸�"; }
        if (result == 2) { timerText.text = "���º�"; }

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
