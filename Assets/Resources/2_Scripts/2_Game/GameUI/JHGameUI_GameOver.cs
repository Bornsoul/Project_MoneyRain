using UnityEngine;
using System.Collections;

public class JHGameUI_GameOver : MonoBehaviour {
    public UISprite m_pFont = null;
    public UISprite m_pBack = null;

    JumpStruct m_stJump;

    Vector3 m_stPos;
    Color m_stColor;
    bool m_bActive = false;
	public void Enter()
    {
        m_stJump.Reset();
        m_stJump.m_fDt = 0.0f;
        m_stJump.m_fJumpDt = 0.0f;
        m_stJump.m_fPower = 5.0f;
        m_stJump.m_fGravity = 500.9f;
        m_stJump.m_eState = E_JUMPSTATE.E_JUMP_DOWN;
        m_bActive = true;

        m_stPos.x = 0.0f;
        m_stPos.y = 740.0f;
        m_pFont.transform.localPosition = m_stPos;
        m_stColor = Color.white;
        m_stColor.a = 0.0f;
        m_pBack.color = m_stColor;
        
        //JAMenuData.I._nGameMoney
        JHUserData_Mng.I.m_pUserData.Save();

        JAMenuData_Mng.I._nGameMoney = JHGameData_Mng.I._GameScore;

        #if !UNITY_EDITOR
        JHGooglePS_Mng.I.ReportScore_HighScore(JAMenuData_Mng.I._nGameMoney);
#endif

    }

    public void Destroy()
    {

    }
	
	// Update is called once per frame
	void Update () {
        if (m_bActive == false) return;
        if (m_stColor.a<1.0f)
        {
            m_stColor.a += 1.0f*Time.deltaTime;
            m_pBack.color = m_stColor;
        }
        else
        {
            if (m_stJump.m_eState == E_JUMPSTATE.E_JUMP_DOWN)
            {
                m_stJump.m_fDt += Time.deltaTime;
                float move = (m_stJump.m_fGravity * ((m_stJump.m_fDt - m_stJump.m_fJumpDt)));
                move *= 10.0f;
                m_stPos.y -= move * Time.deltaTime;
                if (m_stPos.y <= 0.0f)
                {
                    m_stPos.y = 0.0f;
                    m_pBack.enabled = true;
                    m_stJump.m_eState = E_JUMPSTATE.E_JUMP_NONE;
                    JHGame_MainLayer.I.m_pShake.StartUpDownShake(1.3f);
                    CSSoundMng.I.Play("FeverBoom");
                }
                m_pFont.transform.localPosition = m_stPos;
            }
            else
            {
                if(Input.GetMouseButtonDown(0))
                {
                    AutoFade.LoadLevel("5_Result", 0.3f, 0.3f, Color.black);
                }
            }
        }
        

       
        
	}
}
