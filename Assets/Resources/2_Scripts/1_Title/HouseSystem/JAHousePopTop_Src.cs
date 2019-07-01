using UnityEngine;
using System.Collections;

public class JAHousePopTop_Src : MonoBehaviour
{
    public JAHousePopExp_Src m_pExp_Src = null;
    public UILabel m_pBtnLabel = null;
    public UILabel m_pBtnPrice = null;
    public UISprite m_pBtnLabelSprite = null;
    public UISprite m_pBtnPriceSprite = null;

    public GameObject[] m_pBtnGam = null;

    public Animation m_pBtnAni = null;
    private float m_fBtnAniTime = 0f;

    public UISlider[] m_pUpgSlider = null;
    public UILabel[] m_pUpgValueLabel = null;
    public UILabel[] m_pAddBtnLabel = null; //!< Debug

    string[] m_sStatString;
    public string[] _sStatString { get { return m_sStatString; } set { m_sStatString = value; } }
    int[] m_nStats;
    public int[] _nStats { get { return m_nStats; } set { m_nStats = value; } }

    int m_nMaxStat = 8;
    int m_nHpMaxStat = 3;
    int m_nMoneyManyMaxStat = 4;

    int m_nSelectUpg = 0;
    public int _nSelectUpg { get { return m_nSelectUpg; } set { m_nSelectUpg = value; } }
    int m_nMoney = 0;
    public int _nMoney { get { return m_nMoney; } set { m_nMoney = value; } }

    public UILabel[] m_pPriceLabel = null;


    public void Enter()
    {
        m_nMaxStat = 8;
        m_pExp_Src.Enter();

        m_sStatString = new string[2] { "SHp", "SMoneyMany" };

        m_nStats = new int[m_sStatString.Length];

        for (int i = 0; i < m_sStatString.Length; i++)
            m_nStats[i] = JAGameDataFile.I.GetData_Int(m_sStatString[i]);

        for (int i = 0; i < m_pUpgSlider.Length; i++)
            m_pUpgSlider[i].value = ((float)m_nStats[i] / m_nMaxStat) * 1f;

        for (int i = 0; i < m_pUpgValueLabel.Length; i++)
        {
            m_pUpgValueLabel[i].text = m_nStats[i] + " / " + m_nMaxStat;
        }
    }

    void Update()
    {
        m_fBtnAniTime += Time.deltaTime;

        if (m_fBtnAniTime > 3f)
        {
            m_pBtnAni.Play();
            m_fBtnAniTime = Time.deltaTime;
        }

        m_pBtnPrice.text = JAHouseSystem_Mng.I._pHouseData._nHousePrice.ToString();

        

        for (int i = 0; i < m_sStatString.Length; i++)
            if (m_nStats[i] == null) return;

        for (int i = 0; i < m_sStatString.Length; i++)
            m_nStats[i] = JAGameDataFile.I.GetData_Int(m_sStatString[i]);

        for (int i = 0; i < m_pUpgSlider.Length; i++)
            m_pUpgSlider[i].value = ((float)m_nStats[i] / m_nMaxStat) * 1f;

        for (int i = 0; i < m_pUpgValueLabel.Length; i++)
            m_pUpgValueLabel[i].text = m_nStats[i] + " / " + m_nMaxStat;

        if (m_nStats[0] >= m_nHpMaxStat)
        {
            m_pPriceLabel[0].text = "최대치";
        }
        else
        {
            m_pPriceLabel[0].text = Seung2((JAHouseSystem_Mng.I._pHouseData._nHousePrice / 10) / 2, m_nStats[0] + 1).ToString();

        }

        if (m_nStats[1] >= m_nMoneyManyMaxStat)
        {
            m_pPriceLabel[1].text = "최대치";
        }
        else
        {
            m_pPriceLabel[1].text = Seung2((JAHouseSystem_Mng.I._pHouseData._nHousePrice / 10) / 2, m_nStats[1] + 1).ToString();

        }

        #region #Debug#
        for (int i = 0; i < m_pAddBtnLabel.Length; i++)
        {
            if (JAPop_HouseUpg.I.m_bChange == false)
                m_pAddBtnLabel[i].text = "+";
            else
                m_pAddBtnLabel[i].text = "-";
        }
        #endregion
    }

    public void Tutorial_Stat()
    {
        m_nHpMaxStat = 3;
        m_nMoneyManyMaxStat = 4;
    }

    public void Normal_Stat()
    {
        m_nHpMaxStat = m_nMaxStat;
        m_nMoneyManyMaxStat = m_nMaxStat;
    }

    int Seung2(int a, int b)
    {
        int nRe = a;
        for(int i=0;i<b-1;i++)
        {
            nRe = nRe * 2;
        }
        //Debug.Log("Result : " + nRe);
        return nRe;
    }


