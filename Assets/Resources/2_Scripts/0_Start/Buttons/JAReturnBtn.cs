using UnityEngine;
using System.Collections;

public class JAReturnBtn : MonoBehaviour
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
        JAMenuData_Mng.I.m_pPopupData_String.m_bDone = false;
        JAMenuData_Mng.I.m_pStatData_String.m_bDone = false;
        CSPrefabMng.I.DestroyAllPrefabs();

        Destroy(GameObject.Find("DebugLayer(Clone)"));
        Destroy(GameObject.Find("1_JAManager"));
        Destroy(GameObject.Find("0_Library"));
        Destroy(GameObject.Find("2_JHGooglePlayService"));
        CSPrefabMng.I.DestroyOncePrefabs();

        Application.LoadLevel(0);
    }
}
