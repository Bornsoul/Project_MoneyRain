using UnityEngine;
using System.Collections;

public class JAResultMng : CSSingleton<JAResultMng>
{
    enum eState
    {
        E_STATE_NONE,
        E_STATE_START,
        E_STATE_SCROLL,
        E_STATE_END,
    };
    eState m_eState = eState.E_STATE_NONE;
    public JAResultMoney_Src m_pMoney_Src = null;

    public UIScrollView m_pScrollView = null;
    public SpringPanel m_pSpring_Panel = null;
    public UIPanel m_pPanel = null;
    public GameObject m_pScroll_Gam = null;

    public UISprite[] m_pStateBar = null;
    public UIPanel m_pStatePanel = null;

    Vector3 m_stSpringPos;
    Vector3 m_stScrollPos;
    float m_fTime = 0f;

    float m_fStateTime = 0f;
    float m_fStateAlpha = 0f;

    int m_nMyMoney = 0;
    int m_nResultGetMoney = 0;
    float m_fMoneyRate = 0;

    void Start()
    {
        m_pStatePanel.alpha = 0f;

        m_eState = eState.E_STATE_NONE;
        m_stScrollPos = m_pScroll_Gam.transform.localPosition;

        m_nMyMoney = JAGameDataFile.I.GetData_Int("PlayerMoney");
        //JAMenuData_Mng.I._nGameMoney = 123456;

        m_fMoneyRate = (JAMenuData_Mng.I._nGameMoney - (int)((JAMenuData_Mng.I._nGameMoney / 100.0f) * 10.0f));
       // m_fMoneyRate = (JAMenuData_Mng.I._nGameMoney / 100.0f) * 10.0f;
        m_pMoney_Src.Enter(JAMenuData_Mng.I._nGameMoney);

        m_nResultGetMoney = m_nMyMoney + (int)m_fMoneyRate;
        JAGameDataFile.I.SetData("PlayerMoney", m_nResultGetMoney);


    }

    void Update()
    {


        switch (m_eState)
        {
            case eState.E_STATE_NONE:
                m_fTime += Time.deltaTime;

                if (m_fTime > 0.5f)
                {
                    CSSoundMng.I.Play("ResultScore_UP");
                    
                    m_eState = eState.E_STATE_START;
                    return;
                }
                break;
            case eState.E_STATE_START:
                m_pMoney_Src.MoneyUpdate();
                m_pMoney_Src.ResultMoneyUpdate();
                
                if (m_pMoney_Src.GetDone() == true)
                {
                    m_fTime += Time.deltaTime;

                    if (m_fTime > 1.0f)
                    {
                        
                        m_stScrollPos.x = Mathf.SmoothStep(m_stScrollPos.x, -480f, 0.15f);
                        m_eState = eState.E_STATE_SCROLL;
                        return;
                    }
                }
                break;
            case eState.E_STATE_SCROLL:

                m_fTime += Time.deltaTime;
                m_stScrollPos.x = Mathf.SmoothStep(m_stScrollPos.x, -480f, 0.15f);

                if (m_fTime > 2.0f)
                {
                    CSSoundMng.I.Play("TTing");
                    m_stSpringPos.x = 0f;
                    m_stScrollPos.x = -480f;
                    m_pSpring_Panel.target = m_stSpringPos;
                    m_pScroll_Gam.transform.localPosition = m_stScrollPos;
                    m_eState = eState.E_STATE_END;
                    return;
                }
                break;
            case eState.E_STATE_END:

                m_stSpringPos = m_pSpring_Panel.target;
                SetChangeState((int)m_stSpringPos.x);

                JAMenuData_Mng.I._nGameMoney = 0;
                break;
        }

        m_pScroll_Gam.transform.localPosition = m_stScrollPos;

    }


    void SetChangeState(int nIndex)
    {
        switch (nIndex)
        {
            case 480:
                m_pStateBar[1].alpha = 50f / 255f;
                m_pStateBar[0].alpha = 150f / 255f;
                break;
            case 0:
                m_pStateBar[0].alpha = 50f / 255f;
                m_pStateBar[1].alpha = 150f / 255f;
                break;
            default:
                //Debug.Log("asdasdasd");
                break;
        }


        if (m_pSpring_Panel.enabled == true)
        {
            m_fStateTime = 0f;

            m_fStateAlpha = Mathf.SmoothStep(m_fStateAlpha, 1f, 0.16f);

            for (int i = 0; i < m_pStateBar.Length; i++)
                m_pStatePanel.alpha = m_fStateAlpha;
        }
        else
        {
            m_fStateTime += Time.deltaTime;
            if (m_fStateTime > 1.2f)
            {

                m_fStateAlpha = Mathf.SmoothStep(m_fStateAlpha, 0f, 0.16f);

                for (int i = 0; i < m_pStateBar.Length; i++)
                    m_pStatePanel.alpha = m_fStateAlpha;

            }
        }

    }
}
