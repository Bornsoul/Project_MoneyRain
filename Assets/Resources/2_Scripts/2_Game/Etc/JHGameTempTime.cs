using UnityEngine;
using System.Collections;

public class JHGameTempTime : MonoBehaviour {
    public UILabel m_pLabel = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        m_pLabel.text = JHGameData_Mng.I._GameLevelTime.ToString();
	}
}
