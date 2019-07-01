using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi.SavedGame;
using System;

public class ACHIEVEMENTS       //! 업적
{
    public const string _001_Jump = "CgkIzPnS_o0VEAIQAw";
    public const string _002_Head = "CgkIzPnS_o0VEAIQBA";
    public const string _003_Cow = "CgkIzPnS_o0VEAIQBQ";
    public const string _004_1000Score = "CgkIkPGI1IcFEAIQBw";
    public const string _004_10000Score = "CgkIkPGI1IcFEAIQBQ";
    public const string _005_50000Score = "CgkIkPGI1IcFEAIQBg";
};

public class LEADERBOARDS           //! 리더보드(점수기록같은거, 랭킹)
{
    public const string _001_HighScore = "CgkIkPGI1IcFEAIQAw";
  
};

public enum AchievementState        
{
    LOCK,
    UNLOCK
}

public class JHGooglePS_Mng : CSSingleton<JHGooglePS_Mng> {

    public GameObject m_pAchievementObj = null;


	void Awake()
    {
        DontDestroyOnLoad(this);

       
    }

    public void Init()
    {
        //! 지금 이부분 문제있음 원인 모름  이것이 되야 세이브가 되는데 
     /*   PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()        //! config 세팅     
                        .EnableSavedGames()
                        .Build();
        PlayGamesPlatform.InitializeInstance(config);           //! 구글플레이 초기화*/
       
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform          
        PlayGamesPlatform.Activate();                   //! 구글 플레이활성화

        //CSDirector.I.GetDeviceDebug().Log("Google Init");
       

       
        m_pAchievementObj.SetActive(true);
    }

  
    /// <summary>
    /// LEADERBOARDS._001_HighScore 에 기록올리기
    /// </summary>
    /// <param name="nScore"></param>
    public void ReportScore_HighScore(int nScore)
    {
        Social.ReportScore(nScore, LEADERBOARDS._001_HighScore, (bool success) =>
        {
            if (success)
            {
                Debug.Log("ReportScore_HighScore Success");
              //  CSDirector.I.GetDeviceDebug().Log("ReportScore_HighScore Success");
            }
            else
            {
                Debug.Log("ReportScore_HighScore Fail");
              //  CSDirector.I.GetDeviceDebug().Log("ReportScore_HighScore Fail");
            }
        });
    }


    public void UnlockAchievement(string sID)
    {
        if (PlayerPrefs.GetInt(sID) == (int)AchievementState.UNLOCK) return;
        Social.ReportProgress(sID, 100, (bool success) =>
        {
            if (success)
            {
                PlayerPrefs.SetInt(sID, (int)AchievementState.UNLOCK);
            }
        });
    }

    public bool GetUnlockAchievement(string sID)
    {
        if (PlayerPrefs.GetInt(sID) == (int)AchievementState.UNLOCK) return true;
        return false;
    }


  /*  public void IncrementAchievement(string sID)
    {
        PlayGamesPlatform.Instance.IncrementAchievement
        if (PlayerPrefs.GetInt(sID) == (int)AchievementState.UNLOCK) return;
        Social.ReportProgress(sID, 100, (bool success) =>
        {
            if (success)
            {
                PlayerPrefs.SetInt(sID, (int)AchievementState.UNLOCK);
            }
        });
    }*/

    public void ShowAchievementUI()
    {
        Social.ShowAchievementsUI();
    }

    public void ShowLeaderBoardUI(string ID="fuck")
    {
        if(ID=="fuck")
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(ID);
        }
    }

    public void ShowSelectUI()
    {
        uint maxNumToDisplay = 5;
        bool allowCreateNew = false;
        bool allowDelete = true;

        
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        
        savedGameClient.ShowSelectSavedGameUI("Select saved game",
            maxNumToDisplay,
            allowCreateNew,
            allowDelete,
            OnSavedGameSelected);
    }


    public void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
    {
        if (status == SelectUIStatus.SavedGameSelected)
        {
            // handle selected game save
        }
        else
        {
            // handle cancel or error
        }
    }

    //! 로그인
    public void SighIn()
    {
        //CSDirector.I.GetDeviceDebug().Log("Sign in God");
        Social.localUser.Authenticate((bool success) =>             //! 구글 플레이 유저 인증
        {
            if (success)
            {
                Debug.Log("Sign in Success");
               // CSDirector.I.GetDeviceDebug().Log("Sign in Success");
              
            }
            else
            {
                Debug.Log("Sign in Fail");
               // CSDirector.I.GetDeviceDebug().Log("Sign in Fail");
            }
        });
    }

    public void SighInProcess(Action<bool> callback)
    {
        Social.localUser.Authenticate(callback);
    }

    public bool IsLogin()
    {
        return Social.localUser.authenticated;
    }

    //! 로그아웃
    public void SighOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }



	// Update is called once per frame
	void Update () {
	
	}
}
