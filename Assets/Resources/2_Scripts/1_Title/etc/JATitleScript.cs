using UnityEngine;
using System.Collections;

public class JATitleScript : MonoBehaviour
{
    enum eState
    {
        E_STATE_NONE,
        E_STATE_START,
        E_STATE_END,
    };
    eState m_eState;

    Animation m_pAni = null;
    float m_fTime = 0f;

    void Start()
    {
        m_eState = eState.E_STATE_NONE;
        m_pAni = GetComponent<Animation>();

        m_pAni.Play("Ani_TitleStartPop");
    }

    void Update()
    {
        switch ( m_eState )
        {
            case eState.E_STATE_NONE:
                m_fTime += Time.deltaTime;

                if ( m_fTime > 1.2f )
                {
                    m_pAni.Play("Ani_TitleStart");
                    m_eState = eState.E_STATE_END;
                    m_fTime = 0f;
                }
                break;
            case eState.E_STATE_START:
                break;
            case eState.E_STATE_END:
                break;
        }
    }
}
