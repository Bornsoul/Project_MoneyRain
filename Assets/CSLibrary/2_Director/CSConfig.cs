using UnityEngine;
using System.Collections;

public class CSConfig : MonoBehaviour {

    internal Vector2 m_stScreenSize;
    internal AspectRatio m_pRatio = AspectRatio.Aspect16by9;
    internal float m_fRatioAD_Y = 0.0f;
    internal Vector3 m_stRatioScale = Vector3.zero;
    internal string m_sDeviceName = "";
    internal int m_nFrameRate = 60;
    internal bool m_bRunInBackground = true;

	// Use this for initialization
	void Start () {
       
	}

    public void Enter()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Application.runInBackground = m_bRunInBackground;
        Application.targetFrameRate = m_nFrameRate;

        m_stScreenSize.x = Screen.width;
        m_stScreenSize.y = Screen.height;

        m_sDeviceName = SystemInfo.deviceName;

        m_pRatio = AspectRatios.GetAspectRatio();
        SetRadtioScale();
       
    }

    public void SetRadtioScale()
    {
        m_stRatioScale.z = 1.0f;
        switch (AspectRatios.GetAspectRatio())
        {
            case AspectRatio.Aspect4by3:        //! 옵티머스뷰
                m_stRatioScale.x = 1.0f;
                m_stRatioScale.y = 1.34f;
                break;

            case AspectRatio.Aspect5by4:
                m_stRatioScale.x = 1.0f;
                m_stRatioScale.y = 1.43f;
                break;

            case AspectRatio.Aspect16by9:
                m_stRatioScale.x = 1.0f;
                m_stRatioScale.y = 1.0f;
                break;

            case AspectRatio.Aspect16by10:
                m_stRatioScale.x = 1.0f;
                m_stRatioScale.y = 1.11f;
                break;

            case AspectRatio.Aspect3by2:
                m_stRatioScale.x = 1.0f;
                m_stRatioScale.y = 1.2f;
                break;

            case AspectRatio.AspectCustom1280x752:
                m_stRatioScale.x = 1.0f;
                m_stRatioScale.y = 1.05f;
                break;

            case AspectRatio.AspectCustom1024x600:
                m_stRatioScale.x = 1.0f;
                m_stRatioScale.y = 1.05f;
                break;

            case AspectRatio.AspectCustom800x480:
                m_stRatioScale.x = 1.0f;
                m_stRatioScale.y = 1.07f;
                break;

            case AspectRatio.AspectOthers:
                m_stRatioScale.x = 1.0f;
                m_stRatioScale.y = 1.0f;
                break;
        }

        float fTemp = m_stRatioScale.x;
        m_stRatioScale.x = m_stRatioScale.y;
        m_stRatioScale.y = fTemp;
    }


    public void SetRadtioScale_AD()
    {
        m_fRatioAD_Y = 0.0f;
        m_fRatioAD_Y = 50.0f;
        m_stRatioScale.z = 1.0f;
        switch (AspectRatios.GetAspectRatio())
        {
            case AspectRatio.Aspect4by3:        //! 옵티머스뷰
                m_stRatioScale.x = 0.92f;
                m_stRatioScale.y = 1.34f;
                break;

            case AspectRatio.Aspect5by4:
                m_stRatioScale.x = 0.92f;
                m_stRatioScale.y = 1.43f;
                break;

            case AspectRatio.Aspect16by9:
                m_stRatioScale.x = 0.92f;
                m_stRatioScale.y = 1.0f;
               
                break;

            case AspectRatio.Aspect16by10:
                m_stRatioScale.x = 0.92f;
                m_stRatioScale.y = 1.11f;
                break;

            case AspectRatio.Aspect3by2:
                m_stRatioScale.x = 0.92f;
                m_stRatioScale.y = 1.2f;
                break;

            case AspectRatio.AspectCustom1280x752:
                m_stRatioScale.x = 0.92f;
                m_stRatioScale.y = 1.05f;
                break;

            case AspectRatio.AspectCustom1024x600:
                m_stRatioScale.x = 0.92f;
                m_stRatioScale.y = 1.05f;
                break;

            case AspectRatio.AspectCustom800x480:
                m_stRatioScale.x = 0.92f;
                m_stRatioScale.y = 1.07f;
                break;

            case AspectRatio.AspectOthers:
                m_stRatioScale.x = 0.92f;
                m_stRatioScale.y = 1.0f;
                break;
        }

        float fTemp = m_stRatioScale.x;
        m_stRatioScale.x = m_stRatioScale.y;
        m_stRatioScale.y = fTemp;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
