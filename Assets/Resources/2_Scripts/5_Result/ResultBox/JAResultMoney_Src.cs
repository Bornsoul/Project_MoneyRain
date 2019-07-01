using UnityEngine;
using System.Collections;

public class JAResultMoney_Src : MonoBehaviour
{
    public UILabel m_pMoneyLabel = null;
    public UILabel m_pResultMoneyLabel = null;
    public UILabel m_pFuckMoneyLabel = null;

    int m_nMoney1 = 0;
    int m_nMoney2 = 0;
    int m_nMyMoney = 0;

    int m_nResultMoney = 0;
    int m_nFuckMoney = 0;

    bool m_bClick = false;

    public void Enter(int nGetMoney)
    {
        m_bClick = false;
        m_nMyMoney = nGetMoney;
        m_pMoneyLabel.text = "0";
        m_nMoney1 = nGetMoney ;
        m_nMoney2 = (nGetMoney - (int)((nGetMoney / 100.0f) * 10.0f));
        m_nFuckMoney = (int)((nGetMoney / 100.0f) * 10.0f);
        m_pFuckMoneyLabel.text = "";
    }

    public void MoneyUpdate()
    {
        //Debug.Log("FuckMoney : " + m_nMoney1);
        if (m_bClick == true)
        {
            m_nResultMoney = m_nMoney1;
            m_pMoneyLabel.text = JAMenuData_Mng.I.GetIntNumberString(m_nResultMoney);
            return; 
        }

        if (m_nResultMoney >= m_nMoney1) { m_bClick = true; m_nResultMoney = m_nMoney1; return; }
        m_nResultMoney = (int)Mathf.Lerp(m_nResultMoney, m_nMoney1, 0.03f);
        m_nResultMoney++;

        if (Input.GetMouseButtonUp(0))
        {
            m_bClick = true;
        }

        m_pMoneyLabel.text = JAMenuData_Mng.I.GetIntNumberString(m_nResultMoney);
    }

    public void ResultMoneyUpdate()
    {
        if (m_bClick == true)
        {
            m_nResultMoney = m_nMoney2;
            m_pResultMoneyLabel.text = JAMenuData_Mng.I.GetIntNumberString(m_nResultMoney);
            return;
        }

        if (m_nResultMoney >= m_nMoney2) { m_bClick = true; m_nResultMoney = m_nMoney2; return; }
        m_nResultMoney = (int)Mathf.Lerp(m_nResultMoney, m_nMoney2, 0.06f);
        m_nResultMoney++;
        
        
        if (Input.GetMouseButtonUp(0))
        {
            
            m_bClick = true;
        }

        m_pFuckMoneyLabel.text = "[7F7158]세금 [CC4700]10%[-]로 인하여[-]" + System.Environment.NewLine +
            "[FF5900]" + JAMenuData_Mng.I.GetIntNumberString(m_nFuckMoney) + "[-] [7F7158]만큼의 [CC4700]골드[-]가 빠져나갔습니다.[-]";
        m_pResultMoneyLabel.text = JAMenuData_Mng.I.GetIntNumberString(m_nResultMoney);
    }

    public bool GetDone()
    {

        return m_bClick;
    }

}
