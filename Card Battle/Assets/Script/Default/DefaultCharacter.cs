using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Weapon   
{  
    Hand, 
    Sword, 
    Bow,
    Wand,  
    MagicSword,   
    MagicBow
}
public class DefaultCharacter
{
    //�������� �̿��ؼ� �����̶� ���� ����
  
   
    int id; // �÷��̾� ���� ����
    public int Id { get => id;  }

    Weapon weapon; // ���� Ÿ��
    public Weapon Weapon { get => weapon; set => weapon = value; }

    string name; // �÷��̾� �г���
    public string Name { get => name; set => name = value; }
    
    int level; //�÷��̾��� ����
    public int Level { get => level; set => level = value; }

    public int defense;

    public int Defense { get => Defense; set => Defense = value; } //�÷��̾� ����


    int hp; //ĳ���� ü�� 

    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value <= 0 ? 0 : value;
        }
    }
    
    int attackDmg; // ĳ���� ���ݷ�
    
    public int AttackDmg { get => attackDmg; set => value = attackDmg; }//���·µ� �ٽ� Ȯ��
    
    List<Card> cards; //�÷��̾ �������ִ� �� Ȯ��

    Sprite img;
    public Sprite Img { get => img; set => img = value; }

    public delegate void Use();

    public DefaultCharacter(int id, string name, int level, int hp, int attackDmg, int defense, List<Card> cards, Weapon weapon, Sprite img)
    {
        this.id = id;
        this.name = name;
        this.level = level;
        this.hp = hp;
        this.attackDmg = attackDmg;
        this.defense = defense;
        this.cards = cards;
        this.weapon = weapon;
        this.img = img;
    }
}
