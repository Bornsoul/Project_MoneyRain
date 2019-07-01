using UnityEngine;
using System.Collections;

public class JHEnemy_Rock : JHEnemy_Root {
    public UISprite m_pSprite = null;
    float   m_fDownSpeed;
    bool    m_bLife;
    float m_fSpriteRotation;
    float   m_fRotationSpeed;
    JumpStruct m_stJump;
    E_CSDIRECT m_eDieDirect;
    float m_fDieMoveX;

    override public bool Enter()
    {
        if (base.Enter() == false) return false;
        m_fDownSpeed = 500.0f;
        m_bLife = false;
       
        m_stJump.Reset();
        m_stJump.m_fDt = 0.0f;
        m_stJump.m_fJumpDt = 0.0f;
        m_stJump.m_fPower = 5.0f;
        m_stJump.m_fGravity = 8.9f;
        m_fSpriteRotation = 0.0f;
        m_eClass = E_ENEMY_CLASS.E_ROCK;
        m_stColor = Color.white;
        m_fDamege = 8.0f;
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
        m_pSprite.spriteName = "stone2";
        CSSoundMng.I.Play("Hit" + CSRandom.Rand(1, 4).ToString());
    }


	// Update is called once per frame
	void Update () {
        if (base.Update() == false) return;
        
        
        if(m_bLife==true)
        {
            m_fSpriteRotation += m_fRotationSpeed * Time.deltaTime;
            m_stJump.m_fDt += Time.deltaTime;
            float move = (m_stJump.m_fGravity * ((m_stJump.m_fDt - m_stJump.m_fJumpDt)));
            move *= 100.0f;
            m_stPos.y -= move * Time.deltaTime;
        }
        else
        {

            m_stColor.a -= 1.0f * Time.deltaTime;
            if(m_stColor.a<0.1f)
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
        
        ResetTransform();
	}

    
}
