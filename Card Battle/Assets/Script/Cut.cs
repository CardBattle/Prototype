using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cut : MonoBehaviour
{
    // �ν����� â���� Cut ī���� ������ �����ϱ� ���� ����
    public int id;
    public CardType type; //ī�� ����
    public string cardName;
    public List<DefaultBuff> buffs;//28�Ͽ� �ϱ�
    public int effVal;
    public Sprite img;
    DefaultCard cutCard;

    void Start()
    {
        cutCard=new DefaultCard(id, CardType.SWORD, cardName, buffs, effVal, img);
    }
    void Use()//ī�带 ���������� ����� �޼ҵ�
    {
        cutCard.Use();        // ��������ƮUSE�� ���� �޼ҵ带 ��� ���� ���� 
    }

    //��ư���� ���� Ŭ���ϸ� use�� ����� ��������Ʈ 
    //������ 6���� �̷��� ������ �� �� ������ �����?
}