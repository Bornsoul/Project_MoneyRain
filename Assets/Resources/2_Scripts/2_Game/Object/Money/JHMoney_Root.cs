using UnityEngine;
using System.Collections;

public enum E_MONEY_STYLE
{
    E_COIN = 0,            //! 고체, 동전같이 수직으로 떨어짐
    E_PAPER,                //! 종이, 지폐

    E_MAX,
}

public enum E_MONEY_CLASS
{
    E_COIN_BRONZE = 0,
    E_COIN_SILVER,
    E_COIN_GOLD,
    E_PAPER_BRONZE,
    E_PAPER_SILVER,
    E_PAPER_GOLD,
    E_POCKET_BRONZE,
    E_POCKET_SILVER,
    E_POCKET_GOLD,

    E_MAX,
}

public class JHMoney_Root : JHObject_Root {
    protected int m_nGold;
    public int _Gold { get { return m_nGold; } }

    protected E_MONEY_STYLE m_eStyle;
    public E_MONEY_STYLE _Style { get { return m_eStyle; } }

    protected E_MONEY_CLASS m_eClass;
    public E_MONEY_CLASS _Class { get { return m_eClass; } }

    protected JumpStruct    m_stJump;
    protected float         m_fDownSpeed;

    protected float m_fPaper_Amp = 0.0f;
    protected float m_fPaper_Frep = 0.0f;
    protected float m_fPaper_StartTime = 0.0f;
    protected float m_fPaper_TotalTime = 0.0f;
    protected Vector3 m_fOffset;

    protected const float m_fWallX = 332.0f;            //! 최대 벽위치
    protected const float m_fStartY = 700.0f;           //! 소환됬을때 떨어질 Y좌표

    JumpStruct m_stMagnet;
    float   m_fMagnetAddX;
    float   m_fMagnetAddY;
    bool    m_bMagnet;
    public bool _Magnet { get { return m_bMagnet; } }
    bool m_bMagnet_Back;
    float m_fMagnetFindTime;

    virtual public bool Enter()
    {
        if (base.Enter() == false) return false;
        m_nGold = 1;

        m_eStyle = E_MONEY_STYLE.E_COIN;
        m_eClass = E_MONEY_CLASS.E_COIN_BRONZE;

        m_stJump.Reset();

        m_fPaper_Amp = 0.0f;
        m_fPaper_Frep = 0.0f;
        m_fPaper_StartTime = 0.0f;
        m_fPaper_TotalTime = 0.0f;
        m_fOffset = new Vector3(0, 0, 0);
        return true;
    }

    virtual public bool Create()
    {
        if (base.Create() == false) return false;
        float fSpawnX = (float)CSRandom.Rand(0, (int)((m_fWallX) * 2.0f));
     //   fSpawnX += Random.RandomRange(0.0f, 100.0f);
        fSpawnX -= m_fWallX;

        m_stPos = new Vector3(fSpawnX, m_fStartY, 0.0f);

        ResetTransform();
        return true;
    }

    virtual public bool End()
    {
        if (base.End() == false) return false;

        return true;
    }

    virtual public bool Destroy()
    {
        if (base.Destroy() == false) return false;

        return true;
    }

    virtual public void Eated()
    {
        JHGame_MainLayer.I.m_pEffect_Mng.CreateEffect_PT_SoftStar(m_stPos);
        End();
    }

    public int GetMoneyWorthPay()
    {
        return m_nGold + ((m_nGold / 2) * JHGameData_Mng.I.m_pData_Stat._Stat_MoneyWorth);
    }

    // Update is called once per frame
    virtual public bool Update()
    {
        if (base.Update() == false) return false;
        if (m_bActive == false) return false;

        if (m_bMagnet == true)
        {
            MagnetUpdate();
            ResetTransform();
            return false;
        }
        
        //이곳에 일시정지나 이런거 예외처리하면됨
        return true;
    }


    public void SetMagnet()
    {
        if (m_bMagnet == true) return;
        
        Vector3 stDis = GetDistance(m_stPos, JHGame_MainLayer.I._Hero.pSrc._Position);
        m_fMagnetAddX = ((stDis.x / stDis.z));
        m_fMagnetAddY = ((stDis.y / stDis.z));

        m_bMagnet_Back = true;
        m_stMagnet.m_fDt = 0.0f;
        m_stMagnet.m_fJumpDt = m_stMagnet.m_fDt;
        m_fMagnetFindTime = 0.0f;
        m_bMagnet = true;

    }

    public void MagnetUpdate()
    {
        m_stMagnet.m_fDt += Time.deltaTime;
        float fMove = 0.0f;
        if (m_bMagnet_Back == true)
        {
            fMove = 22.0f - (60.9f * (m_stMagnet.m_fDt - m_stMagnet.m_fJumpDt));
            m_stPos.x += (m_fMagnetAddX * -fMove*100.0f) * Time.deltaTime;
            m_stPos.y += (m_fMagnetAddY * -fMove * 100.0f) * Time.deltaTime;
            if (fMove < 0)
            {
                m_stMagnet.m_fJumpDt = m_stMagnet.m_fDt;
                m_bMagnet_Back = false;
            }
        }
        else
        {
            fMove = (60.9f * ((m_stMagnet.m_fDt - m_stMagnet.m_fJumpDt)));
            m_stPos.x += (m_fMagnetAddX * fMove * 100.0f) * Time.deltaTime;
            m_stPos.y += (m_fMagnetAddY * fMove * 100.0f) * Time.deltaTime;

            m_fMagnetFindTime += Time.deltaTime;
            if (m_fMagnetFindTime > 0.1f)
            {
                Vector3 stDis = GetDistance(m_stPos, JHGame_MainLayer.I._Hero.pSrc._Position);
                m_fMagnetAddX = ((stDis.x / stDis.z));
                m_fMagnetAddY = ((stDis.y / stDis.z));
                m_fMagnetFindTime = 0.0f;
            }
        }
    }

    Vector3 GetDistance(Vector3 stPos1, Vector3 stPos2)
    {
        Vector3 vPos;
        vPos.x = stPos2.x - stPos1.x;
        vPos.y = stPos2.y - stPos1.y;

        vPos.z = Mathf.Sqrt((vPos.x * vPos.x) + (vPos.y * vPos.y));

        return vPos;
    }
}
