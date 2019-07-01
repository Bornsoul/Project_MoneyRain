using UnityEngine;
using System.Collections;

public class JADestroyBtn : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    void OnClick()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        JHStart_Scene.I.m_bAppPlay = true;
        Destroy(this);
    }
}
