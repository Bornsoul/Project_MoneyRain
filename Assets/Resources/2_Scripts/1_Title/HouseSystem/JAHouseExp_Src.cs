using UnityEngine;
using System.Collections;

public class JAHouseExp_Src : MonoBehaviour
{

    public UISlider m_pHouseExpSlider = null;

    
    public float m_fValue = 0f;
    public float m_fFinishValue = 0f;
    float m_nRandom = 0;


    public void Enter(float fValue)
    {
        //Debug.Log("ExpEnter : " + fValue);
        m_fValue = fValue;
        //m_fValue = (fValue / 100) * 1.0f;
    }

    public void AddValue(float fValue)
    {
        m_nRandom = Random.Range(5, (int)fValue);

        m_fValue += (m_nRandom / 100) * 1.0f;
        JAHouseSystem_Mng.I._fHouseExp = m_fValue;
        JAGameDataFile.I.SetData("HouseExp", JAHouseSystem_Mng.I._fHouseExp);
        Debug.Log("ExpVal: " + JAHouseSystem_Mng.I._fHouseExp);
    }

    void Update()
    {


        if (m_fFinishValue > 0.999f)
        {
            if (JAHouseSystem_Mng.I._pHouseData._nHouseLevel >= JAMenuData_Mng.I.m_pHouseData_String.m_sLevel.Length - 2)
            {
                JAHouseSystem_Mng.I.m_bLevelMax = true;
                m_fValue = 0.999f;
                m_fFinishValue = 0.999f;

                if (JAHouseSystem_Mng.I.m_bLevelMax == true)
                {
                    JAHouseSystem_Mng.I.Fun_HouseMaxLevelEnd();
                }
                Debug.Log("Exp FULL");
                return;
            }
            else
            {
                m_fValue = 0.999f;
                m_fFinishValue = 0.999f;
                JAHouseSystem_Mng.I.Fun_LevelUp();
                JAHouseSystem_Mng.I.m_bUpgradeDone = true;
            }
            return;
        }


        m_pHouseExpSlider.value = m_fFinishValue;
    }

    void FixedUpdate()
    {
        

        m_fFinishValue = Mathf.Lerp(m_fFinishValue, m_fValue, 0.08f);
    }

   
}
