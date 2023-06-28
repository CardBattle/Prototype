using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    SWORD,
    BOW,
    MAGIC,
    DEFAULT,
}
public class DefaultCard
{
    int id;
    public int Id { get => id; }

    CardType type;
    public CardType Type { get => type; }

    string name;
    public string Name { get => name; }

    public List<Buff> buffs;
    
    int effVal; //효과수치
    public int EffVal { get => effVal; }

    int level;
    public int Level { get => level; set => level = value; }

    int dice;
    public int Dice
    {
        get
        {
            switch (level)
            {
                case 1: dice = Random.Range(1, 7);
                    break;
                case 2: dice = Random.Range(2, 7);
                    break;
                case 3: dice = Random.Range(3, 7);
                    break;
                default: dice = 0;
                    break;
            }
            return dice;
        }
    }

    Sprite img; // 카드 이미지 필요합니다
    public Sprite Img { get => img; }

    delegate void Use(); //카드 사용, 효과 범위를 따로 변수로 두기 보다는 Use에 등록할 때 구현하는게 좋을 듯 합니다.
    
    public DefaultCard(int id, CardType type, string name, List<Buff> buffs, int effVal, Sprite img)
    {
        this.id = id;
        this.type = type;
        this.name = name;
        this.buffs = buffs;
        this.effVal = effVal;
        level = 1;
        this.img = img;
    }
}

