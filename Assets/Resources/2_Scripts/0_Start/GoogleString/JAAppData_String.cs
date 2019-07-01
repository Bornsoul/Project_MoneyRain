using UnityEngine;
using System.Collections;
using LitJson;

public class JAAppData_String : MonoBehaviour
{
    #region ## 구글 관련 ##
    public string m_sWebURL = "https://script.google.com/macros/s/AKfycbynJltU3lTd2s-RAXBTSfJ80BjvkUyhabETHA2v93pe_zQbLtuq/exec";
    public string m_sSheetID = "1ErT291AFavMmq5RPnc2_4OJcZhOxcyWqhKToNVQs-h0";
    public string m_sSheetName = "AppData";
    public string m_sPassword = "dkagh1234.";
    public float m_fLoadWaitTime = 30f;
    //public bool m_bDebugMod = false;

    public bool m_bAutoUpdate = true;
    private bool m_bUpdate;
    private string m_sCurrStat;
    bool m_bSave;
    JsonData[] m_pObject;
    #endregion

    private string[] m_sFields;

    public string m_sCurVer;
    public string m_sTitle;
    public string m_sText;
    public string m_sDownload;
    public string m_sAppPath;

    public bool m_bDone;

    public void Enter()
    {
        //m_bAutoUpdate = true;

        if (m_bAutoUpdate)
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

        //! String
        String();
        //@ String END
        JAMenuData_Mng.I.SetLoadString("앱 정보 읽는다 냥..");


        m_bDone = true;
    }

    void Update()
    {

    }

    public void String()
    {
        m_sFields = new string[] { "CurVer", "Title", "Text", "Download", "AppPath" };

        if (m_pObject[0].Keys.Contains(m_sFields[0]))
        {
            m_sCurVer = m_pObject[0][m_sFields[0]].ToString();
        }

        if (m_pObject[0].Keys.Contains(m_sFields[1]))
        {
            m_sTitle = m_pObject[0][m_sFields[1]].ToString();
        }

        if (m_pObject[0].Keys.Contains(m_sFields[2]))
        {
            m_sText = m_pObject[0][m_sFields[2]].ToString();
        }

        if ( m_pObject[0].Keys.Contains(m_sFields[3]))
        {
            m_sDownload = m_pObject[0][m_sFields[3]].ToString();
        }

        if (m_pObject[0].Keys.Contains(m_sFields[4]))
        {
            m_sAppPath = m_pObject[0][m_sFields[4]].ToString();
        }

    }


    public string GetCurVer()
    {
        return m_sCurVer;
    }

    public string GetTitle()
    {
        return m_sTitle;
    }

    public string GetText()
    {
        return m_sText;
    }

    public string GetDownload()
    {
        return m_sDownload;
    }

    public string GetAppPath()
    {
        return m_sAppPath;
    }
}