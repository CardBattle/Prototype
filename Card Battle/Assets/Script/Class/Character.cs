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

    public List<GameObject> cards; //ī�� �Ŵ������� ĳ���Ͱ� ������ ī�� �������� �����ؾ��ϱ� ������ public

    public void Init()
    {
        buffs = new List<Buff>();
        info = new(id, _name, level, hp, attackDmg, defense, cards, buffs, weapon, img);
    }

}
