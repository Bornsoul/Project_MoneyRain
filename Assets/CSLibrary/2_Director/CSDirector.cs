using UnityEngine;
using System.Collections;

public class CSDirector : CSSingleton<CSDirector> {
    private CSConfig m_pConfig = null;
    public bool m_bDebugMode = true;
    private CSDebug m_pDebug = null;

    CSLoadingShow m_pLoading = null;
    CSScreenMessage m_pScreenMessage = null;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this);

        m_pConfig = transform.GetComponent<CSConfig>();
        m_pConfig.Enter();

       // ShowDebug();
      //  CSDirector.I.DeviceDebugMode();
	}

    void OnDestroy()
    {
        Debug.Log("OnDestroy()/CSDirector.cs");
        if (m_pLoading != null) m_pLoading.Exit();
       
    }

	// Update is called once per frame
	void Update () {
	
	}


    public void ShowDebug()
    {
        if (m_bDebugMode == false) return;

        Debug.Log("Current DeviceName : " + m_pConfig.m_sDeviceName);
        Debug.Log("Current RAspectRatio : " + AspectRatios.GetAspectRatio());
    }
    

    public Vector2 GetScreenSize()
    {
        return m_pConfig.m_stScreenSize;
    }

    public Vector3 GetRatioScale()
    {
        return m_pConfig.m_stRatioScale;
    }

    public void SetRatioADReset()
    {
        m_pConfig.SetRadtioScale_AD();
    }

    public Vector3 GetADYMove()
    {
        Vector3 VeC = Vector3.zero;
        VeC.y = m_pConfig.m_fRatioAD_Y;
        return VeC;
    }

    public string GetDeviceName()
    {
        return m_pConfig.m_sDeviceName;
    }


    public void DeviceDebugMode()
    {
        if (m_pDebug != null) return;
        m_pDebug = (CSDebug)CSLayerMng.I.CreateDebugLayer();
    }

    public CSDebug GetDeviceDebug()
    {
        return m_pDebug;
    }


    public void ShowLoading(GameObject pObj, int nDeapth, float fBlackAlpha)
    {
        if (m_pLoading != null) return;
        m_pLoading = CSPrefabMng.I.CreatePrefab(pObj, "0_Library/CSLoading", "CSLoading").GetComponent<CSLoadingShow>();
        m_pLoading.Enter(nDeapth, fBlackAlpha);
    }

    public void ExitLoading()
    {
        if (m_pLoading == null) return;
        m_pLoading.Exit();
    }


    public void ShowScreenMessage(GameObject pObj, int nDeapth, string sText)
    {
        if (m_pScreenMessage != null)
        {
            m_pScreenMessage.ExitFast();
        }
        m_pScreenMessage = CSPrefabMng.I.CreatePrefab(pObj, "0_Library/CSScreenMessage", "CSScreenMessage").GetComponent<CSScreenMessage>();
        m_pScreenMessage.Enter(nDeapth, sText);
    }

    public void ExitScreenMessage()
    {
        if (m_pLoading == null) return;
        m_pScreenMessage.Exit();
    }
  

}
