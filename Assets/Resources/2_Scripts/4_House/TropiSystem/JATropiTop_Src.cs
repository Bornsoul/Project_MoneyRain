using UnityEngine;
using System.Collections;

public class JATropiTop_Src : MonoBehaviour
{
    public UISprite m_pItemSprite = null;
    public UILabel m_pItemNameLabel = null;
    public UILabel m_pItemStatLabel = null;

    public void Enter(string sItemSpriteName, string sItemName, string sItemStat )
    {
        m_pItemSprite.spriteName = sItemSpriteName;
        m_pItemNameLabel.text = sItemName;
        m_pItemStatLabel.text = sItemStat;
    }

    void Update()
    {

    }
}
