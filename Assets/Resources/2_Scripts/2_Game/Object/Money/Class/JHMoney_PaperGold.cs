using UnityEngine;
using System.Collections;

public class JHMoney_PaperGold : JHMoney_Root {

    override public bool Enter()
    {
        if (base.Enter() == false) return false;
        m_nGold = 300;

        m_eStyle = E_MONEY_STYLE.E_PAPER;
        m_eClass = E_MONEY_CLASS.E_PAPER_GOLD;

        //m_fPaper_Amp = 3200.0f + Random.RandomRange(0.0f, 1000.0f);
        m_fPaper_Amp = Random.RandomRange(2.0f, 15.0f);
        //m_fPaper_Frep = Random.RandomRange(1.0f, 1.4f);
        m_fPaper_Frep = Random.RandomRange(1.0f, 1.25f);
        m_fPaper_StartTime = Time.time;
        m_fDownSpeed = 300.0f;

        
        return true;
    }

    override public bool Create()
    {
        if (base.Create() == false) return false;
       
      
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
        CSSoundMng.I.Play("Coin_3");
        //End();
    }
	
	// Update is called once per frame
	void Update () {
        if (base.Update() == false) return;


        m_fPaper_TotalTime = Time.fixedTime - m_fPaper_StartTime;
        Vector3 pos = m_stPos;
        pos.x -= m_fOffset.x * Time.deltaTime;
        m_fOffset.x = Mathf.Sin(2 * 3.14159f * (m_fPaper_TotalTime * m_fPaper_Frep) + 0.0f) * m_fPaper_Amp * (50.0f - m_fPaper_TotalTime) / 50.0f;
        pos.x += m_fOffset.x;
        m_stPos = pos;

        m_stPos.y -= m_fDownSpeed * Time.deltaTime;
        if (m_stPos.y <= -700.0f)
        {
            m_stPos.y = -700.0f;
            End();
        }
        ResetTransform();
	}
}
