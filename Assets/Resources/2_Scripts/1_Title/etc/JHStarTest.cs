using UnityEngine;
using System.Collections;

public class JHStarTest : MonoBehaviour
{
    public UITexture m_pSprite = null;
    Rect m_pRect;

    public float m_fSpeed = 0.7f;
    public bool m_bHorizon = false;
    public bool m_bDirect = false;

    // Use this for initialization
    void Start()
    {
        m_pRect = m_pSprite.uvRect;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bHorizon == false)
        {            
            if (m_bDirect == false)
            {
                m_pRect.x += m_fSpeed * Time.deltaTime;
                if (m_pRect.x > 1) m_pRect.x = 1 + (m_pRect.x - 1);
                m_pSprite.uvRect = m_pRect;
            }
            else
            {
                m_pRect.x -= m_fSpeed * Time.deltaTime;
                //if (m_pRect.x > 1) m_pRect.x = 1 + (m_pRect.x - 1);
                m_pSprite.uvRect = m_pRect;
            }
        }
        else
        {
            if (m_bDirect == false)
            {
                m_pRect.y += m_fSpeed * Time.deltaTime;
                //if (m_pRect.y > 1) m_pRect.y = 1 + (m_pRect.y - 1);
                m_pSprite.uvRect = m_pRect;
            }
            else
            {
                m_pRect.y -= m_fSpeed * Time.deltaTime;
                //if (m_pRect.y > 1) m_pRect.y = 1 + (m_pRect.y - 1);
                m_pSprite.uvRect = m_pRect;
            }
        }
    }
}
