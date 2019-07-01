using UnityEngine;
using System.Collections;

public class JHTitle_MainLayer : CSSingleton<JHTitle_MainLayer> {
    public GameObject m_pMain = null;
    public Share m_pShare = null;
    string sEnter = System.Environment.NewLine;

	public void Enter()
    {
        m_pMain.SetActive(true);
#if UNITY_EDITOR
        //m_pMain.SetActive(true);
       
#endif
        if( JHAdmob.I!=null) JHAdmob.I.InterstitialView();
       /* CSDirector.I.ShowLoading(transform.gameObject, 300, 1.0f);
        JHGooglePS_Mng.I.Init();
        JHGooglePS_Mng.I.SighInProcess(ProcessAuthentiation);*/
    }

    public void Destroy()
    {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("AA");
            EndingShow();
        }
	}

    public void EndingShow()
    {
        JHTitle_Scene.I._bESC = true;
        CSPrefabMng.I.CreatePrefab(JHTitle_Scene.I._PopLayer1.m_pAnchor, "2_Objects/Popup/Pop_Black", "Pop_BlackScreen");

        JHEndingShow pEndingShow = CSPrefabMng.I.CreatePrefab(transform.name, "2_Objects/EndingShow/JHEndingShow", "JHEndingShow").GetComponent<JHEndingShow>();

        //! 임시 랜덤값 트로피 증정
        bool[] m_bTropi = new bool[10];
        int[] m_nEmptyNum = new int[10];
        int nNum = 0;
        int nRand = NGUITools.RandomRange(0, 9);

        for (int i = 0; i < JAGameDataFile.I._nTropiMaxCount; i++)
        {
            m_bTropi[i] = JAGameDataFile.I.GetData_Bool("Tropi10" + i);

            if (m_bTropi[i] == false)
            {
                nNum++;
                m_nEmptyNum[i] = i;
            }
        }

        nRand = m_nEmptyNum[NGUITools.RandomRange(0, nNum)];
        Debug.Log("GiveTropi : " + nRand);
        JAGameDataFile.I.SetData("Tropi10" + nRand, true);
        //JATropiUI_Mng.I.Add_Tropi(int.Parse("10"+nRand));

        // --

        pEndingShow.Enter("10" + nRand);
    }

    void Btn_Google()
    {
        JHTitle_Scene.I._bESC = true;
        CSSoundMng.I.Play("MenuEF_Button");
    
        if (JHGooglePS_Mng.I.IsLogin() == false)
        {
           
            JHGooglePS_Mng.I.Init();
            JHGooglePS_Mng.I.SighInProcess(ProcessAuthentiation);
        }
        else
        {
            CSPrefabMng.I.CreatePrefab(JHTitle_Scene.I._PopLayer1.m_pAnchor, "2_Objects/Popup/Pop_GoogleUI", "Pop_GooglePlay");
        }
    }

    void Btn_Nanoo()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        Application.OpenURL("http://game.nanoo.so/Moneyshower");
    }

    void Btn_Share()
    {
        JHTitle_Scene.I._bESC = true;
        CSSoundMng.I.Play("MenuEF_Button");

        if ( JAGameDataFile.I.GetData_Bool("Banner1") == true)
        {
            string sAppTitle = "게임을 평가해 주세요!";
            string sAppText = "";
            sAppText += "친구에게 돈쏘나기를" + sEnter + "공유하여 같이 기록" + sEnter;
            sAppText += "달성을 공유해보세요!" + sEnter + "재미가 두!배! 증가합니다!" + sEnter;
            sAppText += "보너스로 5000G를 드립니다.";

            JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_SHBanner", sAppTitle, sAppText, E_JA_POPUP.E_POP_ALL, "JASHBannerBtn");
        }
        else
        {
            m_pShare.Call();
        }
    }

    void Btn_Facebook()
    {
        JHTitle_Scene.I._bESC = true;
        CSSoundMng.I.Play("MenuEF_Button");

        if (JAGameDataFile.I.GetData_Bool("Banner0") == true)
        {
            string sAppTitle = "게임을 평가해 주세요!";
            string sAppText = "";
            sAppText += "저희 돈쏘나기 페이지를" + sEnter + "좋아요 해주세요!" + sEnter;
            sAppText += "3000G를 드립니다!";

            JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_FBBanner", sAppTitle, sAppText, E_JA_POPUP.E_POP_ALL, "JAFBBannerBtn");
        }
        else
        {
            if (checkPackageAppIsPresent("com.facebook.katana"))
            {
                Application.OpenURL("fb://page/1550598821862255"); //there is Facebook app installed so let's use it
            }
            else
            {

                Application.OpenURL("http://www.facebook.com/MoneyShower"); // no Facebook app - use built-in web browser
            }
        }
        
    }

    public bool checkPackageAppIsPresent(string package)
    {
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");

        //take the list of all packages on the device
        AndroidJavaObject appList = packageManager.Call<AndroidJavaObject>("getInstalledPackages", 0);
        int num = appList.Call<int>("size");
        for (int i = 0; i < num; i++)
        {
            AndroidJavaObject appInfo = appList.Call<AndroidJavaObject>("get", i);
            string packageNew = appInfo.Get<string>("packageName");
            if (packageNew.CompareTo(package) == 0)
            {
                return true;
            }
        }
        return false;
    }
 

  
    void ProcessAuthentiation(bool success)
    {
      //  CSDirector.I.GetDeviceDebug().Log("Sign in Go");

        if (success)
        {
            JHUserData_Mng.I.m_pUserData.m_bLogin = true;
            m_pMain.SetActive(true);

          //  CSDirector.I.GetDeviceDebug().Log("Sign in Success");
        }
        else
        {
           // CSDirector.I.GetDeviceDebug().Log("Sign in Fail");
        }
        CSDirector.I.ExitLoading();

    }

   

}
