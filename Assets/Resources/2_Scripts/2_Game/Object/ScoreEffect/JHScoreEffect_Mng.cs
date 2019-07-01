using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class JHScoreEffect_Mng : MonoBehaviour {
    List<CSObjectStruct<JHScoreEffect>> m_pList = null;
    bool m_bActive = false;
    int m_nSpawnCnt = 0;

    bool m_bNecoCheck = false;
    public void Enter()
    {
        m_pList = new List<CSObjectStruct<JHScoreEffect>>();
        m_nSpawnCnt = 0;
        m_bActive = true;
        m_bNecoCheck = false;
    }

    public void Destroy()
    {
        m_bActive = false;
        foreach (CSObjectStruct<JHScoreEffect> pObj in m_pList)
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
    }

    void DeActiveCheck()
    {
        bool bCheck = false;
        do
        {
            bCheck = false;
            foreach (CSObjectStruct<JHScoreEffect> pObj in m_pList)
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

    public void EatMoney(int nScore)
    {
        if (m_bNecoCheck==false)
        {
            if(m_pList.Count>=10)
            {
                CSSoundMng.I.Play("Neco2");
                m_bNecoCheck = true;
            }
        }
        else
        {
            if (m_pList.Count < 10)
            {
                m_bNecoCheck = false;
            }
        }
        foreach (CSObjectStruct<JHScoreEffect> pObj in m_pList)
        {
            if (pObj.pObj != null)
            {
                if (pObj.pSrc._Active == true)
                {
                    pObj.pSrc.PushMe();
                }
            }
        }
        CreateScoreEffect(nScore);
    }

    CSObjectStruct<JHScoreEffect> CreateScoreEffect(int nScore)
    {
        CSObjectStruct<JHScoreEffect> pObject = new CSObjectStruct<JHScoreEffect>();

        pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/ScoreEffect/ScoreEffect_Label", "ScoreEffect_Label" + m_nSpawnCnt.ToString());
        pObject.pSrc = pObject.pObj.GetComponent<JHScoreEffect>();
        pObject.pSrc.Enter();
        pObject.pSrc.Create(nScore);
         
        m_pList.Add(pObject);
        m_nSpawnCnt++;
        return pObject;
    }

	// Update is called once per frame
	void Update () {
        if (m_bActive == false) return;
        DeActiveCheck();
	}
}
