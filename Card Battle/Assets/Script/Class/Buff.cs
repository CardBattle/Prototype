using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public DefaultBuff info;

    [SerializeField]
    private int id;
    [SerializeField]
    private BuffType type;
    [SerializeField]
    private string _name;
    [SerializeField]
    private int turns;
    [SerializeField]
    private BuffUse buffUse;
    
    public void Init()
    {
        info = new(id, type, _name, turns);

        buffUse.Init(this);
    }
}
