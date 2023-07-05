using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleManager : MonoBehaviour
{
    public enum State
    {
        //��� ����
        WaitState,

        //�÷��̾ �ֻ����� �� ��������
        PlayerTurn,
        //���� �ֻ����� �� ��������
        EnemyTurn
    }

    public List<GameObject> playerdDeck;
    public List<GameObject> enemyDack;
    public CardManager cardManager;
    [SerializeField] Transform instanc;
    [SerializeField] Transform my;

    /*public State state; //���� �� ����
    public float timer; //���� ���� �ð�
    public Slider playerHp; // �÷��̾� hp
    public Slider enemyHp; // �� hp
    public Slider timerSlider; //Ÿ�̸� �����̴�
    public TextMeshPro timerText; //Ÿ�̸� �ð�*/

    private void Start()
    {
        foreach (var character in GameObject.FindGameObjectsWithTag("Player"))
            character.GetComponent<Character>().Init();

        CardCombine();
        enemyDack = cardManager.enemyCardObjs;
    }

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.Alpha1))
       {
          Test();
       }
    }

    private void Test()
    {
        if (playerdDeck.Count <= 0)
        {
            CardCombine();
        }
        var Card = Instantiate(playerdDeck[0], new Vector2(instanc.transform.position.x, instanc.transform.position.y),Quaternion.identity); 
        playerdDeck!.RemoveAt(0);
        //Card.transform.position = Vector2.Lerp(transform.position, my.transform.position , 0.05f );
        Card.transform.position = Vector2.MoveTowards(transform.position, my.transform.position, 2);
    }

    void CardCombine()
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
}
