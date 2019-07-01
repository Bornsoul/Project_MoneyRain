using UnityEngine;
using System.Collections;

public class JAHouse_Scene : CSSingleton<JAHouse_Scene>
{
    GameObject m_pScene = null;
    public GameObject m_pScroll = null;

    CSLayer m_pLayer;
    public CSLayer _pLayer { get { return m_pLayer; } set { m_pLayer = value; } }

    CSLayer m_pPopLayer1;
    public CSLayer _PopLayer1 { get { return m_pPopLayer1; } set { m_pPopLayer1 = value; } }

    CSLayer m_pPopLayer2;
    public CSLayer _PopLayer2 { get { return m_pPopLayer2; } set { m_pPopLayer2 = value; } }
    bool[] m_bTropi;
    int nTropi = 0;

    string sEnter = System.Environment.NewLine;
    void Start()
    {
        nTropi = 0; 
        m_bTropi = new bool[10];
        m_pLayer = CSLayerMng.I.CreateLayer("FirstLayer", 0);
        m_pPopLayer1 = CSLayerMng.I.CreateLayer("PopLayer1", 100);
        m_pPopLayer2 = CSLayerMng.I.CreateLayer("PopLayer2", 150);

        m_pScene = CSPrefabMng.I.CreatePrefab(m_pLayer.m_pAnchor, "1_Scene/4_House/HouseMainPrefab", "Main_House");

        for (int i = 0; i < JAGameDataFile.I._nTropiMaxCount; i++)
        {
            m_bTropi[i] = JAGameDataFile.I.GetData_Bool("Tropi10" + i);

            if (m_bTropi[i] == false)
            {
                nTropi++;              
            }
        }
        Debug.Log(nTropi);
        if ( nTropi >= JAGameDataFile.I._nTropiMaxCount )
        {
            string sTropiTitle = "엔딩 트로피";
            string sTropi = "";
            sTropi += "가지고있는 트로피가 없습니다!" + sEnter;
            sTropi += "모든집을 업그레이드후" + sEnter;
            sTropi += "트로피를 모아봅시다!";

            JAMenuData_Mng.I.CreatePopup(m_pPopLayer1.m_pAnchor, "Pop_NoTropi", sTropiTitle, sTropi, E_JA_POPUP.E_POP_OK);
        }
    }

    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Backspace))
        //{
        //    AutoFade.LoadLevel("1_Title", 0.3f, 0.3f, Color.white);
        //}
    }
}
