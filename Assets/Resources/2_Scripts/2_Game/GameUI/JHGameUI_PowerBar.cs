using UnityEngine;
using System.Collections;

public class JHGameUI_PowerBar : MonoBehaviour
{
    public UISlider m_pBar = null;
    bool m_bActive = false;


    public void Enter()
    {
        m_bActive = true;
    }

    public void Destroy()
    {
        m_bActive = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_bActive == false) return;
        m_pBar.value = Mathf.SmoothStep(m_pBar.value, (JHGame_MainLayer.I._Hero.pSrc._CurrPower / JHGame_MainLayer.I._Hero.pSrc._MaxPower) * 1.0f, Time.deltaTime * 20.0f);
        

       



	}
}
