using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffUse : MonoBehaviour
{
    public void Init(Buff buff)
    {
        buff.buff.use = Use;
    }

    public virtual void Use()
    {

    }
}
