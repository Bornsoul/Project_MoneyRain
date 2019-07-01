using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;


public class CSSoundMng : CSSingleton<CSSoundMng> {
    public Dictionary<string, AudioSource> AudioSource_List = null; //!
    public GameObject m_pSoundRepo = null;

    void Awake()
    {
        DontDestroyOnLoad(this);

        AudioSource_List = new Dictionary<string, AudioSource>();

       /* AddSound("BGM/FullMetalPanic", "FullMetalPanic");
        AddSound("BGM/MrChu", "MrChu");

        AddSound("SFX/Menu/MenuEF_Button", "MenuEF_Button");
        AddSound("SFX/Voice/GameEF_GameStart", "GameEF_GameStart");

        AddSound("SFX/Hit1", "Hit1");
        AddSound("SFX/Hit2", "Hit2");
        AddSound("SFX/Hit3", "Hit3");

        AddSound("SFX/Voice/Attack1", "Attack1");
        AddSound("SFX/Voice/Attack2", "Attack2");
        AddSound("SFX/Voice/Attack3", "Attack3");
        AddSound("SFX/Voice/EnemyDie", "EnemyDie");
        AddSound("SFX/Voice/EnemySpawn", "EnemySpawn");
        AddSound("SFX/Voice/Gameover", "Gameover");
        AddSound("SFX/Voice/Hit", "Hit");*/
        //AutoSearchSound();
    }

    void Start()
    {
       
    }

	void OnDestroy()
    {
        if (AudioSource_List == null)
            return;

        if (AudioSource_List.Count == 0)
        {
            return;
        }

        foreach (AudioSource obj in AudioSource_List.Values)
        {
            if (obj != null)
            {

                Destroy(obj);

                continue;
            }
            else
            {

            }
        }
        AudioSource_List.Clear();

        System.GC.Collect();
        Resources.UnloadUnusedAssets();
    }

    public void AutoSearchSound()
    {
        AudioClip[] pAudioArray = null;
        AudioSource audioSource = null;
        AudioClip   audioClip = null;

        pAudioArray = Resources.LoadAll<AudioClip>("5_Sounds");
        foreach (AudioClip pAudio in pAudioArray)
        {
            audioSource = null;
            audioClip = null;
            audioSource = m_pSoundRepo.AddComponent("AudioSource") as AudioSource;
            audioSource.Stop();
            audioClip = pAudio;
            audioSource.clip = audioClip;
            audioSource.loop = false;
            audioSource.playOnAwake = false;
            audioSource.minDistance = 10.0f;
            audioSource.maxDistance = 15.0f;
            AudioSource_List.Add(pAudio.name, audioSource);
        }      
    }

    //@@ 폴더로 불러오기 구현 실패
  /*  public void AutoSearchSound()
    {
        string sPath = "Assets/Resources/5_Sounds";
#if !UNITY_EDITOR
        sPath = Application.persistentDataPath;
#endif
       // Debug.Log(sPath);
        CSDirector.I.GetDeviceDebug().Log("STart-" + sPath);
        string[] sMultiExtension = { "*.prefab", "*.mp3", "*.wav", "*.ogg" };
        
        DirectoryInfo pDir = new DirectoryInfo(sPath);
        FileInfo[] pInfo2 = pDir.GetFiles("*.prefab", SearchOption.AllDirectories);
        CSDirector.I.GetDeviceDebug().Log(pInfo2.Length.ToString());
        foreach(string pExtension in sMultiExtension)
        {
            CSDirector.I.GetDeviceDebug().Log("REady");
            FileInfo[] pInfo = pDir.GetFiles(pExtension, SearchOption.AllDirectories);
            CSDirector.I.GetDeviceDebug().Log(pInfo.Length.ToString());
            string sLoadName = "";
            int nStartNum = -1;
            string sLoadName_Filter = "";
            foreach (FileInfo pFile in pInfo)
            {
                sLoadName = pFile.FullName;
                CSDirector.I.GetDeviceDebug().Log("1-"+sLoadName);
                nStartNum = sLoadName.IndexOf("5_Sounds");
                sLoadName_Filter = sLoadName.Substring(nStartNum + 9);
                sLoadName_Filter = sLoadName_Filter.Replace("\\", "/");
                string sDotFillter = pExtension.Replace("*", "");

                sLoadName_Filter = sLoadName_Filter.Substring(0, sLoadName_Filter.IndexOf(sDotFillter));
                CSDirector.I.GetDeviceDebug().Log("2-" + sLoadName_Filter);
                string sFileName = sLoadName_Filter;
                if (sLoadName_Filter.IndexOf("/")!=-1)
                {
                    for (int i = sLoadName_Filter.Length-1; i >=0 ; i--)
                    {
                        char cT = sLoadName_Filter[i];
                        if(cT=='/')
                        {
                            sFileName = sLoadName_Filter.Substring(i + 1);
                            break;
                        }
                    }    
                }
                AddSound(sLoadName_Filter, sFileName);
               // Debug.Log("@" + sFileName);
            }
        }
        
    }*/

    public void AddSound(string sPath, string sName)
    {
        AudioSource audioSource = null;
        AudioClip audioClip = null;

        audioSource = m_pSoundRepo.AddComponent("AudioSource") as AudioSource;
        audioSource.Stop();

        audioClip = Resources.Load("5_Sounds/"+sPath, typeof(AudioClip)) as AudioClip;
       // Debug.Log(sPath);
        if (audioClip == null)
        {
            Debug.Log(sPath);
            Debug.Log("ERROR_Wrong Path Sound");
            return;
        }

        audioSource.clip = audioClip;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.minDistance = 10.0f;
        audioSource.maxDistance = 15.0f;

        AudioSource_List.Add(sName, audioSource);
    }

    /// <summary>
    /// 재생 함수
    /// </summary>
    /// <param name="index"></param> 음악 번호
    public void Play(string sName, bool bLoop = false)
    {
        if (AudioSource_List.ContainsKey(sName))
        {
            AudioSource_List[sName].Play();
            AudioSource_List[sName].loop = bLoop;
        }
        else
        {
            Debug.Log("ERROR_Not Exist Sound");
        }
    }

    public bool IsPlay(string sName)
    {
        if (AudioSource_List.ContainsKey(sName))
        {
            return AudioSource_List[sName].isPlaying;
        }
        Debug.Log("ERROR_Not Exist Sound");
        return false;
    }

    /// <summary>
    /// 볼륨 조절 함수
    /// </summary>
    /// <param name="index"></param> 음악 번호
    /// <param name="fValue"></param> 볼륨 값
    public void SetVolume(string sName, float fValue)
    {
        if (AudioSource_List.ContainsKey(sName))
        {
            AudioSource_List[sName].volume = fValue;
        }
    }

    /// <summary>
    /// 음악 중지 함수
    /// </summary>
    /// <param name="index"></param> 음악 번호
    public void Stop(string sName)
    {
        if (AudioSource_List.ContainsKey(sName))
        {
            if (AudioSource_List[sName].isPlaying == false) return;
            AudioSource_List[sName].Stop();
        }
    }

    /// <summary>
    /// 모든 음악 중지 함수
    /// </summary>
    public void AllStop()
    {
        foreach (AudioSource obj in AudioSource_List.Values)
        {
            //if (obj.isPlaying == false) continue;
            obj.Stop();
        }
    }

    /// <summary>
    /// 모든 음악 중지 함수
    /// </summary>
    public void AllMute(bool bOn = false)
    {
        float fV = 0.0f;
        if (bOn == true) fV = 1.0f;
        foreach (AudioSource obj in AudioSource_List.Values)
        {
            obj.volume = fV;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
