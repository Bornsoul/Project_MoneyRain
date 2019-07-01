using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JAGameDataFile : CSSingleton<JAGameDataFile>
{
    //! 현재 앱 버전
    string m_sAppVersion = "1";
    public string _sAppVersion { get { return m_sAppVersion; } set { m_sAppVersion = value; } }

    //! 저장 상태
    bool m_bSaveData = false;
    public bool m_bSaveCheck = false;

    //! 플레이어 정보
    int m_nPlayerMoney = 0;
    bool m_bFirstGame = true;

    //! 설정 정보
    bool m_bTouchMod = false;
    bool m_bSoundMod = false;
    bool m_bVibMod = false;
    
    //! 캐릭터 업그레이드
    int m_nSMoneyWorth = 0;
    int m_nSSpeed = 0;
    int m_nSJumpPower = 0;

    //! 집 업그레이드
    int m_nSHp = 0;
    int m_nSMoneyMany = 0;
    int m_nHouseLevel = 0;
    float m_fHouseExp = 0f;

    //! 튜토리얼
    bool m_bTutorialCheck = false;

    //! 트로피
    int m_nTropiMaxCount = 10;
    bool[] m_bTropiList;

    //! 배너 팝업
    int m_nBannerMaxCount = 2;
    bool[] m_bBanners;
    bool m_bAppBanner;
    

    string m_sSaveDataStr = "SaveData001";
    string m_sFirstGameStr = "FirstGame001";

    public string _sSaveDataStr { get { return m_sSaveDataStr; } set { m_sSaveDataStr = value; } }
    public string _sFirstGameStr { get { return m_sFirstGameStr; } set { m_sFirstGameStr = value; } }

    public int _nTropiMaxCount { get { return m_nTropiMaxCount; } set { m_nTropiMaxCount = value; } }
    public int _nBannerMaxCount { get { return m_nBannerMaxCount; } set { m_nBannerMaxCount = value; } }
    void Init()
    {
        //! 이곳에 변수값 초기화합니다.

        //! 저장 상태
        //m_bSaveCheck = true;
        m_bSaveData = !m_bSaveCheck;  //!< 저장할지 말지 결정, JAManager 에서 설정 ( false = 저장안합 , true = 저장함 )

        //! 플레이어 정보
        m_nPlayerMoney = 3500;
        m_bFirstGame = true;
        
        //! 설정 정보
        m_bTouchMod = true;
        m_bSoundMod = true;
        m_bVibMod = true;

        //! 캐릭터 업그레이드
        m_nSMoneyWorth = (int)JAMenuData_Mng.I.m_pStatData_String.GetStat(E_JA_STAT.E_S_MONEY_WORTH);
        m_nSSpeed = (int)JAMenuData_Mng.I.m_pStatData_String.GetStat(E_JA_STAT.E_S_SPEED);
        m_nSJumpPower = (int)JAMenuData_Mng.I.m_pStatData_String.GetStat(E_JA_STAT.E_S_JUMP_POWER);

        //! 집 업그레이드
        m_nSHp = (int)JAMenuData_Mng.I.m_pStatData_String.GetStat(E_JA_STAT.E_S_HP);
        m_nSMoneyMany = (int)JAMenuData_Mng.I.m_pStatData_String.GetStat(E_JA_STAT.E_S_MONEY_MANY);
        m_nHouseLevel = 0;
        m_fHouseExp = 0f;

        //! 튜토리얼
        m_bTutorialCheck = false;

        //! 트로피
        m_nTropiMaxCount = 10;
        m_bTropiList = new bool[m_nTropiMaxCount];
        
        //! 배너
        /* 0 = 페북
         * 1 = 쉐어
         */
        m_nBannerMaxCount = 2;
        m_bBanners = new bool[m_nBannerMaxCount];
        m_bAppBanner = false;
    }

    public void Enter()
    {
        //! 이곳에 변수값 저장합니다.
        if (JAMenuData_Mng.I == null) return;
        Init();
        SetData("JHSaveFuckUp", 1818);// New


        SetData(m_sSaveDataStr, m_bSaveData);

        SetData("PlayerMoney", m_nPlayerMoney);
        SetData(m_sFirstGameStr, m_bFirstGame);

        SetData("TouchMod", m_bTouchMod);
        SetData("SoundMod", m_bSoundMod);
        SetData("VibMod", m_bVibMod);

        SetData("SMoneyWorth", m_nSMoneyWorth);
        SetData("SSpeed", m_nSSpeed);
        SetData("SJumpPower", m_nSJumpPower);

        SetData("SHp", m_nSHp);
        SetData("SMoneyMany", m_nSMoneyMany);
        SetData("HouseLevel", m_nHouseLevel);
        SetData("HouseExp", m_fHouseExp);

        SetData("TutoCheck", m_bTutorialCheck);

        for (int i = 0; i < m_nTropiMaxCount; i++)
        {
            m_bTropiList[i] = false;
            SetData("Tropi10" + i, m_bTropiList[i]);
        }

        for (int i = 0; i < m_nBannerMaxCount; i++)
        {
            m_bBanners[i] = false;
            SetData("Banner" + i, m_bBanners[i]);
        }

        SetData("AppBanner", m_bAppBanner);

        //Debug.Log("SaveData : " + GetData_Bool(m_sSaveDataStr));
        //Debug.Log("FirstGame : " + GetData_Bool(m_sFirstGameStr));
    }

    public void EndingReset()
    {
        Init();

        SetData(m_sSaveDataStr, m_bSaveData);

        SetData("PlayerMoney", m_nPlayerMoney);
        SetData(m_sFirstGameStr, false);

        SetData("TouchMod", true);
        SetData("SoundMod", true);
        SetData("VibMod", true);

        SetData("SMoneyWorth", (int)JAMenuData_Mng.I.m_pStatData_String.GetStat(E_JA_STAT.E_S_MONEY_WORTH));
        SetData("SSpeed", (int)JAMenuData_Mng.I.m_pStatData_String.GetStat(E_JA_STAT.E_S_SPEED));
        SetData("SJumpPower", (int)JAMenuData_Mng.I.m_pStatData_String.GetStat(E_JA_STAT.E_S_JUMP_POWER));

        SetData("SHp", (int)JAMenuData_Mng.I.m_pStatData_String.GetStat(E_JA_STAT.E_S_HP));
        SetData("SMoneyMany", (int)JAMenuData_Mng.I.m_pStatData_String.GetStat(E_JA_STAT.E_S_MONEY_MANY));
        SetData("HouseLevel", 0);
        SetData("HouseExp", 0f);
        ////

        SetData("_HighScore", 0);
        SetData("_AllPlayTime", 0);
        SetData("_CurrScore", 0);
        SetData("_HouseLevel", 0);


        SetData("_JumpCount", 0);
        SetData("_HitBallCount", 0);
        SetData("_HitCowCount", 0);
        SetData("_FirstMoneyGet", 0);

        SetData("_Hidden_HitBallCombo", 0);
        SetData("_Hidden_FullUpgrade", 0);
        SetData("_HiddenNotHitAllType", 0);

        SetData("TutoCheck", m_bTutorialCheck);

        for (int i = 0; i < m_nBannerMaxCount; i++)
        {
            m_bBanners[i] = false;
            SetData("Banner" + i, m_bBanners[i]);
        }

        SetData("AppBanner", false);
    }

    public void AllReset()
    {
        Init();

        SetData(m_sSaveDataStr, m_bSaveData);

        SetData("PlayerMoney", m_nPlayerMoney);
        SetData(m_sFirstGameStr, true);

        SetData("TouchMod", true);
        SetData("SoundMod", true);
        SetData("VibMod", true);

        SetData("SMoneyWorth", (int)JAMenuData_Mng.I.m_pStatData_String.GetStat(E_JA_STAT.E_S_MONEY_WORTH));
        SetData("SSpeed", (int)JAMenuData_Mng.I.m_pStatData_String.GetStat(E_JA_STAT.E_S_SPEED));
        SetData("SJumpPower", (int)JAMenuData_Mng.I.m_pStatData_String.GetStat(E_JA_STAT.E_S_JUMP_POWER));

        SetData("SHp", (int)JAMenuData_Mng.I.m_pStatData_String.GetStat(E_JA_STAT.E_S_HP));
        SetData("SMoneyMany", (int)JAMenuData_Mng.I.m_pStatData_String.GetStat(E_JA_STAT.E_S_MONEY_MANY));
        SetData("HouseLevel", 0);
        SetData("HouseExp", 0f);
        ////

        SetData("_HighScore", 0);
        SetData("_AllPlayTime", 0);
        SetData("_CurrScore", 0);
        SetData("_HouseLevel", 0);
        
        
        SetData("_JumpCount", 0);
        SetData("_HitBallCount", 0);
        SetData("_HitCowCount", 0);
        SetData("_FirstMoneyGet", 0);
        
        SetData("_Hidden_HitBallCombo", 0);
        SetData("_Hidden_FullUpgrade", 0);
        SetData("_HiddenNotHitAllType", 0);

        SetData("TutoCheck", m_bTutorialCheck);

        for (int i = 0; i < m_nTropiMaxCount; i++)
        {
            m_bTropiList[i] = false;
            SetData("Tropi10" + i, m_bTropiList[i]);
        }

        for (int i = 0; i < m_nBannerMaxCount; i++)
        {
            m_bBanners[i] = false;
            SetData("Banner" + i, m_bBanners[i]);
        }

        SetData("AppBanner", false);
    }

    public void SetData(string sKeyName, string Value)
    {
        PlayerPrefs.SetString(sKeyName, Value);        
    }

    public void SetData(string sKeyName, float Value)
    {
        PlayerPrefs.SetFloat(sKeyName, Value);
    }

    public void SetData(string sKeyName, int Value)
    {
        PlayerPrefs.SetInt(sKeyName, Value);
    }

    public void SetData(string sKeyName, bool Value)
    {
        PlayerPrefs.SetInt(sKeyName, Value ? 1 : 0);
    }



    public string GetData_String(string sKeyName)
    {
        return PlayerPrefs.GetString(sKeyName);
    }

    public string GetData_Int(string sKeyName, string Value)
    {
        if (PlayerPrefs.HasKey(sKeyName))
        {
            return GetData_String(sKeyName);
        }
        return Value;
    }

    public float GetData_Float(string sKeyName)
    {
        return PlayerPrefs.GetFloat(sKeyName);
    }

    public float GetData_Int(string sKeyName, float Value)
    {
        if (PlayerPrefs.HasKey(sKeyName))
        {
            return GetData_Float(sKeyName);
        }
        return Value;
    }

    public int GetData_Int(string sKeyName)
    {
        return PlayerPrefs.GetInt(sKeyName);
    }

    public int GetData_Int(string sKeyName, int Value)
    {
        if ( PlayerPrefs.HasKey(sKeyName))
        {
            return GetData_Int(sKeyName);
        }
        return Value;
    }

    public bool GetData_Bool(string sKeyName)
    {
        return PlayerPrefs.GetInt(sKeyName) == 1 ? true : false;
    }

    public bool GetData_Bool(string sKeyName, bool Value)
    {
        if (PlayerPrefs.HasKey(sKeyName))
        {
            return GetData_Bool(sKeyName);
        }

        return Value;
    }

}
