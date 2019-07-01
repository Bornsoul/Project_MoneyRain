using UnityEngine;
using System.Collections;

public enum E_ITEM_CLASS
{
    E_ITEM_HEART = 0,

    E_MAX,
}

public class JHItem_Root : JHObject_Root {

    protected float m_fDownSpeed = 0.0f;

    protected E_ITEM_CLASS m_eClass;
    public E_ITEM_CLASS _Class { get { return m_eClass; } }

    public UISprite m_pBackSprite = null;
    Color m_stBackColor;

    protected const float m_fWallX = 335.0f;            //! 최대 벽위치
    protected const float m_fStartY = 700.0f;           //! 소환됬을때 떨어질 Y좌표

    virtual public bool Enter()
    {
        if (base.Enter() == false) return false;
        m_fDownSpeed = 0.0f;
        m_eClass = E_ITEM_CLASS.E_MAX;
        m_stBackColor = m_pBackSprite.color;
        return true;
    }

    virtual public bool Create()
    {
        if (base.Create() == false) return false;
        float fSpawnX = (float)CSRandom.Rand(0, (int)((m_fWallX - 100.0f) * 2.0f));
        fSpawnX += Random.RandomRange(0.0f, 100.0f);
        fSpawnX -= m_fWallX;

        m_stPos = new Vector3(fSpawnX, m_fStartY, 0.0f);

        StartCoroutine(Cor_BackSpriteColor());
        ResetTransform();
        return true;
    }

    virtual public bool End()
    {
        if (base.End() == false) return false;

        return true;
    }

    virtual public bool Destroy()
    {
        if (base.Destroy() == false) return false;

        return true;
    }
	// Update is called once per frame
    virtual public bool Update()
    {
        if (base.Update() == false) return false;
        if (m_bActive == false) return false;
        m_stPos.y -= m_fDownSpeed * Time.deltaTime;
        m_pBackSprite.transform.Rotate(new Vector3(0.0f, 0.0f, 50.0f * Time.deltaTime));
        if (m_stPos.y <= -700.0f)
        {
            m_stPos.y = -700.0f;
            End();
            return false;
        }
        ResetTransform();
        //이곳에 일시정지나 이런거 예외처리하면됨
        return true;
    }

    virtual public void Eated()
    {
        CSSoundMng.I.Play("Item_Sound");
        End();
    }


    IEnumerator Cor_BackSpriteColor()
    {
        bool bPing = false;
        while (m_bActive==true)
        {
            if (bPing==false)
            {
                m_stBackColor.a -= 5.0f * Time.deltaTime;
                if (m_stBackColor.a < 0.3f)
                {
                    bPing = !bPing;
                }
            }
            else
            {
                m_stBackColor.a += 5.0f * Time.deltaTime;
                if (m_stBackColor.a > 1.1f)
                {
                    bPing = !bPing;
                }
            }
            m_pBackSprite.color = m_stBackColor;
            yield return null;
        }
    }
}
