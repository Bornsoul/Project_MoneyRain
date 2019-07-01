using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyInfo
{
    public E_ENEMY_CLASS eClass;        //! 클래스
    public float fStartTime;            //! 게임시간이 이 변수 시간을 넘으면 조건발동
    public float fEndTime;              //! 게임시간이 이변수 시간을 넘으면 이 장애물은 끝남
    public int nSpaceNum;               //! 장애물 간격,  이 장애물이 소환된 직후 N개의 장애물이 소환되고 나서 이 장애물이 소환될수있음(과도한 소환 방지)
    public int nSpawnPersent;           //! 소환 퍼센트

    public int nCurrSpaceNum;
    public void Enter(E_ENEMY_CLASS _eClass, float _fStartTime, float _fEndTime, int _nSpawnPersent, int _nSpaceNum)
    {
        eClass = _eClass;
        fStartTime = _fStartTime;
        fEndTime = _fEndTime;
       
        nSpawnPersent = _nSpawnPersent;
        nCurrSpaceNum = 0;
        nSpaceNum = _nSpaceNum;
    }

    public void SetCurrSpaceNum(int nN)
    {
        
        nCurrSpaceNum = nN;
    }

    public void AddSpaceNum()
    {
        nCurrSpaceNum++;
    }
}

public class JHEnemy_Mng : MonoBehaviour {
    public List<CSObjectStruct<JHEnemy_Root>> m_pList = null;
    public List<CSObjectStruct<JHEnemy_Root>> _List { get { return m_pList; } }
    
    bool m_bActive = false;
    public bool _Active { get { return m_bActive; } set { m_bActive = value; } }

    int m_nSpawnCnt = 0;

    float m_fCurrSpawnTime = 0.0f;
    float m_fAgoSpawnTime = 0.5f;

    List<EnemyInfo> m_pEnemyInfo = null;

    int m_nCowCount = 0;

    public void Enter()
    {
        m_pList = new List<CSObjectStruct<JHEnemy_Root>>();
        m_nSpawnCnt = 0;
        m_bActive = true;
        m_nCowCount = 0;

        m_pEnemyInfo = new List<EnemyInfo>();

     /*  AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 0.0f, 300.0f, 50, 1);
        //AddEnemyCreateTime(E_ENEMY_CLASS.E_HOMI, 0.0f, 300.0f, 80, 1);
        AddEnemyCreateTime(E_ENEMY_CLASS.E_ROCK, 250.0f, 600.0f, 50, 0);
        AddEnemyCreateTime(E_ENEMY_CLASS.E_COW, 5.0f, 340.0f, 25, 5);
        AddEnemyCreateTime(E_ENEMY_CLASS.E_CAR, 300.0f, 600.0f, 30, 5);*/
      
        //JAMenuData_Mng.I.m_pHouseData_String.GetLevel(nIndex)
       // JAHouseSystem_Mng.I.m_pHouseData._nHouseLevel
        LevelDesing();
    }

