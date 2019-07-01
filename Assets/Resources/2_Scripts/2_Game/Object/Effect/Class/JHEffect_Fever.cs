using UnityEngine;
using System.Collections;

public class JHEffect_Fever : JHEffect_Root {
    public UISprite m_pFever = null;
    public UISprite m_pWhite = null;
    public UISprite m_pCircle = null;

    Color m_stWhiteColor;
	// Use this for initialization
    override public bool Enter()
    {
        if (base.Enter() == false) return false;
        m_stPos = Vector3.zero;
        m_pCircle.transform.localScale = Vector3.zero;
        m_stWhiteColor = Color.white;
        m_stWhiteColor.a = 0.0f;
        m_pWhite.color = m_stWhiteColor;
        m_pFever.transform.localScale = Vector3.one * 2;
        return true;

    }

    override public bool Create()
    {
        if (base.Create() == false) return false;
        m_pCircle.transform.localScale = Vector3.zero;
        m_stWhiteColor = Color.white;
        m_stWhiteColor.a = 0.0f;
        m_pWhite.color = m_stWhiteColor;
        m_pFever.transform.localScale = Vector3.one * 2;

        StartCoroutine(Cor_Fever());
        return true;
    }

   IEnumerator Cor_Fever()
    {
        for (float i = 0.0f; i < 1.0f;i+=Time.deltaTime )
        {
            m_pFever.transform.localScale = Vector3.Lerp(m_pFever.transform.localScale, Vector3.one, 20.0f*Time.deltaTime);
            if (m_pFever.transform.localScale.x < 1.05f) break;
            yield return null;
        }
        StartCoroutine(Cor_White());
        Vector3 stCircle = m_pCircle.transform.localScale;
        Color stCircleColor = m_pCircle.color;
        for (float i = 0.0f; i < 1.0f;i+=Time.deltaTime)
        {
            m_pCircle.transform.localScale = Vector3.Lerp(m_pCircle.transform.localScale, Vector3.one*3.0f, 20.0f * Time.deltaTime);
            stCircleColor.a -= 2.0f * Time.deltaTime;
            m_pCircle.color = stCircleColor;
            yield return null;
        }

        End();
            yield return null;
    }

    IEnumerator Cor_White()
   {
       CSSoundMng.I.Play("Neco1");
       CSSoundMng.I.Play("FeverBoom");
        for (float i = 0.0f; i < 1.0f;i+=Time.deltaTime )
        {
            m_stWhiteColor.a += 7.0f*Time.deltaTime;
            m_pWhite.color = m_stWhiteColor;
            if (m_stWhiteColor.a > 1.1f) break;
            yield return null;
        }

        for (float i = 0.0f; i < 1.0f; i += Time.deltaTime)
        {
            m_stWhiteColor.a -= 7.0f * Time.deltaTime;
            m_pWhite.color = m_stWhiteColor;
            yield return null;
        }
       yield return null;
   }

   override public bool End()
   {
       if (base.End() == false) return false;
       return true;
   }

   override public bool Destroy()
   {
       if (base.Destroy() == false) return false;

       return true;
   }


   // Update is called once per frame
   void Update()
   {
       if (base.Update() == false) return;

       ResetTransform();
   }
}
