using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class JAMenuData_Mng : CSSingleton<JAMenuData_Mng> 
{
    public JAAppData_String m_pAppData_String;
    public JAPopupData_String m_pPopupData_String;
    public JAStatData_String m_pStatData_String;
    public JAHouseData_String m_pHouseData_String;
    public JATropiData_String m_pTropiData_String;

    GameObject m_pObj;

    List<string> m_sLoadStrList = null;
    public List<string> _sLoadStrList { get { return m_sLoadStrList; } set { m_sLoadStrList = value; } }

    int m_nLoadStrCount = 0;
    public int _nLoadStrCount { get { return m_nLoadStrCount; } set { m_nLoadStrCount = value; } }

    int m_nGameMoney = 0;
    public int _nGameMoney { get { return m_nGameMoney; } set { m_nGameMoney = value; } }

    //bool m_bTutorialCheck = false;
    //public bool _bTutorialCheck { get { return m_bTutorialCheck; } set { m_bTutorialCheck = value; } }


    void Start()
    {
        JAGameDataFile.I.SetData(JAGameDataFile.I._sSaveDataStr, true);
        DontDestroyOnLoad(this);
        m_sLoadStrList = new List<string>();
        //m_bAppWidget = true;
        //m_bTutorialCheck = true;
    }

    void Update()
    {
        //if ( Input.GetKeyUp(KeyCode.H))
        //{
        //    m_nGameMoney = 10020;
        //    AutoFade.LoadLevel("5_Result", 0.3f, 0.3f, Color.black);
        //}
    }

    public void SetLoadString(string sStr)
    {
        Debug.Log(sStr);
        m_sLoadStrList.Add(sStr);
        m_nLoadStrCount++;
    }

    /// <summary>
    /// 서버에서 특정 문자를 검사하여 교체해줄떄 사용합니다.
    /// </summary>
    /// <param name="sText"></param>
    /// <param name="sBefore"></param>
    /// <param name="sAfter"></param>
    /// <returns></returns>
    public string ReplaceString(string sText, string sBefore, string sAfter)
    {
        sText = sText.Replace(sBefore, sAfter);
        return sText;
    }

    /// <summary>
    /// 서버에서 [n]문자를 검사하여 개행할때 사용합니다.
    /// </summary>
    /// <param name="sText"></param>
    /// <returns></returns>
    public string ReplaceNewLine(string sText)
    {
        sText = sText.Replace("[n]", System.Environment.NewLine);
        return sText;
    }

    /// <summary>
    /// 숫자를 출력할때 콤바가 같이 출력되게 합니다.
    /// 예:) 1000 -> 1,000
    /// </summary>
    /// <param name="nStartNum"></param>
    /// <returns></returns>
    public string GetIntNumberString(int nStartNum)
    {
        StringBuilder sTotalString = new StringBuilder();
        int nTotal = 0;
        int nLength = 0;
        nTotal = nStartNum;
        nLength = nTotal.ToString().Length;
        for (int i = 0; i < nLength; i++)
        {
            sTotalString.Insert(0, "0");
            if (i % 3 == 0)
                sTotalString.Insert(0, ",");
        }

        return nTotal.ToString(sTotalString.ToString());
    }


    /// <summary>
    /// stringbuilder 로그 출력
    /// </summary>
    /// <param name="sString"></param>
    public void GetLogLong(params string[] sString)
    {
        StringBuilder sStringBD = new StringBuilder();
        for (int i = 0; i < sString.Length; i++)
            sStringBD.Append(sString[i]);

        Debug.Log(sStringBD.ToString());
    }

    /// <summary>
    /// stringbuilder 문장 반환
    /// </summary>
    /// <param name="sString"></param>
    /// <returns></returns>
    public string GetStringLong(params string[] sString)
    {
        StringBuilder sStringBD = new StringBuilder();
        for (int i = 0; i < sString.Length; i++)
            sStringBD.Append(sString[i]);

        return sStringBD.ToString();
    }

    /// <summary>
    /// 팝업창을 띄울때 사용합니다.
    /// </summary>
    /// <param name="pParent"></param>
    /// <param name="sTitleName"></param>
    /// <param name="sTitle"></param>
    /// <param name="sText"></param>
    /// <param name="ePopup"></param>
    public void CreatePopup(GameObject pParent, string sPrefabName, string sTitle, string sText, E_JA_POPUP ePopup = E_JA_POPUP.E_POP_OK, string sAddOKScript = "", string sAddCancelScript = "")
    {
        Debug.Log("Create POPUP");

        CSObjectStruct<JAPopup_Mng> m_pPopup;

        if (Application.loadedLevelName == "0_Start")
            NGUITools.SetActive(JHStart_Scene.I.m_pLoad_Gam, false);

        if (m_pObj == null)
        {
            m_pObj = CSPrefabMng.I.CreatePrefab(pParent, "2_Objects/Popup/Popup", sPrefabName);
            m_pPopup.pObj = m_pObj;
            m_pPopup.pSrc = m_pPopup.pObj.GetComponent<JAPopup_Mng>();
            m_pPopup.pSrc.Enter(sTitle, sText, true);

            if (sAddOKScript != "")
                m_pPopup.pSrc.m_pOKBtnGam.AddComponent(sAddOKScript);

            if (sAddCancelScript != "")
                m_pPopup.pSrc.m_pCancelBtnGam.AddComponent(sAddCancelScript);

            switch (ePopup)
            {
                case E_JA_POPUP.E_POP_OK:
                    m_pPopup.pSrc.m_pOKBtnGam.transform.localPosition = new Vector3(0, 0, 1);
                    NGUITools.SetActive(m_pPopup.pSrc.m_pCancelBtnGam, false);
                    break;
                case E_JA_POPUP.E_POP_CANCEL:
                    m_pPopup.pSrc.m_pCancelBtnGam.transform.localPosition = new Vector3(0, 0, 1);
                    NGUITools.SetActive(m_pPopup.pSrc.m_pOKBtnGam, false);
                    break;
                case E_JA_POPUP.E_POP_DONT:
                    NGUITools.SetActive(m_pPopup.pSrc.m_pCancelBtnGam, false);
                    NGUITools.SetActive(m_pPopup.pSrc.m_pOKBtnGam, false);
                    break;
                case E_JA_POPUP.E_POP_ALL:
                    m_pPopup.pSrc.m_pOKBtnGam.transform.localPosition = new Vector3(130, 0, 1);
                    m_pPopup.pSrc.m_pCancelBtnGam.transform.localPosition = new Vector3(-130, 0, 1);
                    break;

            }
        }

    }

    public void CreatePopup(GameObject pParent, string sPrefabName, string sTitle, string sText, bool bAni = true, E_JA_POPUP ePopup = E_JA_POPUP.E_POP_OK, string sAddOKScript = "", string sAddCancelScript = "")
    {
        Debug.Log("Create POPUP");

        CSObjectStruct<JAPopup_Mng> m_pPopup;

        if (Application.loadedLevelName == "0_Start")
            NGUITools.SetActive(JHStart_Scene.I.m_pLoad_Gam, false);

        if (m_pObj == null)
        {
            m_pObj = CSPrefabMng.I.CreatePrefab(pParent, "2_Objects/Popup/Popup", sPrefabName);
            m_pPopup.pObj = m_pObj;
            m_pPopup.pSrc = m_pPopup.pObj.GetComponent<JAPopup_Mng>();
            m_pPopup.pSrc.Enter(sTitle, sText, bAni);

            if (sAddOKScript != "")
                m_pPopup.pSrc.m_pOKBtnGam.AddComponent(sAddOKScript);

            if (sAddCancelScript != "")
                m_pPopup.pSrc.m_pCancelBtnGam.AddComponent(sAddCancelScript);

            switch (ePopup)
            {
                case E_JA_POPUP.E_POP_OK:
                    m_pPopup.pSrc.m_pOKBtnGam.transform.localPosition = new Vector3(0, 0, 1);
                    NGUITools.SetActive(m_pPopup.pSrc.m_pCancelBtnGam, false);
                    break;
                case E_JA_POPUP.E_POP_CANCEL:
                    m_pPopup.pSrc.m_pCancelBtnGam.transform.localPosition = new Vector3(0, 0, 1);
                    NGUITools.SetActive(m_pPopup.pSrc.m_pOKBtnGam, false);
                    break;
                case E_JA_POPUP.E_POP_DONT:
                    NGUITools.SetActive(m_pPopup.pSrc.m_pCancelBtnGam, false);
                    NGUITools.SetActive(m_pPopup.pSrc.m_pOKBtnGam, false);
                    break;
                case E_JA_POPUP.E_POP_ALL:
                    m_pPopup.pSrc.m_pOKBtnGam.transform.localPosition = new Vector3(130, 0, 1);
                    m_pPopup.pSrc.m_pCancelBtnGam.transform.localPosition = new Vector3(-130, 0, 1);
                    break;

            }
        }

    }

    public GameObject CreatePopupGam(GameObject pParent, string sPrefabName, string sTitle, string sText, E_JA_POPUP ePopup = E_JA_POPUP.E_POP_OK, string sAddOKScript = "", string sAddCancelScript = "")
    {
        Debug.Log("Create POPUP");

        CSObjectStruct<JAPopup_Mng> m_pPopup;
        if (Application.loadedLevelName == "0_Start")
            NGUITools.SetActive(JHStart_Scene.I.m_pLoad_Gam, false);

        if (m_pObj == null)
        {
            m_pObj = CSPrefabMng.I.CreatePrefab(pParent, "2_Objects/Popup/Popup", sPrefabName);
            m_pPopup.pObj = m_pObj;
            m_pPopup.pSrc = m_pPopup.pObj.GetComponent<JAPopup_Mng>();
            m_pPopup.pSrc.Enter(sTitle, sText, true);

            if (sAddOKScript != "")
                m_pPopup.pSrc.m_pOKBtnGam.AddComponent(sAddOKScript);

            if (sAddCancelScript != "")
                m_pPopup.pSrc.m_pCancelBtnGam.AddComponent(sAddCancelScript);

            switch (ePopup)
            {
                case E_JA_POPUP.E_POP_OK:
                    m_pPopup.pSrc.m_pOKBtnGam.transform.localPosition = new Vector3(0, 0, 1);
                    NGUITools.SetActive(m_pPopup.pSrc.m_pCancelBtnGam, false);
                    break;
                case E_JA_POPUP.E_POP_CANCEL:
                    m_pPopup.pSrc.m_pCancelBtnGam.transform.localPosition = new Vector3(0, 0, 1);
                    NGUITools.SetActive(m_pPopup.pSrc.m_pOKBtnGam, false);
                    break;
                case E_JA_POPUP.E_POP_DONT:
                    NGUITools.SetActive(m_pPopup.pSrc.m_pCancelBtnGam, false);
                    NGUITools.SetActive(m_pPopup.pSrc.m_pOKBtnGam, false);
                    break;
                case E_JA_POPUP.E_POP_ALL:
                    m_pPopup.pSrc.m_pOKBtnGam.transform.localPosition = new Vector3(130, 0, 1);
                    m_pPopup.pSrc.m_pCancelBtnGam.transform.localPosition = new Vector3(-130, 0, 1);
                    break;

            }
        }

        return m_pObj;
    }
}
