using System.Collections.Generic;
using UnityEngine;

public enum PropertyType
{
    ATTACK,
    DEFENSE,
}

public class DefaultCard
{
    int id;
    public int Id { get => id; }

    PropertyType property;
    public PropertyType Property { get => property; }

    WeaponType type;
    public WeaponType Type { get => type; }

    string name;
    public string Name { get => name; }

    public List<Buff> buffs;

    int effVal; //ȿ����ġ
    public int EffVal { get => effVal; set => effVal = value; }

    int level;
    public int Level { get => level; set => level = value; }

    int dice;
    public int Dice { get => dice; set => dice = value; }
    public int RandomDice
    {
        get
        {
            switch (Level)
            {
                case 1:
                    Dice = Random.Range(1, 7);
                    break;
                case 2:
                    Dice = Random.Range(2, 7);
                    break;
                case 3:
                    Dice = Random.Range(3, 7);
                    break;
                default:
                    Dice = 0;
                    break;
            }
            return Dice;
        }
    }

    Sprite img; // ī�� �̹��� �ʿ��մϴ�
    public Sprite Img { get => img; }

    public delegate void Use(Character sender, Character receiver);
    public Use use;

    public DefaultCard(int id, PropertyType property, WeaponType type, string name, List<Buff> buffs, int effVal, Sprite img) //�Ŀ� ���� �߰�
    {
        this.id = id;
        this.property = property;
        this.type = type;
        this.name = name;
        this.buffs = buffs;
        this.effVal = effVal;
        level = 1;
        this.img = img;
    }
}

