using UnityEngine;
using System.Collections;

public class JAFBBannerBtn : MonoBehaviour
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
        JAGameDataFile.I.SetData("Banner0", false);
        
        nMoney += 3000;
        JAHouseSystem_Mng.I._nMyMoney += 3000;
        JAGameDataFile.I.SetData("PlayerMoney", nMoney);

        if (JHTitle_MainLayer.I.checkPackageAppIsPresent("com.facebook.katana"))
        {
            Application.OpenURL("fb://page/1550598821862255"); //there is Facebook app installed so let's use it
        }
        else
        {
            
            Application.OpenURL("http://www.facebook.com/MoneyShower"); // no Facebook app - use built-in web browser
        }
    }
}
