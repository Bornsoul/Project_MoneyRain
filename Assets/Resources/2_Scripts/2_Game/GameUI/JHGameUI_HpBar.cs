using UnityEngine;
using System.Collections;

public class JHGameUI_HpBar : MonoBehaviour
{
    public GameObject m_pClock = null;
    public UISprite m_pClockSprite = null;
    public UISlider m_pBar = null;
    bool m_bActive = false;

    public UITexture m_pTestEffect = null;

    Vector3 m_stPos;
    Color m_stCColor;
    public void Enter()
    {
        m_bActive = true;
        m_stPos = Vector3.zero;
        m_stPos.x = 4;
        m_stPos.y = 2;

        m_stCColor = Color.white;
        m_stCColor.a = 0.87f;
    }

    public void Destroy()
    {
        m_bActive = false;
    }
	
	// Update is called once per frame
	void Update () {

        m_pClock.transform.localPosition = m_stPos;
        if (m_bActive == false)
        {
            m_pBar.value = 0.0f;
            m_pTestEffect.SetDimensions((int)(m_pBar.value * 512.0f), 32);
            return ;
        }
        
        if(JHGameData_Mng.I._GameCycle==true)
        {
            m_pBar.value = Mathf.SmoothStep(m_pBar.value, (JHGame_MainLayer.I._Hero.pSrc._CurrHp / JHGame_MainLayer.I._Hero.pSrc._MaxHp) * 1.0f, Time.deltaTime * 8.5f);
        }
        else
        {
            m_pBar.value = Mathf.SmoothStep(m_pBar.value, (JHGame_MainLayer.I._Hero.pSrc._CurrHp / JHGame_MainLayer.I._Hero.pSrc._MaxHp) * 1.0f, Time.deltaTime * 20.0f);
        }
        
   
        
        m_stPos.x = m_pBar.value * 345.0f + 0.0f;

        m_pTestEffect.SetDimensions((int)(m_pBar.value * 512.0f), 32);



	}

    public void Hit()
    {
        StopCoroutine("Cor_ClockHit");
        m_stCColor.r = 1.0f; m_stCColor.g = 0.41f; m_stCColor.b = 0.41f;
        m_pClockSprite.color = m_stCColor;
        StartCoroutine("Cor_ClockHit");
    }

    IEnumerator Cor_ClockHit()
    {
        for (float i = 0.0f; i < 1.0f;i+=Time.deltaTime)
        {
            m_stCColor.g += 1.5f * Time.deltaTime;
            m_stCColor.b = m_stCColor.g;
            m_pClockSprite.color = m_stCColor;
            if (m_stCColor.g >= 1.0f) break;
            yield return null;
        }          
        yield return null;
    }
}
