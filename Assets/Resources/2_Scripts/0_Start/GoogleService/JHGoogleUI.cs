using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class JHGoogleUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
      //  JHGooglePS_Mng.I.Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Btn_Login()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGooglePS_Mng.I.SighIn();
    }

    void Btn_LogOut()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGooglePS_Mng.I.SighOut();
    }

    void Btn_ReportScore()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGooglePS_Mng.I.ReportScore_HighScore(CSRandom.Rand(10, 10000));
    }

    void Btn_UnlockAchievement()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGooglePS_Mng.I.UnlockAchievement(ACHIEVEMENTS._001_Jump);
    }



    void Btn_ShowAchievement()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGooglePS_Mng.I.ShowAchievementUI();
    }

    void Btn_ShowLeaderBoard()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGooglePS_Mng.I.ShowLeaderBoardUI();
    }

    void Btn_ShowSelectUI()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGooglePS_Mng.I.ShowSelectUI();
    }
}
