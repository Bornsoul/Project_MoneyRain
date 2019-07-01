using UnityEngine;
using System.Collections;

public enum E_HEROSTATE
{
    E_NONE = 0,
    E_MOVE,
    E_JUMP,
    E_STURN,
    E_START,
    E_DIE,
    E_MAGNET,

    E_MAX,
}


public class JHHero : JHObject_Root {
    public UISprite m_pSprite = null;
    public UISpriteAnimation m_pAni = null;
    public JHScoreEffect_Mng m_pScoreEffect = null;

    const float m_fWallX = 315.0f;
    const float m_fGroundY = -435.0f;
    public float _GoundY { get { return m_fGroundY; } }

    float m_fMaxHp = 1.0f;
    float m_fCurrHp = 1.0f;
    public float _MaxHp { get { return m_fMaxHp; } set { m_fMaxHp = value; } }
    public float _CurrHp { get { return m_fCurrHp; } set { m_fCurrHp = value; } }

    float m_fMaxPower = 100.0f;
    float m_fCurrPower = 1.0f;
    float m_fPlusPower = 1.0f;
    public float _MaxPower { get { return m_fMaxPower; } set { m_fMaxPower = value; } }
    public float _CurrPower { get { return m_fCurrPower; } set { m_fCurrPower = value; } }

    float m_fSpeed = 200.0f;
    JumpStruct m_stJump;
    E_HEROSTATE m_eState;
    public E_HEROSTATE _State { get { return m_eState; } }
    E_CSDIRECT m_eDirect;
    bool m_bMove = false;

    bool m_bPerfectHit = false;

    override public bool Enter()
    {
        if (base.Enter() == false) return false;
        m_pScoreEffect.Enter();

        m_pSprite.color = m_stColor;


        return true;
    }

    override public bool Create()
    {
        if (base.Create() == false) return false;
        m_eState = E_HEROSTATE.E_MAX;
        m_eDirect = E_CSDIRECT.E_LEFT;

        m_stPos = new Vector3(0.0f, m_fGroundY, 0.0f);

        m_stJump.m_fDt = 0.0f;
        m_stJump.m_fJumpDt = 0.0f;
        m_stJump.m_eState = E_JUMPSTATE.E_JUMP_NONE;
        m_stJump.m_fGravity = 90.8f;
          
        m_bMove = false;

        m_fSpeed = JHGameData_Mng.I.m_pData_Stat._Basic_Speed;
        m_stJump.m_fPower = JHGameData_Mng.I.m_pData_Stat._Basic_JumpPower;

        m_fSpeed += JHGameData_Mng.I.m_pData_Stat._Stat_Speed * JHGameData_Mng.I.m_pData_Stat._Factor_Speed;
        m_stJump.m_fPower += JHGameData_Mng.I.m_pData_Stat._Stat_JumpPower * JHGameData_Mng.I.m_pData_Stat._Factor_JumpPower;


        m_fMaxHp = JHGameData_Mng.I.m_pData_Stat._Basic_Hp;
        
        m_fMaxHp += JHGameData_Mng.I.m_pData_Stat._Stat_Hp * JHGameData_Mng.I.m_pData_Stat._Factor_Hp;
       
        m_fCurrHp = m_fMaxHp;


        m_fMaxPower = 100.0f;
        m_fCurrPower = 0.0f;
        
        StartCoroutine(Cor_State_Start());
        ResetTransform();
        return true;
    }

    override public bool End()
    {
        if (base.End() == false) return false;


        return true;
    }

    public override bool Destroy()
    {
        if (base.Destroy() == false) return false;
        m_pScoreEffect.Destroy();

        return true;

    }

