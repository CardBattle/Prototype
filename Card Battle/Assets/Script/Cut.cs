using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cut : MonoBehaviour
{
    // 인스펙터 창에서 Cut 카드의 변수를 관리하기 위한 변수
    public int id;
    public CardType type; //카드 종류
    public string cardName;
    public List<DefaultBuff> buffs;//28일에 하기
    public int effVal;
    public Sprite img;
    DefaultCard cutCard;

    void Start()
    {
        cutCard=new DefaultCard(id, CardType.SWORD, cardName, buffs, effVal, img);
    }
    void Use()//카드를 선택했을때 사용할 메소드
    {
        cutCard.Use();        // 델리게이트USE에 넣은 메소드를 어떻게 꺼내 쓸지 
    }

    //버튼으로 만들어서 클릭하면 use가 실행됨 델리케이트 
    //나머지 6개도 이렇게 넣으면 될 꺼 같은데 어떤가요?
}