using UnityEngine;
using System.Collections;

public class JAEndYesBtn : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {
        CSSoundMng.I.Play("MenuEF_Button");
        CSPrefabMng.I.DestroyPrefab("Pop_HouseUpg(Clone)");

        JAGameDataFile.I.EndingReset(); //! 엔딩전용 리셋
        JHTitle_MainLayer.I.EndingShow();
        //StartCoroutine(Delay(0.8f));
    }

    IEnumerator Delay(float fWaitTime)
    {
        yield return new WaitForSeconds(fWaitTime);

        JHTitle_MainLayer.I.EndingShow();
    }
}
