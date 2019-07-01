using UnityEngine;
using System.Collections;

public class JAUpgBot_Src : MonoBehaviour
{

    public UISlider[] m_pUpgSlider = null;
    public UILabel[] m_pUpgValueLabel = null;
    public UILabel[] m_pAddBtnLabel = null;

    string[] m_sStatString;
    public string[] _sStatString { get { return m_sStatString; } set { m_sStatString = value; } }
    int[] m_nStats;
    public int[] _nStats { get { return m_nStats; } set { m_nStats = value; } }

    int m_nMaxStat = 8;
    int m_nOneMaxStat = 8;
    int m_nTwoMaxStat = 8;
    int m_nThrMaxStat = 8;

    int m_nSelectUpg = 0;
    public int _nSelectUpg { get { return m_nSelectUpg; } set { m_nSelectUpg = value; } }
    int m_nMoney = 0;
    public int _nMoney { get { return m_nMoney; } set { m_nMoney = value; } }


    public UILabel[] m_pPriceLabel = null;

    public void Enter()
    {
        m_sStatString = new string[3] { "SMoneyWorth", "SSpeed", "SJumpPower" };

        m_nStats = new int[m_sStatString.Length];

        for (int i = 0; i < m_sStatString.Length; i++)
            m_nStats[i] = JAGameDataFile.I.GetData_Int(m_sStatString[i]);

        for (int i = 0; i < m_pUpgSlider.Length; i++)
            m_pUpgSlider[i].value = ((float)m_nStats[i] / m_nMaxStat) * 1f;

        for (int i = 0; i < m_pUpgValueLabel.Length; i++)
        {
            m_pUpgValueLabel[i].text = m_nStats[i] + " / " + m_nMaxStat;
        }

        //Debug.Log("1 : " + m_nStats[0] + ", 2 : " + JAGameDataFile.I.GetData_Int(m_sStatString[0]));
        //Debug.Log("3 : " + m_nStats[1] + ", 4 : " + JAGameDataFile.I.GetData_Int(m_sStatString[1]));
    }

    public void Tutorial_Stat()
    {
        m_nOneMaxStat = 2;
        m_nTwoMaxStat = 2;
        m_nThrMaxStat = 2;
    }

    public void Normal_Stat()
    {
        m_nOneMaxStat = m_nMaxStat;
        m_nTwoMaxStat = m_nMaxStat;
        m_nThrMaxStat = m_nMaxStat;
    }

    private int GetMoneyShow(int nStat)
    {
        Debug.Log("Stat : " + nStat);
        int nResult = 0;
        int[] nPrice = { 3000, 5000, 11000, 20000, 37000, 70000, 135000, 0 };

        nResult = nPrice[nStat-1];
        Debug.Log("Result : " + nResult);
        return nResult;
    }

    void Update()
    {
        
        for (int i = 0; i < m_sStatString.Length; i++)
            if (m_nStats[i] == null) return;

        for (int i = 0; i < m_sStatString.Length; i++)
            m_nStats[i] = JAGameDataFile.I.GetData_Int(m_sStatString[i]);

        for (int i = 0; i < m_pUpgSlider.Length; i++)
            m_pUpgSlider[i].value = ((float)m_nStats[i] / m_nMaxStat) * 1f;

        for (int i = 0; i < m_pUpgValueLabel.Length; i++)
            m_pUpgValueLabel[i].text = m_nStats[i] + " / " + m_nMaxStat;

        if (m_nStats[0] >= m_nOneMaxStat)
            m_pPriceLabel[0].text = "최대치";
        else
            m_pPriceLabel[0].text = GetMoneyShow(m_nStats[0]).ToString();

        if (m_nStats[1] >= m_nTwoMaxStat)
            m_pPriceLabel[1].text = "최대치";
        else
            m_pPriceLabel[1].text = GetMoneyShow(m_nStats[1]).ToString();

        if (m_nStats[2] >= m_nThrMaxStat)
            m_pPriceLabel[2].text = "최대치";
        else
            m_pPriceLabel[2].text = GetMoneyShow(m_nStats[2]).ToString();


        #region #Debug#
        for (int i = 0; i < m_pAddBtnLabel.Length; i++)
        {
            if (JAPop_CharacterUpg.I.m_pUpgTop_Src.m_bChange == false)
                m_pAddBtnLabel[i].text = "+";
            else
                m_pAddBtnLabel[i].text = "-";
        }
        #endregion
    }

