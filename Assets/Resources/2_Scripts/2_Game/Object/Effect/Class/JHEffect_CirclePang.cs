using UnityEngine;
using System.Collections;

public class JHEffect_CirclePang : JHEffect_Root {
    public UISprite m_pCircle = null;

    Color m_stWhiteColor;
	// Use this for initialization
    override public bool Enter()
    {
        if (base.Enter() == false) return false;
      
        return true;

    }

    override public bool Create()
    {
        if (base.Create() == false) return false;
        
        return true;
    }

    public void Create(Vector3 stPos)
    {
        if (Create() == false) return;
        m_stPos = stPos;
        m_stScale = Vector3.zero;
        ResetTransform();
        StartCoroutine(Cor_Circle());
    }


   IEnumerator Cor_Circle()
    {
        m_stWhiteColor = m_pCircle.color;
       
        for (float i = 0.0f; i < 1.0f;i+=Time.deltaTime)
        {
            m_stScale = Vector3.Lerp(m_stScale, Vector3.one * 3.0f, 15.0f * Time.deltaTime);
            m_stWhiteColor.a -= 1.0f * Time.deltaTime;
            m_pCircle.color = m_stWhiteColor;
            if (m_stScale.x >= 3.0f) break;
            yield return null;
        }

        End();

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
       m_stPos = JHGame_MainLayer.I._Hero.pSrc._Position;
       ResetTransform();
   }
}
