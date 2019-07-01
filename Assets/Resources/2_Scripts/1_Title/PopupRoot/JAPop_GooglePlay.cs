using UnityEngine;
using System.Collections;

public class JAPop_GooglePlay : CSSingleton<JAPop_GooglePlay>
{
    public Animation m_pPopAni = null;

    void Start()
    {
        m_pPopAni.Play("Ani_PopupStart");
        JHGooglePS_Mng.I.Init();
    }

    void Update()
    {

    }

    void Btn_Exit()
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


    void Btn_Login()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGooglePS_Mng.I.SighIn();
    }

    void Btn_LogOut()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGooglePS_Mng.I.SighOut();
        Btn_Exit();
    }

   


    void Btn_ShowAchievement()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGooglePS_Mng.I.ShowAchievementUI();
    }

    void Btn_ShowLeaderBoard()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGooglePS_Mng.I.ShowLeaderBoardUI();
    }

   /* void Btn_ShowSelectUI()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGooglePS_Mng.I.ShowSelectUI();
    }*/
}
