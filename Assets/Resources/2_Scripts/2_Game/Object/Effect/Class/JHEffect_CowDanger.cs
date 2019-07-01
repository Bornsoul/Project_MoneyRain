using UnityEngine;
using System.Collections;

public class JHEffect_CowDanger : JHEffect_Root {
    public UISprite m_pCircle = null;
    public UISprite m_pCow = null;

    

   float m_fAmp = 5.0f;
   float m_fFrep = 15.0f;
   float m_fStartTime = 0.0f;
   float m_fTotalTime = 0.0f;
   Vector3 m_fOffset;

   float m_fLifeTime = 0.0f;

    override public bool Enter()
    {
        if (base.Enter() == false) return false;

        m_fStartTime = Time.time;
        CSSoundMng.I.Play("alert_ big");
        return true;
    }

    override public bool Create()
    {
        if (base.Create() == false) return false;
        
        StartCoroutine(Cor_Circle());
        ResetTransform();

        return true;
    }

    public void Create(E_CSDIRECT eDirect)
    {
        if (Create() == false) return;
        m_stPos = Vector3.zero;
        m_stPos.y = -160.0f;
        m_stPos.x = 285.0f;
       
        if(eDirect==E_CSDIRECT.E_LEFT)
        {
            m_stPos.x *= -1.0f;
            
        }
        else
        {
            m_pCow.transform.localScale = new Vector3(-0.8f, 0.8f, 1.0f);
        }
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

    IEnumerator Cor_Circle()
    {
        for (float i = 0.0f; i < 1.0f;i+=Time.deltaTime)
        {
            m_pCircle.transform.localScale = Vector3.Lerp(m_pCircle.transform.localScale, Vector3.zero, i / 3.0f);
            yield return null;
        }


            yield return null;
    }
	
	// Update is called once per frame
	void Update () {
        if (base.Update() == false) return;
        m_fLifeTime += Time.deltaTime;
        if (m_fLifeTime>1.0f)
        {
            End();
        }

        m_fTotalTime = Time.fixedTime - m_fStartTime;
        Vector3 pos = m_pCow.transform.localPosition;
        pos.x -= m_fOffset.x * Time.deltaTime;
        m_fOffset.x = Mathf.Sin(2 * 3.14159f * (m_fTotalTime * m_fFrep) + 0.0f) * m_fAmp * (50.0f - m_fTotalTime) / 50.0f;
        pos.x += m_fOffset.x;
        m_pCow.transform.localPosition = pos;
        ResetTransform();
	}
}
