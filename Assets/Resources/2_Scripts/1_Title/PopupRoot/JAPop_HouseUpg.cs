using UnityEngine;
using System.Collections;

public class JAPop_HouseUpg : CSSingleton<JAPop_HouseUpg>
{
    public enum eState
    {
        E_STATE_NONE,
        E_STATE_HEALTH,
        E_STATE_MONEYMANY,
        E_STATE_UPGRADE,
        E_STATE_EXIT,
    };

    eState m_eState = eState.E_STATE_NONE;
    public eState _eState { get { return m_eState; } }

    public Animation m_pPopAni = null;
    public JAHousePopTop_Src m_pHPopTop_Src = null;

    public bool m_bChange;
    public BoxCollider2D[] m_pBoxColider = null;
    public JADestroyObj m_pDestroyObj = null;
    bool m_bTutorial = false;


    void Start()
    {
        m_eState = eState.E_STATE_NONE;
        m_pPopAni.Play("Ani_PopupStart");
        m_pHPopTop_Src.Enter();

        m_pHPopTop_Src.Normal_Stat();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bTutorial == true) return;
        if ( Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Backspace) )
        {
            Button_Exit();
        }
         
        m_pHPopTop_Src.Fun_MaxLevelBtn(JAHouseSystem_Mng.I.m_bLevelMax);
        //m_pTextLabel.text = "집을 업그레이드 하시겠습니까?\n[A1524E]필요 금액 :[-] " + JAHouseSystem_Mng.I.m_pHouseData._nHousePrice + "\n[5697A3]보유 금액 :[-] " + JAHouseSystem_Mng.I.m_nMyMoney;
    }


    public void Tutorial_Btn()
    {
        for (int i = 0; i < m_pBoxColider.Length; i++)
            m_pBoxColider[i].enabled = false;
    }

    public void TutorialMod(bool bYES)
    {
        m_bTutorial = bYES;
        if ( bYES == true )
        {
            m_pDestroyObj.m_bSetClick = false;
            m_pHPopTop_Src.Tutorial_Stat();
            for (int i = 0; i < m_pBoxColider.Length; i++)
                m_pBoxColider[i].enabled = false;

            m_pBoxColider[1].enabled = true;
            m_pBoxColider[2].enabled = true;
        }
        else
        {
            m_pDestroyObj.m_bSetClick = true;
            m_pHPopTop_Src.Normal_Stat();
            for (int i = 0; i < m_pBoxColider.Length; i++)
                m_pBoxColider[i].enabled = true;
        }
    }

    void Button_ChangeMod()
    {
        m_bChange = !m_bChange;
    }

    void Button_Health()
    {
        if (m_eState == eState.E_STATE_NONE)
        {
            CSSoundMng.I.Play("Shop_Coin");
            if (JAHouseSystem_Mng.I.m_bUpgradeDone == true) return;

            //Debug.Log("Health_Btn");
            m_pHPopTop_Src.Button_Hp();

            //StartCoroutine(Cor_Destroy(0.33f));
        }
    }

    void Button_MoneyMany()
    {
        if (m_eState == eState.E_STATE_NONE)
        {
            CSSoundMng.I.Play("Shop_Coin");
            if (JAHouseSystem_Mng.I.m_bUpgradeDone == true) return;

            //Debug.Log("Money_Mnay_Btn");
            m_pHPopTop_Src.Button_MoneyMany();

            //StartCoroutine(Cor_Destroy(0.33f));
        }
    }

    void Button_HouseUpgrade()
    {
        if (m_eState == eState.E_STATE_NONE)
        {
            CSSoundMng.I.Play("Shop_Coin");
            if (JAHouseSystem_Mng.I.m_bUpgradeDone == true) return;
            else
            {
                if (JAHouseSystem_Mng.I.m_bLevelMax == true)
                {
                    Debug.Log("집 업그레이드 만렙");
                    JAHouseSystem_Mng.I.Fun_HouseMaxLevelEnd();
                    return;
                }
                else
                {
                    Debug.Log("집 업그레이드");
                    if (JAGameDataFile.I.GetData_Int("PlayerMoney") < JAHouseSystem_Mng.I._pHouseData._nHousePrice)
                    {
                        m_pHPopTop_Src.m_pBtnAni.Play();
                        return;
                    }
                    else
                    {
                        JAHouseSystem_Mng.I.Fun_Upgrade();
                    }
                }

            }
        }
    }

    public void Button_Exit()
    {
        JHTitle_Scene.I._bESC = false;
        if (m_eState == eState.E_STATE_NONE)
        {
            CSSoundMng.I.Play("MenuEF_Button");
            if (JAHouseSystem_Mng.I.m_bUpgradeDone == true) return;

            StartCoroutine(Cor_Destroy(0.33f));
            m_eState = eState.E_STATE_EXIT;
        }
    }

    public void Pop_Exit()
    {
        JHTitle_Scene.I._bESC = false;
        StartCoroutine(Cor_Destroy(0.33f));
        m_eState = eState.E_STATE_EXIT;
    }

    IEnumerator Cor_Destroy(float fWaitTime)
    {
        m_pPopAni.Play("Ani_PopupEnd");
        yield return new WaitForSeconds(fWaitTime);

        CSPrefabMng.I.DestroyPrefab(transform.gameObject);
        Destroy(this); 
    }

}
