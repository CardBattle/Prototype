using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Card : MonoBehaviour
{
    public DefaultCard info;

    [SerializeField]
    private int id;
    [SerializeField]
    private CardType type;
    [SerializeField]
    private string _name;
    [SerializeField]
    private int effVal;
    [SerializeField]
    private int level;
    [SerializeField]
    private Sprite img;
    [SerializeField]
    private List<Buff> buffs;

    public void Init()
    {
        buffs = GetComponents<Buff>().ToList();
        foreach (Buff buff in buffs)
        {
            if(buff != null)
                buff.Init();
        }
        info = new(id, type, _name, buffs, effVal, img);

        GetComponent<CardUse>().Init();
    }

    public void Test()
    {
        Debug.Log($"id: {info.Id}\ntype:{info.Type}\nname: {info.Name}\n" +
        $"level: {info.Level}\nimg: {info.Img}");
        if (buffs.Count > 0)
            foreach (Buff buff in buffs)
            {
                Debug.Log($"buff : {buff.info.Name}");
            }
        else
            Debug.Log("No buffs");
    }
}
