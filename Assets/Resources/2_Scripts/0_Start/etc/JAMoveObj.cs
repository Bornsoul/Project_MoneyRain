using UnityEngine;
using System.Collections;

public class JAMoveObj : MonoBehaviour
{

    Vector3 m_stPos;
    Vector3 m_stMove;
    public Vector3 m_stUserPos;

    void Start()
    {
        m_stPos = transform.localPosition;
        m_stMove = m_stPos;
    }

    // Update is called once per frame
    void Update()
    {
        m_stPos.x = Mathf.SmoothStep(m_stPos.x, m_stMove.x + m_stUserPos.x, 0.03f);
        m_stPos.y = Mathf.SmoothStep(m_stPos.y, m_stMove.y + m_stUserPos.y, 0.03f);

        transform.localPosition = m_stPos;
    }
}
