using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class JHEffect_Mng : MonoBehaviour {
    List<CSObjectStruct<JHEffect_Root>> m_pList = null;
    public List<CSObjectStruct<JHEffect_Root>> _List { get { return m_pList; } }

    bool m_bActive = false;
    public bool _Active { get { return m_bActive; } }

    int m_nSpawnCnt = 0;
	// Use this for initialization
    public void Enter()
    {
        m_pList = new List<CSObjectStruct<JHEffect_Root>>();
        m_nSpawnCnt = 0;
        m_bActive = true;
       
    }
    
    public void Destroy()
    {
        m_bActive = false;
        foreach (CSObjectStruct<JHEffect_Root> pObj in m_pList)
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
            foreach (CSObjectStruct<JHEffect_Root> pObj in m_pList)
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
    void Update()
    {
        if (m_bActive == false) return;
        DeActiveCheck();
    }


    public CSObjectStruct<JHEffect_Root> CreateEffect_Danger(E_CSDIRECT eDirect)
    {
        CSObjectStruct<JHEffect_Root> pObject = new CSObjectStruct<JHEffect_Root>();
        JHEffect_CowDanger pSrc = null;
        pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Effect/Effect_CowDanger", "Effect_" + m_nSpawnCnt.ToString());
        pSrc = pObject.pObj.GetComponent<JHEffect_CowDanger>();
        pSrc.Enter();
        pSrc.Create(eDirect);
        pObject.pSrc = (JHEffect_Root)pSrc;

        m_pList.Add(pObject);
        m_nSpawnCnt++;
        return pObject;
    }

    public CSObjectStruct<JHEffect_Root> CreateEffect_Fever()
    {
        CSObjectStruct<JHEffect_Root> pObject = new CSObjectStruct<JHEffect_Root>();
        JHEffect_Fever pSrc = null;
        pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Effect/Effect_Fever", "Effect_" + m_nSpawnCnt.ToString());
        pSrc = pObject.pObj.GetComponent<JHEffect_Fever>();
        pSrc.Enter();
        pSrc.Create();
        pObject.pSrc = (JHEffect_Root)pSrc;

        m_pList.Add(pObject);
        m_nSpawnCnt++;
        return pObject;
    }

    public CSObjectStruct<JHEffect_Root> CreateEffect_PT_SoftStar(Vector3 stPos)
    {
        CSObjectStruct<JHEffect_Root> pObject = new CSObjectStruct<JHEffect_Root>();
        JHEffect_PT_SoftStar pSrc = null;
        pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Effect/Particle/PT_SoftStar", "Effect_" + m_nSpawnCnt.ToString());
        pSrc = pObject.pObj.GetComponent<JHEffect_PT_SoftStar>();
        pSrc.Enter();
        pSrc.Create(stPos);
        pObject.pSrc = (JHEffect_Root)pSrc;

        m_pList.Add(pObject);
        m_nSpawnCnt++;
        return pObject;
    }


    public CSObjectStruct<JHEffect_Root> CreateEffect_PT_MagicPoof(Vector3 stPos)
    {
        CSObjectStruct<JHEffect_Root> pObject = new CSObjectStruct<JHEffect_Root>();
        JHEffect_PT_MagicPoof pSrc = null;
        pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Effect/Particle/PT_MagicPoof", "Effect_" + m_nSpawnCnt.ToString());
        pSrc = pObject.pObj.GetComponent<JHEffect_PT_MagicPoof>();
        pSrc.Enter();
        pSrc.Create(stPos);
        pObject.pSrc = (JHEffect_Root)pSrc;

        m_pList.Add(pObject);
        m_nSpawnCnt++;
        return pObject;
    }

    public CSObjectStruct<JHEffect_Root> CreateEffect_PT_HitRed(Vector3 stPos)
    {
        CSObjectStruct<JHEffect_Root> pObject = new CSObjectStruct<JHEffect_Root>();
        JHEffect_PT_HitRed pSrc = null;
        pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Effect/Particle/PT_HitRed", "Effect_" + m_nSpawnCnt.ToString());
        pSrc = pObject.pObj.GetComponent<JHEffect_PT_HitRed>();
        pSrc.Enter();
        pSrc.Create(stPos);
        pObject.pSrc = (JHEffect_Root)pSrc;

        m_pList.Add(pObject);
        m_nSpawnCnt++;
        return pObject;
    }

    public CSObjectStruct<JHEffect_Root> CreateEffect_PT_HitMisc(Vector3 stPos)
    {
        CSObjectStruct<JHEffect_Root> pObject = new CSObjectStruct<JHEffect_Root>();
        JHEffect_PT_HitMisc pSrc = null;
        pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Effect/Particle/PT_HitMisc", "Effect_" + m_nSpawnCnt.ToString());
        pSrc = pObject.pObj.GetComponent<JHEffect_PT_HitMisc>();
        pSrc.Enter();
        pSrc.Create(stPos);
        pObject.pSrc = (JHEffect_Root)pSrc;
      
        m_pList.Add(pObject);
        m_nSpawnCnt++;
        return pObject;
    }

    public CSObjectStruct<JHEffect_Root> CreateEffect_PT_VortexAir(Vector3 stPos)
    {
        CSObjectStruct<JHEffect_Root> pObject = new CSObjectStruct<JHEffect_Root>();
        JHEffect_PT_VortexAir pSrc = null;
        pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Effect/Particle/PT_Vortex_Air", "Effect_" + m_nSpawnCnt.ToString());
        pSrc = pObject.pObj.GetComponent<JHEffect_PT_VortexAir>();
        pSrc.Enter();
        pSrc.Create(stPos);
        pObject.pSrc = (JHEffect_Root)pSrc;

        m_pList.Add(pObject);
        m_nSpawnCnt++;
        return pObject;
    }

    public CSObjectStruct<JHEffect_Root> CreateEffect_PT_SkyRays()
    {
        CSObjectStruct<JHEffect_Root> pObject = new CSObjectStruct<JHEffect_Root>();
        JHEffect_PT_SkyRays pSrc = null;
        pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Effect/Particle/PT_SkyRays_Loop", "Effect_" + m_nSpawnCnt.ToString());
        pSrc = pObject.pObj.GetComponent<JHEffect_PT_SkyRays>();
        pSrc.Enter();
        pSrc.Create();
        pObject.pSrc = (JHEffect_Root)pSrc;

        m_pList.Add(pObject);
        m_nSpawnCnt++;
        return pObject;
    }

    public CSObjectStruct<JHEffect_Root> CreateEffect_CirclePang(Vector3 stPos)
    {
        CSObjectStruct<JHEffect_Root> pObject = new CSObjectStruct<JHEffect_Root>();
        JHEffect_CirclePang pSrc = null;
        pObject.pObj = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/Effect/Effect_CirclePang", "Effect_" + m_nSpawnCnt.ToString());
        pSrc = pObject.pObj.GetComponent<JHEffect_CirclePang>();
        pSrc.Enter();
        pSrc.Create(stPos);
        pObject.pSrc = (JHEffect_Root)pSrc;

        m_pList.Add(pObject);
        m_nSpawnCnt++;
        return pObject;
    }

    public CSObjectStruct<JHEffect_Root> CreateEffect_Shadow()
    {
        CSObjectStruct<JHEffect_Root> pObject = new CSObjectStruct<JHEffect_Root>();
        JHEffect_Shadow pSrc = null;
        pObject.pObj = CSPrefabMng.I.CreatePrefab(JHGame_MainLayer.I.m_pHero_Mng, "2_Objects/Effect/Effect_Shadow", "Effect_" + m_nSpawnCnt.ToString());
        pSrc = pObject.pObj.GetComponent<JHEffect_Shadow>();
        pSrc.Enter();
        pSrc.Create();
        pObject.pSrc = (JHEffect_Root)pSrc;

        m_pList.Add(pObject);
        m_nSpawnCnt++;
        return pObject;
    }

}
