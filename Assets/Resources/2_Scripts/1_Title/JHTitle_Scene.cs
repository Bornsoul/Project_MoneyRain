using UnityEngine;
using System.Collections;

public class JHTitle_Scene : CSSingleton<JHTitle_Scene>
{

    GameObject m_pScene = null;

    CSLayer m_pLayer;
    public CSLayer _pLayer { get { return m_pLayer; } set { m_pLayer = value; } }

    CSLayer m_pPopLayer1;
    public CSLayer _PopLayer1 { get { return m_pPopLayer1; } set { m_pPopLayer1 = value; } }

    CSLayer m_pPopLayer2;
    public CSLayer _PopLayer2 { get { return m_pPopLayer2; } set { m_pPopLayer2 = value; } }

    GameObject m_pHero = null;
    CSObjectStruct<JHMessageTutorial_Menu> m_pPopTutorial;
    public CSObjectStruct<JHMessageTutorial_Menu> _pPopTutorial { get { return m_pPopTutorial; } set { m_pPopTutorial = value; } }
    internal bool m_bEnd = false;

    bool m_bESC = false;
    public bool _bESC { get { return m_bESC; } set { m_bESC = value; } }
    string Enter = System.Environment.NewLine;

    // Use this for initialization
    void Start()
    {
        m_pLayer = CSLayerMng.I.CreateLayer("FirstLayer", 0);
        m_pPopLayer1 = CSLayerMng.I.CreateLayer("PopLayer1", 100);
        m_pPopLayer2 = CSLayerMng.I.CreateLayer("PopLayer2", 200);

        //m_pScene = CSPrefabMng.I.CreatePrefab(m_pLayer.m_pAnchor, "1_Scene/1_Title/Main_Title", "Main");
        //m_pScene = CSPrefabMng.I.CreatePrefab(m_pLayer.m_pAnchor, "1_Scene/1_Title/TitleMainprefab", "Main");
        m_pScene = CSPrefabMng.I.CreatePrefab(m_pLayer.m_pAnchor, "1_Scene/1_Title/TitleMainPrefab(new)", "Main");

        if (CSSoundMng.I.IsPlay("MrChu") == false)
            CSSoundMng.I.Play("MrChu", true);

        if (JAGameDataFile.I.GetData_Bool(JAGameDataFile.I._sFirstGameStr) == true &&  JAGameDataFile.I.GetData_Bool("TutoCheck") == false )// JAMenuData_Mng.I._bTutorialCheck == false)
        {
            //Debug.Log("asd");
            CSPrefabMng.I.CreatePrefab(m_pPopLayer1.m_pAnchor, "2_Objects/Popup/Pop_Black", "Pop_BlackScreen");
            StartCoroutine(Tutorial_Delay());
            return;
        } 
        else
        {
            if (JAGameDataFile.I.GetData_Bool(JAGameDataFile.I._sFirstGameStr) == false && JAGameDataFile.I.GetData_Bool("TutoCheck") == true)
            {
                m_pPopTutorial.pObj = CSPrefabMng.I.CreatePrefab(m_pPopLayer1.m_pAnchor, "2_Objects/Popup/Pop_TutorialMenu", "Pop_TutorialMenu");
                m_pPopTutorial.pSrc = m_pPopTutorial.pObj.GetComponent<JHMessageTutorial_Menu>();
                m_pPopTutorial.pSrc.Enter();
                
            }
            else
            {
                if ( JAGameDataFile.I.GetData_Bool("AppBanner") == true )
                {
                    string sAppTitle = "게임을 평가해 주세요!";
                    string sAppText = "";
                    sAppText += "돈쏘나기를 플레이 해주셔서" + Enter + "감사합니다." + Enter;
                    sAppText += "저희 게임에 후기와 평점을" + Enter + "피드백 해주시면" + Enter;
                    sAppText += "[i]5000G[/i]를 드립니다!";

                    JAMenuData_Mng.I.CreatePopup(m_pPopLayer1.m_pAnchor, "Pop_AppPlay", sAppTitle, sAppText, E_JA_POPUP.E_POP_ALL, "JAAppBannerBtn", "JAAppBannerCancelBtn");
                }

                if (JAMenuData_Mng.I.m_pPopupData_String.GetPopEnable() == true)
                    JAMenuData_Mng.I.CreatePopup(m_pPopLayer2.m_pAnchor, "GooglePop_0", JAMenuData_Mng.I.m_pPopupData_String.m_sTitle, JAMenuData_Mng.I.ReplaceNewLine(JAMenuData_Mng.I.m_pPopupData_String.m_sText), E_JA_POPUP.E_POP_OK);
            }
        }

        //JHTitle_MainLayer.I.Enter();
       
    }

    IEnumerator Tutorial_Delay()
    {
        yield return new WaitForSeconds(1.2f);

        JAMenuData_Mng.I.CreatePopup(m_pPopLayer2.m_pAnchor, "Pop_Tutorial", "튜토리얼", "처음이구나!\n\n사용법을 익히러 가자 냥!", E_JA_POPUP.E_POP_OK, "JATutorialBtn");
    }

    // Update is called once per frame
    void Update()
    {

        if (m_bEnd == true) return;
        if (m_bESC == true) return;
        if (m_bESC == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                JAMenuData_Mng.I.CreatePopup(m_pPopLayer2.m_pAnchor, "Pop_GoBackground", "종 료", "게임을 정말 종료하시겠습니까?", E_JA_POPUP.E_POP_ALL, "JAEscBtn");
            }
        }

        //if(Input.GetMouseButtonUp(0))
        //{

        //    m_bEnd = true;
        //    CSSoundMng.I.AllStop();
        //    Debug.Log("NExt Scene");
        //    AutoFade.LoadLevel(2, 1.0f, 1.0f, Color.white);

        //}
    }
}
