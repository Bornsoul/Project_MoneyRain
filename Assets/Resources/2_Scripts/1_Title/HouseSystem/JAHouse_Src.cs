using UnityEngine;
using System.Collections;

public class JAHouse_Src : MonoBehaviour
{
    public GameObject m_pHouseSprite_Gam = null;
    public UISprite m_pHouseSprite = null;
    public UILabel m_pHouseNameLabel = null;
    public Animation m_pAni = null;

    Vector3 m_stScale;

    public void Enter(int nHouseLevel, string sHouseName, string sSpriteName)
    {
        m_pHouseSprite.width = 310; // m_pHouseSprite.atlas.GetSprite(sSpriteName).width;
        m_pHouseSprite.height = 410; //m_pHouseSprite.atlas.GetSprite(sSpriteName).height;
        m_pHouseSprite.spriteName = sSpriteName;
        m_pHouseNameLabel.text = "[F2F2F2]"+ nHouseLevel + "Lv.[-] " + sHouseName;

        Ani_Loop();

        m_stScale = Vector3.one;
    }

    void Update()
    {
        
    }

    public void Fun_SpriteExpScale(float fScale)
    {
        m_stScale.x = Mathf.Lerp(m_stScale.x, (1+fScale /2.6f) * 1f, 0.05f);
        m_stScale.y = Mathf.Lerp(m_stScale.y, (1+fScale /2.6f) * 1f, 0.05f);
        m_pHouseSprite_Gam.transform.localScale = m_stScale;
    }

    public void Ani_Loop() { m_pAni.Play("Ani_HouseLoopStart"); }
    public void Ani_Upg() { m_pAni.Play("Ani_HouseUpgStart"); }
}
