using UnityEngine;
using System.Collections;

public class JAGameStartBtn : MonoBehaviour
{
    enum eState
    {
        E_STATE_NONE,
        E_STATE_CLICK,
    }

    eState m_eState = eState.E_STATE_NONE;

    public UISprite m_pBtnSprite = null;
    public GameObject m_pMainTitleGam = null;
    private Vector3 m_stMainTitleVec;
    public Animation m_pBtnAni = null;
    private float m_fBtnAniTime = 0f;

    void Start()
    {
        m_eState = eState.E_STATE_NONE;
        m_stMainTitleVec = m_pMainTitleGam.transform.localPosition;
        m_pBtnAni.Play();
    }

    void Update()
    {
       // if (m_eState != eState.E_STATE_NONE) return;
        m_fBtnAniTime += Time.deltaTime;

        if (m_fBtnAniTime > 3f)
        {
            m_pBtnAni.Play();
            m_fBtnAniTime = Time.deltaTime;
        }

        if (m_eState == eState.E_STATE_CLICK)
        {
            
            Debug.Log(m_stMainTitleVec.y);
            m_stMainTitleVec.y = Mathf.SmoothStep(m_stMainTitleVec.y, 1280f, 0.03f);
            m_pMainTitleGam.transform.localPosition = m_stMainTitleVec;
        }

        
        
    }
 
    void OnPress(bool bPress)
    {
        if (bPress == true)
        {
            m_pBtnSprite.color = new Color(210f / 255f, 210f / 255f, 210f / 255f);
        }
        else
        {
            m_pBtnSprite.color = new Color(1f, 1f, 1f);
        }
    }

    void OnClick()
    {
        if ( m_eState == eState.E_STATE_NONE )
        {
            JHTitle_Scene.I._bESC = true;
            CSSoundMng.I.Play("Neco1");
            CSPrefabMng.I.CreatePrefab(JHTitle_Scene.I._PopLayer1.m_pAnchor, "2_Objects/Popup/Pop_Black", "Pop_Black");
            //CSPrefabMng.I.CreatePrefab(JHTitle_Scene.I._pLayer.m_pAnchor, "2_Objects/Effect/JA/Particle/PT_GroundHit_Gray", "PT_GroundHit_Gray");
            StartCoroutine(Cor_NextScene(0.7f));
            //m_eState = eState.E_STATE_CLICK;
        }
    }


    IEnumerator Cor_NextScene(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);

        CSSoundMng.I.AllStop();
        //Debug.Log("NExt Scene");

        AutoFade.LoadLevel("3_Loading", 0.3f, 0.3f, Color.black);
        
    }
}
