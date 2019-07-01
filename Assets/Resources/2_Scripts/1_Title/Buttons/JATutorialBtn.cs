using UnityEngine;
using System.Collections;

public class JATutorialBtn : MonoBehaviour
{

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
     //   if ( JAGameDataFile.I.GetData_Bool("FirstGame") == true)
       //     JAGameDataFile.I.SetData("FirstGame", false);

        CSSoundMng.I.AllStop();
        AutoFade.LoadLevel("3_Loading", 0.3f, 0.3f, Color.black);
    }
}