    public void Fun_MaxLevelBtn(bool bMax)
    {
        if ( bMax == true )
        {
            NGUITools.SetActive(m_pBtnGam[1], false);
            m_pBtnPrice.enabled = false;
            m_pBtnPriceSprite.enabled = false;

            m_pBtnGam[0].transform.localPosition = new Vector3(0f, -25f, 0f);
            m_pBtnLabel.text = "엔딩보기";
        }
        else
        {
            for (int i = 0; i < m_pBtnGam.Length; i++ )
                NGUITools.SetActive(m_pBtnGam[1], true);
            m_pBtnPrice.enabled = true;
            m_pBtnPriceSprite.enabled = true;

            m_pBtnGam[0].transform.localPosition = new Vector3(0f, 0, 0f);
            m_pBtnLabel.text = "집 강화";
        }
    }

    public void Button_Hp()
    {

        m_nSelectUpg = 0;
        m_nMoney = Seung2((JAHouseSystem_Mng.I._pHouseData._nHousePrice / 10) / 2, GetSelectStats() + 1);

        m_pPriceLabel[0].text = m_nMoney.ToString();

        int nCnt = m_nStats[m_nSelectUpg];
        if (JAPop_HouseUpg.I.m_bChange == false)
        {
            if (GetSelectStats() >= m_nHpMaxStat)
            {
                m_pPriceLabel[0].text = "최대치";
                return;
            }
            if (JAGameDataFile.I.GetData_Int("PlayerMoney") < m_nMoney)
            {
                Debug.Log("Hp 돈부족");
                return;
            }
            else
            {
                //JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_HouseUpgBuy",
                //        "업그레이드",
                //        "[E88136]공연시간[-]를(을) 강화 하시겠습니까?\n\n" + m_nStats[m_nSelectUpg] + "  ->  [E8725E][b]" +
                //         (nCnt = JAPop_HouseUpg.I.m_bChange == false ? ++nCnt : --nCnt) + "[/b][-]", false,
                //        E_JA_POPUP.E_POP_ALL, "JAHouseUpgBuyBtn");

                JAHouseSystem_Mng.I._nMyMoney -= JAPop_HouseUpg.I.m_pHPopTop_Src._nMoney;
                JAGameDataFile.I.SetData("PlayerMoney", JAHouseSystem_Mng.I._nMyMoney);

                JAPop_HouseUpg.I.m_pHPopTop_Src._nStats[JAPop_HouseUpg.I.m_pHPopTop_Src._nSelectUpg]++;

                JAGameDataFile.I.SetData(JAPop_HouseUpg.I.m_pHPopTop_Src._sStatString[JAPop_HouseUpg.I.m_pHPopTop_Src._nSelectUpg],
                JAPop_HouseUpg.I.m_pHPopTop_Src.GetSelectStats());
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
    }

    public void Button_MoneyMany()
    {

        m_nSelectUpg = 1;
        m_nMoney = Seung2((JAHouseSystem_Mng.I._pHouseData._nHousePrice / 10) / 2, GetSelectStats()+1);

        m_pPriceLabel[1].text = m_nMoney.ToString();

        int nCnt = m_nStats[m_nSelectUpg];
        if (JAPop_HouseUpg.I.m_bChange == false)
        {
            if (GetSelectStats() >= m_nMoneyManyMaxStat)
            {
                m_pPriceLabel[1].text = "최대치";
                return;
            }
            if (JAGameDataFile.I.GetData_Int("PlayerMoney") < m_nMoney)
            {
                Debug.Log("MoneyMany 돈부족");
                return;
            }
            else
            {

                //JAMenuData_Mng.I.CreatePopup(JHTitle_Scene.I._PopLayer2.m_pAnchor, "Pop_HouseUpgBuy",
                //        "업그레이드",
                //        "[E88136]공연시간[-]를(을) 강화 하시겠습니까?\n\n" + m_nStats[m_nSelectUpg] + "  ->  [E8725E][b]" +
                //         (nCnt = JAPop_HouseUpg.I.m_bChange == false ? ++nCnt : --nCnt) + "[/b][-]", false,
                //        E_JA_POPUP.E_POP_ALL, "JAHouseUpgBuyBtn");

                JAHouseSystem_Mng.I._nMyMoney -= JAPop_HouseUpg.I.m_pHPopTop_Src._nMoney;
                JAGameDataFile.I.SetData("PlayerMoney", JAHouseSystem_Mng.I._nMyMoney);

                JAPop_HouseUpg.I.m_pHPopTop_Src._nStats[JAPop_HouseUpg.I.m_pHPopTop_Src._nSelectUpg]++;

                JAGameDataFile.I.SetData(JAPop_HouseUpg.I.m_pHPopTop_Src._sStatString[JAPop_HouseUpg.I.m_pHPopTop_Src._nSelectUpg],
                JAPop_HouseUpg.I.m_pHPopTop_Src.GetSelectStats());
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
    }

    public int GetSelectStats()
    {
        //Debug.Log("Fuck : " + m_nStats[m_nSelectUpg]);
        return m_nStats[m_nSelectUpg];
    }
}
