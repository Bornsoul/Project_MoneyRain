using UnityEngine;
using System.Collections;

public class JHGoogle_Achievement : MonoBehaviour {
    public string m_sAchievementID;
    public string m_sUserDataName;

    public int m_pTargetValue = 0;

	// Use this for initialization
	void Start () {
        if (JHGooglePS_Mng.I.GetUnlockAchievement(m_sAchievementID) == true)
        {
          //  CSDirector.I.GetDeviceDebug().Log("업적 로드실패 or 업적 클리어상태");
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (JHGooglePS_Mng.I.GetUnlockAchievement(m_sAchievementID) == true) this.enabled = false;

        if (JHUserData_Mng.I.m_pUserData.GetData(m_sUserDataName) >= m_pTargetValue)
        {
            JHGooglePS_Mng.I.UnlockAchievement(m_sAchievementID);
            this.enabled = false;
        }
    }
}
