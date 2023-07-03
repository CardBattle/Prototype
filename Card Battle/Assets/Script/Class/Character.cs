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

    public List<GameObject> cards; //ī�� �Ŵ������� ĳ���Ͱ� ������ ī�� �������� �����ؾ��ϱ� ������ public
    
    public void Init()
    {
        info = new(id, _name, level, hp, attackDmg, defense, cards, weapon, img);
    }

    public void Test()
    {
        Debug.Log($"id: {info.Id}\nname: {info.Name}\n" +
            $"level: {info.Level}\nhp: {info.Hp}\n" +
            $"attackDmg: {info.AttackDmg}\ndefense: {info.Defense}\n"+
            $"weapon: {info.Weapon}\nimg: {info.Img}");
    }
}
