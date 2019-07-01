using UnityEngine;
using System.Collections;

/// <summary>
/// 화면 디버그 클래스(폰에서 디버그할때 유용하게 쓰임)
/// </summary>
public class CSDebug : CSLayer {
    public UIPanel m_pDebugPanel = null;
    public UILabel m_pDebug = null;
    public UIScrollView m_pScrollView = null;

    public BoxCollider2D m_pScrollCollider = null;
    public UILabel m_pScrollStatuLabel = null;

    void Awake()
    {
        DontDestroyOnLoad(this);
        Button();
    }
    
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        NGUITools.AddWidgetCollider(m_pDebug.gameObject);   //! 로그가 길어지면 스크롤
     //   m_pScrollView.UpdateScrollbars(true);
       // m_pScrollView.SetDragAmount(0, 1, false);
	}

    /// <summary>
    /// 로그 쓰기(printf같은것)
    /// </summary>
    /// <param name="sLog"></param>
    public void Log(string sLog)
    {
        m_pDebug.text = m_pDebug.text + "\n -" + sLog;   
    }

    /// <summary>
    /// 보이거나 보이지 않기
    /// </summary>
    public void Button()
    {
        if (m_pDebugPanel.alpha == 0.0f)
            m_pDebugPanel.alpha = 1.0f;
        else
            m_pDebugPanel.alpha = 0.0f;
    }

    /// <summary>
    /// 스크롤 On/Off
    /// </summary>
    public void Scroll()
    {
        m_pScrollCollider.enabled = !m_pScrollCollider.enabled;
        if (m_pScrollCollider.enabled == false)
        {
            m_pScrollStatuLabel.text = "OFF : SCROLL";
        }
        else
        {
            m_pScrollStatuLabel.text = "ON : SCROLL";
        }
    }

    /// <summary>
    /// 로그 클리어
    /// </summary>
    public void Clear()
    {
        m_pDebug.text = "Debug________________________";
    }
}
