using UnityEngine;
using System.Collections;

public class JAPop_CharacterUpg : CSSingleton<JAPop_CharacterUpg>
{
    public Animation m_pPopAni = null;

    public JAUpgBot_Src m_pUpgBot_Src;
    public JAUpgTop_Src m_pUpgTop_Src;

    public BoxCollider2D[] m_pBoxColider = null;
    public JADestroyObj m_pDestroyObj = null;
    bool m_bTutorial = false;

    void Start()
    {
        m_pPopAni.Play("Ani_PopupStart");

        m_pUpgBot_Src.Enter();
        m_pUpgTop_Src.Enter();
    }

    void Update()
    {
        if (m_bTutorial == true) return;
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Backspace))
        {
            Button_Exit();
        }
    }

    public void Tutorial_Mod(bool bYES)
    {
        m_bTutorial = bYES;
        if (bYES == true)
        {
            m_pDestroyObj.m_bSetClick = false;
            m_pUpgBot_Src.Tutorial_Stat();
            for (int i = 0; i < m_pBoxColider.Length; i++)
                m_pBoxColider[i].enabled = false;

            m_pBoxColider[3].enabled = true;
            m_pBoxColider[4].enabled = true;
            m_pBoxColider[5].enabled = true;
        }
        else
        {
            m_pDestroyObj.m_bSetClick = true;
            m_pUpgBot_Src.Normal_Stat();
            for (int i = 0; i < m_pBoxColider.Length; i++)
                m_pBoxColider[i].enabled = true;
        }
    }
    
    public void Tutorial_Btn()
    {
        for (int i = 0; i < m_pBoxColider.Length; i++)
            m_pBoxColider[i].enabled = false;
    }

    public void Button_Exit()
    {
        JHTitle_Scene.I._bESC = false;
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
