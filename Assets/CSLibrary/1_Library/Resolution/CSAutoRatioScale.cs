using UnityEngine;
using System.Collections;

public class CSAutoRatioScale : MonoBehaviour
{

	// Use this for initialization
	void Start () {
        Debug.Log(CSDirector.I.GetRatioScale());
        if (JHAdmob.I != null)
        {
            if (JHAdmob.I._BannerShow == true)
            {
                CSDirector.I.SetRatioADReset();
                transform.localPosition = CSDirector.I.GetADYMove();

            }
        }
        transform.localScale = CSDirector.I.GetRatioScale();
      
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
