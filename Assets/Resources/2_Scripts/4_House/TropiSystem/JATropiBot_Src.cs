using UnityEngine;
using System.Collections;

public class JATropiBot_Src : MonoBehaviour
{
    public UILabel m_pTextLabel = null;

    public void Enter(string sTextLabel)
    {
        m_pTextLabel.text = sTextLabel;
    }

    void Update()
    {

    }
}
