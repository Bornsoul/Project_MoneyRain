using UnityEngine;
using System.Collections;

public class JABanner : MonoBehaviour
{
    public GameObject[] m_pBannerGam = null;

    // Use this for initialization
    void Start()
    {
        //for (int i = 0; i < JAGameDataFile.I._nBannerMaxCount; i++)
        //{
        //    if (JAGameDataFile.I.GetData_Bool("Banner" + i) == true)
        //    {
        //        NGUITools.SetActive(m_pBannerGam[i], true);
        //    }
        //    else
        //    {
        //        NGUITools.SetActive(m_pBannerGam[i], false);
        //    }
        //}

        NGUITools.SetActive(m_pBannerGam[0], JAGameDataFile.I.GetData_Bool("Banner0"));
        NGUITools.SetActive(m_pBannerGam[1], JAGameDataFile.I.GetData_Bool("Banner1"));
        

        //if ( JAMenuData_Mng.I._bAppWidget == true )
        //{
        //    for ( int i = 0; i<JAMenuData_Mng.I._bBanners.Length; i++)
        //    {
        //        JAMenuData_Mng.I._bBanners[i] = true;
        //    }
        //}

        //for ( int i = 0; i<m_pBannerGam.Length; i++)
        //{
        //    NGUITools.SetActive(m_pBannerGam[i], JAMenuData_Mng.I._bBanners[i]);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < JAGameDataFile.I._nBannerMaxCount; i++)
        //{
        //    if (JAGameDataFile.I.GetData_Bool("Banner" + i) == true)
        //    {
        //        NGUITools.SetActive(m_pBannerGam[i], true);
        //    }
        //    else
        //    {
        //        NGUITools.SetActive(m_pBannerGam[i], false);
        //    }
        //}
        NGUITools.SetActive(m_pBannerGam[0], JAGameDataFile.I.GetData_Bool("Banner0"));
        NGUITools.SetActive(m_pBannerGam[1], JAGameDataFile.I.GetData_Bool("Banner1"));
    }
}