    public void Button_MoneyWorth()
    {
        CSSoundMng.I.Play("Shop_Coin");
        m_nSelectUpg = 0;
        m_nMoney = GetMoneyShow(GetSelectStats());
        Debug.Log(m_nMoney);

        int nCnt = m_nStats[m_nSelectUpg];
        if (JAPop_CharacterUpg.I.m_pUpgTop_Src.m_bChange == false)
        {
            if (GetSelectStats() >= m_nOneMaxStat) return;
            if (JAGameDataFile.I.GetData_Int("PlayerMoney") < m_nMoney)
            {
                Debug.Log("MoneyWorth 돈부족");
                return;
            }
            else
            {
                //JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_HouseUpgBuy",
                //        "업그레이드",
                //        "[E88136]공연시간[-]를(을) 강화 하시겠습니까?\n\n" + m_nStats[m_nSelectUpg] + "  ->  [E8725E][b]" +
                //         (nCnt = JAPop_HouseUpg.I.m_bChange == false ? ++nCnt : --nCnt) + "[/b][-]", false,
                //        E_JA_POPUP.E_POP_ALL, "JAHouseUpgBuyBtn");

                JAHouseSystem_Mng.I._nMyMoney -= m_nMoney;
                JAGameDataFile.I.SetData("PlayerMoney", JAHouseSystem_Mng.I._nMyMoney);

                JAPop_CharacterUpg.I.m_pUpgBot_Src._nStats[JAPop_CharacterUpg.I.m_pUpgBot_Src._nSelectUpg]++;

                JAGameDataFile.I.SetData(JAPop_CharacterUpg.I.m_pUpgBot_Src._sStatString[JAPop_CharacterUpg.I.m_pUpgBot_Src._nSelectUpg],
                JAPop_CharacterUpg.I.m_pUpgBot_Src.GetSelectStats());
            }
        }
        else
        {
            if (GetSelectStats() <= 1) return;
            JAHouseSystem_Mng.I._nMyMoney += m_nMoney;
            JAGameDataFile.I.SetData("PlayerMoney", JAHouseSystem_Mng.I._nMyMoney);

            m_nStats[m_nSelectUpg]--;
            JAGameDataFile.I.SetData(m_sStatString[m_nSelectUpg], GetSelectStats());
        }


        //int nCnt = m_nStats[(int)E_JA_STAT.E_S_MONEY_WORTH];
        //if (m_nStats[(int)E_JA_STAT.E_S_MONEY_WORTH] >= m_nMaxStat)
        //{
        //    JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_CharUpgBuyMax",
        //        "업그레이드",
        //        "더이상 업그레이드를\n 할수없습니다.",
        //        E_JA_POPUP.E_POP_OK);
        //    return;
        //}

        //m_nSelectUpg = (int)E_JA_STAT.E_S_MONEY_WORTH;
        //JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_CharUpgBuy",
        //        "업그레이드",
        //        "[E88136]돈가치[-]를(을) 강화 하시겠습니까?\n\n" + m_nStats[(int)E_JA_STAT.E_S_MONEY_WORTH] + "  ->  [E8725E][b]" + 
        //        (nCnt = JAPop_CharacterUpg.I.m_pUpgTop_Src.m_bChange ==false ? ++nCnt : --nCnt) + "[/b][-]",
        //        E_JA_POPUP.E_POP_ALL, "JAUpgradeBuyBtn");

    }

