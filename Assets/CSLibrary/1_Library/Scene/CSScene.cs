using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class CSScene : MonoBehaviour {

    void Awake()
    {
        if (CSPrefabMng.I != null)  CSPrefabMng.I.DestroyAllPrefabs();
    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy()/in " + Application.loadedLevelName+"_CCScene");
        if (CSPrefabMng.I != null)  CSPrefabMng.I.DestroyOncePrefabs();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
