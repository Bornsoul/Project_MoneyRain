using UnityEngine;
using System.Collections;

/// <summary>
/// NGUI 스크롤 할때 Scale 1,1,1 클립핑 관련 문제 해결해주는 클래스
///     UIPanel있는 오브젝트에다가 넣고 호리즌 버티컬만 설정해주면 됨
///     짱깨 문서 출처
/// </summary>
public class CSScrollPanelFix : MonoBehaviour
{
    public SCPFScreenDirection screenDirection;
    //horizontal表示水平滑动；vertical表示垂直滑动。  
    public enum SCPFScreenDirection
    {
        horizontal,
        vertical
    }
    private Transform parent;
    private Transform child;
    private float ScaleSize;
    private float rateX;
    private float rateY;
    UIPanel PanelScript;
    void Start()
    {
        parent = transform.parent;
        child = transform.GetChild(0);
        PanelScript = transform.GetComponent<UIPanel>();

        SetPanel();
    }
    void SetPanel()
    {
        transform.parent = null;
        child.parent = null;


        if (screenDirection == SCPFScreenDirection.vertical)
        {
            ScaleSize = transform.localScale.y;
            rateX = ScaleSize / transform.localScale.x;
            rateY = 1;
        }
        else if (screenDirection == SCPFScreenDirection.horizontal)
        {
            ScaleSize = transform.localScale.x;
            rateX = 1;
            rateY = ScaleSize / transform.localScale.y;
        }

        transform.localScale = new Vector4(ScaleSize, ScaleSize, ScaleSize, ScaleSize);
        transform.parent = parent;
        child.parent = transform;
        PanelScript.clipRange = new Vector4(PanelScript.clipRange.x, PanelScript.clipRange.y, PanelScript.clipRange.z * rateX, PanelScript.clipRange.w * rateY);
    }

}