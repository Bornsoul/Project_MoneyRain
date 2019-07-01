using UnityEngine;
using System.Collections;

public class JAUpgTop_Src : MonoBehaviour
{
    //public UILabel[] m_pStatLabel = null;
   // string[] m_sStatString;


    public bool m_bChange = false;

    public void Enter()
    {
        m_bChange = false;
        //m_sStatString = new string[] { "돈가치", "스피드", "점프력" };
    }

    void Update()
    {

        //m_pStatLabel[0].text = "[E85542]" +
        //    m_sStatString[0] + "[-] : [E88136]" +
        //    JAMenuData_Mng.I.m_pStatData_String.GetBase() +
        //    "[-][7F754F] + [-][E8725E]" +
        //    JAPop_CharacterUpg.I.m_pUpgBot_Src._nStats[0] +
        //    "[-]";

    }

    public void Button_Change()
    {
        m_bChange = !m_bChange;
    }

    public void Button_Reset()
    {

        JAGameDataFile.I.Enter();
       
    }
}
