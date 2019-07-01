using UnityEngine;
using System.Collections;

public class JAResult_Scene : CSSingleton<JAResult_Scene>
{
    GameObject m_pScene = null;
    CSLayer m_pLayer;
    public CSLayer _pLayer { get { return m_pLayer; } set { m_pLayer = value; } }
    CSLayer m_pPopLayer1;
    public CSLayer _PopLayer1 { get { return m_pPopLayer1; } set { m_pPopLayer1 = value; } }

    void Start()
    {
        m_pLayer = CSLayerMng.I.CreateLayer("FirstLayer", 0);
        m_pPopLayer1 = CSLayerMng.I.CreateLayer("PopLayer1", 100);

        m_pScene = CSPrefabMng.I.CreatePrefab(m_pLayer.m_pAnchor, "1_Scene/5_Result/ResultMainPrefab", "Result_House");
    }

    void Update()
    {

    }
}
