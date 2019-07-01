using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public struct MoneyInfo
{
    public E_MONEY_CLASS eClass;
    public float fStratTime;
    public float fEndTime;
   public MoneyInfo(E_MONEY_CLASS _eClass, float _fStartTime, float _fEndTime)
    {
        eClass = _eClass;
        fStratTime = _fStartTime;
        fEndTime = _fEndTime;
    }
}

public class JHMoney_Mng : MonoBehaviour {
    List<CSObjectStruct<JHMoney_Root>> m_pList = null;
    public List<CSObjectStruct<JHMoney_Root>> _List { get { return m_pList; } }

    bool m_bActive = false;
    public bool _Active { get { return m_bActive; } set { m_bActive = value; } }

    int m_nSpawnCnt = 0;

    float m_fCurrSpawnTime = 0.0f;
    float m_fAgoSpawnTime = 0.08f;

    int m_nMaxMoney = 0;

    List<MoneyInfo> m_pMoneyInfo = null;

    bool  m_bFever = false;
    public bool _Fever { get { return m_bFever; } }
    float m_fFeverTime = 0.0f;

    public void Enter()
    {
        m_pList = new List<CSObjectStruct<JHMoney_Root>>();
        m_nSpawnCnt = 0;
        m_bActive = true;

        m_nMaxMoney = JHGameData_Mng.I.m_pData_Stat._Basic_MoneyMany;
        m_nMaxMoney += JHGameData_Mng.I.m_pData_Stat._Stat_MoneyMany * JHGameData_Mng.I.m_pData_Stat._Factor_MoneyMany;

        m_fAgoSpawnTime = JHGameData_Mng.I.m_pData_Stat._Basic_MoneyTime;
        m_fAgoSpawnTime -= JHGameData_Mng.I.m_pData_Stat._Stat_MoneyMany * JHGameData_Mng.I.m_pData_Stat._Factor_MoneyTime;


        /*   m_stMoneyInfo = new MoneyInfo[(int)E_MONEY_CLASS.E_MAX];
           m_stMoneyInfo[(int)E_MONEY_CLASS.E_COIN_BRONZE] = new MoneyInfo(0.0f, 45.0f);
           m_stMoneyInfo[(int)E_MONEY_CLASS.E_COIN_SILVER] = new MoneyInfo(15.0f, 90.0f);
           m_stMoneyInfo[(int)E_MONEY_CLASS.E_COIN_GOLD] = new MoneyInfo(45.0f, 150.0f);

           m_stMoneyInfo[(int)E_MONEY_CLASS.E_PAPER_BRONZE] = new MoneyInfo(50.0f, 250.0f);
           m_stMoneyInfo[(int)E_MONEY_CLASS.E_PAPER_SILVER] = new MoneyInfo(120.0f, 350.0f);
           m_stMoneyInfo[(int)E_MONEY_CLASS.E_PAPER_GOLD] = new MoneyInfo(220.0f, 500.0f);

           m_pMoneyInfo = new List<MoneyInfo>();
           AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_BRONZE, 0.0f, 45.0f);
           AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_SILVER, 15.0f, 90.0f);
           AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_GOLD, 45.0f, 150.0f);

           AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_BRONZE, 50.0f, 250.0f);
           AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_SILVER, 120.0f, 350.0f);
           AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_GOLD, 220.0f, 700.0f);

           AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_BRONZE, 385.0f, 500.0f);
           AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_SILVER, 420.0f, 600.0f);
           AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_GOLD, 485.0f, 700.0f);*/
        m_pMoneyInfo = new List<MoneyInfo>();
        LevelDesing();
        Debug.Log(m_nMaxMoney + ", " + m_fAgoSpawnTime);
    }

