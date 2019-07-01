using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class JHUserSaveData
{
    Dictionary<string, int> m_pDataList;
    public bool m_bLogin;

    public void Enter()
    {
        m_bLogin = false;

        m_pDataList = new Dictionary<string, int>();

        //! 필수 변수(예: 최고점수, 집 단계 등)
        m_pDataList.Add("_HighScore", JAGameDataFile.I.GetData_Int("_HighScore"));
        m_pDataList.Add("_AllPlayTime", JAGameDataFile.I.GetData_Int("_AllPlayTime"));
        m_pDataList.Add("_CurrScore", JAGameDataFile.I.GetData_Int("_CurrScore"));
        m_pDataList.Add("_HouseLevel", JAGameDataFile.I.GetData_Int("_HouseLevel"));

        //! 업적 관련 잡다 변수
        m_pDataList.Add("_JumpCount", JAGameDataFile.I.GetData_Int("_JumpCount"));
        m_pDataList.Add("_HitBallCount", JAGameDataFile.I.GetData_Int("_HitBallCount"));
        m_pDataList.Add("_HitCowCount", JAGameDataFile.I.GetData_Int("_HitCowCount"));
        m_pDataList.Add("_FirstMoneyGet", JAGameDataFile.I.GetData_Int("_FirstMoneyGet"));

        m_pDataList.Add("_Hidden_HitBallCombo", JAGameDataFile.I.GetData_Int("_Hidden_HitBallCombo"));
        m_pDataList.Add("_Hidden_FullUpgrade", JAGameDataFile.I.GetData_Int("_Hidden_FullUpgrade"));
        m_pDataList.Add("_HiddenNotHitAllType", JAGameDataFile.I.GetData_Int("_HiddenNotHitAllType"));


       // CSDirector.I.GetDeviceDebug().Log("@@" + JAGameDataFile.I.GetData_Int("_JumpCount"));
       // CSDirector.I.GetDeviceDebug().Log("@@" + JAGameDataFile.I.GetData_Int("_HitBallCount"));
    }

    public void Save()
    {
        JAGameDataFile.I.SetData("_HighScore", GetData("_HighScore"));
        JAGameDataFile.I.SetData("_AllPlayTime", GetData("_AllPlayTime"));
        JAGameDataFile.I.SetData("_CurrScore", GetData("_CurrScore"));
        JAGameDataFile.I.SetData("_HouseLevel", GetData("_HouseLevel"));

        JAGameDataFile.I.SetData("_JumpCount", GetData("_JumpCount"));
        JAGameDataFile.I.SetData("_HitBallCount", GetData("_HitBallCount"));
        JAGameDataFile.I.SetData("_HitCowCount", GetData("_HitCowCount"));
        JAGameDataFile.I.SetData("_FirstMoneyGet", GetData("_FirstMoneyGet"));

        JAGameDataFile.I.SetData("_Hidden_HitBallCombo", GetData("_Hidden_HitBallCombo"));
        JAGameDataFile.I.SetData("_Hidden_FullUpgrade", GetData("_Hidden_FullUpgrade"));
        JAGameDataFile.I.SetData("_HiddenNotHitAllType", GetData("_HiddenNotHitAllType"));
    }

    public void Destroy()
    {
        m_pDataList.Clear();
        m_pDataList = null;
    }

    public int GetData(string sName)
    {
        if (m_pDataList.ContainsKey(sName))
        {
            return m_pDataList[sName];
        }
        return -1;
    }

    public void SetData(string sName, int nValue)
    {
        if (m_pDataList.ContainsKey(sName))
        {
            m_pDataList[sName] = nValue;
        }
    }

    public void PlusData(string sName)
    {
        if (m_pDataList.ContainsKey(sName))
        {
            m_pDataList[sName]++;
        }
    }
}

public class JHUserData_Mng : CSSingleton<JHUserData_Mng> {
    public JHUserSaveData m_pUserData;


	// Use this for initialization
	void Start () {
        m_pUserData = new JHUserSaveData();
        m_pUserData.Enter();
	}

    void OnDestroy()
    {
        m_pUserData.Destroy();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
