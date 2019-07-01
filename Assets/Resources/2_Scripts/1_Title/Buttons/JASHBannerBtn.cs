using UnityEngine;
using System.Collections;

public class JASHBannerBtn : MonoBehaviour
{
    int nMoney = 0;
    // Use this for initialization
    void Start()
    {
        nMoney = JAGameDataFile.I.GetData_Int("PlayerMoney");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JAGameDataFile.I.SetData("Banner1", false);
        
        nMoney += 5000;
        JAHouseSystem_Mng.I._nMyMoney += 5000;
        JAGameDataFile.I.SetData("PlayerMoney", nMoney);

        JHTitle_MainLayer.I.m_pShare.Call();
    }
}
