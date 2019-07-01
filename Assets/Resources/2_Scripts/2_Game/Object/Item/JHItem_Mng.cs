using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public struct ItemInfo
{
    public bool bActive;
    public float fCreateTime;
    public E_ITEM_CLASS eClass;
    public ItemInfo(E_ITEM_CLASS _eClass, float fTime)
    {
        eClass = _eClass;
        bActive = true;
        fCreateTime = fTime;
    }

    public void DeActive()
    {
        bActive = false;
        fCreateTime = 10000.0f;
    }
}

public class JHItem_Mng : MonoBehaviour {
    List<CSObjectStruct<JHItem_Root>> m_pList = null;
    public List<CSObjectStruct<JHItem_Root>> _List { get { return m_pList; } }

    bool m_bActive = false;
    public bool _Active { get { return m_bActive; } }

    int m_nSpawnCnt = 0;

    List<ItemInfo> m_pItemInfoList = null;

	// Use this for initialization
    public void Enter()
    {
        m_pList = new List<CSObjectStruct<JHItem_Root>>();
        m_nSpawnCnt = 0;
        m_bActive = true;

        m_pItemInfoList = new List<ItemInfo>();

        int nR = CSRandom.Rand(0, 100);
        if(nR<=25)
        {
            AddItemCreateTime(E_ITEM_CLASS.E_ITEM_HEART, (float)CSRandom.Rand(5, 150));
        }
       // AddItemCreateTime(E_ITEM_CLASS.E_ITEM_HEART,5);
     //   AddItemCreateTime(E_ITEM_CLASS.E_ITEM_HEART, 5.0f);

        
    }

    public void Destroy()
    {
        m_bActive = false;
        foreach (CSObjectStruct<JHItem_Root> pObj in m_pList)
        {
            if (pObj.pObj != null)
            {
                if (pObj.pSrc != null) pObj.pSrc.Destroy();
                CSPrefabMng.I.DestroyPrefab(pObj.pObj);
                continue;
            }
        }
        m_pList.Clear();
        m_pList = null;

        m_pItemInfoList.Clear();
        m_pItemInfoList = null;
    }

    void DeActiveCheck()
    {
        bool bCheck = false;
        do
        {
            bCheck = false;
            foreach (CSObjectStruct<JHItem_Root> pObj in m_pList)
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

    void ItemInfoListUpdate()
    {
        bool bCheck = false;
        do
        {
            bCheck = false;
            foreach (ItemInfo pSrc in m_pItemInfoList)
            {
                if(pSrc.bActive==true)
                {
                    if(pSrc.fCreateTime<=JHGameData_Mng.I._GameLevelTime)
                    {
                        CreateItem(pSrc.eClass);
                        m_pItemInfoList.Remove(pSrc);
                        bCheck = true;
                        break;
                    }
                }
            }
        } while (bCheck==true);
    }

    void AddItemCreateTime(E_ITEM_CLASS eClass, float fCreateTime)
    {
        ItemInfo pInfo = new ItemInfo(eClass, fCreateTime);
        m_pItemInfoList.Add(pInfo);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bActive == false) return;
        if (JHGameData_Mng.I._GamePause == true) return;
        if (JHGameData_Mng.I._GameCycle == true) return;
        if (JHGameData_Mng.I._TutorialState == true) return;

        ItemInfoListUpdate();
        DeActiveCheck();
       
    }

    public CSObjectStruct<JHItem_Root> CreateItem(E_ITEM_CLASS eClass)
    {
        CSObjectStruct<JHItem_Root> pObject = new CSObjectStruct<JHItem_Root>();
        switch (eClass)
        {
            case E_ITEM_CLASS.E_ITEM_HEART:
                pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Item/Item_Heart", "Item_" + m_nSpawnCnt.ToString());
                pObject.pSrc = pObject.pObj.GetComponent<JHItem_Heart>();
                pObject.pSrc.Enter();
                pObject.pSrc.Create();
                break;
        }
        m_pList.Add(pObject);
        m_nSpawnCnt++;
        return pObject;
    }
}
