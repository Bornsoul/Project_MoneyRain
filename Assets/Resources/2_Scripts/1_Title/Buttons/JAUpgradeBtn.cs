using UnityEngine;
using System.Collections;

public class JAUpgradeBtn : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    void OnClick()
    {
        JHTitle_Scene.I._bESC = true;
        CSSoundMng.I.Play("MenuEF_Button");
        CSPrefabMng.I.CreatePrefab(JHTitle_Scene.I._PopLayer1.m_pAnchor, "2_Objects/Popup/Pop_CharUpg", "Pop_CharUpg");
    }
}
