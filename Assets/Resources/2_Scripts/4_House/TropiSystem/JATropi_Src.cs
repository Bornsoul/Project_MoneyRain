using UnityEngine;
using System.Collections;

public class JATropi_Src : MonoBehaviour
{
    public UILabel m_pNameLabel;
    public UISprite m_pItemSprite;

    public int m_nIndex;
    public int m_nID;

    public Animation m_pItemAni = null;
    float m_fTime = 0f;

    public void Enter(int nIndex, int nID)
    {
        m_nIndex = nIndex;
        m_nID = nID;
        
        m_pNameLabel.text = JAMenuData_Mng.I.m_pTropiData_String.GetName_ID(nID);
        m_pItemSprite.spriteName = JAMenuData_Mng.I.m_pTropiData_String.GetSpriteName_ID(nID);
    }

    void Update()
    {

        m_fTime += Time.deltaTime;
        if ( m_fTime > 3.2f )
        {
            m_pItemAni.Play();
            m_fTime = 0f;
        }
    }

    void OnPress(bool bPress)
    {
        if ( bPress == true )
        {
            JATropiUI_Mng.I.m_nItemID = m_nID;
        }
        else
        {

        }
    }

    void OnClick()
    {
        Debug.Log("ID: " + m_nID + ", ItemID : " + JATropiUI_Mng.I.m_nItemID);
        JATropiUI_Mng.I.m_nItemID = m_nID;
        CSPrefabMng.I.CreatePrefab(JAHouse_Scene.I._PopLayer1.m_pAnchor, "2_Objects/Popup/Pop_TropiInfo", "Pop_Tropi_" + m_nID);
    }
}
