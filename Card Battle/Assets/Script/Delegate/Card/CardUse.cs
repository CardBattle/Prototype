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
        //���� - ������Ÿ�� ���� X ������ �ص�.
        if(card.info.buffs.Count > 0 )
        {
            foreach(var buff in card.info.buffs)
            {
                buff.info.use(sender, receiver);
            }
        }

        receiver.info.Hp -= CalculateDmg(sender.info.AttackDmg, card.info.Dice, 1);//�Ŀ� 1=>effectiveness ��� �Լ��� ����
    }

    int CalculateDmg(int attackDmg, int dice, float effectiveness)
    {
        return (int)Mathf.Round((attackDmg + dice) * effectiveness);
    }

}
