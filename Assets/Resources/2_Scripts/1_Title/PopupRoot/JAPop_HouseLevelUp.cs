using UnityEngine;
using System.Collections;

public class JAPop_HouseLevelUp : MonoBehaviour
{
    enum eState
    {
        E_STATE_NONE,
        E_STATE_START,
        E_STATE_LEVEL,
        E_STATE_SCALE,
        E_STATE_HOUSE,
        E_STATE_END,
    };

    eState m_eState = eState.E_STATE_NONE;
    public Animation m_pPopAni = null;
    public Animation m_pAni_New = null;
    public Animation m_pAni_OId = null;
    public Animation m_pAni_House = null;
    public Animation m_pAni_HouseUp = null;

    public GameObject m_pOldLabel_Gam = null;
    public GameObject m_pNewLabel_Gam = null;
    public UILabel m_pOldLabel = null;
    public UILabel m_pNewLabel = null;

    public UISprite m_pHouseImg = null;

    float m_fTime = 0f;

    Vector3 m_stNew_Scale;
    bool m_bNewScale = false;

    bool m_bClick = false;
    bool m_bTadaSound = false;
    bool m_bTadaSound2 = false;
    void Start()
    {
        m_bNewScale = false;
        m_bTadaSound = false;
        m_bTadaSound2 = false;
        NGUITools.SetActive(m_pOldLabel_Gam, false);
        NGUITools.SetActive(m_pNewLabel_Gam, false);
        m_pPopAni.Play("Ani_PopupStart");

        m_stNew_Scale = m_pNewLabel_Gam.transform.localScale;

        m_pHouseImg.spriteName = "House" + (JAHouseSystem_Mng.I._pHouseData.m_nHouseLevel - 1);//JAHouseSystem_Mng.I._pHouseData.m_sHouseSpriteName;
        m_pHouseImg.width = 310; // m_pHouseImg.atlas.GetSprite("House" + (JAHouseSystem_Mng.I._pHouseData.m_nHouseLevel - 1)).width;
        m_pHouseImg.height = 410; // m_pHouseImg.atlas.GetSprite("House" + (JAHouseSystem_Mng.I._pHouseData.m_nHouseLevel - 1)).height;

        //m_pHouseImg.spriteName = JAHouseSystem_Mng.I._pHouseData.m_sHouseSpriteName;
        //m_pHouseImg.width = m_pHouseImg.atlas.GetSprite(JAHouseSystem_Mng.I._pHouseData.m_sHouseSpriteName).width;
        //m_pHouseImg.height = m_pHouseImg.atlas.GetSprite(JAHouseSystem_Mng.I._pHouseData.m_sHouseSpriteName).height;
        //CSPrefabMng.I.CreatePrefab("Particle", "2_Objects/Effect/JA/PT_CartoonFight", "PT_CarttonFight");
        m_eState = eState.E_STATE_NONE;
    }


