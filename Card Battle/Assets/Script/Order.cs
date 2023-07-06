using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] Renderer[] backOrders;
    [SerializeField] Renderer[] frontOrders;
    [SerializeField] string sortingLayerName;

    int order;
    public void SetOriginOrder(int order)
    {
        this.order = order;
        SettingOrder(order);
    }

    public void DragOrder(bool drag)
    {
        int order = this.order;

        if (drag)
        {
            SettingOrder(100);
        }
        if (!drag)
        {
            SettingOrder(order);
        }
    }

    public void SettingOrder(int order)
    {
        int mulOrder = order * 10;

        foreach (var renderer in backOrders)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder;
        }

        foreach (var renderer in frontOrders)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder + 1;
        }
    }

}
