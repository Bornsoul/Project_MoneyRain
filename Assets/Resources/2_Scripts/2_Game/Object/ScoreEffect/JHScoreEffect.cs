using UnityEngine;
using System.Collections;

public class JHScoreEffect : JHObject_Root
{
    public UISprite m_pSprite = null;
    public UILabel m_pLabel;
    int m_nScore = 0;
    float m_fFinalY = 0.0f;

    override public bool Enter()
    {
        if (base.Enter() == false) return false;

        return true;
    }

    public bool Create(int nScore)
    {
        if (base.Create() == false) return false;
        m_nScore = nScore;
        m_stScale = new Vector3(1.2f, 1.2f, 1.0f);
        m_pLabel.text = "+"+m_nScore.ToString();
        m_stColor = m_pLabel.color;
        m_stColor.a = 3.0f;
        m_pLabel.color = m_stColor;
        m_stPos = Vector3.zero;
        m_stPos.x = -15.0f;
        if(nScore<100)
        {
            m_pSprite.transform.localPosition = new Vector3(57.0f, -2.0f, 0.0f);
        }

        ResetTransform();
        return true;
    }

    override public bool Destroy()
    {
        if (base.Destroy() == false) return false;

        return true;
    }

    override public bool End()
    {
        if (base.End() == false) return false;

        return true;
    }

    public void PushMe()
    {
       // m_stPos.y += 32.0f;
        StartCoroutine(Cor_Push());
    }

    IEnumerator Cor_Push()
    {
        m_pSprite.enabled = false;
        m_stScale = new Vector3(0.8f, 0.8f, 1.0f);
        float fY = m_fFinalY += 38.0f;
        for (float i = 0.0f; i < 1.0f;i+=Time.deltaTime)
        {
            if (m_stPos.y >= fY) break;
            m_stPos.y = Mathf.Lerp(m_stPos.y, fY, i / 5);
        }

        yield return null;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_bActive == false) return;
        m_stPos.y += 50.0f * Time.deltaTime;
        m_stColor.a -= 4.0f * Time.deltaTime;
        m_pLabel.color = m_stColor;
        if(m_stColor.a<=0.0f)
        {
            End();
        }
        ResetTransform();
	}
}
