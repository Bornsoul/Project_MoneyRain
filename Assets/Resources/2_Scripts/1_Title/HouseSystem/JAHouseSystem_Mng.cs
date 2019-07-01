using UnityEngine;
using System.Collections;

/// <summary>
/// 집 시스템 매니저
/// </summary>
public class JAHouseSystem_Mng : CSSingleton<JAHouseSystem_Mng>
{

    JAHouseSystem_Stat m_pHouseData = null;
    public JAHouseSystem_Stat _pHouseData { get { return m_pHouseData; } set { m_pHouseData = value; } }

    public JAHouse_Src m_pHouse_Src;
    public JAHouseExp_Src m_pHouseExp_Src;

    int m_nMyMoney = 0;
    public int _nMyMoney { get { return m_nMyMoney; } set { m_nMyMoney = value; } }
    int m_nHouseLevel = 0;
    public int _nHouseLevel { get { return m_nHouseLevel; } set { m_nHouseLevel = value; } }
    float m_fHouseExp = 0f;
    public float _fHouseExp { get { return m_fHouseExp; } set { m_fHouseExp = value; } }

    public UILabel m_pMoneyLabel = null;

    public bool m_bUpgradeDone = false;
    public bool m_bLevelMax = false;

    bool m_bHouseMaxEnd = false;
    public bool _bHouseMaxEnd { get { return m_bHouseMaxEnd; } set { m_bHouseMaxEnd = value; } }

    void Start()
    {
        m_bUpgradeDone = false;
        m_bLevelMax = false;
        m_nMyMoney = JAGameDataFile.I.GetData_Int("PlayerMoney");
        m_nHouseLevel = JAGameDataFile.I.GetData_Int("HouseLevel");
        m_fHouseExp = JAGameDataFile.I.GetData_Float("HouseExp");

        m_pHouseData = GetComponent<JAHouseSystem_Stat>();

        m_pHouseData.Enter(m_nHouseLevel);

        m_pHouse_Src.Enter(m_pHouseData._nHouseLevel, m_pHouseData._sHouseName, m_pHouseData._sHouseSpriteName);
        m_pHouseExp_Src.Enter(JAGameDataFile.I.GetData_Float("HouseExp"));
    }

    void Update()
    {
        m_pHouse_Src.Fun_SpriteExpScale(m_pHouseExp_Src.m_pHouseExpSlider.value);

        m_pMoneyLabel.text = JAMenuData_Mng.I.GetIntNumberString(m_nMyMoney);
    }


    void Btn_DebugMaxLevel()
    {

        if (m_pHouseData._nHouseLevel >= JAMenuData_Mng.I.m_pHouseData_String.m_sLevel.Length - 2)
        {
            Debug.Log("DebugMaxLevel FULL");
            return;
        }
        else
        {
            //JAHouseSystem_Mng.I.Fun_Upgrade();

            m_nHouseLevel++;
            m_fHouseExp = 0f;
            JAGameDataFile.I.SetData("HouseLevel", m_nHouseLevel);
            JAGameDataFile.I.SetData("HouseExp", m_fHouseExp);
            m_pHouseExp_Src.m_fFinishValue = 0f;
            m_pHouseExp_Src.m_fValue = 0f;

            m_pHouseData.Enter(m_pHouseData._nHouseLevel);
            m_pHouse_Src.Enter(m_pHouseData._nHouseLevel, m_pHouseData._sHouseName, m_pHouseData._sHouseSpriteName);
        }
        Debug.Log("1: " + m_pHouseData._nHouseLevel);
        Debug.Log("2: " + m_nHouseLevel);
        Debug.Log("3: " + JAGameDataFile.I.GetData_Int("HouseLevel"));
    }

    /// <summary>
    /// 업그레이드 팝업출력 버튼 클릭시
    /// </summary>
    public void Btn_Upgrade()
    {
        JHTitle_Scene.I._bESC = true;
        CSSoundMng.I.Play("MenuEF_Button");
        CSPrefabMng.I.CreatePrefab(JHTitle_Scene.I._PopLayer1.m_pAnchor, "2_Objects/Popup/Pop_HouseUpg", "Pop_HouseUpg");
        //JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer.m_pAnchor, "Pop_HouseUpg", "업그레이드", "집을 업그레이드 하시겠습니까?\n\n\n[A1524E]필요 금액 :[-] " + m_pHouseData._nHousePrice + "\n\n[5697A3]보유 금액 :[-] " + m_nMyMoney, E_JA_POPUP.E_POP_ALL, "JAHouseOKBtn");

    }

