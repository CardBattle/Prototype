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
    private WeaponType type;
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



    public PRS originPRS;
    public void Init()
    {
        buffs = GetComponents<Buff>().ToList();
        foreach (Buff buff in buffs)
        {
            if (buff != null)
                buff.Init();
        }
        info = new(id, type, _name, buffs, effVal, img);

        GetComponent<CardUse>().Init();
    }

    private void OnMouseOver()
    {
        BattleManager.Bm.CardMouseOver(this);
    }
    private void OnMouseExit()
    {
        BattleManager.Bm.CardMouseExit(this);
    }
    private void OnMouseUp()
    {

    }

    private void OnMouseDown()
    {

    }

    public void MoveTransform(PRS prs, bool Lerp, int Speed = 10)
    {
        if (Lerp)
        {
            transform.position = Vector3.Lerp(transform.position, prs.pos, Speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, prs.rot, Speed * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, prs.scale, Speed * Time.deltaTime);
        }
        if (!Lerp)
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
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
