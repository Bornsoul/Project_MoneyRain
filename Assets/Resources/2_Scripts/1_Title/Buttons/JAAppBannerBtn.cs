using UnityEngine;
using System.Collections;

public class JAAppBannerBtn : MonoBehaviour
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
        JAGameDataFile.I.SetData("AppBanner", false);

        nMoney += 5000;
        JAHouseSystem_Mng.I._nMyMoney += 5000;
        JAGameDataFile.I.SetData("PlayerMoney", nMoney);

        Application.OpenURL(JAMenuData_Mng.I.m_pAppData_String.GetAppPath());
    }
}
