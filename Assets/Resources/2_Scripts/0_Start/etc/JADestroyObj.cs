using UnityEngine;
using System.Collections;

public class JADestroyObj : CSSingleton<JADestroyObj>
{
    public GameObject m_pObject;
    public Animation m_pAni;

    public bool m_bSetTime = false;
    public bool m_bSetClick = false;

    public float m_fDestroyTime;

    
    void Start()
    {
        if ( m_bSetTime == true )
        {
            StartCoroutine(Destroy());
        }
        {
            //CSPrefabMng.I.DestroyPrefab(m_pObject);
            //Destroy(this);
        }
    }

    IEnumerator Destroy()
    {
        if (m_pAni != null)
        {
            StartCoroutine(Cor_Destroy(0.33f));
        }
        else
        {
            yield return new WaitForSeconds(m_fDestroyTime);

            CSPrefabMng.I.DestroyPrefab(m_pObject);
            Destroy(this);
        }
    }

    void OnClick()
    {
        if (Application.loadedLevelName != "4_House")
         JHTitle_Scene.I._bESC = false;

        CSSoundMng.I.Play("MenuEF_Button");
        if (m_bSetClick == true)
        {
            if (m_pAni != null)
            {
                StartCoroutine(Cor_Destroy(0.33f));
            }
            else
            {
                CSPrefabMng.I.DestroyPrefab(m_pObject);
                Destroy(this);
            }
        }
    }

    IEnumerator Cor_Destroy(float fWaitTime)
    {
        m_pAni.Play("Ani_PopupEnd");
        yield return new WaitForSeconds(fWaitTime);

        CSPrefabMng.I.DestroyPrefab(m_pObject);
        Destroy(this);
    }
}