    void LevelDesing()
    {
        int nLevel = JAMenuData_Mng.I.m_pHouseData_String.GetLevel(JAGameDataFile.I.GetData_Int("HouseLevel"));
       // CSDirector.I.GetDeviceDebug().Log(nLevel.ToString());
        nLevel--;  
        //45+(Up*20)  UP기본 1 = 최저 65 최대 205 ( 물략먹을 가능성있음)
        switch (nLevel)
        {
            case 0:
           /*     AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_BRONZE, 0.0f, 105.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_SILVER, 0.0f, 170.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_GOLD, 0.0f, 200.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_BRONZE, 145.0f, 250.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_SILVER, 170.0f, 350.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_GOLD, 220.0f, 700.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_BRONZE, 385.0f, 500.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_SILVER, 420.0f, 600.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_GOLD, 485.0f, 700.0f);*/
                AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_BRONZE, 0.0f, 105.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_SILVER, 35.0f, 170.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_GOLD, 50.0f, 200.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_BRONZE, 145.0f, 250.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_SILVER, 170.0f, 350.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_GOLD, 220.0f, 700.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_BRONZE, 385.0f, 500.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_SILVER, 420.0f, 600.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_GOLD, 485.0f, 700.0f);
                break;
            case 1:
                

               /* AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_BRONZE, 0.0f, 250.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_SILVER,0.0f, 350.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_GOLD, 0.0f, 700.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_BRONZE, 300.0f, 500.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_SILVER, 420.0f, 600.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_GOLD, 485.0f, 700.0f);*/

                AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_BRONZE, 0.0f, 15.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_SILVER, 10.0f, 75.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_GOLD, 35.0f, 245.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_BRONZE, 120.0f, 250.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_SILVER, 180.0f, 350.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_GOLD, 300.0f, 700.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_BRONZE, 300.0f, 500.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_SILVER, 420.0f, 600.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_GOLD, 485.0f, 700.0f);
                break;
            case 2:
               /*  AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_BRONZE, 0.0f, 500.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_SILVER, 0.0f, 600.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_GOLD, 0.0f, 700.0f);*/
                AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_SILVER, 0.0f, 75.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_GOLD, 35.0f, 245.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_BRONZE, 100.0f, 250.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_SILVER, 140.0f, 350.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_GOLD, 180.0f, 700.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_BRONZE, 300.0f, 500.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_SILVER, 420.0f, 600.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_GOLD, 485.0f, 700.0f);
                break;
            case 3:
               /*  AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_BRONZE, 0.0f, 30.0f);
                  AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_SILVER, 0.0f, 65.0f);
                  AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_GOLD, 0.0f, 245.0f);

                  AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_BRONZE, 0.0f, 250.0f);
                  AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_SILVER, 0.0f, 350.0f);
                  AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_GOLD, 0.0f, 700.0f);

                  AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_BRONZE, 0.0f, 500.0f);
                  AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_SILVER, 0.0f, 600.0f);
                  AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_GOLD, 0.0f, 700.0f);*/
                // AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_BRONZE, 0.0f, 30.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_SILVER, 0.0f, 65.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_GOLD, 25.0f, 245.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_BRONZE, 90.0f, 250.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_SILVER, 130.0f, 350.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_GOLD, 150.0f, 700.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_BRONZE, 300.0f, 500.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_SILVER, 420.0f, 600.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_GOLD, 485.0f, 700.0f);

                break;
            case 4:
                // AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_BRONZE, 0.0f, 30.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_SILVER, 0.0f, 65.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_GOLD, 25.0f, 245.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_BRONZE, 90.0f, 250.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_SILVER, 130.0f, 350.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_GOLD, 150.0f, 700.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_BRONZE, 300.0f, 500.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_SILVER, 420.0f, 600.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_GOLD, 485.0f, 700.0f);
                break;
            case 5:
                // AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_BRONZE, 0.0f, 30.0f);
              //  AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_SILVER, 0.0f, 65.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_GOLD, 0.0f, 125.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_BRONZE, 40.0f, 120.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_SILVER, 70.0f, 350.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_GOLD, 100.0f, 700.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_BRONZE, 120.0f, 500.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_SILVER, 420.0f, 600.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_GOLD, 485.0f, 700.0f);
                break;
            case 6:
                // AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_BRONZE, 0.0f, 30.0f);
                //  AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_SILVER, 0.0f, 65.0f);
              //  AddMoneyCreateTime(E_MONEY_CLASS.E_COIN_GOLD, 0.0f, 125.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_BRONZE, 0.0f, 50.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_SILVER, 40.0f, 80.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_GOLD, 55.0f, 700.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_BRONZE, 90.0f, 500.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_SILVER, 110.0f, 600.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_GOLD, 120.0f, 700.0f);
                break;
            case 7:
                  AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_BRONZE, 0.0f, 30.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_SILVER, 30.0f, 70.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_PAPER_GOLD, 55.0f, 110.0f);

                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_BRONZE, 70.0f, 500.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_SILVER, 80.0f, 600.0f);
                AddMoneyCreateTime(E_MONEY_CLASS.E_POCKET_GOLD, 110.0f, 700.0f);
                break;
        }
    }

    public void Destroy()
    {
        m_bActive = false;
        foreach (CSObjectStruct<JHMoney_Root> pObj in m_pList)
        {
            if (pObj.pObj != null)
            {
                if(pObj.pSrc!=null) pObj.pSrc.Destroy();
                CSPrefabMng.I.DestroyPrefab(pObj.pObj);
                continue;
            }
        }
        m_pList.Clear();
        m_pList = null;

        m_pMoneyInfo.Clear();
        m_pMoneyInfo = null;
    }

    void DeActiveCheck()
    {
        bool bCheck = false;
        do
        {
            bCheck = false;
            foreach (CSObjectStruct<JHMoney_Root> pObj in m_pList)
            {
                if (pObj.pObj != null)
                {
                    if (pObj.pSrc._Active == false)
                    {
                        pObj.pSrc.Destroy();
                        CSPrefabMng.I.DestroyPrefab(pObj.pObj);
                        m_pList.Remove(pObj);
                        bCheck = true;
                        break;
                    }
                }
            }
        } while (bCheck);
    }

	// Update is called once per frame
	void Update () {
        if (m_bActive == false) return;
        if (JHGameData_Mng.I._GamePause == true) return;
        if (JHGameData_Mng.I._GameCycle == true) return;
        DeActiveCheck();
        if (JHGameData_Mng.I._TutorialState == true) return;
        MoneyInfoListUpdate();
       // Debug.Log(GetCurrMoneyNum());
       /* if(Input.GetKeyDown(KeyCode.K))
        {
            SetFever();
        }*/
        m_fCurrSpawnTime += Time.deltaTime;
        if (m_bFever == true)
        {
            FeverUpdate();
            return;
        }

        if(GetCurrMoneyNum()<m_nMaxMoney)
        {
            if (m_fCurrSpawnTime > m_fAgoSpawnTime)
            {
                CreateMoney(SelectLevelMoeny());
               // CreateMoney((E_MONEY_CLASS)CSRandom.Rand(0, (int)E_MONEY_CLASS.E_MAX));
                //CreateMoney(E_MONEY_CLASS.E_PAPER_BRONZE);
                m_fCurrSpawnTime = 0.0f;
            }
        }
	}


    public void SetFever()
    {
        m_bFever = true;
        m_fFeverTime = 0.0f;
        JHGame_MainLayer.I.m_pEffect_Mng.CreateEffect_PT_SkyRays();
        JHGame_MainLayer.I.m_pEffect_Mng.CreateEffect_Fever();
        JHGame_MainLayer.I.m_pBG.FeverOff(2.0f);
        JHGame_MainLayer.I.m_pEnemy_Mng.AllHit();
    }

    void FeverUpdate()
    {
        m_fFeverTime += Time.deltaTime;
        if (m_fFeverTime>3.0f)
        {
            m_fFeverTime = 0.0f;
            m_bFever = false;
        }
        if (m_fCurrSpawnTime > 0.05f)
        {
            CreateMoney(E_MONEY_CLASS.E_POCKET_GOLD);
           
            m_fCurrSpawnTime = 0.0f;
        }
    }


    void MoneyInfoListUpdate()
    {
        bool bCheck = false;
        do
        {
            bCheck = false;
            foreach (MoneyInfo pSrc in m_pMoneyInfo)
            {
                if (pSrc.fEndTime <= JHGameData_Mng.I._GameLevelTime)
                {
                    m_pMoneyInfo.Remove(pSrc);
                    bCheck = true;
                    break;
                }
            }
        } while (bCheck == true);
    }

    void AddMoneyCreateTime(E_MONEY_CLASS eClass, float fStartTime, float fEndTime)
    {
        MoneyInfo pInfo = new MoneyInfo(eClass, fStartTime, fEndTime);
        m_pMoneyInfo.Add(pInfo);
    }

    E_MONEY_CLASS SelectLevelMoeny()
    {
        int nR = -1;
        for (int i = 0; i < 300;i++)
        {
            nR = CSRandom.Rand(0, m_pMoneyInfo.Count);
            if (JHGameData_Mng.I._GameLevelTime >= m_pMoneyInfo[nR].fStratTime &&
                JHGameData_Mng.I._GameLevelTime < m_pMoneyInfo[nR].fEndTime)
            {
                return m_pMoneyInfo[nR].eClass;
            }
        }
        return E_MONEY_CLASS.E_PAPER_SILVER;
    }

    public void AllMagnet()
    {
        foreach (CSObjectStruct<JHMoney_Root> pObj in m_pList)
        {
            if (pObj.pObj != null)
            {
                if (pObj.pSrc._Active == true)
                {
                    pObj.pSrc.SetMagnet();
                }
            }
        }
    }

    public bool GetMagnet()
    {
        foreach (CSObjectStruct<JHMoney_Root> pObj in m_pList)
        {
            if (pObj.pObj != null)
            {
                if (pObj.pSrc._Active == true)
                {
                   if(pObj.pSrc._Magnet==true)
                   {
                       return true;
                   }
                }
            }
        }
        return false;
    }

    int GetCurrMoneyNum()
    {
        int nNum = 0;
        foreach (CSObjectStruct<JHMoney_Root> pObj in m_pList)
        {
            if (pObj.pObj != null)
            {
                if (pObj.pSrc._Active == true)
                {
                    nNum++;
                }
            }
        }
        return nNum;
    }

    public CSObjectStruct<JHMoney_Root> CreateMoney(E_MONEY_CLASS eClass)
    {
        CSObjectStruct<JHMoney_Root> pObject = new CSObjectStruct<JHMoney_Root>();
        switch(eClass)
        {
            case E_MONEY_CLASS.E_COIN_BRONZE :
                pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Money/Coin_Bronze", "Money_" + m_nSpawnCnt.ToString());
                pObject.pSrc = pObject.pObj.GetComponent<JHMoney_CoinBronze>();
                pObject.pSrc.Enter();
                pObject.pSrc.Create();
                break;
            case E_MONEY_CLASS.E_COIN_SILVER:
                pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Money/Coin_Sliver", "Money_" + m_nSpawnCnt.ToString());
                pObject.pSrc = pObject.pObj.GetComponent<JHMoney_CoinSilver>();
                pObject.pSrc.Enter();
                pObject.pSrc.Create();
                break;
            case E_MONEY_CLASS.E_COIN_GOLD:
                pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Money/Coin_Gold", "Money_" + m_nSpawnCnt.ToString());
                pObject.pSrc = pObject.pObj.GetComponent<JHMoney_CoinGold>();
                pObject.pSrc.Enter();
                pObject.pSrc.Create();
                break;
            case E_MONEY_CLASS.E_PAPER_BRONZE:
                pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Money/Paper_Bronze", "Money_" + m_nSpawnCnt.ToString());
                pObject.pSrc = pObject.pObj.GetComponent<JHMoney_PaperBronze>();
                pObject.pSrc.Enter();
                pObject.pSrc.Create();
                break;
            case E_MONEY_CLASS.E_PAPER_SILVER:
                pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Money/Paper_Silver", "Money_" + m_nSpawnCnt.ToString());
                pObject.pSrc = pObject.pObj.GetComponent<JHMoney_PaperSilver>();
                pObject.pSrc.Enter();
                pObject.pSrc.Create();
                break;
            case E_MONEY_CLASS.E_PAPER_GOLD:
                pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Money/Paper_Gold", "Money_" + m_nSpawnCnt.ToString());
                pObject.pSrc = pObject.pObj.GetComponent<JHMoney_PaperGold>();
                pObject.pSrc.Enter();
                pObject.pSrc.Create();
                break;
            case E_MONEY_CLASS.E_POCKET_BRONZE:
                pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Money/Pocket_Bronze", "Money_" + m_nSpawnCnt.ToString());
                pObject.pSrc = pObject.pObj.GetComponent<JHMoney_PocketBronze>();
                pObject.pSrc.Enter();
                pObject.pSrc.Create();
                break;
            case E_MONEY_CLASS.E_POCKET_SILVER:
                pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Money/Pocket_Silver", "Money_" + m_nSpawnCnt.ToString());
                pObject.pSrc = pObject.pObj.GetComponent<JHMoney_PocketSilver>();
                pObject.pSrc.Enter();
                pObject.pSrc.Create();
                break;
            case E_MONEY_CLASS.E_POCKET_GOLD:
                pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Money/Pocket_Gold", "Money_" + m_nSpawnCnt.ToString());
                pObject.pSrc = pObject.pObj.GetComponent<JHMoney_PocketGold>();
                pObject.pSrc.Enter();
                pObject.pSrc.Create();
                break;
        }
        m_pList.Add(pObject);
        m_nSpawnCnt++;
        return pObject;
    }

}
