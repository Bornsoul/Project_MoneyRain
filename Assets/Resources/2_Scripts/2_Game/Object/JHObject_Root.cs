using UnityEngine;
using System.Collections;


public enum E_JUMPSTATE
{
    E_JUMP_NONE = 0,
    E_JUMP_DOWN,
    E_JUMP_UP,
};

public struct JumpStruct
{
    public float m_fDt;
    public float m_fJumpDt;
    public E_JUMPSTATE m_eState;
    public float m_fGravity;
    public float m_fPower;

    public void Reset()
    {
        m_fDt = 0.0f;
        m_fJumpDt = 0.0f;
        m_eState = E_JUMPSTATE.E_JUMP_NONE;
        m_fGravity = 90.0f;
        m_fPower = 30.0f;
    }
};

public class JHObject_Root : MonoBehaviour {
    protected Vector3   m_stPos;
    protected Vector3   m_stScale;
    protected float     m_fRotation;
    protected Color     m_stColor;

    protected bool      m_bActive;

   
    public Vector3 _Position { get { return m_stPos; } set { m_stPos = value; } }
    public Vector3 _Scale { get { return m_stScale; } set { m_stScale = value; } }
    public Color _Color { get { return m_stColor; } set { m_stColor = value; } }
    public bool _Active { get { return m_bActive; } set { m_bActive = value; } }


    virtual public bool Enter()
    {
        m_stPos = new Vector3(-10000.0f, -10000.0f, 0.0f);
        m_stScale = Vector3.one;
        m_fRotation = 0.0f;
        m_stColor = Color.white;

        m_bActive = false;

        transform.localPosition = m_stPos;
        transform.localScale = m_stScale;
        transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, m_fRotation));

        return true;
    }

    virtual public bool Create()
    {
        m_bActive = true;

        return true;
    }

    virtual public bool End()
    {
        m_bActive = false;
        m_stPos = new Vector3(-10000.0f, -10000.0f, 0.0f);
        transform.localPosition = m_stPos;
        return true;
    }

    virtual public bool Destroy()
    {
        m_bActive = false;

        return true;
    }

    protected void ResetTransform()
    {
        transform.localPosition = m_stPos;
        transform.localScale = m_stScale;
        transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, m_fRotation));
    }
	
	// Update is called once per frame
	virtual public bool Update () {
        if (m_bActive == false) return false;
        if (JHGameData_Mng.I._GamePause == true) return false;
        if (JHGameData_Mng.I._GameCycle == true) return false;
    
        //이곳에 일시정지나 이런거 예외처리하면됨
        return true;
	}
}
