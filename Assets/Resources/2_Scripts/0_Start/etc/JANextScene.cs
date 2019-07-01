using UnityEngine;
using System.Collections;

public class JANextScene : MonoBehaviour
{
    public bool m_bUseFade = true;
    public string m_sNextScene;
    public Vector2 m_stFadeInOut;
    public Color m_pColor;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        if (m_bUseFade == true)
            AutoFade.LoadLevel(m_sNextScene, m_stFadeInOut.x, m_stFadeInOut.y, m_pColor);
        else
            Application.LoadLevel(m_sNextScene);
    }
}
