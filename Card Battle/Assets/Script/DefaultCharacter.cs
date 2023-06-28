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
    //열거형을 이용해서 직업이랑 무기 구분
  
   
    int id; // 플레이어 직업 구분
    public int Id { get => id;  }

    Weapon weapon; // 무기 타입

    string name; // 플레이어 닉네임
    public string Name { get => name; set => name = value; }
    
    int level; //플레이어의 레벨
    public int Level { get => level; set => level = value; }

    public int defense;

    public int Defense { get => Defense; set => Defense = value; } //플레이어 방어력


    int hp; //캐릭터 체력 

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
    
    int attackDmg; // 캐릭터 공격력
    
    public int AttackDmg { get => attackDmg; set => value = attackDmg; }//공력력도 다시 확인
    
    List<Card> cards; //플레이어가 가지고있는 덱 확인

    public DefaultCharacter(int id, string name, int level, int hp, int attackDmg, int defense List<Card> cards, Weapon weapon)
    {
        this.id = id;
        this.name = name;
        this.level = level;
        this.hp = hp;
        this.attackDmg = attackDmg;
        this.defense = defense;
        this.cards = cards;
        this.weapon = weapon;
    }
}
