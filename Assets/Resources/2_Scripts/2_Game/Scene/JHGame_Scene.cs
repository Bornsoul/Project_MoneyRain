using UnityEngine;
using System.Collections;

public class JHGame_Scene : CSSingleton<JHGame_Scene>
{
    CSObjectStruct<JHGame_MainLayer> m_pGame;
    CSObjectStruct<JHGame_UILayer> m_pUI;
    CSLayer m_pGameLayer;
    public CSLayer _GameLayer { get { return m_pGameLayer; } set { m_pGameLayer = value; } }
    CSLayer m_pUILayer;
    public CSLayer _UILayer { get { return m_pUILayer; } set { m_pUILayer = value; } }

    


	// Use this for initialization
	void Start () {
        m_pGameLayer = CSLayerMng.I.CreateLayer("GameLayer", 0);
        m_pUILayer = CSLayerMng.I.CreateLayer("UILayer", 100);

        m_pGame.pObj = CSPrefabMng.I.CreatePrefab(m_pGameLayer.m_pAnchor, "1_Scene/2_Game/GameMainPrefab", "GameMainPrefab");
        m_pGame.pSrc = m_pGame.pObj.GetComponent<JHGame_MainLayer>();
        m_pGame.pSrc.Enter();
        m_pUI.pObj = CSPrefabMng.I.CreatePrefab(m_pUILayer.m_pAnchor, "1_Scene/2_Game/GameUI", "GameUI");
        m_pUI.pSrc = m_pUI.pObj.GetComponent<JHGame_UILayer>();
        m_pUI.pSrc.Enter();
	}

    void OnDestroy()
    {
        if (CSDirector.I == null) return;
        if(m_pGame.pSrc!=null) m_pGame.pSrc.Destroy();
        if (m_pUI.pSrc != null) m_pUI.pSrc.Destroy();
    }
	
	// Update is called once per frame
	void Update () {
      
	}
}
