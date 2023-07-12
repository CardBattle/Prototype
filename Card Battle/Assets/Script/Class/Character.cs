using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public DefaultCharacter info;

    [SerializeField]
    private string _name;
    [SerializeField]
    private int id;
    [SerializeField]
    private int level;
    [SerializeField]
    private int defense;
    [SerializeField]
    private int hp;
    [SerializeField]
    private int attackDmg;
    [SerializeField]
    private WeaponType weapon;
    [SerializeField]
    private Sprite img;
    
    public List<Buff> buffs;

    public List<GameObject> cards; //카드 매니저에서 캐릭터가 소유한 카드 프리팹을 접근해야하기 때문에 public

    public void Init()
    {
        buffs = new List<Buff>();
        info = new(id, _name, level, hp, attackDmg, defense, cards, buffs, weapon, img);
    }

}
