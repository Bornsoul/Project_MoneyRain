using UnityEngine;
using System.Collections;

public class CSDebug_Ratio : MonoBehaviour {
    public UILabel ui_Label = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ui_Label.text = "Ratio : " + AspectRatios.GetAspectRatio();
	}
}
