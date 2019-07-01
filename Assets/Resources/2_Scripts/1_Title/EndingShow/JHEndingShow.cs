using UnityEngine;
using System.Collections;

public class JHEndingShow : MonoBehaviour {
    public JHEndingShow_Particle m_pParticle = null;
    public UISprite m_pTropi_Sprite = null;

    public UISprite[] m_pBlack = null;

    public UILabel m_pName = null;
    public GameObject m_pCaptine = null;
    public UILabel m_pFlabel = null;
    public UILabel m_pFlabel_Eng = null;

    /// <summary>
    /// 호출
    /// </summary>
    /// <param name="sTropiSpriteName"></param> 이미지 이름
    /// <param name="sTropiName"></param>  트로피 이름
	public void Enter(string sTropiSpriteName = "100", string sTropiName = "감자 트로피")
    {
        Color pColor = Color.white;
        pColor.a = 0.0f;

       
        for (int j = 0; j < m_pBlack.Length; j++)
        {
            m_pBlack[j].color = pColor;
        }
        m_pParticle.gameObject.SetActive(false);
        m_pName.text = JAMenuData_Mng.I.m_pTropiData_String.GetName_ID(int.Parse(sTropiSpriteName));
        m_pTropi_Sprite.spriteName = sTropiSpriteName;
        m_pTropi_Sprite.MakePixelPerfect();
        m_pTropi_Sprite.enabled = false;
        m_pName.enabled = false;
        StartCoroutine(Cor_Start());
    }

    /// <summary>
    /// The End후 한번 터치햇을떄 호출되는 함수
    /// </summary>
    public void ChoGiHwa()
    {
        ///////////////////////////////////////////////////////
        JHTitle_Scene.I._bESC = false;
        AutoFade.LoadLevel("4_House", 0.3f, 0.3f, Color.white);
    }

    public UILabel m_PEnd = null;
   
    IEnumerator Cor_Start()
    {
        CSSoundMng.I.AllStop();
        CSSoundMng.I.Play("Lighting");
        Color pColor = Color.black;
        pColor.a = 0.6f;
        AutoFade.LoadFade(0.3f, 0.3f, pColor);
        yield return new WaitForSeconds(0.9f);
        pColor.a = 0.8f;
        AutoFade.LoadFade(0.3f, 0.3f, pColor);
        yield return new WaitForSeconds(0.7f);
        pColor.a = 1.0f;
        AutoFade.LoadFade(0.6f, 0.5f, pColor);
        yield return new WaitForSeconds(0.6f);
        Black();
        yield return new WaitForSeconds(0.3f);
        CSSoundMng.I.Play("EndingSong");
        m_pParticle.gameObject.SetActive(true);
        StartCoroutine(Tropimove());

        yield return new WaitForSeconds(3.0f);
       // m_pParticle.m_pParticle.Stop();
        Vector3 sfdfa = Vector3.zero;
        for (float i = 0.0f; i < 1.0f;i+=Time.deltaTime)
        {
            sfdfa.y = Mathf.Lerp(sfdfa.y, 200.0f, 5.0f * Time.deltaTime);
            m_pCaptine.transform.localPosition = sfdfa;
            m_pParticle.transform.localPosition = sfdfa;
            yield return null;
        }
        m_pFlabel.gameObject.SetActive(true);

            //     m_pParticle.Enter();
            //   m_pParticle.Create(Vector3.zero);
        yield return new WaitForSeconds(9.0f);
        m_PEnd.gameObject.SetActive(true);
        while(true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                CSSoundMng.I.AllStop();
                ChoGiHwa();
                break;
            }
            yield return null;
        }
     
            
        yield return null;
    }

   

    IEnumerator Tropimove()
    {
        m_pTropi_Sprite.enabled = true;
        Color pColor = Color.white;
        Vector3 pPos =   new Vector3(0.0f, 30.0f, 0.0f);
        pColor.a = 0.0f;
        m_pName.enabled = true;
        m_pName.color = pColor;
        m_pTropi_Sprite.color = pColor;
        m_pTropi_Sprite.transform.localPosition= pPos;
        for(float i=0.0f; i<1.0f;i+=Time.deltaTime)
        {
            if (pPos.y > 0.0f) pPos.y -= 30.0f * Time.deltaTime;
            pColor.a += 1.0f * Time.deltaTime;
            m_pTropi_Sprite.color = pColor;
            m_pName.color = pColor;
            m_pTropi_Sprite.transform.localPosition = pPos;
           
            yield return null;
        }
        pPos.y = 0.0f;
        pColor.a = 1.0f;
        m_pTropi_Sprite.color = pColor;
        m_pTropi_Sprite.transform.localPosition = pPos;

    }

    void Black()
    {
        Color pColor = Color.white;
        pColor.a = 1.0f;
       
          
            for (int j = 0; j < m_pBlack.Length;j++)
            {
                m_pBlack[j].color = pColor;
            }
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
