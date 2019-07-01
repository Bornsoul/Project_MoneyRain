using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class JHAdmob : CSSingleton<JHAdmob> {
    InterstitialAd interstitial;
	 BannerView bannerView;

     bool m_bActive = false;

     bool m_bBannerShow = false;
     public bool _BannerShow { get { return m_bBannerShow; } }
    // Use this for initialization
    void Start ()
    {
        
        #if !UNITY_EDITOR
        m_bActive=true;
           AdSize adSize = new AdSize(360, 50);
        bannerView = new BannerView("ca-app-pub-5184581489958811/4588424084", adSize, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
        interstitial = new InterstitialAd("ca-app-pub-5184581489958811/6843886487");
       

        BannderView(true);
        DontDestroyOnLoad(this);
#endif

    }

    public void BannderView(bool bShow=true)
    {
        if (m_bActive == false) return;
        m_bBannerShow = bShow;
        if(bShow==true)
        {
            bannerView.Show();
        }
        else
        {
            bannerView.Hide();
        }
    }

    public void InterstitialView()
    {
       
        if (m_bActive == false) return;
    
        StartCoroutine(Cor_inter());
       
    }

    IEnumerator Cor_inter()
    {
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
        bannerView.LoadAd(request);
        bannerView.Show();
        while(true)
        {
            if (interstitial.IsLoaded())
            {
                interstitial.Show();  // 전면 광고 Show
                break;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    // Update is called once per frame
    void Update () 
    {

    }

}
