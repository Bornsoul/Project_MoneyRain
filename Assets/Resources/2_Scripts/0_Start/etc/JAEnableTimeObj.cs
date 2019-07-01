using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JAEnableTimeObj : MonoBehaviour
{
    public GameObject m_pObj;
    public float m_fStartTime;
    public bool m_bEnable;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(m_fStartTime);

        NGUITools.SetActive(m_pObj, m_bEnable);
    }
}
