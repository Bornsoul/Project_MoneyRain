using UnityEngine;
using System.Collections;

public class JHGameUI_TestEffect : MonoBehaviour
{
    public UITexture m_pSprite = null;
    Rect m_pRect;
	// Use this for initialization
	void Start () {
        m_pRect = m_pSprite.uvRect;
	}
	
	// Update is called once per frame
	void Update () {
        m_pRect.x += 0.3f * Time.deltaTime;
        if (m_pRect.x > 1) m_pRect.x = 1 + (m_pRect.x - 1);
        m_pSprite.uvRect = m_pRect;

        
	}
}
