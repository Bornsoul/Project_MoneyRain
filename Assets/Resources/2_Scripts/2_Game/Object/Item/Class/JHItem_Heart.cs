using UnityEngine;
using System.Collections;

public class JHItem_Heart : JHItem_Root
{
    float m_fRecoveryHp_Persent = 0.0f;

    override public bool Enter()
    {
        if (base.Enter() == false) return false;
        m_fRecoveryHp_Persent = 30.0f;
        m_fDownSpeed = 300.0f;
       
        return true;
    }

    override public bool Create()
    {
        if (base.Create() == false) return false;

        
        ResetTransform();
        return true;
    }

    override public bool End()
    {
        if (base.End() == false) return false;

        return true;
    }

    override public bool Destroy()
    {
        if (base.Destroy() == false) return false;

        return true;
    }

    override public void Eated()
    {
        JHGame_MainLayer.I.m_pEffect_Mng.CreateEffect_PT_HitMisc(m_stPos);
        base.Eated();
        float fRecovery = (JHGame_MainLayer.I._Hero.pSrc._MaxHp * m_fRecoveryHp_Persent)/100.0f;
     //   Debug.Log("Recovery + " + fRecovery);
        JHGame_MainLayer.I._Hero.pSrc.Recovery_Hp(fRecovery);
        JHGame_MainLayer.I.m_pMoney_Mng.SetFever();
       
    }

	// Update is called once per frame
	void Update () {
        if (base.Update() == false) return;


	}
}
