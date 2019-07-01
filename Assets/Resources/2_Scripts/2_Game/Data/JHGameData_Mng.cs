using UnityEngine;
using System.Collections;




/// <summary>
/// 게임 데이터 무역관리자(스코어, 일시정지, 등등)
/// </summary>
public class JHGameData_Mng : CSSingleton<JHGameData_Mng> {
    public JHGameData_Stat m_pData_Stat = null;
  


    bool m_bActive = false;

    int m_nGameScore = 0;
    public int _GameScore { get { return m_nGameScore; } set { m_nGameScore = value; } }

    bool m_bGamePause = false;
    public bool _GamePause { get { return m_bGamePause; } }

    bool m_bGameCycle = false;
    public bool _GameCycle { get { return m_bGameCycle; } }

    float m_fGameLevelTime = 0.0f;
    public float _GameLevelTime { get { return m_fGameLevelTime; } }

    float m_fRealPlayTime = 0.0f;
    public float _RealPlayTime { get { return m_fRealPlayTime; } }

    bool m_bGSencer = false;
    public bool _GSencer { get { return m_bGSencer; } set { m_bGSencer = value; } }
   


    bool m_bSound = true;
    public bool _Sound { get { return m_bSound; } set { m_bSound = value; } }
    bool m_bVibe = true;
    public bool _Vibe { get { return m_bVibe; } set { m_bVibe = value; } }

    bool m_bTutorialState = false;
    public bool _TutorialState { get { return m_bTutorialState; } set { m_bTutorialState = value; } }
	public void Enter()
    {
        m_pData_Stat.Enter();
       


        m_nGameScore = 0;
        m_bGamePause = false;
        m_bActive = true;
        m_bGameCycle = true;
    }

    public void Destroy()
    {
       
    }

    public void SetPause(bool bPause)
    {
        m_bGamePause = bPause;
    }

    public void SetCycle(bool bCycle)
    {
        m_bGameCycle = bCycle;
    }

    public void SetGameOver()
    {
        SetCycle(true);
        JHGame_UILayer.I.ShowGameOver();
    }
	
	// Update is called once per frame
	void Update () {
        if (m_bActive == false) return;
       // Debug.Log(m_fGameLevelTime);
        m_bVibe = JAGameDataFile.I.GetData_Bool("VibMod");
        m_bGSencer = !JAGameDataFile.I.GetData_Bool("TouchMod");

        if (m_bGamePause == false && m_bGameCycle==false)
        {
            m_fGameLevelTime += Time.deltaTime;
            m_fRealPlayTime += Time.deltaTime;
        }
	}
}
