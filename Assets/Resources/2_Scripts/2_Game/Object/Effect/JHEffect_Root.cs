using UnityEngine;
using System.Collections;

public enum E_EFFECT_CLASS
{
    E_COWDANGER = 0,
    E_PT_SOFTSTAR,
    E_PT_MAGICPOOF,
    E_PT_HITRED,
    E_PT_HITMISC,

    E_MAX,
}

public class JHEffect_Root : JHObject_Root {
    protected E_EFFECT_CLASS m_eClass;
    public E_EFFECT_CLASS _Class { get { return m_eClass; } }

    virtual public bool Enter()
    {
        if (base.Enter() == false) return false;
        m_eClass = E_EFFECT_CLASS.E_MAX;
       

        return true;
    }

    virtual public bool Create()
    {
        if (base.Create() == false) return false;


        ResetTransform();

        return true;
    }

    virtual public bool End()
    {
        if (base.End() == false) return false;
        return true;
    }

    virtual public bool Destroy()
    {
        if (base.Destroy() == false) return false;

        return true;
    }


    virtual public bool Update()
    {
        if (base.Update() == false) return false;
        if (m_bActive == false) return false;

        //이곳에 일시정지나 이런거 예외처리하면됨
        return true;
    }
}
