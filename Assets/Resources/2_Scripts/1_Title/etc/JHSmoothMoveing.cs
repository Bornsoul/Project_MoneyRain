using UnityEngine;
using System.Collections;

public enum E_JHSMOOTHMOVEING
{
    E_UPDOWN = 0,
    E_LEFTRIGHT,
}

public class JHSmoothMoveing : MonoBehaviour {
    public E_JHSMOOTHMOVEING m_eMove = E_JHSMOOTHMOVEING.E_UPDOWN;
    public float m_fMoveRange = 20.0f;
    public float m_fMoveSpeed = 20.0f;

    float m_fFirstPos;
    float m_fSecondPos;

    Vector3 m_stPos;
    Vector3 m_stOriginPos;

    float m_fSpeed = 0.0f;

    bool m_bPing = false;

	// Use this for initialization
	void Start () {
        m_stOriginPos = transform.localPosition;
        if(m_eMove==E_JHSMOOTHMOVEING.E_UPDOWN)
        {
            m_fFirstPos = m_stOriginPos.y - m_fMoveRange;
            m_fSecondPos = m_stOriginPos.y + m_fMoveRange;
        }
        else
        {
            m_fFirstPos = m_stOriginPos.x - m_fMoveRange;
            m_fSecondPos = m_stOriginPos.x + m_fMoveRange;
        }
        m_stPos = transform.localPosition;
	   
	}
	
	// Update is called once per frame
	void Update () {
        if (m_eMove == E_JHSMOOTHMOVEING.E_UPDOWN)
        {
            m_fFirstPos = m_stOriginPos.y - m_fMoveRange;
            m_fSecondPos = m_stOriginPos.y + m_fMoveRange;
        }
        else
        {
            m_fFirstPos = m_stOriginPos.x - m_fMoveRange;
            m_fSecondPos = m_stOriginPos.x + m_fMoveRange;
        }
        
        if (m_eMove == E_JHSMOOTHMOVEING.E_UPDOWN)
        {
            if(m_bPing==false)
            {
                if(m_stPos.y<=m_fFirstPos)
                {
                    m_stPos.y += m_fMoveSpeed * Time.deltaTime;
                }
                else
                {
                    m_stPos.y -= m_fMoveSpeed * Time.deltaTime;
                }
                if(Mathf.Abs(m_stPos.y-m_fFirstPos)<5.0f)
                {
                    m_bPing = !m_bPing;
                }
            }
            else
            {
                if (m_stPos.y <= m_fSecondPos)
                {
                    m_stPos.y += m_fMoveSpeed * Time.deltaTime;
                }
                else
                {
                    m_stPos.y -= m_fMoveSpeed * Time.deltaTime;
                }
                if (Mathf.Abs(m_stPos.y - m_fSecondPos) < 5.0f)
                {
                    m_bPing = !m_bPing;
                }
            }
        }
        else
        {
            if (m_bPing == false)
            {
                m_stPos.x = Mathf.Lerp(m_stPos.x, m_fFirstPos, Time.deltaTime * m_fMoveSpeed);
                if (Mathf.Abs(m_stPos.x - m_fFirstPos) < 5.0f)
                {
                    m_bPing = !m_bPing;
                }
            }
            else
            {
                m_stPos.x = Mathf.Lerp(m_stPos.x, m_fSecondPos, Time.deltaTime * m_fMoveSpeed);
                if (Mathf.Abs(m_stPos.x - m_fSecondPos) < 5.0f)
                {
                    m_bPing = !m_bPing;
                }
            }
        }
        transform.localPosition = m_stPos;
	}
}
