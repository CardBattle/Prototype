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
        //���� - ������Ÿ�� ���� X ������ �ص�.
        if(card.card.buffs.Count > 0 )
        {
            foreach(var buff in card.card.buffs)
            {
                buff.buff.use(sender, receiver);
            }
        }

        receiver.character.Hp -= CaculateDmg(sender.character.AttackDmg, card.card.Dice, 1);//�Ŀ� 1=>effectiveness ��� �Լ��� ����
    }

    int CaculateDmg(int attackDmg, int dice, float effectiveness)
    {
        return (int)Mathf.Round((attackDmg + dice) * effectiveness);
    }

}
