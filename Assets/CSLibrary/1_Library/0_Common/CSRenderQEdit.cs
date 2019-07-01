using UnityEngine;
using System.Collections;

/// <summary>
/// 쉐이더 내부 RenderQ 설정할수있는 클래스
/// 
/// 파티클중에 NGUI Depth조절하고싶을때 사용하면된다.
/// NGUI의 기본 RenderQ는 3000이다. 그래서 3000보다 높으면 앞에 나오고 낮으면 뒤에 나오게 된다.
/// </summary>
public class CSRenderQEdit : MonoBehaviour {
    public int nRenderQ = 3000;

	// Use this for initialization
	void Start () {

        if (renderer != null) renderer.material.renderQueue = nRenderQ;
       
        Transform[] allChildren = transform.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.gameObject.renderer == null) continue;
          //  Debug.Log(nRenderQ);
            child.gameObject.renderer.material.renderQueue = nRenderQ;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
