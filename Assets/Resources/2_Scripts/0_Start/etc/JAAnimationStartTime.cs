using UnityEngine;
using System.Collections;

public class JAAnimationStartTime : MonoBehaviour
{
    Animation m_pAni = null;
    public float m_fStartTime;
    public string m_sAniName;

   IEnumerator Start()
    {
        m_pAni = GetComponent<Animation>();
        yield return new WaitForSeconds(m_fStartTime);

        m_pAni.Play(m_sAniName);
    }
}
