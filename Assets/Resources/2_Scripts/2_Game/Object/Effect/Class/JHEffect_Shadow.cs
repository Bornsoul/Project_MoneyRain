using UnityEngine;
using System.Collections;

public class JHEffect_Shadow : JHEffect_Root {


    bool m_bStart = false;
	// Use this for initialization
    override public bool Enter()
    {
        if (base.Enter() == false) return false;
        m_stPos = Vector3.zero;
        m_stPos.x = JHGame_MainLayer.I._Hero.pSrc._Position.x - 3.5f;
        m_stPos.y = JHGame_MainLayer.I._Hero.pSrc._GoundY - 92.0f;
        m_bStart = false;
        m_stScale = Vector3.zero;
        return true;

    }

    override public bool Create()
    {
        if (base.Create() == false) return false;
        m_stScale.x = 0.0f;
        m_stScale.y = 0.0f;
        return true;
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
    //   if (base.Update() == false) return;
       if(m_bStart==false)
       {
           m_stScale.x += 0.5f * Time.deltaTime;
           if (m_stScale.x >= 0.6f) m_bStart = true;
       }
       else
       {
           m_stScale.x = 1.0f - (Mathf.Abs(m_stPos.y - JHGame_MainLayer.I._Hero.pSrc._Position.y) * 0.001f);
           if (m_stScale.x < 0.5f) m_stScale.x = 0.5f;
       }
       m_stPos.x = JHGame_MainLayer.I._Hero.pSrc._Position.x - 3.5f;
       m_stPos.y = JHGame_MainLayer.I._Hero.pSrc._GoundY - 92.0f;

      
       
       m_stScale.y = m_stScale.x;


       ResetTransform();
   }
}
