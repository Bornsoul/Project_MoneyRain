using UnityEngine;
using System.Collections;

public class CSLoadingShow : MonoBehaviour {
    int m_nPanelDepth = 100;
    float m_fBlackAlpha = 1.0f;

    public UIPanel m_pPanel = null;
    public UISprite m_pBlack = null;
	// Use this for initialization
	void Start () {
      
	}

    public void Enter(int dp, float fap)
    {
        m_nPanelDepth = dp;
        m_fBlackAlpha = fap;
        m_pPanel.depth = m_nPanelDepth;
        m_pBlack.color = new Color(1.0f, 1.0f, 1.0f, m_fBlackAlpha);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Exit()
    {
        CSPrefabMng.I.DestroyPrefab(transform.gameObject);
    }
}
