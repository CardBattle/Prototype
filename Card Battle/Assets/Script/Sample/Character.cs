using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public DefaultCharacter character;

    public string name;
    public int level;
    public int defense;
    public int hp;
    public int attackDmg;

    public Weapon weapon;
    public Sprite img;

    int id = 0;

    List<Card> cards;

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
