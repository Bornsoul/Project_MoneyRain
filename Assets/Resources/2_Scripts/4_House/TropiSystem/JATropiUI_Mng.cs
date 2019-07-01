using UnityEngine;
using System.Collections;

public class JATropiUI_Mng : CSSingleton<JATropiUI_Mng>
{
    int m_nIndex = 0;

    public int m_nItemID = 0;
    bool[] m_bTropi;

    void Start()
    {
        m_bTropi = new bool[10];
        //Add_Tropi(100);
        //Add_Tropi(101);
        //Add_Tropi(102);
        //Add_Tropi(103);
        //Add_Tropi(100);
        
        for ( int i = 0; i<JAGameDataFile.I._nTropiMaxCount; i++)
        {
            m_bTropi[i] = JAGameDataFile.I.GetData_Bool("Tropi10" + i);

            if ( m_bTropi[i] == true )
            {
                Debug.Log(i + ". Tropi : " + m_bTropi[i]);
                Add_Tropi(int.Parse("10" + i));
            }
            else
            {

            }
        }

    }

    void Update()
    {

    }

    public void Add_Tropi(int nID)
    {
        CSObjectStruct<JATropi_Src> m_pTropi_Gam;
        int nX = 0;

        nX = m_nIndex % 2 == 0 ? -142 : 142;

        m_pTropi_Gam.pObj = CSPrefabMng.I.CreatePrefab(GameObject.Find("Tropi_Scroll/Grid").gameObject, "2_Objects/HouseItem/Item", "Tropi_" + nID + "_" + m_nIndex);
        m_pTropi_Gam.pObj.transform.localPosition = new Vector3(nX, 512 + -256 * m_nIndex, -1);
        m_pTropi_Gam.pSrc = m_pTropi_Gam.pObj.GetComponent<JATropi_Src>();
        m_pTropi_Gam.pSrc.Enter(m_nIndex, nID);

        m_nIndex++;
    }
}
