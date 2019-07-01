using UnityEngine;
using System.Collections;

public class JHGame_MainLayer : CSSingleton<JHGame_MainLayer>
{
  

    public GameObject m_pHero_Mng = null;
    public JHEnemy_Mng m_pEnemy_Mng = null;
    public JHMoney_Mng m_pMoney_Mng = null;
    public JHItem_Mng m_pItem_Mng = null;
    public JHEffect_Mng m_pEffect_Mng = null;

    public JHShake m_pShake = null;
    public JHGameBG m_pBG = null;

    public JHGameUI_Score m_pScore = null;

    CSObjectStruct<JHHero> m_pHero;
    public CSObjectStruct<JHHero> _Hero { get { return m_pHero; } }

    bool m_bActive = false;

    public void Enter()
    {
        JHGameData_Mng.I.Enter();

        m_pHero.pObj = CSPrefabMng.I.CreatePrefab(m_pHero_Mng, "2_Objects/Hero/Hero", "Hero");
        m_pHero.pSrc = m_pHero.pObj.GetComponent<JHHero>();

        m_pHero.pSrc.Enter();
        m_pHero.pSrc.Create();

        m_pMoney_Mng.Enter();

        m_pEnemy_Mng.Enter();

        m_pItem_Mng.Enter();

        m_pEffect_Mng.Enter();

        m_bActive = true;

        m_pScore.Enter();
    }

    public void Destroy()
    {
        m_bActive = false;
        m_pScore.Destroy();
        m_pMoney_Mng.Destroy();
        m_pEnemy_Mng.Destroy();
        m_pHero.pSrc.Destroy();
        m_pItem_Mng.Destroy();
        m_pEffect_Mng.Destroy();
        

        CSPrefabMng.I.DestroyPrefab(m_pHero.pObj);

        JHGameData_Mng.I.Destroy();
    }
	
	// Update is called once per frame
	void Update () {
        if (m_bActive == false) return;

	}
}
