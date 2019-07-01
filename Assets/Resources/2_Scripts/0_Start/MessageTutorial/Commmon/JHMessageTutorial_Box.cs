using UnityEngine;
using System.Collections;


public enum E_MTSIZE
{
    E_NORMAL = 0,
    E_BIG,
    E_SMALL,
    E_SMALL_WIDTH,

    E_MAX,
}
public class JHMessageTutorial_Box : MonoBehaviour {
    public BoxCollider2D m_pCollider = null;

    public UISprite m_pBoard = null;
    public UISprite m_pFace = null;
    public UILabel m_pLabel = null;

    public UISprite m_pTouchme = null;
    public UILabel m_pTOuchmeLabel = null;
	// Use this for initialization

    bool m_bOpen = false;

    E_MTSIZE m_eMtSize;
    float m_fSize_NormalX = 629.0f;
    float m_fSize_NormalY = 359.0f;
    float m_fSize_BigX = 629.0f;
    float m_fSize_BigY = 448.0f;
    float m_fSize_SmallX = 629.0f;
    float m_fSize_SmallY = 225.0f;
    float m_fSize_Small_WidthX = 505.0f;
    float m_fSize_Small_WidthY = 225.0f;

    float m_fFacePos_NormalX = 554.4f;
    float m_fFacePos_NormalY = -249.25f;
    float m_fFacePos_BigX = 554.4f;
    float m_fFacePos_BigY = -338.68f;
    float m_fFacePos_SmallX = 554.4f;
    float m_fFacePos_SmallY = -122.69f;
    float m_fFacePos_Small_WidthX = 465.88f;
    float m_fFacePos_Small_WidthY = -122.69f;

    float m_fTouchPos_NormalX = 462.39f;
    float m_fTouchPos_NormalY = -275.92f;
    float m_fTouchPos_BigX = 554.4f;
    float m_fTouchPos_BigY = -364.5f;
    float m_fTouchPos_SmallX = 478.1f;
    float m_fTouchPos_SmallY = -148.0f;
    float m_fTouchPos_Small_WidthX = 390.5f;
    float m_fTouchPos_Small_WidthY = -148.0f;



     float m_fBoardSizeX = 629.0f;
     float m_fBoardSizeY = 359.0f;

    /// <summary>
    /// 초기화
    /// </summary>
    /// <param name="fX"></param> 생선할 좌표
    /// <param name="fY"></param> 생성할 좌표
    public void Enter(float fX = -313.0f, float fY = 205.0f, E_MTSIZE eMtsize = E_MTSIZE.E_NORMAL)
    {
        switch(eMtsize)
        {
            case E_MTSIZE.E_NORMAL :
                m_fBoardSizeX = m_fSize_NormalX;
                m_fBoardSizeY = m_fSize_NormalY;
                m_pFace.transform.localPosition = new Vector3(m_fFacePos_NormalX, m_fFacePos_NormalY, 0.0f);
                m_pTouchme.transform.localPosition = new Vector3(m_fTouchPos_NormalX, m_fTouchPos_NormalY, 0.0f);
                break;
            case E_MTSIZE.E_BIG :
                m_fBoardSizeX = m_fSize_BigX;
                m_fBoardSizeY = m_fSize_BigY;
                m_pFace.transform.localPosition = new Vector3(m_fFacePos_BigX, m_fFacePos_BigY, 0.0f);
                m_pTouchme.transform.localPosition = new Vector3(m_fTouchPos_BigX, m_fTouchPos_BigY, 0.0f);
                break;
            case E_MTSIZE.E_SMALL :
                m_fBoardSizeX = m_fSize_SmallX;
                m_fBoardSizeY = m_fSize_SmallY;
                m_pFace.transform.localPosition = new Vector3(m_fFacePos_SmallX, m_fFacePos_SmallY, 0.0f);
                m_pTouchme.transform.localPosition = new Vector3(m_fTouchPos_SmallX, m_fTouchPos_SmallY, 0.0f);
                break;
            case E_MTSIZE.E_SMALL_WIDTH :
                m_fBoardSizeX = m_fSize_Small_WidthX;
                m_fBoardSizeY = m_fSize_Small_WidthY;
                m_pFace.transform.localPosition = new Vector3(m_fFacePos_Small_WidthX, m_fFacePos_Small_WidthY, 0.0f);
                m_pTouchme.transform.localPosition = new Vector3(m_fTouchPos_Small_WidthX, m_fTouchPos_Small_WidthY, 0.0f);
                break;
        }

        m_pBoard.SetDimensions(90, 86);
        m_pBoard.enabled = false;
        
        transform.localPosition = new Vector3(fX, fY, 0.0f);
        m_pLabel.text = "";
        m_pLabel.enabled = false;
     
        m_pTouchme.enabled = false;
        m_pTOuchmeLabel.enabled = false;
        m_pFace.enabled = false;
        m_bOpen = false;
    }

