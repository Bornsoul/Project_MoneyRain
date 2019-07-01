using UnityEngine;
using System.Collections;

public class JHMessageTutorial_Menu : MonoBehaviour
{

    public JHMessageTutorial_Box m_pBox = null;
    public JHMessageTutorial_Text m_pText = null;

    public UISprite m_pTutoBack = null;

    public void Enter()
    {
        m_pTutoBack.enabled = false;
        m_pBox.gameObject.SetActive(true);

        m_pBox.Enter();

        StartCoroutine(Cor_Tu1());
    }

    IEnumerator Cor_Tu1()
    {
        yield return new WaitForSeconds(1f);
        m_pTutoBack.enabled = true;
        m_pTutoBack.spriteName = "tuto_0";
        m_pBox.Enter();
        m_pBox.Open(true);
        m_pBox.SetText(m_pText.m_pText[0]);

        yield return new WaitForSeconds(0.8f);
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }
        m_pBox.SetText(m_pText.m_pText[1]);
        
        yield return new WaitForSeconds(0.8f);    
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }
        m_pTutoBack.enabled = false;
        m_pBox.Close();
        //! 집업글 팝업창 생성
        JAHouseSystem_Mng.I.Btn_Upgrade();
        JAPop_HouseUpg.I.m_pDestroyObj.m_bSetClick = false;
        JAPop_HouseUpg.I.Tutorial_Btn();
        // --

        yield return new WaitForSeconds(0.5f);
        m_pBox.Open(true);
        m_pBox.SetText(m_pText.m_pText[2]);
        JAPop_HouseUpg.I.TutorialMod(true);

        yield return new WaitForSeconds(0.8f);
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }
        m_pBox.Close();
        m_pTutoBack.enabled = true;
        m_pTutoBack.spriteName = "tuto_1";

        //! 집 시간 3 물량 4 찍으면 넘어감
        yield return new WaitForSeconds(0.6f);
        while (true)
        {
            if (JAPop_HouseUpg.I.m_pHPopTop_Src._nStats[0] >= 3 &&
                JAPop_HouseUpg.I.m_pHPopTop_Src._nStats[1] >= 4)
            {
                if (Input.GetMouseButtonUp(0)) break;
                yield return null;
            }
            yield return null;
        }
        // --
        m_pBox.Move(-313.71f, 300f);
        m_pTutoBack.spriteName = "tuto_2";

        yield return new WaitForSeconds(1.2f);

        m_pBox.Open(true);
        m_pBox.SetText(m_pText.m_pText[3]);

        yield return new WaitForSeconds(0.8f);
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }
        m_pTutoBack.enabled = false;
        JAPop_HouseUpg.I.TutorialMod(false);
        m_pBox.Move(-313.71f, 205.4f);
        m_pBox.Close();
        //! 집업그레이드 종료
        JAPop_HouseUpg.I.Button_Exit(); 
        // --

        //! 캐릭터 업그레이드 팝업
        yield return new WaitForSeconds(0.3f);
        CSPrefabMng.I.CreatePrefab(JHTitle_Scene.I._PopLayer1.m_pAnchor, "2_Objects/Popup/Pop_CharUpg", "Pop_CharUpg");
        JAPop_CharacterUpg.I.Tutorial_Mod(true);
        // --

        yield return new WaitForSeconds(0.4f);
        m_pBox.Open(true);
        m_pBox.SetText(m_pText.m_pText[4]);

        yield return new WaitForSeconds(0.8f);
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }
        m_pBox.Close();

        m_pTutoBack.enabled = true;
        m_pTutoBack.spriteName = "tuto_3";

        //! 캐릭터 능력치 선택
        yield return new WaitForSeconds(0.8f);
        while (true)
        {
            if (JAPop_CharacterUpg.I.m_pUpgBot_Src._nStats[0] >= 2 ||
                JAPop_CharacterUpg.I.m_pUpgBot_Src._nStats[1] >= 2 ||
                JAPop_CharacterUpg.I.m_pUpgBot_Src._nStats[2] >= 2)
            {
                if (Input.GetMouseButtonUp(0)) break;
                
                
                yield return null;
            }
            yield return null;
        }
        // --
        yield return new WaitForSeconds(0.2f);
        JAPop_CharacterUpg.I.Tutorial_Btn();
        m_pTutoBack.enabled = false;

        m_pBox.Open(true);
        m_pBox.SetText(m_pText.m_pText[5]);
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }
        
        //! 캐릭 업글 팝업 종료
        JAPop_CharacterUpg.I.Button_Exit();
        JAPop_CharacterUpg.I.Tutorial_Mod(false);
        m_pTutoBack.enabled = false;
        // --
        
        m_pBox.Close();
        m_pTutoBack.enabled = true;
        m_pTutoBack.spriteName = "tuto_4";

        yield return new WaitForSeconds(0.3f);
        m_pBox.Open(true);
        m_pBox.Move(-313.71f, 410f);
        m_pBox.SetText(m_pText.m_pText[6]);
        
        yield return new WaitForSeconds(0.8f);
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }
        m_pBox.SetText(m_pText.m_pText[7]);


        yield return new WaitForSeconds(0.8f);
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }
        m_pTutoBack.spriteName = "tuto_5";

        m_pBox.SetText(m_pText.m_pText[8]);

        yield return new WaitForSeconds(0.8f);
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }
        m_pBox.Close();

        //JAMenuData_Mng.I._bTutorialCheck = false;
        JAGameDataFile.I.SetData("TutoCheck", false);
        JAGameDataFile.I.SetData("AppBanner", true);

        for (int i = 0; i < JAGameDataFile.I._nBannerMaxCount; i++ )
        {
            JAGameDataFile.I.SetData("Banner"+i, true);
        }
            

        CSPrefabMng.I.DestroyPrefab(JHTitle_Scene.I._pPopTutorial.pObj);
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
