using UnityEngine;
using System.Collections;

public class JHMessageTutorial_Game : MonoBehaviour
{
    public JHMessageTutorial_Box m_pBox = null;
    public JHMessageTutorial_Text m_pText = null;

    public JHMessageTutorial_Mokpyo m_pMokpyo = null;
    // Use this for initialization
    void Start()
    {

    }

    public void Enter()
    {
        JHGameData_Mng.I._TutorialState = true;

        // JHGame_MainLayer.I.m_pEnemy_Mng._Active = false;
        // JHGame_MainLayer.I.m_pMoney_Mng._Active = false;
        JHGame_MainLayer.I._Hero.pSrc._Active = false;

        m_pBox.gameObject.SetActive(true);

        m_pBox.Enter();
        StartCoroutine("Cor_Tu1");
    }
    //! 빡빡이 스타일 코드
    IEnumerator Cor_Tu1()
    {
        yield return new WaitForSeconds(2.5f);
        m_pBox.Enter();
        m_pBox.Open(true);
        m_pBox.SetText(m_pText.m_pText[0]);

        yield return new WaitForSeconds(0.8f);
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }
        m_pBox.SetText(m_pText.m_pText[10]);
        yield return new WaitForSeconds(0.8f);
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }
        m_pBox.SetText(m_pText.m_pText[1]);
        m_pBox.Touch(false);
        yield return new WaitForSeconds(0.2f);
        JHGame_MainLayer.I._Hero.pSrc._Active = true;
        JHGame_UILayer.I.m_pTutorial.Enter();
        JHGame_UILayer.I.m_pTutorial.Temp_UpDelete(false);
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }

        yield return new WaitForSeconds(2.5f);

        m_pBox.Move(-313.71f, 343.76f);
        m_pBox.SetText("");
        yield return new WaitForSeconds(0.3f);
        JHGame_UILayer.I.m_pTutorial.Temp_UpDelete(true);
        m_pBox.SetText(m_pText.m_pText[2]);

        while (true)
        {
            if (JHGame_MainLayer.I._Hero.pSrc._State == E_HEROSTATE.E_JUMP) break;
            yield return null;
        }
        yield return new WaitForSeconds(2.5f);
        //JHGame_UILayer.I.m_pTutorial.DeActive();
        m_pBox.SetText(m_pText.m_pText[3]);
        m_pBox.Touch(true);
        yield return new WaitForSeconds(0.8f);
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }
        JHGame_MainLayer.I._Hero.pSrc._Active = false;
        JHGame_MainLayer.I.m_pMoney_Mng.CreateMoney(E_MONEY_CLASS.E_COIN_BRONZE);
        yield return new WaitForSeconds(0.2f);
        JHGame_MainLayer.I.m_pMoney_Mng.CreateMoney(E_MONEY_CLASS.E_COIN_BRONZE);
        yield return new WaitForSeconds(0.2f);
        JHGame_MainLayer.I.m_pMoney_Mng.CreateMoney(E_MONEY_CLASS.E_COIN_BRONZE);

        yield return new WaitForSeconds(0.5f);
        m_pBox.SetText(m_pText.m_pText[4]);
        yield return new WaitForSeconds(0.8f);
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }
        StartCoroutine("Cor_Money");
        m_pBox.SetText("");
        m_pBox.Close();
        m_pMokpyo.gameObject.SetActive(true);
        m_pMokpyo.Enter();
        JHGame_MainLayer.I._Hero.pSrc._Active = true;
        yield return new WaitForSeconds(0.8f);
        while (true)
        {
            if (JHGameData_Mng.I._GameScore >= 777) break;
            yield return null;
        }
        m_pMokpyo.gameObject.SetActive(false);
        JHUserData_Mng.I.m_pUserData.PlusData("_FirstMoneyGet");   //! 업적 달성
        StopCoroutine("Cor_Money");
        yield return new WaitForSeconds(1.5f);
        JHGame_MainLayer.I._Hero.pSrc._Active = false;
        JHGame_MainLayer.I._Hero.pSrc.TutorialMove();
        yield return new WaitForSeconds(1.5f);
        JHGame_MainLayer.I.m_pEnemy_Mng.CreateEnemy(E_ENEMY_CLASS.E_BALL);
        yield return new WaitForSeconds(2.1f);
        m_pBox.Open(true);
        yield return new WaitForSeconds(0.2f);
        m_pBox.SetText(m_pText.m_pText[5]);
        yield return new WaitForSeconds(0.8f);
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }
        m_pBox.Close();
        JHGame_MainLayer.I.m_pEnemy_Mng.CreateEnemy(E_ENEMY_CLASS.E_COW);
        yield return new WaitForSeconds(1.0f);
        JHGameData_Mng.I.SetCycle(true);
        JHGame_MainLayer.I._Hero.pSrc._Active = true;
        m_pBox.Open(true);
        yield return new WaitForSeconds(0.2f);
        m_pBox.SetText(m_pText.m_pText[6]);
        yield return new WaitForSeconds(0.8f);
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }
        JHGameData_Mng.I.SetCycle(false);
        m_pBox.Close();
        while (true)
        {
            if (JHGame_MainLayer.I.m_pEnemy_Mng.m_pList.Count <= 0)
            {
                m_pBox.Open(true);
                m_pBox.SetText(m_pText.m_pText[7]);
                break;
            }
            if (JHGame_MainLayer.I._Hero.pSrc._State == E_HEROSTATE.E_STURN)
            {
                m_pBox.Open(true);
                m_pBox.SetText(m_pText.m_pText[8]);
                break;
            }
            yield return null;
        }
        yield return new WaitForSeconds(1.8f);
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }
        m_pBox.Close();
        StartCoroutine("Cor_Money");
        while (true)
        {
            if (JHGame_MainLayer.I._Hero.pSrc._CurrHp < 3.0f)
            {
                break;
            }
            JHGame_MainLayer.I._Hero.pSrc._CurrHp -= 1.8f*Time.deltaTime;
            JHGame_MainLayer.I.m_pEnemy_Mng.TutorialUpdate();
            yield return null;
        }
        StopCoroutine("Cor_Money");
        JHGame_MainLayer.I._Hero.pSrc._Active = false;
        JHGame_MainLayer.I._Hero.pSrc.TutorialMove();
        yield return new WaitForSeconds(1.8f);
        m_pBox.Open(true);
        m_pBox.SetText(m_pText.m_pText[9]);
        yield return new WaitForSeconds(1.8f);
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) break;
            yield return null;
        }
        m_pBox.Close();
        End();
    }

    void End()
    {
        JAGameDataFile.I.SetData(JAGameDataFile.I._sFirstGameStr, false);
        //JAMenuData_Mng.I._bTutorialCheck = true;
        JAGameDataFile.I.SetData("TutoCheck", true);
        JHGameData_Mng.I.SetGameOver();
    }


    IEnumerator Cor_Money()
    {
        float fTime = 0.0f;
        while (true)
        {
            fTime += Time.deltaTime;
            if (fTime > 0.1f)
            {
                JHGame_MainLayer.I.m_pMoney_Mng.CreateMoney(E_MONEY_CLASS.E_COIN_BRONZE);
                fTime = 0.0f;
            }

            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StopCoroutine("Cor_Tu1");
            End();
        }
    }
}
