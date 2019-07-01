using UnityEngine;
using System.Collections;

public class JAVersionBtn : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHStart_Scene.I.m_bAppPlay = false;
        Application.OpenURL(JAMenuData_Mng.I.m_pAppData_String.GetDownload());
    }
}
