using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public DefaultBuff buff;

    public int id;
    public BuffType type;
    public string name;
    public int turns;

    public BuffUse buffUse;
    
    public void Init()
    {
        buff = new(id, type, name, turns);

        buffUse.Init(this);
    }
}
