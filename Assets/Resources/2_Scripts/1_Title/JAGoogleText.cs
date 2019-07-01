using UnityEngine;
using System.Collections;

public class JAGoogleText : MonoBehaviour
{

    public UILabel m_pTitleLabel = null;
    public UILabel m_pTextLabel = null;
    string abc;
    void Start()
    {

        abc = JAMenuData_Mng.I.m_pPopupData_String.m_sText;

    }

    void Update()
    {
        m_pTitleLabel.text = JAMenuData_Mng.I.m_pPopupData_String.m_sTitle;
        m_pTextLabel.text = JAMenuData_Mng.I.ReplaceNewLine(abc); // JAMenuData_Mng.I.m_pGoogleString.m_sText;
    }
}
