using UnityEngine;
using System.Collections;

public class JAPopup_Mng : MonoBehaviour
{
    public GameObject m_pOKBtnGam = null;
    public GameObject m_pCancelBtnGam = null;

    public UILabel m_pTitleLabel = null;
    public UILabel m_pTextLabel = null;

    public Animation m_pPopAni = null;
    public Animation[] m_pBtnAni = null;
    float m_fBtnAni = 0f;

    bool m_bAni = false;

    public void Enter(string sTitle, string sText, bool bAni)
    {

        m_bAni = bAni;
        m_pPopAni.enabled = bAni;
        if (bAni == true)
        {
            m_pPopAni.Play("Ani_PopupStart");
            for (int i = 0; i < m_pBtnAni.Length; i++)
                m_pBtnAni[i].Play();
        }
        m_pTitleLabel.text = sTitle;
        m_pTextLabel.text = sText.Replace("\n", System.Environment.NewLine);
        
        if (Application.loadedLevelName != "4_House")
            JHTitle_Scene.I._bESC = true;
    }

    void Update()
    {
        if (Application.loadedLevelName != "4_House")
        {
            if (JHTitle_Scene.I._bESC == false)
            {
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    Button_Cancel();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Button_Cancel();
                }
            }
        }

        ButtonAni();
    }

    void ButtonAni()
    {
        m_fBtnAni += Time.deltaTime;

        if ( m_fBtnAni > 2f)
        {
            for (int i = 0; i < m_pBtnAni.Length; i++)
                m_pBtnAni[i].Play();
            m_fBtnAni = 0f;
        }
    }

    public void Button_Ok()
    {
        if (m_bAni == true)
        {
            m_pBtnAni[0].Play();
            StartCoroutine(Cor_Destroy(0.33f));
        }
        else
        {
            CSPrefabMng.I.DestroyPrefab(transform.gameObject);
            Destroy(this);
        }
    }

    public void Button_Cancel()
    {
        JHTitle_Scene.I._bESC = false;
        if (m_bAni == true)
        {
            m_pBtnAni[1].Play();
            StartCoroutine(Cor_Destroy(0.33f));    
        }
        else
        {
            CSPrefabMng.I.DestroyPrefab(transform.gameObject);
            Destroy(this);
        }
    }

    IEnumerator Cor_Destroy(float fWaitTime)
    {
        m_pPopAni.Play("Ani_PopupEnd");
        yield return new WaitForSeconds(fWaitTime);

        CSPrefabMng.I.DestroyPrefab(transform.gameObject);
        Destroy(this);
    }
}
