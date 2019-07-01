using UnityEngine;
using System.Collections;

public class JAPop_TropiInfo : MonoBehaviour
{
    public Animation m_pPopAni = null;
    public JATropiTop_Src m_pTropiTop_Src = null;
    public JATropiBot_Src m_pTropiBot_Src = null;


    void Start()
    {
        m_pPopAni.Play("Ani_PopupStart");

        m_pTropiTop_Src.Enter(JAMenuData_Mng.I.m_pTropiData_String.GetSpriteName_ID(JATropiUI_Mng.I.m_nItemID),
            JAMenuData_Mng.I.m_pTropiData_String.GetName_ID(JATropiUI_Mng.I.m_nItemID),
            JAMenuData_Mng.I.m_pTropiData_String.GetStat_ID(JATropiUI_Mng.I.m_nItemID));

        m_pTropiBot_Src.Enter(JAMenuData_Mng.I.m_pTropiData_String.GetInfo_ID(JATropiUI_Mng.I.m_nItemID));

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Backspace))
        {
            Button_Exit();
        }
    }

    void Button_Exit()
    {
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
