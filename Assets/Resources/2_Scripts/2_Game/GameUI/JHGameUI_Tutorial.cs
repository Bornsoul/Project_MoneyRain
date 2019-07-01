using UnityEngine;
using System.Collections;

public class JHGameUI_Tutorial : MonoBehaviour {

    public GameObject m_pTouch = null;
    public GameObject m_pJoy = null;

    public UIPanel m_pPanel = null;

    public UISprite m_pUp1 = null;
    public UISprite m_pUp2 = null;

    bool m_bOn = false;
    float m_fTime = 0.0f;
   bool m_bActive =false;
	// Use this for initialization
	void Start () {
	
	}

    public void Temp_UpDelete(bool bb)
    {
        m_pUp1.enabled = bb;
        m_pUp2.enabled = bb;
    }

    public void Enter()
    {
        m_bActive = true;
        m_bOn = false;
        On();
    }

    public void DeActive()
    {
        m_bActive = false;
        m_pTouch.SetActive(false);
        m_pJoy.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (m_bActive == false) return;

        if(JHGameData_Mng.I._GSencer==true && m_pJoy.activeSelf==false)
        {
            m_pJoy.SetActive(true);
            m_pTouch.SetActive(false);
        }
        else if (JHGameData_Mng.I._GSencer == false && m_pTouch.activeSelf == false)
        {
            m_pJoy.SetActive(false);
            m_pTouch.SetActive(true);
        }

        if(m_bOn==true)
        {
            if(Input.touchCount>0)
            {
                //Off();
            }
        }
        else
        {
            m_fTime += Time.deltaTime;
            if (m_fTime > 1.0f)
            {
                On();
                m_fTime = 0.0f;
            }
        }
        
	}

    public void On()
    {
        StopCoroutine("Cor_Off");
        StartCoroutine("Cor_On");
    }


    public void Off()
    {
        StopCoroutine("Cor_On");
        StartCoroutine("Cor_Off");
    }
    IEnumerator Cor_On()
    {
        m_bOn = true;
        for (float i = 0.0f; i < 1.0f;i+=Time.deltaTime)
        {
            m_pPanel.alpha += 2.5f * Time.deltaTime;
            if (m_pPanel.alpha>=0.6f)
            {
                break;
            }
            yield return null;
        }
        bool bPing = false;
        while(true)
        {
            if(bPing==false)
            {
                m_pPanel.alpha -= 0.5f * Time.deltaTime;
                if(m_pPanel.alpha<0.3f)
                {
                    bPing = !bPing;
                }
            }
            else
            {
                m_pPanel.alpha += 1.5f * Time.deltaTime;
                if (m_pPanel.alpha >= 0.6f)
                {
                    bPing = !bPing;
                }
            }
            yield return null;
        }
    }

    IEnumerator Cor_Off()
    {
        m_bOn = false;
        for (float i = 0.0f; i < 1.0f; i += Time.deltaTime)
        {
            m_pPanel.alpha -= 3.5f * Time.deltaTime;
            if (m_pPanel.alpha <= 0.0f)
            {
                break;
            }
            yield return null;
        }
        yield return false;
    }
}
