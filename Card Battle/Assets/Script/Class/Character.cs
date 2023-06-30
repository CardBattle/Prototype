using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public DefaultCharacter character;

    //������ ����� ������ �Ұ��� �ƴϸ� �ٸ� Ŭ������ ������� Ȯ��

    public string name;
    public int id; // ĳ�������� ��������
    public int level;
    public int defense;
    public int hp;
    public int attackDmg;

    public WeaponType weapon;
    public Sprite img; // ĳ���� �̹���

    

    public List<GameObject> cards; //�÷��̾� �� Ȯ��
    
    public void Init()
    {
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
