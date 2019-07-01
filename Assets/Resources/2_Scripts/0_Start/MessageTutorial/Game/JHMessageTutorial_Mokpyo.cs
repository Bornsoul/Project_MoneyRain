using UnityEngine;
using System.Collections;

public class JHMessageTutorial_Mokpyo : MonoBehaviour {
    public UISprite m_pIcon = null;
    public UISprite m_pBack = null;
    public UILabel m_pLabel = null;
	// Use this for initialization
	void Start () {
	
	}

    public void Enter()
    {
        StartCoroutine(Cor_Mo());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Cor_Mo()
    {
        Color stColor = Color.white;
        bool bPing = false;
        stColor.a = 0.0f;
        m_pIcon.color = stColor;
        m_pBack.color = stColor;
        m_pLabel.color = stColor;
        while(true)
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
            m_pIcon.color = stColor;
            m_pBack.color = stColor;
            m_pLabel.color = stColor;
            yield return null;
        }

    }
}
