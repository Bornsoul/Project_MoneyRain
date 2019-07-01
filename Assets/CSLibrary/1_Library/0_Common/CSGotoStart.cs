using UnityEngine;
using System.Collections;

public class CSGotoStart : MonoBehaviour {

    void Awake()
    {
        GameObject Obj = GameObject.Find("0_Library");

        if (Obj == null)
            Application.LoadLevel(0);

    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
