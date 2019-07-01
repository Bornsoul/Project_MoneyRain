using UnityEngine;
using System.Collections;

public class JHGameUI_Score : MonoBehaviour {
    public UILabel m_pLabel = null;
    public UILabel _Label { get { return m_pLabel; } }

    float leftrightDecaytime = 0.0f;
    Vector3 offset = new Vector3(0, 0, 0);
    Vector3 position;
    Vector3 Oriposition = new Vector3(0.6634292f, 340.7226f, -0.01f);
    float startTime;
    float amp = 12.03f;
    float freq = 3f;
    float phase = 0f;

    bool updown;
    bool leftright;

    bool m_bActive = false;


    string sTemp = "0";
	public void Enter()
    {
        m_bActive = true;
        Oriposition = transform.localPosition;
        sTemp = JAMenuData_Mng.I.GetIntNumberString(JHGameData_Mng.I._GameScore);
        m_pLabel.text = sTemp;
    }

    public void Destroy()
    {
        m_bActive = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_bActive == false) return;
        sTemp = JAMenuData_Mng.I.GetIntNumberString(JHGameData_Mng.I._GameScore);
        if (m_pLabel.text.CompareTo(sTemp)==-1)
        {
            StartLeftRightShake(0.3f);
        }
        m_pLabel.text = JAMenuData_Mng.I.GetIntNumberString(JHGameData_Mng.I._GameScore);


        if (leftright) LeftRightShake();
        else
        {
            transform.localPosition = Oriposition;
        }
	}

    /// <summary>
    /// 좌우 진동 함수
    /// </summary>
    /// <param name="decay"></param> 시간
    public void StartLeftRightShake(float decay)
    {
        position = Oriposition;
        startTime = Time.time;
        leftrightDecaytime = decay + Time.fixedTime - startTime;
        position = this.gameObject.transform.localPosition;
        leftright = true;
    }

    /// <summary>
    /// 진동 멈춤
    /// </summary>
    public void StopShake()
    {
        updown = false;
        leftright = false;
        this.gameObject.transform.localPosition = position;
        offset = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// 좌우 진동 함수
    /// </summary>
    public void LeftRightShake()
    {

        float totaltime = Time.fixedTime - startTime;
        if (totaltime < leftrightDecaytime)
        {
            Vector3 pos = this.gameObject.transform.localPosition;

            pos -= offset;

            offset.x = Mathf.Sin(2 * 3.14159f * (totaltime * freq) + phase) * amp * (leftrightDecaytime - totaltime) / leftrightDecaytime;

            pos += offset;

            this.gameObject.transform.localPosition = pos;
        }
        else
        {
            offset = new Vector3(0, 0, 0);
            this.gameObject.transform.localPosition = position;
            leftright = false;
        }
    }
}
