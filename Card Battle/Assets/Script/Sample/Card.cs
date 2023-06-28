using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Card : MonoBehaviour
{
    public DefaultCard card;

    int id = 0;
    public CardType type;
    public string name;
    public int effVal;
    public int level;
    public Sprite img;

    public List<Buff> buffs;

    public void Init()
    {
        card = new(id, type, name, buffs, effVal, img);
    }
    public void Test()
    {
        Debug.Log($"id: {card.Id}\ntype:{card.Type}\nname: {card.Name}\n" +
        $"level: {card.Level}\nimg: {card.Img}");
    }
}
