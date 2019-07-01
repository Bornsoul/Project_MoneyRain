using UnityEngine;
using System.Collections;
using System.IO;

public class Share : MonoBehaviour
{
		public string title;
		public string content;
		static AndroidJavaClass sharePluginClass;
		static AndroidJavaClass unityPlayer;
		static AndroidJavaObject currActivity;
        static AndroidJavaClass uriClass;
      
		void Start ()
		{

#if UNITY_EDITOR
        return;
#endif

            sharePluginClass = new AndroidJavaClass("com.ari.tool.UnityAndroidTool");
            if (sharePluginClass == null)
            {
                Debug.Log("sharePluginClass is null");
            }
            else
            {
                Debug.Log("sharePluginClass is not null");
            }
            unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

               
		}

		void Update ()
		{
			//	if (Input.GetKeyUp (KeyCode.Menu)) {
			//			CallShare (title, "", content);
			//	}
		}
    public void Call()
        {
            CallShare(title, "", content);
        }

    public void PictureCall(string aUrl)
    {
        #if UNITY_EDITOR
        return;
#endif

       // CSDirector.I.GetDeviceDebug().Log("PPictureCall In");
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
       
        //instantiate the object Intent
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

        //call setAction setting ACTION_SEND as parameter
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

        //instantiate the class Uri
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
      
        //instantiate the object Uri with the parse of the url's file

        aUrl = "file://" + aUrl;
         // CSDirector.I.GetDeviceDebug().Log(aUrl);

        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", aUrl);

        //call putExtra with the uri object of the file
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
       
        //set the type of file
        intentObject.Call<AndroidJavaObject>("setType", "image/png");

        //instantiate the class UnityPlayer
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");



      

        //instantiate the object currentActivity
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

        //call the activity with our Intent
        currentActivity.Call("startActivity", intentObject);
        
    }

		public static void CallShare (string handline, string subject, string text)
    {

#if UNITY_EDITOR
        return;
#endif
        Debug.Log ("share call start");
				sharePluginClass.CallStatic ("share", new object[] {
						handline,
						"[돈쏘나기]\n귀여운 꼬냥이와 함께 부자 되보세요!",
						"https://www.facebook.com/MoneyShower?ref=hl"
				});
				Debug.Log ("share call end");
		}
}
