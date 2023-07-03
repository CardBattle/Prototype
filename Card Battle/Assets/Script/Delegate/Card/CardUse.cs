using UnityEngine;

public class CardUse : MonoBehaviour
{
    public Card card;
    public void Init()
    {
        card = GetComponent<Card>();
        card.info.use = Use;
    }

    public virtual void Use(Character sender, Character receiver)
    {
        //버프 - 프로토타입 구현 X 연동만 해둠.
        if(card.info.buffs.Count > 0 )
        {
            foreach(var buff in card.info.buffs)
            {
                buff.info.use(sender, receiver);
            }
        }

        receiver.info.Hp -= CalculateDmg(sender.info.AttackDmg, card.info.Dice, 1);//후에 1=>effectiveness 계산 함수로 변경
    }

    int CalculateDmg(int attackDmg, int dice, float effectiveness)
    {
        return (int)Mathf.Round((attackDmg + dice) * effectiveness);
    }

}
