using UnityEngine;
using System.Collections;

public class JHEnemy_Cow : JHEnemy_Root
{
    public UISpriteAnimation m_pAni = null;
    public UISprite m_pSprite = null;
    bool m_bLife;
    JumpStruct m_stJump;
    E_CSDIRECT m_eDirect;
    float m_fSpeed;
    const float m_fGroundY = -475.0f;
    bool m_bStart = false;

    public GameObject m_pSize = null;
    float m_fRotaz = 0.0f;

    public ParticleSystem m_pParticle_Left = null;
    public ParticleSystem m_pParticle_Right = null;

    override public bool Enter()
    {
        if (base.Enter() == false) return false;
      

        m_bLife = false;
        m_stJump.Reset();
        m_stJump.m_fDt = 0.0f;
        m_stJump.m_fJumpDt = 0.0f;
        m_stJump.m_fPower = 12.0f;
        m_stJump.m_fGravity = 25.9f;
        m_eClass = E_ENEMY_CLASS.E_COW;
        m_bStart = false;

        m_fDamege = 8.0f;
        return true;
    }

    override public bool Create()
    {
        if (base.Create() == false) return false;
        m_fSpeed = 340.0f;

        m_stPos.y = m_fGroundY;
        
        m_eDirect = E_CSDIRECT.E_RIGHT;
        m_stPos.x = 460.0f;
      
        if(CSRandom.Rand(0, 100)<50)
        {
            m_eDirect = E_CSDIRECT.E_LEFT;
            m_stPos.x = -460.0f;
            m_stScale.x = -1.0f;
            m_pParticle_Left.gameObject.SetActive(true);
        }
        else
        {
            m_pParticle_Right.gameObject.SetActive(true);
            m_fSpeed *= -1.0f;
        }
        JHGame_MainLayer.I.m_pEffect_Mng.CreateEffect_Danger(m_eDirect);

        StartCoroutine(Cor_CowStart());
        return true;
    }

    IEnumerator Cor_CowStart()
    {
        yield return new WaitForSeconds(0.8f);
     //   CSSoundMng.I.Play("CSSoundMng.I.Play("CatDrop");");
        CSSoundMng.I.Play("TongMove");
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
        
            JHGame_MainLayer.I.m_pEffect_Mng.CreateEffect_PT_MagicPoof(JHGame_MainLayer.I._Hero.pSrc._Position);

            CSSoundMng.I.Stop("TongMove");
        m_bLife = false;
        m_pParticle_Left.gameObject.SetActive(false);
        m_pParticle_Right.gameObject.SetActive(false);
        m_pAni.enabled = false;
        StartCoroutine(Cor_Jump());
        m_pSprite.spriteName = "WineDestroy";
      //  m_pAni.namePrefix = "cowstun_";
      //  m_pAni.mIndex = 0;
      //  m_pSprite.spriteName = "cowstun_1";
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

         /*     m_pSize.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, m_fRotaz));
 
            if (m_fSpeed>=0.0f)
            {
                m_fRotaz -= 380.0f * Time.deltaTime;
               
            }
            else
            {
                m_fRotaz += 380.0f * Time.deltaTime;
                
            }*/
           
        }
        else
        {
            m_stPos.x -= (m_fSpeed/1.2f) * Time.deltaTime;
        }
        if (m_stPos.x > 460.0f || m_stPos.x < -460.0f)
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

    IEnumerator Cor_Jump()
    {
        m_stJump.m_fDt = 0.0f;
        m_stJump.m_fJumpDt = 0.0f;
        m_stJump.m_eState = E_JUMPSTATE.E_JUMP_UP;
        do
        {
            m_stJump.m_fDt += Time.deltaTime;

            if (m_stJump.m_eState == E_JUMPSTATE.E_JUMP_DOWN)
            {
                float move = (m_stJump.m_fGravity * ((m_stJump.m_fDt - m_stJump.m_fJumpDt)));
                move *= 100.0f;
                m_stPos.y -= move * Time.deltaTime;
            }
            else
            {
                float move = 0.0f;

                move = m_stJump.m_fPower - (m_stJump.m_fGravity * (m_stJump.m_fDt - m_stJump.m_fJumpDt));
                move *= 100.0f;

                m_stPos.y += move * Time.deltaTime;
                if (move < 0)
                {
                    m_stJump.m_fJumpDt = m_stJump.m_fDt;
                    m_stJump.m_eState = E_JUMPSTATE.E_JUMP_DOWN;
                }
            }
            yield return null;
        } while (m_bActive == true);
        yield return null;
    }

}
