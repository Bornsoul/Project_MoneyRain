using UnityEngine;
using System.Collections;

public class JHGameUI_PausePopup : MonoBehaviour {
    public GameObject m_pSoundCheck = null;
    public GameObject m_pVibeCheck = null;

    public GameObject m_pTouchCheck = null;
    public GameObject m_pSencerCheck = null;

	// Use this for initialization
	void Start () {
        SetState();
	}
	
	// Update is called once per frame
	void Update () {
        if (JHGameData_Mng.I._GameCycle == true && Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Btn_Continue();
        }
	}

    void SetState()
    {
        m_pSoundCheck.SetActive(JHGameData_Mng.I._Sound);
        m_pVibeCheck.SetActive(JHGameData_Mng.I._Vibe);
        m_pTouchCheck.SetActive(!JHGameData_Mng.I._GSencer);
        m_pSencerCheck.SetActive(JHGameData_Mng.I._GSencer);
    }

    void Btn_Sound()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGameData_Mng.I._Sound = !JHGameData_Mng.I._Sound;
        SetState();
    }

    void Btn_Vibe()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGameData_Mng.I._Vibe = !JHGameData_Mng.I._Vibe;
        SetState();
    }

    void Btn_Touch()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGameData_Mng.I._GSencer = false;
        SetState();
    }

    void Btn_Sencer()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGameData_Mng.I._GSencer = true;
        SetState();
    }

    void Btn_Continue()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGameData_Mng.I.SetCycle(false);
        SetState();
        CSPrefabMng.I.DestroyPrefab(transform.gameObject);
    }

    void Btn_Exit()
    {
        CSSoundMng.I.Play("MenuEF_Button");
      
        JHGameData_Mng.I.SetGameOver();
        AutoFade.LoadLevel("1_Title", 0.3f, 0.3f, Color.black);
    }
}
