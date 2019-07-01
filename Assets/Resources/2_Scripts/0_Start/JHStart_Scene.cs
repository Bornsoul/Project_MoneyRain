using UnityEngine;
using System.Collections;

public class JHStart_Scene : CSSingleton<JHStart_Scene>
{


    GameObject m_pPop;
    CSLayer m_pPopLayer;
    public CSLayer _PopLayer { get { return m_pPopLayer; } set { m_pPopLayer = value; } }

    private bool bGo = false;
    float fTime = 0.0f;
    public UISprite m_pLoadingAni = null;

    string m_sAppString = string.Empty;
    public bool m_bAppPlay = false;

    public GameObject m_pLoad_Gam = null;
    public UISlider m_pLoadSlider = null;
    public UILabel m_pLoadLabel = null;
    bool m_bLoadSliderEnd = false;

    void Start()
    {
        m_pPopLayer = CSLayerMng.I.CreateLayer("PopLayer", 100);

        CSPrefabMng.I.ResourceLoadinList(E_RESOURCETYPE.E_PERMANENT, "0_Library/CSDebugLayer");
        CSPrefabMng.I.ResourceLoadinList(E_RESOURCETYPE.E_PERMANENT, "0_Library/CSLayer");

        m_pLoadSlider.value = 0f;
        m_pLoadLabel.text = "";

        JAMenuData_Mng.I.SetLoadString("정보 읽는다 냥..");

        JAMenuData_Mng.I.m_pAppData_String.Enter();
        JAMenuData_Mng.I.m_pPopupData_String.Enter();
        JAMenuData_Mng.I.m_pStatData_String.Enter();
        JAMenuData_Mng.I.m_pHouseData_String.Enter();
        JAMenuData_Mng.I.m_pTropiData_String.Enter();

        JAGameDataFile.I.SetData("AppVersion", JAGameDataFile.I._sAppVersion);

        bGo = false;
        fTime = 0.0f;
    }

    /// <summary>
    /// 이 곳에 로딩할 데이터를 넣으면 됨
    /// </summary>
    /// <returns></returns>
    IEnumerator DataLoading()
    {
        if (JAGameDataFile.I.m_bSaveCheck == false)
        {
            JAGameDataFile.I.Enter(); //!< 저장 클래스

            JAMenuData_Mng.I.SetLoadString("세이브파일 못 읽는다 냥..");
        }
        else
        {
           // CSDirector.I.GetDeviceDebug().Log(JAGameDataFile.I.GetData_Int("JHSaveFuckUp").ToString());
            //if (JAGameDataFile.I.GetData_Bool(JAGameDataFile.I._sSaveDataStr) == false)
            Debug.Log(JAGameDataFile.I.GetData_Int("JHSaveFuckUp"));
            if (JAGameDataFile.I.GetData_Int("JHSaveFuckUp")==0)
            {
                
                JAGameDataFile.I.Enter();
            }
                

            JAMenuData_Mng.I.SetLoadString("세이브파일 읽는다 냥..");
        }
        
        CSPrefabMng.I.ResourceLoadinList_Folder(E_RESOURCETYPE.E_PERMANENT, "1_Scene");
        CSPrefabMng.I.ResourceLoadinList_Folder(E_RESOURCETYPE.E_PERMANENT, "2_Objects");

        CSSoundMng.I.AutoSearchSound();

#if !UNITY_EDITOR
        JHGooglePS_Mng.I.Init();
        
        JHGooglePS_Mng.I.SighInProcess(ProcessAuthentiation);
        JAMenuData_Mng.I.SetLoadString("구글플레이 접속중이다 냥...");

        for (float i = 0.0f; i < 5.0f;i+=Time.deltaTime )
        {
            if (JHUserData_Mng.I.m_pUserData.m_bLogin == true) break;
            yield return null;
        }
      
#endif

        JAMenuData_Mng.I.SetLoadString("끝낫다 냥..!");
        m_bLoadSliderEnd = true;
       
        //  JHGooglePS_Mng.I.SighIn();
        yield return true;
    }


    IEnumerator Cor_Loading()
    {
        yield return StartCoroutine(DataLoading());

        yield return new WaitForSeconds(1f);
        AutoFade.LoadLevel("1_Title", 0.2f, 0.2f, Color.white);
        //Application.LoadLevel("1_Title");

    }


    void ProcessAuthentiation(bool success)
    {
      //  CSDirector.I.GetDeviceDebug().Log("Sign in Go");

        if (success)
        {
            JHUserData_Mng.I.m_pUserData.m_bLogin = true;
            //CSDirector.I.GetDeviceDebug().Log("Sign in Success"); 
        }
        else
        {
           // CSDirector.I.GetDeviceDebug().Log("Sign in Fail");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (m_bLoadSliderEnd == false)
            m_pLoadSlider.value = JAMenuData_Mng.I._sLoadStrList.Count / 10f;
        else
            m_pLoadSlider.value = 1f;

        m_pLoadLabel.text = JAMenuData_Mng.I._sLoadStrList[JAMenuData_Mng.I._nLoadStrCount - 1];

        if (JAMenuData_Mng.I.m_pAppData_String.m_bDone == false) { return; }

        if (JAMenuData_Mng.I.m_pPopupData_String.m_bDone == false) { return; }
        if (JAMenuData_Mng.I.m_pStatData_String.m_bDone == false) { return; }
        if (JAMenuData_Mng.I.m_pHouseData_String.m_bDone == false) return;
        if (JAMenuData_Mng.I.m_pTropiData_String.m_bDone == false) return;
        if (bGo == true) return;

        if (JAGameDataFile.I.GetData_String("AppVersion") != JAMenuData_Mng.I.m_pAppData_String.GetCurVer())
        {
            m_sAppString = JAMenuData_Mng.I.m_pAppData_String.GetText(); //!< 서버에서 버전정보를 받아옵니다.

            //! 서버내에 [a], [b] 문자를 읽은후 다른 값 교체합니다.
            m_sAppString = JAMenuData_Mng.I.ReplaceString(m_sAppString, "[a]", JAGameDataFile.I.GetData_String("AppVersion")); //!< 현재 앱버전
            m_sAppString = JAMenuData_Mng.I.ReplaceString(m_sAppString, "[b]", JAMenuData_Mng.I.m_pAppData_String.GetCurVer()); //!< 서버 버전

            //NGUITools.SetActive(m_pLoad_Gam, false);
            
            JAMenuData_Mng.I.CreatePopupGam(_PopLayer.m_pAnchor, "AppVersionPop", JAMenuData_Mng.I.m_pAppData_String.GetTitle(), JAMenuData_Mng.I.ReplaceNewLine(m_sAppString), E_JA_POPUP.E_POP_OK, "JAVersionBtn", "JADestroyBtn");
            if (m_bAppPlay == false) return;
        }
        
        fTime += Time.deltaTime;
        if (fTime > 0.5f)
        {
            bGo = true;
            StartCoroutine(Cor_Loading());

        }
    }
}
