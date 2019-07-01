using UnityEngine;
using System.Collections;
using LitJson;

public class JATropiData_String : MonoBehaviour
{

    #region ## 구글 관련 ##
    public string m_sWebURL = "https://script.google.com/macros/s/AKfycbynJltU3lTd2s-RAXBTSfJ80BjvkUyhabETHA2v93pe_zQbLtuq/exec";
    public string m_sSheetID = "1pU2vjiMmJ7aA3PL5HSSbTCcGhoKBVxjj1BV8VnW8DuM";
    public string m_sSheetName = "TropiData";
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

    public string[] m_sID;
    public string[] m_sName;
    public string[] m_sStat;
    public string[] m_sInfo;
    public string[] m_sSpriteName;

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
        JAMenuData_Mng.I.SetLoadString("트로피 정보 읽는다 냥..");

        m_bDone = true;
    }

    void Update()
    {

    }

    public void String()
    {
        m_sFields = new string[] { "nID", "sName", "sStat", "sInfo", "sSpriteName" };

        m_sID = new string[m_pObject.Length];
        m_sName = new string[m_pObject.Length];
        m_sStat = new string[m_pObject.Length];
        m_sInfo = new string[m_pObject.Length];
        m_sSpriteName = new string[m_pObject.Length];

        for (int i = 0; i < m_pObject.Length; i++)
        {
            if (m_pObject[i].Keys.Contains(m_sFields[0]))
            {
                m_sID[i] = m_pObject[i][m_sFields[0]].ToString();
            }
            if (m_pObject[i].Keys.Contains(m_sFields[1]))
            {
                m_sName[i] = m_pObject[i][m_sFields[1]].ToString();
            }
            if (m_pObject[i].Keys.Contains(m_sFields[2]))
            {
                m_sStat[i] = m_pObject[i][m_sFields[2]].ToString();
            }
            if (m_pObject[i].Keys.Contains(m_sFields[3]))
            {
                m_sInfo[i] = m_pObject[i][m_sFields[3]].ToString();
            }
            if (m_pObject[i].Keys.Contains(m_sFields[4]))
            {
                m_sSpriteName[i] = m_pObject[i][m_sFields[4]].ToString();
            }
        }
    }

    public int GetID(int nIndex) { return int.Parse(m_sID[nIndex]); }
    public string GetName(int nIndex) { return m_sName[nIndex]; }
    public string GetStat(int nIndex) { return m_sStat[nIndex]; }
    public string GetInfo(int nIndex) { return m_sInfo[nIndex]; }
    public string GetSpriteName(int nIndex) { return m_sSpriteName[nIndex]; }

    

    public string GetName_ID(int nID)
    {
        for ( int i = 0; i<m_pObject.Length; i++)
        {
            if ( nID == GetID(i) )
            {
                return GetName(i);
            }
        }

        return string.Empty;
    }

    public string GetStat_ID(int nID)
    {
        for (int i = 0; i < m_pObject.Length; i++)
        {
            if (nID == GetID(i))
            {
                return GetStat(i);
            }
        }

        return string.Empty;
    }

    public string GetInfo_ID(int nID)
    {
        for (int i = 0; i < m_pObject.Length; i++)
        {
            if (nID == GetID(i))
            {
                return GetInfo(i);
            }
        }

        return string.Empty;
    }

    public string GetSpriteName_ID(int nID)
    {
        for (int i = 0; i < m_pObject.Length; i++)
        {
            if (nID == GetID(i))
            {
                return GetSpriteName(i);
            }
        }

        return string.Empty;
    }
}
