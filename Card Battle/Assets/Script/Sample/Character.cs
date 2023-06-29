using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public DefaultCharacter character;

    //마왕은 여기다 포함을 할건지 아니면 다른 클래스를 만들건지 확인

    public string name;
    public int id; // 캐릭터인지 마왕인지
    public int level;
    public int defense;
    public int hp;
    public int attackDmg;

    public WeaponType weapon;
    public Sprite img; // 캐릭터 이미지

    

    List<Card> cards; //플레이어 덱 확인

    public void Start()
    {
        Init();
        Test();//작동되는 확인
    }
    //Init은 나중에 Loader에서 한번에

    
    public void Init()
    {
        cards = new List<Card>();
        character = new(id, name, level, hp, attackDmg, defense, cards, weapon, img);
    }

    public void Test()
    {
        Debug.Log($"id: {character.Id}\nname: {character.Name}\n" +
            $"level: {character.Level}\nhp: {character.Hp}\n" +
            $"attackDmg: {character.AttackDmg}\ndefense: {character.Defense}\n"+
            $"weapon: {character.Weapon}\nimg: {character.Img}");
    }
}
