using UnityEngine;
using System.Collections;

public class JHGameData_Stat : MonoBehaviour
{
    //-----------------------------------------------------------------------------
    float   m_fBasic_Speed = 0.0f;          //! 기본 능력치
    float   m_fBasic_JumpPower = 0.0f;
    int     m_nBasic_MoneyMany = 0;
    float   m_fBasic_MoneyTime = 0.0f;            
    float   m_fBasic_Hp = 45.0f;

    public float    _Basic_Speed { get { return m_fBasic_Speed; } }
    public float    _Basic_JumpPower { get { return m_fBasic_JumpPower; } }
    public int      _Basic_MoneyMany { get { return m_nBasic_MoneyMany; } }
    public float    _Basic_MoneyTime { get { return m_fBasic_MoneyTime; } }
    public float    _Basic_Hp { get { return m_fBasic_Hp; } }


    //-----------------------------------------------------------------------------
    int m_nStat_Speed = 0;                  //! 능력치 값 (Max 8)
    int m_nStat_JumpPower = 0;
    int m_nStat_MoneyWorth = 0;         //! 돈 가치

    int m_nStat_MoneyMany = 0;
    int m_nStat_Hp = 0;

    public int _Stat_Speed { get { return m_nStat_Speed; } }
    public int _Stat_JumpPower { get { return m_nStat_JumpPower; } }
    public int _Stat_MoneyWorth { get { return m_nStat_MoneyWorth; } }
    public int _Stat_MoneyMany { get { return m_nStat_MoneyMany; } }
    public int _Stat_Hp { get { return m_nStat_Hp; } }


    //-----------------------------------------------------------------------------
    float   m_fFactor_Speed = 0.0f;               //! 계수 (Result = Stat*Factor)
    float   m_fFactor_JumpPower = 0.0f;
    int     m_nFactor_MoneyMany = 0;
    float   m_fFactor_MoneyTime = 0.315f;
    float m_fFactor_Hp = 20.0f;

    public float    _Factor_Speed { get { return m_fFactor_Speed; } }
    public float    _Factor_JumpPower { get { return m_fFactor_JumpPower; } }
    public int      _Factor_MoneyMany { get { return m_nFactor_MoneyMany; } }
    public float    _Factor_MoneyTime { get { return m_fFactor_MoneyTime; } }
    public float    _Factor_Hp { get { return m_fFactor_Hp; } }

    //-----------------------------------------------------------------------------
     
    
    


    public void Enter()
    {
        
        //! 서버 능력치 불러오기 예시
        //! JAMenuData_Mng.I.m_pStatData_String.GetBase(E_JA_STAT.E_E_HP);
        //! JAMenuData_Mng.I.m_pStatData_String.GetStat(E_JA_STAT.E_E_HP);
        //! JAMenuData_Mng.I.m_pStatData_String.GetFactor(E_JA_STAT.E_E_HP);
        
        m_fBasic_Speed = JAMenuData_Mng.I.m_pStatData_String.GetBase(E_JA_BASE.E_B_SPEED);
        m_fBasic_JumpPower = JAMenuData_Mng.I.m_pStatData_String.GetBase(E_JA_BASE.E_B_JUMP_POWER);
        m_nBasic_MoneyMany = (int)JAMenuData_Mng.I.m_pStatData_String.GetBase(E_JA_BASE.E_B_MONEY_MANY);
        m_fBasic_MoneyTime = JAMenuData_Mng.I.m_pStatData_String.GetBase(E_JA_BASE.E_B_MONEY_TIME);
        m_fBasic_Hp = JAMenuData_Mng.I.m_pStatData_String.GetBase(E_JA_BASE.E_B_HP);


        /// 캐릭터 업그레이드
        m_nStat_Speed = JAGameDataFile.I.GetData_Int("SSpeed");
        m_nStat_JumpPower = JAGameDataFile.I.GetData_Int("SJumpPower");
        m_nStat_MoneyWorth = JAGameDataFile.I.GetData_Int("SMoneyWorth");
        //////////////////////////////////////////////////////////////////////////////////////////////////

        /// 집 업그레이드
        m_nStat_MoneyMany = JAGameDataFile.I.GetData_Int("SMoneyMany");
        m_nStat_Hp = JAGameDataFile.I.GetData_Int("SHp");


        m_fFactor_Speed = JAMenuData_Mng.I.m_pStatData_String.GetFactor(E_JA_FACTOR.E_F_SPEED);
        m_fFactor_JumpPower = JAMenuData_Mng.I.m_pStatData_String.GetFactor(E_JA_FACTOR.E_F_JUMP_POWER);
        m_nFactor_MoneyMany = (int)JAMenuData_Mng.I.m_pStatData_String.GetFactor(E_JA_FACTOR.E_F_MONEY_MANY);
        m_fFactor_MoneyTime = JAMenuData_Mng.I.m_pStatData_String.GetFactor(E_JA_FACTOR.E_F_MONEY_TIME);
        m_fFactor_Hp = JAMenuData_Mng.I.m_pStatData_String.GetFactor(E_JA_FACTOR.E_F_HP);    



       
       
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
