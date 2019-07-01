using UnityEngine;
using System.Collections;

public class JALoading_Tip : MonoBehaviour
{
    enum eState
    {
        E_STATE_NONE,
        E_STATE_ALPHA_START,
        E_STATE_ALPHA_END,
        E_STATE_ANI_END,
    };

    enum eLangState
    {
        E_LANG_KOR,
        E_LANG_ENG,
    }

    public Animation m_pAni = null;
    public UILabel m_pTipLabel = null;
    public UISprite m_pTipSprite = null;

    eState m_eState = eState.E_STATE_NONE;
    eLangState m_eLang = eLangState.E_LANG_KOR;

    public float m_fAlpha = 0.0f;

    public string[] m_sTipStringKOR;
    public string[] m_sTipStringENG;

    int m_nRandTip = 0;
    
    void Start()
    {
        /* 
         * 점프해서 돈을 먹으면 더 많으 돈이 떨어져요!
         * Can Eat More Money when you eat the money to jump
         * 
         * 구글플레이 업적을 달성하여 트로피를 얻어보세요!
         * Achieve the Google play achievements to get trophies!!
         * 
         * 집 업그레이드를 통해 더욱 많은 돈을 얻을 수 있습니다.
         * Can get More Money by Upgrades in Main Menu
         * 
         * 사실 호...호랑이가.. 아니라 고양이 입니다..!
         * In Fact He is not a t...tiger. He is a Cat
         * 
         * 집을 모두 업그레이드하면 과연 무슨일이 벌어질까요?
         * Do you know What happen'd When you Upgrade Full to House?
         * 
         * 잘보면 위 고양이는 위아래 위위아래로 움직이고 있답니다.
         * 
         * 트로피에는 특별한 능력이 존재합니다. 정말이라구요!
         * Trophy has Special ability. Is Really!!
         */
        m_eLang = eLangState.E_LANG_KOR;
        m_sTipStringKOR = new string[] {"[i]점프해서 돈을 먹으면 더 많은 돈이 떨어져요![-]",
                                    "[i]구글플레이 업적을 달성하여 트로피를 얻어보세요![-]",
                                    "[i]집 업그레이드를 통해 더욱 많은 돈을"+System.Environment.NewLine+
                                    "얻을 수 있습니다.[-]",
                                    "[i]사실 호... 호랑이가.. 아니라 고양이 입니다..![-]",
                                    "[i]집을 모두 업그레이드하면 과연" + System.Environment.NewLine + "무슨일이 벌어질까요?[-]",
                                    "[i]잘보면 위 고양이는 위아래 위위아래로" + System.Environment.NewLine +
                                    "움직이고 있답니다.[-]",
                                    "[i]트로피에는 특별한 능력이 존재합니다." + System.Environment.NewLine +
                                    "정말이라구요![-]"};

        m_sTipStringENG = new string[] {"[i]Can Eat More Money when you eat the" + System.Environment.NewLine  +
                                    "money to jump[-]",
                                    "[i]Achieve the Google play achievements to" + System.Environment.NewLine  + 
                                    "get trophies!![-]",
                                    "[i]Can get More Money by Upgrades in Main Menu[-]",
                                    "[i]In Fact He is not a t...tiger. He is a Cat[-]",
                                    "[i]Do you know What happen'd When you" + System.Environment.NewLine  +
                                    "Upgrade Full to House?[-]",
                                    "[i]Trophy has Special ability. Is Really!![-]"};
        
        m_eState = eState.E_STATE_NONE;
        m_pAni.Play("Ani_GameTipStart");

        
        m_pTipLabel.alpha = 0.0f;
        m_pTipSprite.alpha = 0.0f;
        switch ( m_eLang )
        {
            case eLangState.E_LANG_KOR:
                m_nRandTip = Random.Range(0, m_sTipStringKOR.Length);
                break;
            case eLangState.E_LANG_ENG:
                m_nRandTip = Random.Range(0, m_sTipStringENG.Length);
                break;
        }
        
        //Debug.Log("RAND TIP : " + m_nRandTip);

        AniEnd();
    }

    //void RandCheck()
    //{
    //    int i, j;

    //    for (i = 0; i < m_sTipString.Length; i++)
    //    {
    //        m_nRandTip = Random.Range(0, m_sTipString.Length);

    //        for (j = 0; j < i; j++)
    //        {
    //            if (m_nRandArr[j] == m_nRandTip)
    //                break;
    //        }

    //        if (i == j)
    //        {
    //            m_nRandArr[i++] = m_nRandTip;
    //        }
    //    }
    //}

    void Update()
    {
        switch (m_eState)
        {
            case eState.E_STATE_NONE:
                break;
            case eState.E_STATE_ALPHA_START:
                m_fAlpha = Mathf.SmoothStep(m_fAlpha, 1f, 0.06f);
                break;
            case eState.E_STATE_ALPHA_END:
                m_fAlpha = Mathf.SmoothStep(m_fAlpha, 0f, 0.2f);
                break;
            case eState.E_STATE_ANI_END:
                //Debug.Log("End");
                break;
        }

        m_pTipLabel.alpha = m_fAlpha;
        m_pTipSprite.alpha = m_fAlpha;
    }

    public void AlphaStart()
    {
        //Debug.Log("Alpha Start");
        m_eState = eState.E_STATE_ALPHA_START;
    }

    public void AlphaEnd()
    {
        //Debug.Log("Alpha End");
        m_eState = eState.E_STATE_ALPHA_END;
    }

    public void AniStart()
    {
    }

    public void AniEnd()
    {
        switch (m_eLang)
        {
            case eLangState.E_LANG_KOR:
                m_nRandTip = Random.Range(0, m_sTipStringKOR.Length);
                m_pTipLabel.text = " [FFE551]# 게임 팁 #[-]" + System.Environment.NewLine + m_sTipStringKOR[m_nRandTip];
                break;
            case eLangState.E_LANG_ENG:
                m_nRandTip = Random.Range(0, m_sTipStringENG.Length);
                m_pTipLabel.text = " [FFE551]# Game Tip #[-]" + System.Environment.NewLine + m_sTipStringENG[m_nRandTip];
                break;
        }
        //if (m_nRandTip >= m_sTipString.Length) { m_nRandTip = Random.Range(0, m_sTipString.Length-1); }
        
        m_fAlpha = 0f;
        //m_nRandTip++;
        m_eState = eState.E_STATE_ANI_END;
    }
}
