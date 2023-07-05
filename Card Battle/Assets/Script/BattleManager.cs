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
        //��� ����
        WaitState,

        //�÷��̾ �ֻ����� �� ��������
        PlayerTurn,
        //���� �ֻ����� �� ��������
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