    public void Button_Speed()
    {
        CSSoundMng.I.Play("Shop_Coin");
        m_nSelectUpg = 1;
        m_nMoney = GetMoneyShow(GetSelectStats());

        int nCnt = m_nStats[m_nSelectUpg];
        if (JAPop_CharacterUpg.I.m_pUpgTop_Src.m_bChange == false)
        {
            if (GetSelectStats() >= m_nTwoMaxStat) return;
            if (JAGameDataFile.I.GetData_Int("PlayerMoney") < m_nMoney)
            {
                Debug.Log("Speed 돈부족");
                return;
            }
            else
            {
                //JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_HouseUpgBuy",
                //        "업그레이드",
                //        "[E88136]공연시간[-]를(을) 강화 하시겠습니까?\n\n" + m_nStats[m_nSelectUpg] + "  ->  [E8725E][b]" +
                //         (nCnt = JAPop_HouseUpg.I.m_bChange == false ? ++nCnt : --nCnt) + "[/b][-]", false,
                //        E_JA_POPUP.E_POP_ALL, "JAHouseUpgBuyBtn");

                JAHouseSystem_Mng.I._nMyMoney -= m_nMoney;
                JAGameDataFile.I.SetData("PlayerMoney", JAHouseSystem_Mng.I._nMyMoney);

                JAPop_CharacterUpg.I.m_pUpgBot_Src._nStats[JAPop_CharacterUpg.I.m_pUpgBot_Src._nSelectUpg]++;

                JAGameDataFile.I.SetData(JAPop_CharacterUpg.I.m_pUpgBot_Src._sStatString[JAPop_CharacterUpg.I.m_pUpgBot_Src._nSelectUpg],
                JAPop_CharacterUpg.I.m_pUpgBot_Src.GetSelectStats());
            }
        }
        else
        {
            if (GetSelectStats() <= 1) return;
            JAHouseSystem_Mng.I._nMyMoney += m_nMoney;
            JAGameDataFile.I.SetData("PlayerMoney", JAHouseSystem_Mng.I._nMyMoney);

            m_nStats[m_nSelectUpg]--;
            JAGameDataFile.I.SetData(m_sStatString[m_nSelectUpg], GetSelectStats());
        }

        //int nCnt = m_nStats[(int)E_JA_STAT.E_S_SPEED];
        //if (m_nStats[(int)E_JA_STAT.E_S_SPEED] >= m_nMaxStat)
        //{
        //    JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_CharUpgBuyMax",
        //        "업그레이드",
        //        "더이상 업그레이드를\n 할수없습니다.",
        //        E_JA_POPUP.E_POP_OK);
        //    return;
        //}

        //m_nSelectUpg = (int)E_JA_STAT.E_S_SPEED;
        //JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_CharUpgBuy",
        //        "업그레이드",
        //        "[E88136]스피드[-]를(을) 강화 하시겠습니까?\n\n" + m_nStats[(int)E_JA_STAT.E_S_SPEED] + "  ->  [E8725E][b]" +
        //         (nCnt = JAPop_CharacterUpg.I.m_pUpgTop_Src.m_bChange == false ? ++nCnt : --nCnt) + "[/b][-]",
        //        E_JA_POPUP.E_POP_ALL, "JAUpgradeBuyBtn");

    }

