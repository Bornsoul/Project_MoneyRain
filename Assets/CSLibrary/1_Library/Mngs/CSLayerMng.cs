using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 레이어 관리 매니저
/// </summary>
public class CSLayerMng : CSSingleton<CSLayerMng> {

    public List<CSLayer> m_pLayerList = null;
    private bool[] m_bSwitchCullLayer;
    private int m_nMaxLayer = 11;

    void Awake()
    {
        m_pLayerList = new List<CSLayer>();
        m_bSwitchCullLayer = new bool[m_nMaxLayer];
        for(int i=0;i<m_nMaxLayer;i++)
        {
            m_bSwitchCullLayer[i] = false;
        }
    }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}

    void OnDestroy()
    {
        Debug.Log("OnDestroy()/CSLayerMng.cs");
        m_pLayerList.Clear();
        m_pLayerList = null;
    }

    /// <summary>
    /// 레이어리스트 비우기
    /// </summary>
    public void DestroyList()
    {
        m_pLayerList.Clear();
    }

    /// <summary>
    /// 레이어 만들기
    /// </summary>
    /// <param name="sName"></param>    레이어 이름
    /// <param name="nDepth"></param>   뎁스
    /// <returns></returns>
    public CSLayer CreateLayer(string sName, int nDepth)
    {
        foreach (CSLayer pObj in m_pLayerList)
        {
            if (pObj.transform.name == sName)
            {
#if _debug
            Debug.Log(" OverLapName Layer [CreatePrefab/CSLayerMng.cs]");
#endif
                return null;
            }
        }

        GameObject pLayerObj = CSPrefabMng.I.CreatePrefab("0_Library/CSLayer", sName);
        if(pLayerObj==null)
        {
#if _debug
            Debug.Log(" Can't Find LayerObj [CreatePrefab/CSLayerMng.cs]");
#endif
            return null;
        }
        CSLayer pLayerSrc = pLayerObj.GetComponent<CSLayer>();
        bool bCreate = false;
        for (int i = 0; i < m_nMaxLayer;i++)
        {
            if(m_bSwitchCullLayer[i]==false)
            {
                bCreate = true;
                //Debug.Log(LayerMask.NameToLayer("CSLayer" + i));
                pLayerSrc.Enter(LayerMask.NameToLayer("CSLayer" + i), nDepth, i);
                m_pLayerList.Add(pLayerSrc);
                return pLayerSrc;
            }
        }
        if (bCreate==false)
        {
#if _debug
            Debug.Log(" Max Layer  [CreatePrefab/CSLayerMng.cs]");
#endif
            return null;
        }

            return null;
    }


    /// <summary>
    /// 디버그 레이어 만들기
    /// </summary>
    /// <returns></returns>
    public CSLayer CreateDebugLayer()
    {
        string sName = "DebugLayer";
        foreach (CSLayer pObj in m_pLayerList)
        {
            if (pObj.transform.name == sName)
            {
#if _debug
            Debug.Log(" OverLapName Layer [CreatePrefab/CSLayerMng.cs]");
#endif
                return null;
            }
        }

        GameObject pLayerObj = CSPrefabMng.I.CreatePrefab("0_Library/CSDebugLayer", sName);
        CSPrefabMng.I.m_pLive_PrefabList.Remove(pLayerObj.name);
        if (pLayerObj == null)
        {
#if _debug
            Debug.Log(" Can't Find LayerObj [CreatePrefab/CSLayerMng.cs]");
#endif
            return null;
        }
        CSLayer pLayerSrc = pLayerObj.GetComponent<CSLayer>();
        bool bCreate = false;
        for (int i = 0; i < m_nMaxLayer; i++)
        {
            if (m_bSwitchCullLayer[i] == false)
            {
                bCreate = true;
                
                pLayerSrc.Enter(LayerMask.NameToLayer("CSLayerDebug"), 3000, i);
               // m_pLayerList.Add(pLayerSrc);
                return pLayerSrc;
            }
        }
        if (bCreate == false)
        {
#if _debug
            Debug.Log(" Max Layer  [CreatePrefab/CSLayerMng.cs]");
#endif
            return null;
        }

        return null;
    }
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
