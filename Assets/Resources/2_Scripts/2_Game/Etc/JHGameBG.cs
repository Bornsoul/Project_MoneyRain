using UnityEngine;
using System.Collections;

public class JHGameBG : MonoBehaviour
{
    
    public UISprite m_pBlack = null;
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Off()
    {
        StartCoroutine(Cor_Off());
    }

    IEnumerator Cor_Off()
    {
        for (float i = 0.0f; i < 1.0f;i+=Time.deltaTime)
        {
            m_pBlack.color = Color.Lerp(m_pBlack.color, Color.clear, i / 2);
            yield return null;
        }
            yield return null;
    }

    public void FeverOff(float fTime)
    {
        StartCoroutine(Cor_FeverOff(fTime));
    }

    IEnumerator Cor_FeverOff(float fTime)
    {
        m_pBlack.color = Color.white;
        yield return new WaitForSeconds(fTime);
        for (float i = 0.0f; i < 1.0f; i += Time.deltaTime)
        {
            m_pBlack.color = Color.Lerp(m_pBlack.color, Color.clear, i / 2);
            yield return null;
        }
        yield return null;
    }
}
