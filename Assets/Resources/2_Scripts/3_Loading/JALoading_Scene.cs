using UnityEngine;
using System.Collections;

public class JALoading_Scene : CSSingleton<JALoading_Scene>
{
    enum eState
    {
        E_HERO_NONE,
        E_HERO_DOWN,
        E_HERO_UP,
    }

    float m_fTime = 0.0f;

    private eState m_eState = eState.E_HERO_DOWN;
    public GameObject m_pHeroGam = null;
    private Vector3 m_stHeroVec;
    private float m_fStateTime = 0f;


    private float m_fLoadingTime = 5f; //! 임시로 넘어가는 시간을 설정합니다.

    void Start()
    {
        m_eState = eState.E_HERO_DOWN;
        m_stHeroVec = m_pHeroGam.transform.localPosition;

    }

    IEnumerator DataLoading()
    {
        yield return true;
    }

    IEnumerator Cor_Loading()
    {
        yield return StartCoroutine(DataLoading());

        AutoFade.LoadLevel("2_Game", 0.5f, 0.5f, Color.black);
        //Application.LoadLevel("2_Game");
    }

    void Update()
    {
        if (m_eState == eState.E_HERO_NONE) return;

        m_fTime += Time.deltaTime;
        if (m_fTime > m_fLoadingTime)
        {
            StartCoroutine(Cor_Loading());
        }

        switch ( m_eState )
        {
            case eState.E_HERO_DOWN:
                
                m_stHeroVec.y = Mathf.SmoothStep(m_stHeroVec.y, -70f, 0.03f);
                if (m_stHeroVec.y < -55f) { m_eState = eState.E_HERO_UP; }
                break;
            case eState.E_HERO_UP:
                m_stHeroVec.y = Mathf.SmoothStep(m_stHeroVec.y, 20f, 0.03f);
                if (m_stHeroVec.y > 3f) { m_eState = eState.E_HERO_DOWN; }
                break;
        }

        m_pHeroGam.transform.localPosition = m_stHeroVec;
    }
}