    public void Button_JumpPower()
    {
        CSSoundMng.I.Play("Shop_Coin");
        m_nSelectUpg = 2;
        m_nMoney = GetMoneyShow(GetSelectStats());

        int nCnt = m_nStats[m_nSelectUpg];
        if (JAPop_CharacterUpg.I.m_pUpgTop_Src.m_bChange == false)
        {
            if (GetSelectStats() >= m_nThrMaxStat) return;
            if (JAGameDataFile.I.GetData_Int("PlayerMoney") < m_nMoney)
            {
                Debug.Log("JumpPower 돈부족");
                return;
            }
            else
            {
                //JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_HouseUpgBuy",
                //        "업그레이드",
                //        "[E88136]공연시간[-]를(을) 강화 하시겠습니까?\n\n" + m_nStats[m_nSelectUpg] + "  ->  [E8725E][b]" +
                //         (nCnt = JAPop_HouseUpg.I.m_bChange == false ? ++nCnt : --nCnt) + "[/b][-]", false,
                //        E_JA_POPUP.E_POP_ALL, "JAHouseUpgBuyBtn");

                JAHouseSystem_Mng.I._nMyMoney -= m_nMoney;
                JAGameDataFile.I.SetData("PlayerMoney", JAHouseSystem_Mng.I._nMyMoney);

                JAPop_CharacterUpg.I.m_pUpgBot_Src._nStats[JAPop_CharacterUpg.I.m_pUpgBot_Src._nSelectUpg]++;

                JAGameDataFile.I.SetData(JAPop_CharacterUpg.I.m_pUpgBot_Src._sStatString[JAPop_CharacterUpg.I.m_pUpgBot_Src._nSelectUpg],
                JAPop_CharacterUpg.I.m_pUpgBot_Src.GetSelectStats());
            }
        }
        else
        {
            if (GetSelectStats() <= 1) return;
            JAHouseSystem_Mng.I._nMyMoney += m_nMoney;
            JAGameDataFile.I.SetData("PlayerMoney", JAHouseSystem_Mng.I._nMyMoney);

            m_nStats[m_nSelectUpg]--;
            JAGameDataFile.I.SetData(m_sStatString[m_nSelectUpg], GetSelectStats());
        }
        //int nCnt = m_nStats[(int)E_JA_STAT.E_S_JUMP_POWER];
        //if (m_nStats[(int)E_JA_STAT.E_S_JUMP_POWER] >= m_nMaxStat)
        //{
        //    JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_CharUpgBuyMax",
        //        "업그레이드",
        //        "더이상 업그레이드를\n 할수없습니다.",
        //        E_JA_POPUP.E_POP_OK);
        //    return;
        //}

        //m_nSelectUpg = (int)E_JA_STAT.E_S_JUMP_POWER;
        //JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_CharUpgBuy",
        //        "업그레이드",
        //        "[E88136]스피드[-]를(을) 강화 하시겠습니까?\n\n" + m_nStats[(int)E_JA_STAT.E_S_JUMP_POWER] + "  ->  [E8725E][b]" +
        //         (nCnt = JAPop_CharacterUpg.I.m_pUpgTop_Src.m_bChange == false ? ++nCnt : --nCnt) + "[/b][-]",
        //        E_JA_POPUP.E_POP_ALL, "JAUpgradeBuyBtn");
    }

    //public void Button_MoneyMany()
    //{
    //    int nCnt = m_nStats[(int)E_JA_STAT.E_C_MONEY_MANY];
    //    if (m_nStats[(int)E_JA_STAT.E_C_MONEY_MANY] >= m_nMaxStat)
    //    {
    //        JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_CharUpgBuyMax",
    //            "업그레이드",
    //            "더이상 업그레이드를\n 할수없습니다.",
    //            E_JA_POPUP.E_POP_OK);
    //        return;
    //    }

    //    m_nSelectUpg = (int)E_JA_STAT.E_C_MONEY_MANY;
    //    JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_CharUpgBuy",
    //            "업그레이드",
    //            "[E88136]스피드[-]를(을) 강화 하시겠습니까?\n\n" + m_nStats[(int)E_JA_STAT.E_C_MONEY_MANY] + "  ->  [E8725E][b]" + (++nCnt) + "[/b][-]",
    //            E_JA_POPUP.E_POP_ALL, "JAUpgradeBuyBtn");
    //}

    //public void Button_Health()
    //{
    //    int nCnt = m_nStats[(int)E_JA_STAT.E_E_HP];
    //    if (m_nStats[(int)E_JA_STAT.E_E_HP - 1] >= m_nMaxStat)
    //    {
    //        JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_CharUpgBuyMax",
    //             "업그레이드",
    //             "더이상 업그레이드를\n 할수없습니다.",
    //             E_JA_POPUP.E_POP_OK);
    //        return;
    //    }

    //    m_nSelectUpg = (int)E_JA_STAT.E_E_HP;
    //    JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_CharUpgBuy",
    //            "업그레이드",
    //            "[E88136]스피드[-]를(을) 강화 하시겠습니까?\n\n" + m_nStats[(int)E_JA_STAT.E_E_HP] + "  ->  [E8725E][b]" + (++nCnt) + "[/b][-]",
    //            E_JA_POPUP.E_POP_ALL, "JAUpgradeBuyBtn");
    //}

    public int GetSelectStats()
    {
        return m_nStats[m_nSelectUpg];
    }
}