    /// <summary>
    /// 박스 열기
    /// </summary>
    /// <param name="bTOuch"></param> 박스 열리고 나서  Touch Me 아이콘 뜨냐 안뜨냐 설정 
    public void Open(bool bTOuch)
    {
        m_bOpen = false;
        m_pBoard.enabled = true ;
        StopCoroutine("Cor_Open");
        StopCoroutine("Cor_Close");
        StartCoroutine("Cor_Open", bTOuch);
    }

    IEnumerator Cor_Open(bool bTOuch)
    {
        float fX =90.0f, fY = 86.0f;
        m_pFace.enabled = true;
        Color stFaceColor = m_pFace.color;
        stFaceColor.a = 0.0f;
        m_pFace.color = stFaceColor;
        for(float i=0.0f;i<0.3f;i+=Time.deltaTime)
        {
            fX = Mathf.Lerp(fX, m_fBoardSizeX, i * 2.0f);
            fY = Mathf.Lerp(fY, m_fBoardSizeY, i * 2.0f);
            m_pBoard.SetDimensions((int)fX, (int)fY);
            yield return null;
        }
        m_pBoard.SetDimensions((int)m_fBoardSizeX, (int)m_fBoardSizeY);
        m_bOpen = true;

        for(float i=0.0f; i<1.0f;i+=Time.deltaTime)
        {
            stFaceColor.a += 5.0f * Time.deltaTime;
            m_pFace.color = stFaceColor;
            if (stFaceColor.a >= 1.0f) break;
            yield return null;
        }
        stFaceColor.a = 1.0f;
        m_pFace.color = stFaceColor;
       
        m_pLabel.enabled = true;
        m_pCollider.enabled = false;
       if(bTOuch==true)
       {
           m_pCollider.enabled = true;
           Touch(true);
       }
    }

    /// <summary>
    /// 박스에 TOuch Me 표시 켜기 끄기
    /// </summary>
    /// <param name="bb"></param>
    public void Touch(bool bb)
    {
        if(bb)
        {
            StartCoroutine("Cor_TouchMe");
            m_pCollider.enabled = true;
        }
        else
        {
            StopCoroutine("Cor_TouchMe");
            m_pTouchme.enabled = false;
            m_pTOuchmeLabel.enabled = false;
            m_pCollider.enabled = false;
        }
    }
  
    /// <summary>
    /// 닫기
    /// </summary>
    public void Close()
    {
        m_pCollider.enabled = false;
        m_bOpen = false;
        m_pLabel.text = "";
        m_pLabel.enabled = false;
        StopCoroutine("Cor_TouchMe");
        m_pTouchme.enabled = false;
        m_pTOuchmeLabel.enabled = false;
        m_pFace.enabled = false;
        StopCoroutine("Cor_Open");
        StopCoroutine("Cor_Close");
        StartCoroutine("Cor_Close");
    }

    IEnumerator Cor_Close()
    {
        float fX = m_fBoardSizeX, fY = m_fBoardSizeY;
        for (float i = 0.0f; i < 0.25f; i += Time.deltaTime)
        {
            fX = Mathf.Lerp(fX, 90.0f, i * 2.0f);
            fY = Mathf.Lerp(fY, 80.0f, i * 2.0f);
            m_pBoard.SetDimensions((int)fX, (int)fY);
            yield return null;
        }
        m_pBoard.enabled = false;
    }

    /// <summary>
    /// 박스 움직이기
    /// </summary>
    /// <param name="fX"></param> 좌표
    /// <param name="fY"></param> 좌표
    public void Move(float fX, float fY)
    {
        StartCoroutine(Cor_Move(fX, fY));
    }

    IEnumerator Cor_Move(float fX, float fY)
    {
        Vector3 stV = new Vector3(fX, fY, 0.0f);
        for (float i = 0.0f; i < 1.0f; i += Time.deltaTime)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, stV, i * 2.0f);
            yield return null;
        }
        yield return null;
    }

    /// <summary>
    /// 텍스트 설정
    /// </summary>
    /// <param name="sString"></param>
    public void SetText(string sString)
    {
        m_pLabel.text = sString;
    }

    IEnumerator Cor_TouchMe()
    {
        Color stColor = Color.white;
        bool bPing = false;
        stColor.a = 0.0f;
        m_pTouchme.enabled = true;
        m_pTOuchmeLabel.enabled = true;
        m_pTouchme.color = stColor;
        m_pTOuchmeLabel.color = stColor;
        while (m_bOpen)
        {
            if (bPing == false)
            {
                stColor.a += 3.0f * Time.deltaTime;
                if (stColor.a >= 1.1f)
                {
                    bPing = !bPing;
                }
            }
            else
            {
                stColor.a -= 2.5f * Time.deltaTime;
                if (stColor.a <= 0.3f)
                {
                    bPing = !bPing;
                }
            }
            m_pTouchme.color = stColor;
            m_pTOuchmeLabel.color = stColor;
            yield return null;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