    void Btn_House()
    {
        JHTitle_Scene.I._bESC = true;
        CSSoundMng.I.Play("MenuEF_Button");
        AutoFade.LoadLevel("4_House", 0.3f, 0.3f, Color.white);
    }

    void Btn_Credit()
    {
        JHTitle_Scene.I._bESC = true;
        CSSoundMng.I.Play("MenuEF_Button");
        CSPrefabMng.I.CreatePrefab(JHTitle_Scene.I._PopLayer2.m_pAnchor, "2_Objects/Popup/Pop_Credit", "Pop_Credit");
    }

    /// <summary>
    /// 게이지가 100% 상태일때 호출
    /// </summary>
    public void Fun_LevelUp()
    {
        if (m_bUpgradeDone == true) return;
        m_bUpgradeDone = true;
        StartCoroutine(Cor_LevelupDelay(0.2f));
        JAPop_HouseUpg.I.Pop_Exit();
        CSDebug_Device.print("집 레벨업");
        JHUserData_Mng.I.m_pUserData.PlusData("_HouseLevel");
    }

    IEnumerator Cor_LevelupDelay(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);
        CSPrefabMng.I.CreatePrefab(JHTitle_Scene.I._PopLayer2.m_pAnchor, "2_Objects/Popup/Pop_HouseLevelUp", "Pop_HouseLevelUp");

        JAPop_HouseUpg.I.m_pHPopTop_Src._nStats[0] = 1;
        JAPop_HouseUpg.I.m_pHPopTop_Src._nStats[1] = 1;

        JAGameDataFile.I.SetData(JAPop_HouseUpg.I.m_pHPopTop_Src._sStatString[0],
        JAPop_HouseUpg.I.m_pHPopTop_Src._nStats[0]);
        JAGameDataFile.I.SetData(JAPop_HouseUpg.I.m_pHPopTop_Src._sStatString[1],
        JAPop_HouseUpg.I.m_pHPopTop_Src._nStats[1]);

        m_pHouseExp_Src.m_fFinishValue = 0f;
        m_pHouseExp_Src.m_fValue = 0f;

        m_nHouseLevel++;
        m_fHouseExp = 0f;
        JAGameDataFile.I.SetData("HouseLevel", m_nHouseLevel);
        JAGameDataFile.I.SetData("HouseExp", m_fHouseExp);

        m_pHouseData.Enter(m_nHouseLevel);
        m_pHouse_Src.Enter(m_pHouseData._nHouseLevel, m_pHouseData._sHouseName, m_pHouseData._sHouseSpriteName);
        m_bUpgradeDone = false;
    }

    /// <summary>
    /// 업그레이드 팝업창에서 구매 버튼을 누를시 호출
    /// </summary>
    public void Fun_Upgrade()
    {
        m_bUpgradeDone = true;
        StartCoroutine(Cor_Upgrade(0.1f));
    }

    IEnumerator Cor_Upgrade(float fWaitTime)
    {

        yield return new WaitForSeconds(fWaitTime);
        //m_pHouse_Src.Ani_Upg();

        StartCoroutine(Cor_Ani());

        m_pHouseData.Enter(m_nHouseLevel);
        m_pHouseExp_Src.AddValue(m_pHouseData._fHouseExp);
        m_nMyMoney -= m_pHouseData._nHousePrice;
        JAGameDataFile.I.SetData("PlayerMoney", m_nMyMoney);
        m_bUpgradeDone = false;
    }

    IEnumerator Cor_Ani()
    {
        m_pHouse_Src.Ani_Upg();
        yield return new WaitForSeconds(0.5f);

        m_pHouse_Src.Ani_Loop();
    }

    public void Fun_HouseMaxLevelEnd()
    {
        JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_Ended", "엔딩보기", "모든집을 업그레이드 하였습니다." + System.Environment.NewLine +
                            "엔딩 및 트로피가 증정되며" + System.Environment.NewLine + "게임은 초기화됩니다.", E_JA_POPUP.E_POP_ALL, "JAEndYesBtn", "");
    }
}
