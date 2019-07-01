using UnityEngine;
using System.Collections;

public class JAHousePopExp_Src : MonoBehaviour
{

    public UISlider m_pExpSlider = null;
    public UILabel m_pLevelLabel = null;

    public void Enter()
    {
        m_pExpSlider.value = JAHouseSystem_Mng.I.m_pHouseExp_Src.m_pHouseExpSlider.value;
        m_pLevelLabel.text = "Level" + JAHouseSystem_Mng.I._pHouseData._nHouseLevel;
    }

    void Update()
    {
        m_pExpSlider.value = JAHouseSystem_Mng.I.m_pHouseExp_Src.m_pHouseExpSlider.value;
        m_pLevelLabel.text = "Level" + JAHouseSystem_Mng.I._pHouseData._nHouseLevel;
    }

}
