using UnityEngine;
using System.Collections;

public class JHEffect_PT_HitRed : JHEffect_Root
{
    public ParticleSystem m_pParticle = null;

    override public bool Enter()
    {
        if (base.Enter() == false) return false;

   
        return true;
    }

    override public bool Create()
    {
        if (base.Create() == false) return false;


        ResetTransform();
        return true;
    }

    public void Create(Vector3 stPos)
    {
        if (Create() == false) return;
        m_stPos = stPos;
        StartCoroutine(Cor_Timer());
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

   IEnumerator Cor_Timer()
    {
        yield return new WaitForSeconds(1.0f);
        End();

        yield return null;
    }
	
	// Update is called once per frame
	void Update () {
        if (base.Update() == false) return;
       
        ResetTransform();
	}
}
