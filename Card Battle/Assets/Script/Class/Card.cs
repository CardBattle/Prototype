using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Card : MonoBehaviour
{
    public DefaultCard card;

    public int id;
    public CardType type;
    public string name;
    public int effVal;
    public int level;
    public Sprite img;

    public List<Buff> buffs;


    public void Init()
    {
        buffs = GetComponents<Buff>().ToList();
        foreach (Buff buff in buffs)
        {
            if(buff != null)
                buff.Init();
        }
        card = new(id, type, name, buffs, effVal, img);
    }
    public void Test()
    {
        Debug.Log($"id: {card.Id}\ntype:{card.Type}\nname: {card.Name}\n" +
        $"level: {card.Level}\nimg: {card.Img}");
        if (buffs.Count > 0)
            foreach (Buff buff in buffs)
            {
                Debug.Log($"buff : {buff.buff.Name}");
            }
        else
            Debug.Log("No buffs");
    }
}
