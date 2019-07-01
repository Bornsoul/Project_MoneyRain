using UnityEngine;
using System.Collections;

public enum E_ENEMY_CLASS
{
    E_BALL = 0,
    E_COW,
    E_ROCK,
    E_CAR,
    E_HOMI,

    E_MAX,
}

public class JHEnemy_Root : JHObject_Root {
    protected E_ENEMY_CLASS m_eClass;
    public E_ENEMY_CLASS _Class { get { return m_eClass; } }

    protected float m_fDamege;
    public float _Damege { get { return m_fDamege; } }

    protected float m_fWallX = 335.0f;            //! 최대 벽위치
    protected float m_fStartY = 700.0f;           //! 소환됬을때 떨어질 Y좌표

    virtual public bool Enter()
    {
        if (base.Enter() == false) return false;
        m_eClass = E_ENEMY_CLASS.E_MAX;
        m_fDamege = 1;

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

    virtual public void HitPlayer()
    {

    }


    virtual public bool Update()
    {
        if (base.Update() == false) return false;
        if (m_bActive == false) return false;

        //이곳에 일시정지나 이런거 예외처리하면됨
        return true;
    }
}