    void Update()
    {
        m_pOldLabel.text = "Level " + (JAMenuData_Mng.I.m_pHouseData_String.GetLevel(JAGameDataFile.I.GetData_Int("HouseLevel") - 1));
        m_pNewLabel.text = "Level " + JAMenuData_Mng.I.m_pHouseData_String.GetLevel(JAGameDataFile.I.GetData_Int("HouseLevel"));

        if (m_bTadaSound == true)
        {
            CSSoundMng.I.Play("Tada");
            m_bTadaSound = false;
        }

        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Backspace))
        {
            Button_Exit();
        }
        if (m_eState != eState.E_STATE_NONE)
        {
            if (Input.GetMouseButtonUp(0))
            {
                CSSoundMng.I.Play("MenuEF_Button");
                m_bClick = true;
                if (m_eState != eState.E_STATE_END)
                    m_bTadaSound2 = true;
            }

            if (m_bClick == true)
            {
                if (m_bTadaSound2 == true )
                {
                    m_bTadaSound = true;
                    m_bTadaSound2 = false;
                }
                
                m_eState = eState.E_STATE_END;
            }
        }

        switch (m_eState)
        {
            case eState.E_STATE_NONE:
                m_fTime += Time.deltaTime;

                if (m_fTime > 1f)
                {
                    NGUITools.SetActive(m_pOldLabel_Gam, true);
                    m_pAni_OId.Play("Ani_HouseLevelup_Old_Start");
                    m_pAni_House.Play("Ani_HouseLevelup_House_Start");

                    m_eState = eState.E_STATE_START;
                    m_fTime = 0f;
                }
                break;
            case eState.E_STATE_START:
                m_fTime += Time.deltaTime;

                if (m_fTime > 1f)
                {
                    NGUITools.SetActive(m_pNewLabel_Gam, true);
                    m_pAni_OId.Play("Ani_HouseLevelup_Old_End");
                    m_pAni_New.Play("Ani_HouseLevelup_New_Start");
                    
                    m_eState = eState.E_STATE_LEVEL;
                    m_fTime = 0f;
                }
                break;
            case eState.E_STATE_LEVEL:
                m_fTime += Time.deltaTime;

                m_pAni_HouseUp.Play("Ani_HouseUpgStart");

                if (m_fTime > 0.5f)
                {
                    m_pHouseImg.spriteName = JAHouseSystem_Mng.I._pHouseData.m_sHouseSpriteName;
                    m_pHouseImg.width = 310;
                    m_pHouseImg.height = 410;
                    //m_pHouseImg.width = m_pHouseImg.atlas.GetSprite(JAHouseSystem_Mng.I._pHouseData.m_sHouseSpriteName).width;
                    //m_pHouseImg.height = m_pHouseImg.atlas.GetSprite(JAHouseSystem_Mng.I._pHouseData.m_sHouseSpriteName).height;
                    NGUITools.SetActive(m_pOldLabel_Gam, false);
                    m_pAni_HouseUp.Stop("Ani_HouseUpgStart");
                    m_pAni_New.Stop("Ani_HouseLevelup_New_Start");
                    m_bTadaSound = true;
                    m_eState = eState.E_STATE_SCALE;
                    m_fTime = 0f;
                }
                break;
            case eState.E_STATE_SCALE:
                m_fTime += Time.deltaTime;
                if (m_bNewScale == false)
                {
                    m_stNew_Scale.x = Mathf.SmoothStep(m_stNew_Scale.x, 2f, 0.05f);
                    m_stNew_Scale.y = Mathf.SmoothStep(m_stNew_Scale.y, 2f, 0.05f);
                    if (m_stNew_Scale.x > 1.2f)
                    {
                        m_stNew_Scale.x = 1.2f;
                        m_stNew_Scale.y = 1.2f;
                        m_bNewScale = true;
                    }
                }
                else
                {
                    m_stNew_Scale.x = Mathf.SmoothStep(m_stNew_Scale.x, 0.9f, 0.05f);
                    m_stNew_Scale.y = Mathf.SmoothStep(m_stNew_Scale.y, 0.9f, 0.05f);
                    if (m_stNew_Scale.x < 1.1f)
                    {
                        m_stNew_Scale.x = 1.1f;
                        m_stNew_Scale.y = 1.1f;
                        m_bNewScale = false;
                    }
                }
                m_pNewLabel_Gam.transform.localScale = m_stNew_Scale;

                if (m_fTime > 1.3f)
                {
                    Button_Exit();

                    m_fTime = 0f;
                }
                break;
            case eState.E_STATE_HOUSE:
                break;
            case eState.E_STATE_END:
                
                m_fTime += Time.deltaTime;
                
                m_pHouseImg.spriteName = JAHouseSystem_Mng.I._pHouseData.m_sHouseSpriteName;
                m_pHouseImg.width = 310;
                m_pHouseImg.height = 410;
                //m_pHouseImg.width = m_pHouseImg.atlas.GetSprite(JAHouseSystem_Mng.I._pHouseData.m_sHouseSpriteName).width;
                //m_pHouseImg.height = m_pHouseImg.atlas.GetSprite(JAHouseSystem_Mng.I._pHouseData.m_sHouseSpriteName).height;

                NGUITools.SetActive(m_pOldLabel_Gam, false);
                NGUITools.SetActive(m_pNewLabel_Gam, true);
                m_pOldLabel_Gam.transform.localPosition = new Vector3(-800, -100, 0);
                m_pNewLabel_Gam.transform.localPosition = new Vector3(0, -100, 0);
                     
                if (m_bNewScale == false)
                {
                    m_stNew_Scale.x = Mathf.SmoothStep(m_stNew_Scale.x, 2f, 0.05f);
                    m_stNew_Scale.y = Mathf.SmoothStep(m_stNew_Scale.y, 2f, 0.05f);
                    if (m_stNew_Scale.x > 1.2f)
                    {
                        m_stNew_Scale.x = 1.2f;
                        m_stNew_Scale.y = 1.2f;
                        m_bNewScale = true;
                    }
                }
                else
                {
                    m_stNew_Scale.x = Mathf.SmoothStep(m_stNew_Scale.x, 0.9f, 0.05f);
                    m_stNew_Scale.y = Mathf.SmoothStep(m_stNew_Scale.y, 0.9f, 0.05f);
                    if (m_stNew_Scale.x < 1.1f)
                    {
                        m_stNew_Scale.x = 1.1f;
                        m_stNew_Scale.y = 1.1f;
                        m_bNewScale = false;
                    }
                }
                m_pNewLabel_Gam.transform.localScale = m_stNew_Scale;


                if (m_fTime > 1.5f)
                {
                    Button_Exit();

                    m_fTime = 0f;
                }
                break;
        }
    }

    void Button_Exit()
    {
        JHTitle_Scene.I._bESC = false;
        m_pPopAni.Play("Ani_PopupEnd");

        StartCoroutine(Cor_Destroy());
    }

    IEnumerator Cor_Destroy()
    {
        yield return new WaitForSeconds(0.33f);

        Destroy(this);
        CSPrefabMng.I.DestroyPrefab(gameObject);
    }

}
