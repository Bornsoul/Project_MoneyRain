using UnityEngine;
using System.Collections;

public class JHEffect_PT_HitMisc : JHEffect_Root
{
    public ParticleSystem m_pParticle = null;
    public UIPanel m_pPanel = null;
    float m_fAlpha;

    override public bool Enter()
    {
        if (base.Enter() == false) return false;


        return true;
    }

    override public bool Create()
    {
        if (base.Create() == false) return false;

      


       
        return true;
    }

    public void Create(Vector3 stPos)
    {
        if (Create() == false) return;
        m_stPos = stPos;
        m_stPos.y += 80.0f;
        m_fAlpha = 2.0f;
        m_pPanel.alpha = m_fAlpha;
        ResetTransform();
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



    // Update is called once per frame
    void Update()
    {
        if (base.Update() == false) return;
        m_stPos.y += 100.0f * Time.deltaTime;
        m_fAlpha-= 2.0f * Time.deltaTime;
        m_pPanel.alpha = m_fAlpha;
        if (m_pPanel.alpha < 0.0f)
        {
            End();
        }

        /*  if (m_pParticle.IsAlive() == false)
          {
              m_pParticle.gameObject.SetActive(false);
              End();
          }*/
        ResetTransform();
    }
}