	// Update is called once per frame
	void Update () {
        if (JHGameData_Mng.I._TutorialState == true) ResetTransform();
        if (base.Update() == false) return;

        if (JHGameData_Mng.I._TutorialState == false) m_fCurrHp -= Time.deltaTime;
        if (m_fCurrHp<=0.0f)
        {
            JHGameData_Mng.I.SetGameOver();
            ChangeState(E_HEROSTATE.E_DIE, true);
            m_fCurrHp = 0.0f;
        }

       /* if(Input.GetKeyDown(KeyCode.K))
        {
            JHGame_MainLayer.I.m_pEffect_Mng.CreateEffect_PT_VortexAir(m_stPos);
            JHGame_MainLayer.I.m_pMoney_Mng.AllMagnet();
        }*/

        TouchMoveUpdate();
        ResetTransform();
	}

    public void TutorialMove()
    {
        StartCoroutine(Cor_TutorialMove());
    }

    IEnumerator Cor_TutorialMove()
    {
        m_eDirect = E_CSDIRECT.E_RIGHT;
        m_pSprite.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        if(m_stPos.x>0.0f)
        {
            m_eDirect = E_CSDIRECT.E_LEFT;
            m_pSprite.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        while(true)
        {
            if (m_eDirect == E_CSDIRECT.E_LEFT)
            {
                m_stPos.x -= m_fSpeed * Time.deltaTime;
                if (m_stPos.x <= -m_fWallX) m_stPos.x = -m_fWallX;
            }
            else if (m_eDirect == E_CSDIRECT.E_RIGHT)
            {
                m_stPos.x += m_fSpeed * Time.deltaTime;
                if (m_stPos.x >= m_fWallX) m_stPos.x = m_fWallX;
            }
            if(Mathf.Abs(m_stPos.x-0.0f)<30.0f)
            {
                ChangeState(E_HEROSTATE.E_NONE);
                break;
            }
            yield return null;
        }

        yield return null;
    }


    IEnumerator Cor_State_None()
    {
        if (m_eState == E_HEROSTATE.E_NONE) yield break;
        if (m_eState == E_HEROSTATE.E_JUMP) yield break;
        //if (m_eState == E_HEROSTATE.E_STURN) yield break;
        m_eState = E_HEROSTATE.E_NONE;
        m_pAni.namePrefix = "normal_";
        m_pAni.mIndex = 0;
        m_pAni.loop = true;
        m_bMove = false;

        do
        {
           
           // TouchMoveUpdate();
            yield return null;
        } while (m_eState == E_HEROSTATE.E_NONE);

        yield return null;
    }

    IEnumerator Cor_State_Move()
    {
        if (m_eState == E_HEROSTATE.E_MOVE) yield break;
        if (m_eState == E_HEROSTATE.E_JUMP) yield break;
        if (m_eState == E_HEROSTATE.E_STURN) yield break;
        m_eState = E_HEROSTATE.E_MOVE;
        m_pAni.namePrefix = "run_";
        m_pAni.mIndex = 0;
        m_pAni.loop = true;

        do
        {
        
           
            yield return null;
        } while (m_eState == E_HEROSTATE.E_MOVE);

        yield return null;
    }

    IEnumerator Cor_State_Jump()
    {
        if (m_eState == E_HEROSTATE.E_JUMP) yield break;
        if (m_eState == E_HEROSTATE.E_STURN) yield break;
       
        CSSoundMng.I.Play("Hero_Jump");
        m_eState = E_HEROSTATE.E_JUMP;
        m_pAni.namePrefix = "jump_";
        m_pAni.mIndex = 0;
        m_pAni.loop = false;
        m_stJump.m_fDt = 0.0f;
        m_stJump.m_fJumpDt = 0.0f;
        m_stJump.m_eState = E_JUMPSTATE.E_JUMP_UP;
        JHUserData_Mng.I.m_pUserData.PlusData("_JumpCount");
       
        
        do
        {
          //  TouchMoveUpdate();
            if (JHGameData_Mng.I._TutorialState == false && base.Update() == false)
            {
                yield return null;
                continue;
            }
            m_stJump.m_fDt += Time.deltaTime;

            if (m_stJump.m_eState==E_JUMPSTATE.E_JUMP_DOWN)
            {
                float move = (m_stJump.m_fGravity * ((m_stJump.m_fDt - m_stJump.m_fJumpDt)));
                move *= 100.0f;
                m_stPos.y -= move * Time.deltaTime;
                if (m_stPos.y <= m_fGroundY)
                {
                    m_stJump.m_eState = E_JUMPSTATE.E_JUMP_NONE;
                    m_stPos.y = m_fGroundY;
                    m_eState = E_HEROSTATE.E_MAX;
                    m_stJump.m_fDt = 0.0f;
                    m_stJump.m_fJumpDt = 0.0f;
                    ChangeState(E_HEROSTATE.E_MOVE);
                    break;
                }
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
        } while (m_eState == E_HEROSTATE.E_JUMP);
        yield return null;
    }

    IEnumerator Cor_State_Sturn()
    {
        m_bPerfectHit = true;
        m_pAni.namePrefix = "stun2_";
        m_pAni.mIndex = 0;
        m_pAni.loop = false;
        m_stJump.m_fDt = 0.0f;
        m_stJump.m_fJumpDt = 0.0f;
        m_eState = E_HEROSTATE.E_STURN;
        m_stJump.m_eState = E_JUMPSTATE.E_JUMP_NONE;
        float fTime = 0.0f;
        do
        {
            if (JHGameData_Mng.I._TutorialState==false && base.Update() == false)
            {
                yield return null;
                continue;
            }
            m_stJump.m_fDt += Time.deltaTime;
            fTime += Time.deltaTime;
            if (fTime > 1.0f)
            {
                ChangeState(E_HEROSTATE.E_NONE);
                JHUserData_Mng.I.m_pUserData.SetData("_Hidden_HitBallCombo", 0);
                break;
            }
            
            if (m_stPos.y <= m_fGroundY)
            {
                m_stPos.y = m_fGroundY;
                m_stJump.m_fDt = 0.0f;
                m_stJump.m_fJumpDt = 0.0f;
            }
            else
            {
                float move = (m_stJump.m_fGravity * ((m_stJump.m_fDt - m_stJump.m_fJumpDt)));
                move *= 100.0f;
                m_stPos.y -= move * Time.deltaTime;
            }
            yield return null;
        } while (m_eState == E_HEROSTATE.E_STURN);
      
        yield return null;
    }

    IEnumerator Cor_State_Start()
    {
        m_eState = E_HEROSTATE.E_START;
        m_pAni.namePrefix = "aa";
        m_pAni.mIndex = 0;
        m_pAni.loop = true;
        m_pSprite.spriteName = "stun2_2";
        m_stPos.y = 730.0f;
        m_stJump.m_fDt = 0.0f;
        m_stJump.m_fJumpDt = 0.0f;
        bool bSt = false;
        yield return new WaitForSeconds(0.1f);
        JHGame_MainLayer.I.m_pEffect_Mng.CreateEffect_Shadow();
        CSSoundMng.I.Play("CatDrop");
        float fTime = 0.0f;
        do
        {
           
            fTime += Time.deltaTime;
            if (fTime < 0.8f)
            {
                yield return null;
                continue;
            }
            m_stJump.m_fDt += Time.deltaTime;
            if (fTime > 2.3f)
            {
                ChangeState(E_HEROSTATE.E_NONE);
                if(JHGame_UILayer.I.m_pPicture._Process==false)
                {
                    JHGameData_Mng.I.SetCycle(false);

                }

                CSSoundMng.I.Play("bgm01", true);
                break;
            }

            if (m_stPos.y <= m_fGroundY)
            {
                if(bSt==false)
                {
                    JHGame_MainLayer.I.m_pShake.Reset();
                    JHGame_MainLayer.I.m_pShake.StartUpDownShake(0.8f);
                    JHGame_MainLayer.I.m_pBG.Off();
                    m_pAni.namePrefix = "stun2_";
                    m_pAni.mIndex = 0;
                    m_pAni.loop = false;
                    bSt = true;

                    JHGame_UILayer.I.m_pHpBar.Enter();
                    CSSoundMng.I.Play("Uck");
                    CSSoundMng.I.Play("Hit3");
                }
                m_stPos.y = m_fGroundY;
                m_stJump.m_fDt = 0.0f;
                m_stJump.m_fJumpDt = 0.0f;
            }
            else
            {
                float move = (m_stJump.m_fGravity * ((m_stJump.m_fDt - m_stJump.m_fJumpDt)));
                move *= 100.0f;
                m_stPos.y -= move * Time.deltaTime;
            }
            ResetTransform();
            yield return null;
        } while (m_eState == E_HEROSTATE.E_START);
        yield return null;
    }

    IEnumerator Cor_State_Die()
    {
        StopCoroutine("Cor_State_Sturn");
        m_eState = E_HEROSTATE.E_DIE;
        m_pAni.namePrefix = "stun2_";
        m_pAni.mIndex = 0;
        m_pAni.loop = false;
        m_stJump.m_fDt = 0.0f;
        m_stJump.m_fJumpDt = 0.0f;
        if (m_bPerfectHit==false)
        {
            JHUserData_Mng.I.m_pUserData.PlusData("_HiddenNotHitAllType");
        }
        do
        {
            if (base.Update() == false) yield return null;
            m_stJump.m_fDt += Time.deltaTime;
            if (m_stPos.y <= m_fGroundY)
            {
                m_stPos.y = m_fGroundY;
                m_stJump.m_fDt = 0.0f;
                m_stJump.m_fJumpDt = 0.0f;
            }
            else
            {
                float move = (m_stJump.m_fGravity * ((m_stJump.m_fDt - m_stJump.m_fJumpDt)));
                move *= 100.0f;
                m_stPos.y -= move * Time.deltaTime;
            }
            ResetTransform();
            yield return null;
        } while (true);
      
        yield return null;
    }

    IEnumerator Cor_State_Magnet()
    {
        m_eState = E_HEROSTATE.E_MAGNET;
        JHGame_MainLayer.I.m_pMoney_Mng.AllMagnet();
       yield return new WaitForSeconds(1.0f);
        
        do
        {
            if (base.Update() == false) yield return null;
            if(JHGame_MainLayer.I.m_pMoney_Mng.GetMagnet()==false)
            {
                ChangeState(E_HEROSTATE.E_NONE);
                break;
            }
            ResetTransform();
            yield return null;
        } while (m_eState == E_HEROSTATE.E_MAGNET);

        yield return null;
    }

    void Joypad()
    {
       

        if (Input.touchCount >= 2)
        {
            ChangeState(E_HEROSTATE.E_JUMP);
        }

        if (JHGame_UILayer.I.m_pJoyStick.m_eDirect==E_CSDIRECT.E_CENTER)
        {
           
            ChangeState(E_HEROSTATE.E_NONE);
            return;
        }



        if (JHGame_UILayer.I.m_pJoyStick.m_eDirect == E_CSDIRECT.E_LEFT)
        {
            m_eDirect = E_CSDIRECT.E_LEFT;
            m_pSprite.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            m_bMove = true;
        }
        else if (JHGame_UILayer.I.m_pJoyStick.m_eDirect == E_CSDIRECT.E_RIGHT)
        {
            m_eDirect = E_CSDIRECT.E_RIGHT;
            m_pSprite.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            m_bMove = true;
        }
        if (m_bMove == true)
        {
            if (m_eDirect == E_CSDIRECT.E_LEFT)
            {
                m_stPos.x -= m_fSpeed * Time.deltaTime;
                if (m_stPos.x <= -m_fWallX) m_stPos.x = -m_fWallX;
            }
            else if (m_eDirect == E_CSDIRECT.E_RIGHT)
            {
                m_stPos.x += m_fSpeed * Time.deltaTime;
                if (m_stPos.x >= m_fWallX) m_stPos.x = m_fWallX;
            }
            ChangeState(E_HEROSTATE.E_MOVE);
        }
       
    }

    void GSensor()
    {
        
        float fPosX = Input.acceleration.x;

        if (Input.touchCount >= 1)
        {
            ChangeState(E_HEROSTATE.E_JUMP);
        }

        if (Mathf.Abs(fPosX) < 0.06f)
        {
            if (fPosX == 0.0f) return;
            ChangeState(E_HEROSTATE.E_NONE);
            return;
        }

       

        if (fPosX < 0.0f)
        {
            m_eDirect = E_CSDIRECT.E_LEFT;
            m_pSprite.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            m_bMove = true;
        }
        else
        {
            m_eDirect = E_CSDIRECT.E_RIGHT;
            m_pSprite.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            m_bMove = true;
        }
        if (m_bMove == true)
        {
            if (m_eDirect == E_CSDIRECT.E_LEFT)
            {
                m_stPos.x -= m_fSpeed * Time.deltaTime;
                if (m_stPos.x <= -m_fWallX) m_stPos.x = -m_fWallX;
            }
            else if (m_eDirect == E_CSDIRECT.E_RIGHT)
            {
                m_stPos.x += m_fSpeed * Time.deltaTime;
                if (m_stPos.x >= m_fWallX) m_stPos.x = m_fWallX;
            }
            ChangeState(E_HEROSTATE.E_MOVE);
        }
       
    }

    void TouchMoveUpdate()
    {
        if (m_eState == E_HEROSTATE.E_STURN) return;
        if (m_eState == E_HEROSTATE.E_START) return;
        if (m_eState == E_HEROSTATE.E_DIE) return;

        if (JHGameData_Mng.I._GSencer == true)
        {
            //GSensor();
            Joypad();
            return;
        }
        
      //  if (Input.touchCount >=2 ||
      //      (Input.GetKey(KeyCode.LeftArrow) == true && Input.GetKey(KeyCode.RightArrow) == true))
      //  {
      //      ChangeState(E_HEROSTATE.E_NONE);
      //  }
         if(Input.touchCount==1 ||
            (Input.anyKey==true))
        {
            Vector3 pTouchPos = Vector3.zero;


            //if(Input.touchCount==1) pTouchPos = JHGame_Scene.I._GameLayer.m_pCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            if (Input.touchCount >= 1) pTouchPos = JHGame_Scene.I._GameLayer.m_pCamera.ScreenToWorldPoint(Input.touches[Input.touchCount - 1].position);
           
            if (pTouchPos.y >= 0.75f) return;
            if ((pTouchPos.y >= -0.45f) || Input.GetKey(KeyCode.UpArrow))
            {
                ChangeState(E_HEROSTATE.E_JUMP);
            }
          /*  if ((Input.touchCount == 1 && Mathf.Abs(pTouchPos.x-transform.position.x)<0.08f) )
            {
                ChangeState(E_HEROSTATE.E_NONE);
                return;
            }*/

           // if ((Input.touchCount==1 && pTouchPos.x < 0.0f) || Input.GetKey(KeyCode.LeftArrow) == true)
            if ((pTouchPos.x < 0.0f) || Input.GetKey(KeyCode.LeftArrow) == true)
            {
                m_eDirect = E_CSDIRECT.E_LEFT;
             //   m_pSprite.transform.localPosition = new Vector3(-15.0f, 0.0f, 0.0f);
                m_pSprite.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
          //  else if((Input.touchCount==1 && pTouchPos.x>=0.0f) || Input.GetKey(KeyCode.RightArrow) == true)
            else if ((pTouchPos.x >= 0.0f) || Input.GetKey(KeyCode.RightArrow) == true)
            {
                m_eDirect = E_CSDIRECT.E_RIGHT;
               // m_pSprite.transform.localPosition = new Vector3(15.0f, 0.0f, 0.0f);
                m_pSprite.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            m_bMove = true;
        }
        else if(Input.touchCount==0)
        {
            m_bMove = false;
            if(m_stJump.m_eState==E_JUMPSTATE.E_JUMP_NONE)
            {
                ChangeState(E_HEROSTATE.E_NONE);
            }
        }

        if(m_bMove==true || m_stJump.m_eState!=E_JUMPSTATE.E_JUMP_NONE)
        {
            if(m_eDirect==E_CSDIRECT.E_LEFT)
            {
                m_stPos.x -= m_fSpeed * Time.deltaTime;
                if (m_stPos.x <= -m_fWallX) m_stPos.x = -m_fWallX;
            }
            else if (m_eDirect == E_CSDIRECT.E_RIGHT)
            {
                m_stPos.x += m_fSpeed * Time.deltaTime;
                if (m_stPos.x >= m_fWallX) m_stPos.x = m_fWallX;
            }
            ChangeState(E_HEROSTATE.E_MOVE);
        }
    }


    void ChangeState(E_HEROSTATE eState, bool bForced=false)
    {
        if (bForced==false && m_eState == eState) return;
        if (m_eState == E_HEROSTATE.E_DIE) return;
        if (bForced == true)
        {
            for (int i = 0; i < (int)E_HEROSTATE.E_MAX; i++)
            {
                switch ((E_HEROSTATE)i)
                {
                    case E_HEROSTATE.E_NONE:
                        StopCoroutine("Cor_State_None");
                        break;
                    case E_HEROSTATE.E_MOVE:
                        StopCoroutine("Cor_State_Move");
                        break;
                    case E_HEROSTATE.E_JUMP:
                        StopCoroutine("Cor_State_Jump");
                        break;
                    case E_HEROSTATE.E_STURN:
                        StopCoroutine("Cor_State_None");
                        break;
                }
            }
        }
       /* 
        }*/
        switch (eState)
        {
            case E_HEROSTATE.E_NONE:
                StartCoroutine("Cor_State_None");
                break;
            case E_HEROSTATE.E_MOVE:
                StartCoroutine("Cor_State_Move");
                break;
            case E_HEROSTATE.E_JUMP:
                StartCoroutine("Cor_State_Jump");
                break;
            case E_HEROSTATE.E_STURN:
                StartCoroutine("Cor_State_Sturn");
                break;
            case E_HEROSTATE.E_DIE :
                StartCoroutine("Cor_State_Die");
                break;
            case E_HEROSTATE.E_MAGNET :
                StartCoroutine("Cor_State_Magnet");
                break;
               
        }
    }


    void EatMoney(int nGold)
    {
        JHGameData_Mng.I._GameScore += nGold;
        JHUserData_Mng.I.m_pUserData.SetData("_CurrScore", JHGameData_Mng.I._GameScore);
        m_pScoreEffect.EatMoney(nGold);
       
        //Debug.Log(nGold);
    }

    void Hited(float fDamege = 0.0f)
    {
        JHGame_MainLayer.I.m_pShake.Reset();
        JHGame_MainLayer.I.m_pShake.StartUpDownShake(0.5f);
        if (JHGameData_Mng.I._Vibe == true) Handheld.Vibrate();

        if (JHGameData_Mng.I._TutorialState == false)  m_fCurrPower = m_fCurrPower / 2.0f;
        JHGame_UILayer.I.m_pHpBar.Hit();
        if(m_eState==E_HEROSTATE.E_STURN)
        {
            StopCoroutine("Cor_State_Sturn");
            ChangeState(E_HEROSTATE.E_NONE);
        }
        else
        {
            float fPerDam = (JHGame_MainLayer.I._Hero.pSrc._MaxHp * fDamege) / 100.0f;
            Minus_Hp(fPerDam);
        }
        ChangeState(E_HEROSTATE.E_STURN);
    }

    public void Minus_Hp(float fHp)
    {
        m_fCurrHp -= fHp;
        if (m_fCurrHp < 0.0f)
        {
            m_fCurrHp = 0.0f ;
        }
    }

    public void Recovery_Hp(float fHp)
    {
        m_fCurrHp += fHp;
        if(m_fCurrHp>m_fMaxHp)
        {
            m_fCurrHp = m_fMaxHp;
        }
    }

   void OnTriggerEnter2D(Collider2D other)
   {
       //if(m_eState!=E_HEROSTATE.E_STURN)
     //  {
           JHMoney_Root pMoney = other.GetComponent<JHMoney_Root>();
           if (pMoney != null)
           {
               if (pMoney._Active == true)
               {
                   if (pMoney._Magnet == false)
                   {
                       m_fCurrPower += m_fPlusPower;
                       if (m_fCurrPower >= m_fMaxPower)
                       {
                           if(JHGameData_Mng.I._Vibe==true)Handheld.Vibrate();
                           float fRecovery = (JHGame_MainLayer.I._Hero.pSrc._MaxHp * 3) / 100.0f;
                           //   Debug.Log("Recovery + " + fRecovery);
                           JHGame_MainLayer.I._Hero.pSrc.Recovery_Hp(fRecovery);

                           CSSoundMng.I.Play("FeverBoom");
                           CSSoundMng.I.Play("Neco1");
                           JHGame_MainLayer.I.m_pEffect_Mng.CreateEffect_PT_VortexAir(m_stPos);
                           JHGame_MainLayer.I.m_pEffect_Mng.CreateEffect_CirclePang(m_stPos);
                           JHGame_MainLayer.I.m_pMoney_Mng.AllMagnet();
                           AutoFade.LoadFade(0.1f, 0.1f, Color.white);
                           m_fCurrPower = 0.0f;
                       }
                   }
          
                   
                   EatMoney(pMoney.GetMoneyWorthPay());
                  // EatMoney(pMoney._Gold);
                   pMoney.Eated();
                   return;
               }
           }
     //  }
      

       JHEnemy_Root pEnemy = other.GetComponent<JHEnemy_Root>();
       if (pEnemy != null)
       {
           if (pEnemy._Active == true)
           {
              // Debug.Log(pEnemy._Class);
               switch(pEnemy._Class)
               {
                   case E_ENEMY_CLASS.E_BALL :
                       if (pEnemy._Position.y < m_stPos.y-15.0f) return;
                       pEnemy.HitPlayer();
                       Hited(pEnemy._Damege);
                       JHUserData_Mng.I.m_pUserData.PlusData("_HitBallCount");
                       JHUserData_Mng.I.m_pUserData.PlusData("_Hidden_HitBallCombo");
                       return; 
                   case E_ENEMY_CLASS.E_COW :      
                       pEnemy.HitPlayer();
                       Hited(pEnemy._Damege);
                       JHUserData_Mng.I.m_pUserData.PlusData("_HitCowCount");
                       return;
                   case E_ENEMY_CLASS.E_ROCK:
                       if (pEnemy._Position.y < m_stPos.y - 15.0f) return;
                       pEnemy.HitPlayer();
                       Hited(pEnemy._Damege);
                       JHUserData_Mng.I.m_pUserData.PlusData("_HitBallCount");
                       JHUserData_Mng.I.m_pUserData.PlusData("_Hidden_HitBallCombo");
                       return;
                   case E_ENEMY_CLASS.E_CAR:
                       pEnemy.HitPlayer();
                       Hited(pEnemy._Damege);
                       JHUserData_Mng.I.m_pUserData.PlusData("_HitCowCount");
                       return;
                   case E_ENEMY_CLASS.E_HOMI :
                       if (pEnemy._Position.y < m_stPos.y - 15.0f) return;
                       pEnemy.HitPlayer();
                       Hited(pEnemy._Damege);
                       JHUserData_Mng.I.m_pUserData.PlusData("_HitBallCount");
                       JHUserData_Mng.I.m_pUserData.PlusData("_Hidden_HitBallCombo");
                       return;
               }
              
           }
       }

       if (m_eState != E_HEROSTATE.E_STURN)
       {
           JHItem_Root pItem = other.GetComponent<JHItem_Root>();
           if (pItem != null)
           {
               if (pItem._Active == true)
               {
                   pItem.Eated();
                   return;
               }
           }
       }
    }

}
