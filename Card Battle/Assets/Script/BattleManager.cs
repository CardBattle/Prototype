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

        //�÷��̾ �ֻ����� �� ��������
        PlayerTurn,

        //���� �ֻ����� �� ��������
        EnemyTurn
    }
    //ĳ����
    public GameObject player;
    public GameObject enemy;

    //�� ���̶� ��ο� ����Ʈ
    public List<GameObject> playerdDeck;
    public List<GameObject> enemyDeck;
    public List<Card> playerCards;
    public List<Card> enemyCards;

    //ī�� �Ŵ���
    public CardManager cardManager;

    //
    public TextMeshPro Selet;
    //���� �־��� �� ī�� ��ġ
    public PRS originalPlace;

    //�� ����
    public State state;

    //�������� �ƴ��� üũ
    public bool myCard;
    //���� ��ī�� ���� �ö���� �ϱ�
    //public bool cardSelect;

    //��ο� ī�� ��ġ ������Ʈ
    [SerializeField] Transform myDeckPosition;
    [SerializeField] Transform enemyDeckPosition;
    [SerializeField] Transform myCardUp;
    [SerializeField] Transform myCardDown;
    [SerializeField] Transform enemyCardUp;
    [SerializeField] Transform enemyCardDown;

    public static Action<bool> OnAddCard;
    public Card selectCard;

    int order = 0;
    int enemyOrder = 0;

    /*public State state; //���� �� ����
    public float timer; //���� ���� �ð�
    public Slider playerHp; // �÷��̾� hp
    public Slider enemyHp; // �� hp
    public Slider timerSlider; //Ÿ�̸� �����̴�
    public TextMeshPro timerText; //Ÿ�̸� �ð�*/

    private void Awake()
    {
        if (Bm == null)
        {
            Bm = this;
        }

        foreach (var character in GameObject.FindGameObjectsWithTag("Player"))
            character.GetComponent<Character>().Init();

        cardManager.Init();

        OnAddCard = Add;
    }
    private void Start()
    {
        StartCoroutine(StartGame(5));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(StartGame(1));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            enemyCards[0].GetComponent<Card>().EnemyCardFront();
        }
    }
    public void StateTurn()
    {
        //���� ���۵ɶ� ���⼭ �÷��̾� ���� ����� �ǰ��� üũ
        //�÷��̾ ���� ������ BattleResult() �Ѿ
    }
    public void DeckCheck()
    {
        //�÷��̾ �� ���� ī�尡 �ִ��� üũ
        //������ DeckPull()�� �̵�
    }
    public void CardDraw()
    {
        //ī�� ��ο� ��
        //��ο� �� ī�� ������ ���
    }
    public void CardSelectionTurn()
    {
        //���⼭ Ÿ�̸� üũ
        //ī�� ����
    }
    public void DiceTurn()
    {
        //ī�� ������ ���߰ų� ���� ������ 0���� �ʱⰪ����
        //�÷��̾� �� �ֻ��� üũ
    }
    public void DiceResultTurn()
    {
        //�ֻ��� ��������� ī�� ȿ�� �ߵ�
        //�ٽ� StateTurn()���� ���ư�
    }
    //����� �ٸ� �б���
    public void DeckPull()
    {
        //ī�� ������ ä���ִ� �޼���
        //���⼭ ī�� ���� �޼������ ��ٷȴٰ� ���� DiceTurn() ���� �̵�
    }
    public void BattleResult()
    {
        //���� ���
    }

    void CardCombine(bool myCard)
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
            print(originalPlace.pos);

            card.originPRS = new PRS(new Vector2(-1.8f, 2.7f), Utlis.Qi, Vector3.one * 4.2f);
            card.MoveTransform(card.originPRS, false);

            card.cardSelect = true;

            state = State.SelectCard;
            CardAlignment(myCard);

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

            CardAlignment(myCard);
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

    void Add(bool myCard)
    {
        if (myCard)
        {
            var cardObject = Instantiate(playerdDeck[0], new Vector2(myDeckPosition.transform.position.x, myDeckPosition.transform.position.y), Utlis.Qi);
            var card = cardObject.GetComponent<Card>();
            card.Init();

            card.CardFront(myCard);
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
