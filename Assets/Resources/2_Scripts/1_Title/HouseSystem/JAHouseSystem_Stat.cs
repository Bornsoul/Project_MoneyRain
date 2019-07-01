using UnityEngine;
using System.Collections;

/// <summary>
/// 집 능력치 데이터
/// </summary>
public class JAHouseSystem_Stat : MonoBehaviour
{

    public int m_nHouseLevel = 1;
    public int m_nHousePrice = 0;
    public float m_fHouseExp = 0;
    public string m_sHouseName = string.Empty;
    public string m_sHouseSpriteName = string.Empty;

    public int _nHouseLevel { get { return m_nHouseLevel; } }
    public int _nHousePrice { get { return m_nHousePrice; } }
    public float _fHouseExp { get { return m_fHouseExp; } }
    public string _sHouseName { get { return m_sHouseName; } }
    public string _sHouseSpriteName { get { return m_sHouseSpriteName; } }


    public void Enter(int nIndex)
    {
        m_nHouseLevel = JAMenuData_Mng.I.m_pHouseData_String.GetLevel(nIndex);
        m_nHousePrice = JAMenuData_Mng.I.m_pHouseData_String.GetUpgPrice(nIndex);
        m_fHouseExp = JAMenuData_Mng.I.m_pHouseData_String.GetUpgExp(nIndex);
        m_sHouseName = JAMenuData_Mng.I.m_pHouseData_String.GetBuildName(nIndex);
        m_sHouseSpriteName = JAMenuData_Mng.I.m_pHouseData_String.GetSpriteName(nIndex);
    }

    void Update()
    {

    }
}
