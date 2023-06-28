using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Weapon   
{  
    Hands, 
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
    public int Id { get => id; set => id = value; }

    Weapon weapon; // ���� Ÿ��

    string name; // �÷��̾� �г���
    public string Name { get => name; set => name = value; }
    
    int level; //�÷��̾��� ����
    public int Level { get => level; set => level = value; }

    public int deffenceDmg;

    public int DeffenceDmg { get => deffenceDmg; set => deffenceDmg = value; } //�÷��̾� ����


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
    
    List<DefalultCard> cards; //�÷��̾ �������ִ� �� Ȯ��

    public void CreateCharacter(int id, string name, int level, int hp, int attackDmg, List<DefalultCard> cards, Weapon weapon)
    {
        this.id = id;
        this.name = name;
        this.level = level;
        this.hp = hp;
        this.attackDmg = attackDmg;
        this.cards = cards;
        this.weapon = weapon;
    }
}
