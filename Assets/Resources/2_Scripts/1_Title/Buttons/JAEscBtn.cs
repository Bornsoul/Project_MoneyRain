using UnityEngine;
using System.Collections;

public class JAEscBtn : MonoBehaviour
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
        if (Application.platform == RuntimePlatform.Android)
        {
            Application.Quit();
            return;
        }
        //AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //curActivity = jc.GetStatic<androidjavaobject>("currentActivity");
        //jc.Call("ConfirmQuit");
    }
}
