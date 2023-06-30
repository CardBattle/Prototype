using UnityEngine;

public class CardUse : MonoBehaviour
{
    public Card card;
    public void Init()
    {
        card = GetComponent<Card>();
        card.card.use = Use;
    }

    public virtual void Use(Character sender, Character receiver)
    {
        //버프 - 프로토타입 구현 X 연동만 해둠.
        if(card.card.buffs.Count > 0 )
        {
            foreach(var buff in card.card.buffs)
            {
                buff.buff.use(sender, receiver);
            }
        }

        receiver.character.Hp -= CaculateDmg(sender.character.AttackDmg, card.card.Dice, 1);//후에 1=>effectiveness 계산 함수로 변경
    }

    int CaculateDmg(int attackDmg, int dice, float effectiveness)
    {
        return (int)Mathf.Round((attackDmg + dice) * effectiveness);
    }

}
