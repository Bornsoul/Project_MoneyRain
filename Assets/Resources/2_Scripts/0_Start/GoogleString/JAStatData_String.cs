using UnityEngine;
using System.Collections;
using LitJson;

public class JAStatData_String : MonoBehaviour
{
    #region ## 구글 관련 ##
    public string m_sWebURL = "https://script.google.com/macros/s/AKfycbynJltU3lTd2s-RAXBTSfJ80BjvkUyhabETHA2v93pe_zQbLtuq/exec";
    public string m_sSheetID = "1TJ-zKtER3_vPy8XZHLXgAgu-fF_B_Ue0AxKd5KaP5LA";
    public string m_sSheetName = "Data";
    public string m_sPassword = "dkagh1234.";
    public float m_fLoadWaitTime = 30f;
    //public bool m_bDebugMod = false;

    public bool m_bAutoUpdate = true;
    private bool m_bUpdate;
    private string m_sCurrStat;
    bool m_bSave;
    JsonData[] m_pObject;
    #endregion

    private string[] m_sFields1;
    private string[] m_sFields2;

    public string[] m_sBase;
    public string[] m_sStat;
    public string[] m_sFactor;

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

        //! LoadData
        String();
        //@ LoadData END
        JAMenuData_Mng.I.SetLoadString("스텟 정보 읽는다 냥..");

        m_bDone = true;
    }

    void Update()
    {

    }

    public void String()
    {

        m_sFields1 = new string[] { "Speed", "JumpPower", "MoneyMany", "MoneyTime", "Hp" };
        m_sFields2 = new string[] { "MoneyWorth", "Speed", "JumpPower", "Hp", "MoneyMany" };


        m_sBase = new string[5];
        m_sStat = new string[5];
        m_sFactor = new string[5];

        for (int i = 0; i < m_sFields1.Length; i++)
        {
            if ( m_pObject[0].Keys.Contains("B" + m_sFields1[i]))
            {
                m_sBase[i] = m_pObject[0]["B"+m_sFields1[i]].ToString();
            }
        }

        for (int i = 0; i < m_sFields2.Length; i++)
        {
            if (m_pObject[0].Keys.Contains("S" + m_sFields2[i]))
            {
                m_sStat[i] = m_pObject[0]["S" + m_sFields2[i]].ToString();
            }
        }

        for (int i = 0; i < m_sFields1.Length; i++)
        {
            if (m_pObject[0].Keys.Contains("F" + m_sFields1[i]))
            {
                m_sFactor[i] = m_pObject[0]["F" + m_sFields1[i]].ToString();
            }
        }
    }


    /// <summary>
    /// 기본능력치의 값을 가져온다.
    /// </summary>
    /// <param name="eState"></param>
    /// <returns></returns>
    public float GetBase(E_JA_BASE eState)
    {
        return System.Convert.ToSingle(m_sBase[(int)eState]);
    }
    public float GetBase(int nState)
    {
        return System.Convert.ToSingle(m_sBase[nState]);
    }

    /// <summary>
    /// 업글 횟수의 값을 가져온다.
    /// </summary>
    /// <param name="eState"></param>
    /// <returns></returns>
    public float GetStat(E_JA_STAT eState)
    {
        return System.Convert.ToSingle(m_sStat[(int)eState]);
        
    }
    public float GetStat(int nState)
    {

        return System.Convert.ToSingle(m_sStat[nState]);

    }
    /// <summary>
    /// 수치값을 가져온다.
    /// </summary>
    /// <param name="eState"></param>
    /// <returns></returns>
    public float GetFactor(E_JA_FACTOR eState)
    {
        return System.Convert.ToSingle(m_sFactor[(int)eState]);
    }

    public float GetFactor(int bState)
    {
        return System.Convert.ToSingle(m_sFactor[bState]);
    }
}
