using UnityEngine;
using System.Collections;

public class JAOptionTop_Src : MonoBehaviour
{
    


    public void Enter()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(m_pToggles[(int)eToggle.E_TOGGLE_SOUND].value);
    }


    void Button_Sound()
    {
        JAPop_Option.I._nSelectIndex = 0;
        JAPop_Option.I.m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_SOUND] = !JAPop_Option.I.m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_SOUND];
        JAGameDataFile.I.SetData("SoundMod", JAPop_Option.I.m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_SOUND]);
        JAPop_Option.I.Button_Click();
    }

    void Button_Vibrate()
    {
        JAPop_Option.I._nSelectIndex = 1;
        JAPop_Option.I.m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_VIBRATE] = !JAPop_Option.I.m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_VIBRATE];
        JAGameDataFile.I.SetData("VibMod", JAPop_Option.I.m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_VIBRATE]);
        JAPop_Option.I.Button_Click();
    }

    void Button_Touch1()
    {
        if (JAPop_Option.I.m_pToggles[2].value == true)
        {
            JAPop_Option.I.m_pToggles[2].value = true;
            JAPop_Option.I.m_pToggles[3].value = false;
        }
        else
        {
            JAPop_Option.I.m_pToggles[2].value = false;
            JAPop_Option.I.m_pToggles[3].value = true;
        }
        JAPop_Option.I._nSelectIndex = 2;
        JAPop_Option.I.m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_TOUCH_1] = !JAPop_Option.I.m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_TOUCH_1];
        JAGameDataFile.I.SetData("TouchMod", JAPop_Option.I.m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_TOUCH_1]);
        JAPop_Option.I.Button_Click();
    }

    void Button_Touch2()
    {
        if (JAPop_Option.I.m_pToggles[3].value == true)
        {
            JAPop_Option.I.m_pToggles[3].value = true;
            JAPop_Option.I.m_pToggles[2].value = false;
        }
        else
        {
            JAPop_Option.I.m_pToggles[3].value = false;
            JAPop_Option.I.m_pToggles[2].value = true;
        }
        JAPop_Option.I._nSelectIndex = 3;
        JAPop_Option.I.m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_TOUCH_1] = !JAPop_Option.I.m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_TOUCH_1];
        JAGameDataFile.I.SetData("TouchMod", JAPop_Option.I.m_bCheck[(int)E_JA_TOGGLE.E_TOGGLE_TOUCH_1]);
        JAPop_Option.I.Button_Click();
    }

    
}
