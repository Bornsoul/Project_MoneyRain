using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
 

public class JHPicture : MonoBehaviour {
    GameObject m_pObject = null;
    UITexture m_pTexture = null;
    
    Texture2D tex;

    
    
    bool m_bProcess = false;
    public bool _Process { get { return m_bProcess; } }
	// Use this for initialization
	void Start () {
	//Texture2D
        m_bProcess = false;
       
       
	}

 IEnumerator Cor_Te()
    {
        tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);

        yield return null; 
    }

    IEnumerator Cor_Picture()
    {
        m_bProcess = true;
        yield return new WaitForEndOfFrame();
        
        JHGameData_Mng.I.SetCycle(true);
       

        yield return StartCoroutine(Cor_Te());
       
        yield return new WaitForEndOfFrame();
        AutoFade.LoadFade(0.2f, 0.3f, Color.white);
        tex.Apply(true);
        CSSoundMng.I.Play("CameraShot");
        m_pObject = CSPrefabMng.I.CreatePrefab(this.gameObject, "2_Objects/GameUI/GameUI_PictureTexture", "PictureTexter");
        m_pObject.GetComponent<Animation>().Play("Test2_Picture");
        m_pTexture = m_pObject.GetComponent<UITexture>();
        m_pTexture.mainTexture = tex;
   
        yield return new WaitForSeconds(1.5f);

        CSPrefabMng.I.CreatePrefab(transform.parent.gameObject, "2_Objects/GameUI/GameUI_SharePopup", "GameUI_SharePopup");

        yield return null;
    }    

    public void Share()
    {
        CSDirector.I.ShowLoading(JHGame_UILayer.I.gameObject, 500, 1.0f);
        StartCoroutine(Cor_Share());
    }

    IEnumerator Cor_Share()
    {
        yield return StartCoroutine(Cor_ShareIn());
        CSDirector.I.ExitLoading();
        yield return null;
    }

    IEnumerator Cor_ShareIn()
    {
        yield return new WaitForSeconds(0.1f);
        byte[] imageByte = tex.EncodeToPNG();
        string timea = Time.time.ToString();
        timea = timea.Replace(".", "_");
        string path = Application.persistentDataPath + "/MoneyRain_" + timea;
        path += ".png";
        File.WriteAllBytes(path, imageByte);
        JHGame_UILayer.I.m_pShare.PictureCall(path);
        yield return null;
    }

    public void Save()
    {
        CSDirector.I.ShowLoading(JHGame_UILayer.I.gameObject, 500, 1.0f);
        StartCoroutine(Cor_Save());
    }

    IEnumerator Cor_Save()
    {
        yield return StartCoroutine(Cor_SaveIn());
        CSDirector.I.ExitLoading();
        CSDirector.I.ShowScreenMessage(JHGame_UILayer.I.gameObject, 300, "스크린샷 저장 완료");
      //  CSDirector.I.GetDeviceDebug().Log("Svae Complite");
        yield return null;
    }

    IEnumerator Cor_SaveIn()
    {
        yield return new WaitForSeconds(0.1f);
     //   CSDirector.I.GetDeviceDebug().Log("Svae In");
        byte[] imageByte = tex.EncodeToPNG();
        string timea = Time.time.ToString();
        timea = timea.Replace(".", "_");
        // string path = "storage/emulated/0/Pictures/MoneyRain_" + timea;
        Directory.CreateDirectory(Application.persistentDataPath + "/../../../../Pictures");
        string path = Application.persistentDataPath + "/../../../../Pictures/MoneyRain_" + timea;

        path += ".png";
        File.WriteAllBytes(path, imageByte);
        Scan(path);
        yield return null;
    }



    /// <summary>
    /// 갤러리 Refresh하는것
    /// </summary>
    /// <param name="sUrl"></param>
    public void Scan(string sUrl)
    {

#if UNITY_EDITOR
        return;
#endif

        sUrl = "file://" + sUrl;
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        string action = intentClass.GetStatic<string>("ACTION_MEDIA_SCANNER_SCAN_FILE");

        // Intent intentObject = new Intent(action);
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent", action);

        // Uri uriObject = Uri.parse("file:" + filePath);
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", sUrl);

        // intentObject.setData(uriObject);
        intentObject.Call<AndroidJavaObject>("setData", uriObject);

        // this.sendBroadcast(intentObject);
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("sendBroadcast", intentObject);
    }




    public void ExitPicture()
    {
        DestroyImmediate(tex);
        CSPrefabMng.I.DestroyPrefab(m_pObject);
        JHGameData_Mng.I.SetCycle(false);
        m_bProcess = false;
    }

    public void TakePicture()
    {
        StartCoroutine(Cor_Picture());
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
