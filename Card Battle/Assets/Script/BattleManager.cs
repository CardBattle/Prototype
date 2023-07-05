using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleManager : MonoBehaviour
{
    public enum State
    {
        //대기 상태
        WaitState,

        //플레이어가 주사위가 더 높았을때
        PlayerTurn,
        //적에 주사위가 더 높았을때
        EnemyTurn
    }

    public List<GameObject> playerdDeck;
    public List<GameObject> enemyDack;
    public CardManager cardManager;
    [SerializeField] Transform instanc;
    [SerializeField] Transform my;

    /*public State state; //지금 턴 상태
    public float timer; //현재 남은 시간
    public Slider playerHp; // 플레이어 hp
    public Slider enemyHp; // 적 hp
    public Slider timerSlider; //타이머 슬라이더
    public TextMeshPro timerText; //타이머 시간*/

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
}
