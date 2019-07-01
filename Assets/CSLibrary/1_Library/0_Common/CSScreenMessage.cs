using UnityEngine;
using System.Collections;

public class CSScreenMessage : MonoBehaviour {
    public UISprite m_pSprite = null;
    public UILabel m_pLabel = null;
    public UIPanel m_pPanel = null;

    bool m_bEnd = false;
	// Use this for initialization
	void Start () {
	
	}

    public void Enter(int nDepth, string sText)
    {
        m_pPanel.depth = nDepth;
        m_pLabel.text = sText;
        m_pPanel.alpha = 0.0f;
        m_bEnd = false;
        StartCoroutine(Cor_Start());

    }

    IEnumerator Cor_Start()
    {
        m_pPanel.alpha = 0.0f;
        while(true)
        {
            m_pPanel.alpha += 10.0f * Time.deltaTime;
            if (m_pPanel.alpha >= 1.0f) break;
            yield return null;
        }
        StartCoroutine(Cor_Update());
        yield return null;
    }

    IEnumerator Cor_End()
    {
        while (true)
        {
            m_pPanel.alpha -= 10.0f * Time.deltaTime;
            if (m_pPanel.alpha <= 0.0f) break;
            yield return null;
        }
        CSPrefabMng.I.DestroyPrefab(transform.gameObject);
        yield return null;
    }

    IEnumerator Cor_Update()
    {
        yield return new WaitForSeconds(1.0f);
        Exit();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Exit()
    {
        if (m_bEnd == true) return;
        m_bEnd = true;
        StartCoroutine(Cor_End());
    }

    public void ExitFast()
    {
        CSPrefabMng.I.DestroyPrefab(transform.gameObject);
    }
}
