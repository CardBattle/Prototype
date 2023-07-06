using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;
using TMPro;
using DG.Tweening;

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
    private Sprite frontCard;
    [SerializeField]
    private Sprite backCard;
    [SerializeField]
    private List<Buff> buffs;

    [SerializeField]
    private SpriteRenderer cardimg;
    [SerializeField]
    private TextMeshPro _Name;
    [SerializeField]
    private TextMeshPro effect;

    public bool myCard;

    public bool cardSelect;
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
        if (myCard && BattleManager.Bm.state != BattleManager.State.CardDecision)
            BattleManager.Bm.CardMouseOver(this);
    }
    private void OnMouseExit()
    {
        if (myCard && BattleManager.Bm.state != BattleManager.State.CardDecision)
            BattleManager.Bm.CardMouseExit(this);
    }
    private void OnMouseDown()
    {
        if (myCard)
        {
            if (cardSelect)
            {
                BattleManager.Bm.CardSelectDown(this);
            }
            else
            {
                BattleManager.Bm.CardMoustDown(this);
            }
        }
    }

    public void CardFront(bool myCard)
    {
        this.myCard = myCard;

        if (myCard)
        {
            cardimg.sprite = img;
            _Name.text = _name;
            effect.text = effVal.ToString();
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = backCard;
            _Name.text = " ";
            effect.text = " ";
        }
    }

    public void EnemyCardFront()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = frontCard;
        cardimg.sprite = img;
        _Name.text = _name;
        effect.text = effVal.ToString();
    }
    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }

    /* public void MoveTransform(PRS prs, bool Lerp, int Speed = 10)
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
     }*/


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
