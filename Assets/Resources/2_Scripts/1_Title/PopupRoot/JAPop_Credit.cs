using UnityEngine;
using System.Collections;

public class JAPop_Credit : CSSingleton<JAPop_Credit>
{
    public Animation m_pPopAni = null;
    public Animation m_pTitleAni1 = null;
    public Animation m_pTitleAni2 = null;

    float m_fTitleTime = 0f;

    // Use this for initialization
    void Start()
    {
        m_pPopAni.Play("Ani_PopupStart");
    }

    // Update is called once per frame
    void Update()
    {
        if (JHTitle_Scene.I._bESC == false)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Pop_Exit();
            }
        }

        m_fTitleTime += Time.deltaTime;

        if ( m_fTitleTime > 3f)
        {
            m_pTitleAni2.Play();
            m_fTitleTime = 0f;
        }
    }


    public void Pop_Exit()
    {
        JHTitle_Scene.I._bESC = false;
        CSSoundMng.I.Play("MenuEF_Button");
        StartCoroutine(Cor_Destroy(0.33f));
    }

    IEnumerator Cor_Destroy(float fWaitTime)
    {
        m_pPopAni.Play("Ani_PopupEnd");
        yield return new WaitForSeconds(fWaitTime);

        CSPrefabMng.I.DestroyPrefab(transform.gameObject);
        Destroy(this);
    }
}
