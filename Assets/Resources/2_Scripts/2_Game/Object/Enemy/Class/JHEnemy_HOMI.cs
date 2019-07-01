using UnityEngine;
using System.Collections;

public class JHEnemy_HOMI : JHEnemy_Root
{
    public UISprite m_pSprite = null;
    float   m_fDownSpeed;
    bool    m_bLife;
    float m_fSpriteRotation;
    float   m_fRotationSpeed;
    JumpStruct m_stJump;
    E_CSDIRECT m_eDieDirect;
    float m_fDieMoveX;
    float m_fPlusMove = 30.0f;

    override public bool Enter()
    {
        if (base.Enter() == false) return false;
        m_fDownSpeed = 500.0f;
        m_bLife = false;
        m_fRotationSpeed = 350.0f;
        m_fDieMoveX = 450.0f;
        m_stJump.Reset();
        m_stJump.m_fDt = 0.0f;
        m_stJump.m_fJumpDt = 0.0f;
        m_stJump.m_fPower = 5.0f;
        m_stJump.m_fGravity = 7.9f;
        m_fSpriteRotation = 0.0f;
        m_eClass = E_ENEMY_CLASS.E_HOMI;

        m_fDamege = 10.0f;
        return true;
    }

    override public bool Create()
    {
        if (base.Create() == false) return false;
        m_bLife = true;
        float fSpawnX = (float)CSRandom.Rand(0, (int)((m_fWallX) * 2.0f));
        //   fSpawnX += Random.RandomRange(0.0f, 100.0f);
        fSpawnX -= m_fWallX;

        m_stPos = new Vector3(fSpawnX, m_fStartY, 0.0f);
        if(JHGame_MainLayer.I._Hero.pSrc._Position.x<=m_stPos.x)
        {
            m_fPlusMove *= -1.0f;
        }

        m_stJump.m_fGravity += Random.RandomRange(0.0f, 2.0f);

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

    override public void HitPlayer()
    {
        if (m_bLife == false) return;
        JHGame_MainLayer.I.m_pEffect_Mng.CreateEffect_PT_MagicPoof(m_stPos);
        m_bLife = false;
        m_eDieDirect = E_CSDIRECT.E_RIGHT;
        m_fRotationSpeed *= -1.0f;
        if(JHGame_MainLayer.I._Hero.pSrc._Position.x>=m_stPos.x)
        {
            m_eDieDirect = E_CSDIRECT.E_LEFT;
            m_fDieMoveX *= -1.0f;
            m_fRotationSpeed *= -1.0f;
        }
     
       

        CSSoundMng.I.Play("Hit" + CSRandom.Rand(1, 4).ToString());
    }


	// Update is called once per frame
	void Update () {
        if (base.Update() == false) return;


        if (m_bLife == true)
        {
            m_stPos.x += m_fPlusMove * Time.deltaTime;
            m_fSpriteRotation += m_fRotationSpeed * Time.deltaTime;
            m_stJump.m_fDt += Time.deltaTime;
            float move = (m_stJump.m_fGravity * ((m_stJump.m_fDt - m_stJump.m_fJumpDt)));
            move *= 100.0f;
            m_stPos.y -= move * Time.deltaTime;
        }
        else
        {

            m_stColor.a -= 1.0f * Time.deltaTime;
            if (m_stColor.a < 0.1f)
            {
                End();
            }
            m_pSprite.color = m_stColor;
        }
        if (m_stPos.y <= -700.0f)
        {
            m_stPos.y = -700.0f;
            End();
        }
        m_pSprite.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, m_fSpriteRotation));
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
        } while (m_bActive==true);
        yield return null;
    }

    IEnumerator Cor_Banjjack()
    {
        float fTime = 0.0f;
        bool bPing = false;
        do
        {
            fTime += Time.deltaTime;
          
            if (fTime > 0.08f)
            {
               
                if(bPing==false)
                {
                    m_stColor.a = 0.3f;
                }
                else
                {
                    m_stColor = Color.white;
                }
                bPing = !bPing;
                fTime = 0.0f;
            }
           
            m_pSprite.color = m_stColor;
            yield return null;
        } while (m_bActive == true);
        yield return null;
    }
}
