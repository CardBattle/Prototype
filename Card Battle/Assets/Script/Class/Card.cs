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
    private PropertyType property;
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

    // 내 카드인지 아닌지 체크하는 변수
    private bool myCard;

    // 내 방어카드인지 적 방어카드인지 체크
    public bool defenseCheck;
    // 카드위치를 바꾸기 위한 변수
    public bool cardSelect;
    // 카드 윈래 위치를 저장하는 클래스
    public PRS originPRS;
    public void Init()
    {

        buffs = GetComponents<Buff>().ToList();
        foreach (Buff buff in buffs)
        {
            if (buff != null)
                buff.Init();
        }
        info = new(id, property, type, _name, buffs, effVal, img);

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
