using UnityEngine;
using System.Collections;

public class JHEnemy_Car : JHEnemy_Root
{
  
    public UISprite m_pSprite = null;
    bool m_bLife;
    JumpStruct m_stJump;
    E_CSDIRECT m_eDirect;
    float m_fSpeed;
    const float m_fGroundY = -462.0f;
    bool m_bStart = false;
    override public bool Enter()
    {
        if (base.Enter() == false) return false;

        m_bLife = false;
        m_eClass = E_ENEMY_CLASS.E_CAR;
        m_bStart = false;

        m_fDamege = 15.0f;
        return true;
    }

    override public bool Create()
    {
        if (base.Create() == false) return false;
        m_fSpeed = 280.0f;

        m_stPos.y = m_fGroundY;
        
        m_eDirect = E_CSDIRECT.E_RIGHT;
        m_stPos.x = 480.0f;
       
        if(CSRandom.Rand(0, 100)<50)
        {
            m_eDirect = E_CSDIRECT.E_LEFT;
            m_stPos.x = -480.0f;
            m_stScale.x = -1.0f;
            
        }
        else
        {
            m_fSpeed *= -1.0f;
        }
        JHGame_MainLayer.I.m_pEffect_Mng.CreateEffect_Danger(m_eDirect);

        StartCoroutine(Cor_CarStart());
        return true;
    }

    IEnumerator Cor_CarStart()
    {
        yield return new WaitForSeconds(0.8f);
        CSSoundMng.I.Play("Car_Sound");
        m_bLife = true;
        m_bStart = true;
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

    override public void HitPlayer()
    {
        if (m_bLife == false) return;
        m_bLife = false;
        JHGame_MainLayer.I.m_pEffect_Mng.CreateEffect_PT_MagicPoof(JHGame_MainLayer.I._Hero.pSrc._Position);
        CSSoundMng.I.Play("Hit" + CSRandom.Rand(1, 4).ToString());
        CSSoundMng.I.Play("car_crash");
    }


    // Update is called once per frame
    void Update()
    {
        if (base.Update() == false) return;
        if (m_bStart == false) return;
        if (m_bLife==true)
        {
            m_stPos.x += m_fSpeed * Time.deltaTime;
        }
        else
        {
            m_stPos.x -= (m_fSpeed/1.2f) * Time.deltaTime;
        }
        if (m_stPos.x > 480.0f || m_stPos.x < -480.0f)
        {
            End();
        }
        if (m_stPos.y <= -700.0f)
        {
            m_stPos.y = -700.0f;

            End();
        }
        ResetTransform();
    }

    
}