    void LevelDesing()
    {
        int nLevel = JAMenuData_Mng.I.m_pHouseData_String.GetLevel(JAGameDataFile.I.GetData_Int("HouseLevel"));
        nLevel--;
        Debug.Log("Lvel = "+ nLevel);
       //45+(Up*20)  UP기본 1 = 최저 65 최대 205 ( 물략먹을 가능성있음)
        switch(nLevel)
        {
            case 0:
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 0.0f, 130.0f, 50, 2);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_COW, 20.0f, 400.0f, 15, 10);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 130.0f, 180.0f, 75, 2);
               
                break;
            case 1:
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 0.0f, 90.0f, 50, 2);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_COW, 20.0f, 400.0f, 36, 5);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 90.0f, 220.0f, 80, 2);
               
                break;
            case 2:
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 0.0f, 60.0f, 75, 2);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_COW, 20.0f, 100.0f, 30, 7);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_COW, 100.0f, 400.0f, 40, 5);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 60.0f, 180.0f, 80, 1);
              
                break;
            case 3:
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 0.0f, 90.0f, 50, 2);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_COW, 20.0f, 110.0f, 25, 5);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 90.0f, 90.0f, 80, 1);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_COW, 110.0f, 120.0f, 50, 3);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_COW, 120.0f, 400.0f, 50, 5);
                break;
            case 4:
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 0.0f, 90.0f, 50, 2);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_COW, 20.0f, 110.0f, 25, 5);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 90.0f, 100.0f, 40, 0);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 100.0f, 300.0f, 100, 1);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_COW, 110.0f, 400.0f, 70, 5);
                break;
            case 5:
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 0.0f, 110.0f, 50, 1);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_COW, 20.0f, 110.0f, 25, 5);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 110.0f, 120.0f, 70, 0);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 120.0f, 300.0f, 100, 1);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_COW, 110.0f, 400.0f, 50, 4);
                break;
            case 6:
                 AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 0.0f, 110.0f, 50, 1);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_COW, 20.0f, 110.0f, 25, 5);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 110.0f, 120.0f, 80, 0);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 120.0f, 300.0f, 100, 1);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_COW, 110.0f, 400.0f, 50, 4);
                break;
            case 7:
                 AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 0.0f, 110.0f, 50, 1);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_COW, 20.0f, 110.0f, 25, 5);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 110.0f, 120.0f, 90, 0);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_BALL, 120.0f, 300.0f, 60, 0);
                AddEnemyCreateTime(E_ENEMY_CLASS.E_COW, 110.0f, 400.0f, 50, 3);
                break;
        }

    }


    public void Destroy()
    {
        m_bActive = false;
        foreach (CSObjectStruct<JHEnemy_Root> pObj in m_pList)
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

        m_pEnemyInfo.Clear();
        m_pEnemyInfo = null;
    }

    void DeActiveCheck()
    {
        bool bCheck = false;
        do
        {
            bCheck = false;
            foreach (CSObjectStruct<JHEnemy_Root> pObj in m_pList)
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

    void EnemyInfoListUpdate()
    {
        bool bCheck = false;
        do
        {
            bCheck = false;
            foreach (EnemyInfo pSrc in m_pEnemyInfo)
            {
                if (pSrc.fEndTime <= JHGameData_Mng.I._GameLevelTime)
                {
                    m_pEnemyInfo.Remove(pSrc);
                    bCheck = true;
                    break;
                }
            }
        } while (bCheck == true);
    }

    public void AllHit()
    {
        foreach (CSObjectStruct<JHEnemy_Root> pObj in m_pList)
        {
            if (pObj.pSrc._Active==true)
            {
                pObj.pSrc.HitPlayer();
                continue;
            }
        }
    }

    void AddEnemyCreateTime(E_ENEMY_CLASS _eClass, float _fStartTime, float _fEndTime, int _nSpawnPersent, int nSpaceNum)
    {
        EnemyInfo pInfo = new EnemyInfo();
        pInfo.Enter(_eClass, _fStartTime, _fEndTime, _nSpawnPersent, nSpaceNum);
        m_pEnemyInfo.Add(pInfo);
    }

	// Update is called once per frame
	void Update () {
        if (m_bActive == false) return;
        if (JHGameData_Mng.I._GamePause == true) return;
        if (JHGameData_Mng.I._GameCycle == true) return;
        DeActiveCheck();
        if (JHGameData_Mng.I._TutorialState == true) return;

        if (JHGame_MainLayer.I.m_pMoney_Mng._Fever == true) return;
        m_fCurrSpawnTime += Time.deltaTime;
        if(m_fCurrSpawnTime>m_fAgoSpawnTime)
        {
           // CreateEnemy(E_ENEMY_CLASS.E_COW);
            CreateEnemy(SelectEnemy());
            foreach (EnemyInfo pObj in m_pEnemyInfo)
            {
                if (JHGameData_Mng.I._GameLevelTime >= pObj.fStartTime &&
                    JHGameData_Mng.I._GameLevelTime < pObj.fEndTime)
                {
                    pObj.AddSpaceNum();
                }

                //   Debug.Log(pObj.nCurrSpaceNum);

            }
            m_fCurrSpawnTime = 0.0f;
        }
	}

    public void TutorialUpdate()
    {
        m_fCurrSpawnTime += Time.deltaTime;
        if (m_fCurrSpawnTime > m_fAgoSpawnTime)
        {
            // CreateEnemy(E_ENEMY_CLASS.E_COW);
            CreateEnemy(SelectEnemy());
            foreach (EnemyInfo pObj in m_pEnemyInfo)
            {
                if (JHGameData_Mng.I._GameLevelTime >= pObj.fStartTime &&
                    JHGameData_Mng.I._GameLevelTime < pObj.fEndTime)
                {
                    pObj.AddSpaceNum();
                }

                //   Debug.Log(pObj.nCurrSpaceNum);

            }
            m_fCurrSpawnTime = 0.0f;
        }
    }

    E_ENEMY_CLASS SelectEnemy()
    {
        
        int nR = -1;
        for (int i = 0; i < 500; i++)
        {
            nR = CSRandom.Rand(0, m_pEnemyInfo.Count);
            if (JHGameData_Mng.I._GameLevelTime >= m_pEnemyInfo[nR].fStartTime &&
                JHGameData_Mng.I._GameLevelTime < m_pEnemyInfo[nR].fEndTime)
            {  
                if(CSRandom.Rand(0, 100)<m_pEnemyInfo[nR].nSpawnPersent)
                {
                    if (m_pEnemyInfo[nR].nSpaceNum <= m_pEnemyInfo[nR].nCurrSpaceNum)
                    {
                        m_pEnemyInfo[nR].SetCurrSpaceNum(0);
                        return m_pEnemyInfo[nR].eClass;
                    }
                    else
                    {
                       // return E_ENEMY_CLASS.E_MAX;
                    }
                }      
                else
                {
                    return E_ENEMY_CLASS.E_MAX;
                }
            }
        }
        return E_ENEMY_CLASS.E_MAX;
    }

    public CSObjectStruct<JHEnemy_Root> CreateEnemy(E_ENEMY_CLASS eClass)
    {
       
        CSObjectStruct<JHEnemy_Root> pObject = new CSObjectStruct<JHEnemy_Root>();
        if (eClass == E_ENEMY_CLASS.E_MAX) return pObject;
        switch (eClass)
        {
            case E_ENEMY_CLASS.E_BALL :
                pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Enemy/Enemy_Ball", "Enemy_" + m_nSpawnCnt.ToString());
                pObject.pSrc = pObject.pObj.GetComponent<JHEnemy_Ball>();
                pObject.pSrc.Enter();
                pObject.pSrc.Create();
                break;
            case E_ENEMY_CLASS.E_COW:
                pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Enemy/Enemy_Cow", "Enemy_" + m_nSpawnCnt.ToString());
                pObject.pSrc = pObject.pObj.GetComponent<JHEnemy_Cow>();
                pObject.pSrc.Enter();
                pObject.pSrc.Create();
                break;
            case E_ENEMY_CLASS.E_ROCK:
                pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Enemy/Enemy_Rock", "Enemy_" + m_nSpawnCnt.ToString());
                pObject.pSrc = pObject.pObj.GetComponent<JHEnemy_Rock>();
                pObject.pSrc.Enter();
                pObject.pSrc.Create();
                break;
            case E_ENEMY_CLASS.E_CAR:
                pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Enemy/Enemy_Car", "Enemy_" + m_nSpawnCnt.ToString());
                pObject.pSrc = pObject.pObj.GetComponent<JHEnemy_Car>();
                pObject.pSrc.Enter();
                pObject.pSrc.Create();
                break;
            case E_ENEMY_CLASS.E_HOMI:
                   pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Enemy/Enemy_HOMI", "Enemy_" + m_nSpawnCnt.ToString());
                pObject.pSrc = pObject.pObj.GetComponent<JHEnemy_HOMI>();
                pObject.pSrc.Enter();
                pObject.pSrc.Create();
                break;
        }
        m_pList.Add(pObject);
        m_nSpawnCnt++;
        return pObject;
    }

}
