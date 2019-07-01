using UnityEngine;
using System.Collections;
using LitJson;


public class JAPopupData_String : MonoBehaviour
{
    #region ## 구글 관련 ##
    public string m_sWebURL = "https://script.google.com/macros/s/AKfycbynJltU3lTd2s-RAXBTSfJ80BjvkUyhabETHA2v93pe_zQbLtuq/exec";
    public string m_sSheetID = "1rij-SGneG2RHUtvScXmRTpKpIfRqI1eJunsGn6xy5D8";
    public string m_sSheetName = "TextData";
    public string m_sPassword = "dkagh1234.";
    public float m_fLoadWaitTime = 30f;
    //public bool m_bDebugMod = false;

    public bool m_bAutoUpdate = true;
    private bool m_bUpdate;
    private string m_sCurrStat;
    bool m_bSave;
    JsonData[] m_pObject;
    #endregion

    public string m_sTableName = "Korean";
    private string m_sField1;
    private string m_sField2;

    public string m_sTitle;
    public string m_sText;
    public bool m_bPopEnable;

    public bool m_bDone;

    public void Enter()
    {
        //m_bAutoUpdate = true;

        if ( m_bAutoUpdate )
        {
            //Debug.Log("Google Enter");
            m_bUpdate = false;
            m_bSave = false;
            m_bDone = false;

            m_sCurrStat = "Offline";
            ConnectGoogle();
        }

    }

    void ConnectGoogle()
    {
        if (m_bUpdate)
            return;

        //Debug.Log("Google Connect");
        m_bUpdate = true;
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        m_sField1 = m_sTableName + "1";
        m_sField2 = m_sTableName + "2";
        //Debug.Log("Field1 = " + m_sField1 + "  /  " + "Field2 = " + m_sField2);

        string connectionString = m_sWebURL + "?ssid=" + m_sSheetID + "&sheet=" + m_sSheetName + "&pass=" + m_sPassword + "&action=GetData";
        Debug.Log("Connecting to webservice on " + connectionString);

        WWW www = new WWW(connectionString);

        float elapsedTime = 0.0f;
        m_sCurrStat = "Stablishing Connection... ";

        while (!www.isDone)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= m_fLoadWaitTime)
            {
                m_sCurrStat = "Max wait time reached, connection aborted.";
                Debug.Log(m_sCurrStat);
                m_bUpdate = false;
                JAMenuData_Mng.I.CreatePopup(JHStart_Scene.I._PopLayer.m_pAnchor, "GooglePop_Error", "연결실패", "죄송합니다!\n서버접속에 실패했습니다.\n 다시 시도해주세요!\nwait time connection aborted.", E_JA_POPUP.E_POP_OK, "JAReturnBtn");
                break;
            }

            yield return null;
        }

        if (!www.isDone || !string.IsNullOrEmpty(www.error))
        {
            m_sCurrStat = "Connection error after" + elapsedTime.ToString() + "seconds: " + www.error;
            Debug.LogError(m_sCurrStat);
            m_bUpdate = false;
            yield break;
        }

        string response = www.text;
        Debug.Log(elapsedTime + " : " + response);
        m_sCurrStat = "Connection stablished, parsing data...";

        if (response == "\"Incorrect Password.\"")
        {
            m_sCurrStat = "Connection error: Incorrect Password.";
            Debug.LogError(m_sCurrStat);
            m_bUpdate = false;
            JAMenuData_Mng.I.CreatePopup(JHStart_Scene.I._PopLayer.m_pAnchor, "GooglePop_Error", "연결실패", "죄송합니다!\n서버접속에 실패했습니다.\n다시 시도해주세요!\nIncorrect Password.", E_JA_POPUP.E_POP_OK, "JAReturnBtn");
            yield break;
        }

        try
        {
            m_pObject = JsonMapper.ToObject<JsonData[]>(response);
        }
        catch
        {
            m_sCurrStat = "Data error: could not parse retrieved data as json.";
            Debug.LogError(m_sCurrStat);
            m_bUpdate = false;
            JAMenuData_Mng.I.CreatePopup(JHStart_Scene.I._PopLayer.m_pAnchor, "GooglePop_Error", "연결실패", "죄송합니다!\n서버접속에 실패했습니다.\n다시 시도해주세요!\nData error", E_JA_POPUP.E_POP_OK, "JAReturnBtn");
            yield break;
        }

        m_sCurrStat = "Data Successfully Retrieved!";
        m_bUpdate = false;

        //! PopupString
        PopupString();        
        //@ PopupString END
        JAMenuData_Mng.I.SetLoadString("팝업 정보 읽는다 냥..");

        m_bDone = true;
    }

    void Update()
    {

    }

    public void PopupString()
    {
        m_bPopEnable = false;
        if (m_pObject[0].Keys.Contains(m_sField1))
            m_sTitle = m_pObject[0][m_sField1].ToString();

        if (m_pObject[0].Keys.Contains(m_sField2))
            m_sText = m_pObject[0][m_sField2].ToString();

        if (m_pObject[0].Keys.Contains("PopEnable"))
        {
            if (m_pObject[0]["PopEnable"].ToString() == "1")
                m_bPopEnable = true;
        }
    }

    public string GetTitle()
    {
        return m_sTitle;
    }

    public string GetText()
    {
        return m_sText;
    }

    public bool GetPopEnable()
    {
        return m_bPopEnable;
    }
}
