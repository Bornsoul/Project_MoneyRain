using UnityEngine;
using System.Collections;

public class JHMoney_PocketBronze : JHMoney_Root
{

    override public bool Enter()
    {
        if (base.Enter() == false) return false;
        m_nGold = 450;

        m_eStyle = E_MONEY_STYLE.E_COIN;
        m_eClass = E_MONEY_CLASS.E_POCKET_BRONZE;

        m_stJump.Reset();
        m_stJump.m_fGravity = 9.2f;
       
        return true;
    }

    override public bool Create()
    {
        if (base.Create() == false) return false;
        m_stJump.m_fDt = 0.0f;
        m_stJump.m_fJumpDt = 0.0f;
        m_stJump.m_eState = E_JUMPSTATE.E_JUMP_DOWN;
      
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
        base.Eated();
        CSSoundMng.I.Play("Money_Sound");
      //  End();
    }
	
	// Update is called once per frame
	void Update () {
        if (base.Update() == false) return;
        
        m_stJump.m_fDt += Time.deltaTime;

        
            //Debug.Log("aa");
            float move = (m_stJump.m_fGravity * ((m_stJump.m_fDt - m_stJump.m_fJumpDt)));
            move *= 100.0f;
            m_stPos.y -= move * Time.deltaTime;
            if (m_stPos.y <= -700.0f)
            {
                m_stPos.y = -700.0f;
                End();
            }
        
        ResetTransform();
	}
}
