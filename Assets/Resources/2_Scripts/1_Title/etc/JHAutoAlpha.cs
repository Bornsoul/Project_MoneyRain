using UnityEngine;
using System.Collections;

public class JHAutoAlpha : MonoBehaviour {
    public UISprite m_pSprite = null;
    public float m_fTime = 1.0f;
    public float m_fRateTime = 0.5f;
    Color m_pColor;
    
	// Use this for initialization
	void Start () {
        StartCoroutine(Cor_Color());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Cor_Color()
    {
        m_pColor = m_pSprite.color;
        float i = 0.0f;
        while(true)
        {
            i = 0.0f;
            while (i < m_fRateTime)
            {
                i += Time.deltaTime * (1.0f / m_fTime);
                m_pColor.a = Mathf.Lerp(m_pColor.a, 0.0f, i);
                m_pSprite.color = m_pColor;
                yield return null;
            }
            i = 0.0f;
            while (i < m_fRateTime)
            {
                i += Time.deltaTime * (1.0f / m_fTime);
                m_pColor.a = Mathf.Lerp(m_pColor.a, 1.0f, i);
                m_pSprite.color = m_pColor;
                yield return null;
            }
            yield return null;
        }
    }
}
