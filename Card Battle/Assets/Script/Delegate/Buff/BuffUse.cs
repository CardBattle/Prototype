using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BuffUse : MonoBehaviour
{
    Buff buff;
    public void Init(Buff buff)
    {
        this.buff = buff;
        this.buff.info.use = Use;
    }

    public virtual void Use(Character sender, Character receiver)
    {
        receiver.info.buffs.Add(buff);
    }

    //자기 자신에게 주는 버프, 디버프인경우
    protected void SelfUse(Character self)
    {
        self.info.buffs.Add(buff);
    }
}
