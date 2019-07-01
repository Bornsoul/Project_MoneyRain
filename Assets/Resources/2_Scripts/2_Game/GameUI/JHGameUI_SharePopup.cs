using UnityEngine;
using System.Collections;

public class JHGameUI_SharePopup : MonoBehaviour {
    public UIPanel m_pPanel = null;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Btn_Exit();
        }
	}

    IEnumerator Cor_Start()
    {

        yield return null;
    }

    void Btn_Share()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGame_UILayer.I.m_pPicture.Share();
    }

    void Btn_Save()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGame_UILayer.I.m_pPicture.Save();
    }

    void Btn_Exit()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHGame_UILayer.I.m_pPicture.ExitPicture();
        CSPrefabMng.I.DestroyPrefab(transform.gameObject);
    }
}
