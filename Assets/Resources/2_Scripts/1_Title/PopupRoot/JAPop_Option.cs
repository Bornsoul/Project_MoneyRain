using UnityEngine;
using System.Collections;

public class JAPop_Option : CSSingleton<JAPop_Option>
{


    public Animation m_pPopAni = null;
    public JAOptionTop_Src m_pOption_Srcc = null;

    public UIToggle[] m_pToggles = null;

    int m_nSelectIndex = 0;
    public int _nSelectIndex { get { return m_nSelectIndex; } set { m_nSelectIndex = value; } }

    public UILabel m_pTitleLabel = null;
    public UILabel[] m_pBtnLabel = null;

    public bool[] m_bCheck = null;
    public JADestroyObj m_pDestroyObj = null;

    void Start()
    {
        //JHTitle_Scene.I._bESC = true;
        m_bCheck = new bool[4];
        m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_SOUND] = JAGameDataFile.I.GetData_Bool("SoundMod");
        m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_VIBRATE] = JAGameDataFile.I.GetData_Bool("VibMod");
        m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_TOUCH_1] = JAGameDataFile.I.GetData_Bool("TouchMod");
        m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_TOUCH_2] = !JAGameDataFile.I.GetData_Bool("TouchMod");

        m_pToggles[0].value = m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_SOUND];
        m_pToggles[1].value = m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_VIBRATE];
        m_pToggles[2].value = m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_TOUCH_1];
        m_pToggles[3].value = m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_TOUCH_2];
       
        switch (Application.loadedLevelName)
        {
            case "1_Title":
                JHTitle_Scene.I._bESC = true;
                m_pTitleLabel.text = "옵션";
                m_pBtnLabel[0].text = "만든이";
                m_pBtnLabel[1].text = "초기화";
                break;
            case "2_Game":
                m_pTitleLabel.text = "일시정지";
                m_pBtnLabel[0].text = "계속하기";
                m_pBtnLabel[1].text = "나가기";
                m_pDestroyObj.m_bSetClick = false;
                break;
        }

        m_pPopAni.Play("Ani_PopupStart");
    }

    void Update()
    {
        if (JAGameDataFile.I.GetData_Bool("SoundMod") == false)
        {
            CSSoundMng.I.AllMute(false);
        }
        else
        {
            CSSoundMng.I.AllMute(true);
        }

        if (Application.loadedLevelName == "2_Game")
        {
            if (JHGameData_Mng.I._GameCycle == true && Input.GetKeyDown(KeyCode.Escape) == true)
            {
                Button_Exit();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) == true)
            {
                Button_Exit();
            }
        }

        //if ( m_pDestroyObj.m_bSetClick == true )
        //{
        //    JHGameData_Mng.I.SetCycle(false);
        //    Button_Exit();
        //}

    }

    public void Button_Click()
    {
        switch (m_pToggles[_nSelectIndex].value)
        {
            case true:

                Debug.Log("Btn : " + m_nSelectIndex + ", TRUE ");
                break;
            case false:
                Debug.Log("Btn : " + m_nSelectIndex + ", FALSE ");
                break;
        }
    }

    /// <summary>
    /// 하단 첫번째 버튼
    /// </summary>
    public void Button_One()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        switch (Application.loadedLevelName)
        {
            case "1_Title":
                CSPrefabMng.I.CreatePrefab(JHTitle_Scene.I._PopLayer2.m_pAnchor, "2_Objects/Popup/Pop_Credit", "Pop_Credit");
                //Debug.Log("만든이");

                break;
            case "2_Game":
                JHGameData_Mng.I.SetCycle(false);
                Button_Exit();
                break;
        }
    }

    /// <summary>
    /// 하단 두번째 버튼
    /// </summary>
    public void Button_Two()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        switch (Application.loadedLevelName)
        {
            case "1_Title":

                JAGameDataFile.I.AllReset();
                AutoFade.LoadLevel("1_Title", 0.5f, 0.5f, Color.black);
                break;
            case "2_Game":
                JHGameData_Mng.I.SetGameOver();
                AutoFade.LoadLevel("1_Title", 0.5f, 0.5f, Color.black);
                break;
        }
    }


    void Button_Exit()
    {
        
        CSSoundMng.I.Play("MenuEF_Button");
        switch (Application.loadedLevelName)
        {
            case "1_Title":
                JHTitle_Scene.I._bESC = false;


                break;
            case "2_Game":
                JHGameData_Mng.I.SetCycle(false);

                break;
        }
        StartCoroutine(Cor_Destroy(0.33f));
    }

    IEnumerator Cor_Destroy(float fWaitTime)
    {
        m_pPopAni.Play("Ani_PopupEnd");
        yield return new WaitForSeconds(fWaitTime);

        CSPrefabMng.I.DestroyPrefab(transform.gameObject);
        Destroy(this);
    }
}
