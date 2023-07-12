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
        if (card.info.buffs.Count > 0)
        {
            bool isExist = false;
            foreach (var buff in card.info.buffs)
            {
                foreach (var exist in receiver.info.buffs)
                {
                    if (exist.info.Id == buff.info.Id)
                    {
                        isExist = true;
                        exist.info.CurrentTurn += buff.info.Turns;
                        break;
                    }
                }
                if (!isExist)
                    receiver.info.buffs.Add(buff);
            }
        }
    }

    protected int CalculateDmg(int attackDmg, int dice, int effVal, float effectiveness)
    {
        return (int)((attackDmg + dice + effVal) * effectiveness);
    }

    /// <summary>
    /// type1���� ���Ǵ� ī���� Ÿ����, type2���� ���ݹ޴� ĳ������ ���� Ÿ���� �ִ´�.
    /// </summary>
    /// <param name="type1">����� ī���� Ÿ��</param>
    /// <param name="type2">���ݹ޴� ĳ������ Ÿ��</param>
    /// <returns>���� ���� float�� ����</returns>
    protected float CalculateEffect(WeaponType type1, WeaponType type2)
    {
        if (type2 == WeaponType.BOSS) return 0.5f;

        if (type1 == WeaponType.DEFAULT || type2 == WeaponType.DEFAULT) return 1f;
        if (type1 == WeaponType.SWORD)
        {
            if (type2 == WeaponType.WAND) return 0.5f;
            if (type2 == WeaponType.BOW) return 2f;
        }
        if (type1 == WeaponType.BOW)
        {
            if (type2 == WeaponType.SWORD) return 0.5f;
            if (type2 == WeaponType.WAND) return 2f;
        }
        if (type1 == WeaponType.WAND)
        {
            if (type2 == WeaponType.BOW) return 0.5f;
            if (type2 == WeaponType.SWORD) return 2f;
        }

        return 1f;
    }

}
