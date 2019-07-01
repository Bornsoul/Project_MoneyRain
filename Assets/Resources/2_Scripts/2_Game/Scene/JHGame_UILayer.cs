using UnityEngine;
using System.Collections;

public class JHGame_UILayer : CSSingleton<JHGame_UILayer> {
 //   public JHGameUI_Score m_pScore = null;
    public JHGameUI_HpBar m_pHpBar = null;
    public JHGameUI_GameOver m_pGameOver = null;
    public JHGameUI_PowerBar m_pPowerBar = null;
    public JHPicture m_pPicture = null;
    public UIJoystick m_pJoyStick = null;
    public JHGameUI_Tutorial m_pTutorial = null;
    public JHMessageTutorial_Game m_pMessageTutorial = null;

    public Share m_pShare = null;

    bool m_bActive = false;

    public void Enter()
    {
        m_bActive = true;
     //   m_pScore.Enter();
       
        m_pPowerBar.Enter();
       // m_pTutorial.Enter();
    
        if(JAGameDataFile.I.GetData_Bool(JAGameDataFile.I._sFirstGameStr)==true)
        {
            JAGameDataFile.I.SetData(JAGameDataFile.I._sFirstGameStr, false);

            m_pMessageTutorial.Enter();
        }
      //  
    }

    public void Destroy()
    {
        m_bActive = false;
      //  m_pScore.Destroy();
        m_pHpBar.Destroy();
        m_pGameOver.Destroy();
        m_pPowerBar.Destroy();

        m_pTutorial.DeActive();
    }

    public void ShowGameOver()
    {
        m_pGameOver.gameObject.SetActive(true);
        m_pGameOver.Enter();
        CSSoundMng.I.Stop("bgm01");
    }
	
	// Update is called once per frame
	void Update () {
        if (m_bActive == false) return;

        if (JHGameData_Mng.I._GameCycle == false && Input.GetKeyDown(KeyCode.Escape) == true)
        {
            if (JHGameData_Mng.I._TutorialState == true) return;
            Btn_Pause();
        }
	}

    void Btn_Pause()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        if (JHGameData_Mng.I._TutorialState == true) return;
        if (JHGameData_Mng.I._GamePause == true) return;
        if (JHGameData_Mng.I._GameCycle == true) return;
        JHGameData_Mng.I.SetCycle(true);
        m_pJoyStick.GetOutOff();
       // CSPrefabMng.I.CreatePrefab(transform.gameObject, "2_Objects/GameUI/GameUI_PausePopup", "PausePopup");
        CSPrefabMng.I.CreatePrefab(transform.gameObject, "2_Objects/Popup/Pop_Option", "Pop_Option");
    }

    void Btn_Camera()
    {
        if (m_pPicture._Process == true) return;
        if (JHGameData_Mng.I._TutorialState == true) return;
        m_pPicture.TakePicture();
    }

    void Btn_GSencer()
    {
        JHGameData_Mng.I._GSencer = !JHGameData_Mng.I._GSencer;

    }
}
