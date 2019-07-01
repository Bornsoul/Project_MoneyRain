using UnityEngine;
using System.Collections;

public class JHResult_SharePopup : MonoBehaviour
{
    public UIPanel m_pPanel = null;

    internal JHPicture_Result m_pPic = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetKeyDown(KeyCode.Escape)==true)
        {
            Btn_Exit();
        }
	}

    public void SetPicture(JHPicture_Result pPic)
    {
        m_pPic = pPic;
    }


    IEnumerator Cor_Start()
    {

        yield return null;
    }

    void Btn_Share()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        m_pPic.Share();
       // JHGame_UILayer.I.m_pPicture.Share();
    }

    void Btn_Save()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        m_pPic.Save();
      //  JHGame_UILayer.I.m_pPicture.Save();
    }

    void Btn_Exit()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        m_pPic.ExitPicture();
       // JHGame_UILayer.I.m_pPicture.ExitPicture();
        CSPrefabMng.I.DestroyPrefab(transform.gameObject);
    }
}
