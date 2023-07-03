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

        receiver.info.Hp -= CalculateDmg(sender.info.AttackDmg, card.info.Dice, card.info.EffVal, 
            CalculateEffect(card.info.Type, receiver.info.Weapon));

        Debug.Log(receiver.info.Hp);
    }

    int CalculateDmg(int attackDmg, int dice, int effVal, float effectiveness)
    {
        return (int)Mathf.Round((attackDmg + dice + effVal) * effectiveness);
    }

    /// <summary>
    /// type1���� ���Ǵ� ī���� Ÿ����, type2���� ���ݹ޴� ĳ������ ���� Ÿ���� �ִ´�.
    /// </summary>
    /// <param name="type1">����� ī���� Ÿ��</param>
    /// <param name="type2">���ݹ޴� ĳ������ Ÿ��</param>
    /// <returns>���� ���� float�� ����</returns>
    float CalculateEffect(WeaponType type1, WeaponType type2)
    {
        if (type2 == WeaponType.BOSS) return 0.5f;

        if (type1 == WeaponType.DEFAULT || type2 == WeaponType.DEFAULT) return 1f;
        if (type1 == WeaponType.SWORD)
        {
            if (type2 == WeaponType.WAND) return 0.5f;
            if (type2 == WeaponType.BOW) return 2f;
        }
        if(type1 == WeaponType.BOW)
        {
            if (type2 == WeaponType.SWORD) return 0.5f;
            if (type2 == WeaponType.WAND) return 2f;
        }
        if(type1 == WeaponType.WAND)
        {
            if (type2 == WeaponType.BOW) return 0.5f;
            if (type2 == WeaponType.SWORD) return 2f;
        }

        return 0f;
    }

}
