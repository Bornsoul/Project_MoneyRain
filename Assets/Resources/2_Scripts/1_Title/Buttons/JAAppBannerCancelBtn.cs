using UnityEngine;
using System.Collections;

public class JAAppBannerCancelBtn : MonoBehaviour
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
        JAGameDataFile.I.SetData("AppBanner", false);
    }
}
