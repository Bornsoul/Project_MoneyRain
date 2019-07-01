using UnityEngine;
using System.Collections;

public class JAOptionBtn : MonoBehaviour
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
        JHTitle_Scene.I._bESC = true;
        CSSoundMng.I.Play("MenuEF_Button");
        CSPrefabMng.I.CreatePrefab(JHTitle_Scene.I._PopLayer1.m_pAnchor, "2_Objects/Popup/Pop_Option", "Pop_Option");
    }
}
