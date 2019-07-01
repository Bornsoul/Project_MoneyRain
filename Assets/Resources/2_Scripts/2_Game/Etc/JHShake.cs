using UnityEngine;
using System.Collections;

/// <summary>
/// ī�޶�� �����ִ� Ŭ����
/// </summary>
public class JHShake : MonoBehaviour
{
    float updownDecaytime = 0.0f;
    float leftrightDecaytime = 0.0f;
    Vector3 offset = new Vector3(0, 0, 0);
    Vector3 position;
    float startTime;
    float amp = 5.03f; // ���� . ���Ϸ� �̵��ϴ� ����. ���� Ŭ���� �� ũ�� ���� .
    float freq = 10f; // ������. �ʴ� ���� � Ƚ��. ���� Ŭ���� �� �� ���� .
    float phase = 0f; // ���� . �Լ��� ���� ������

    bool updown;
    bool leftright;

    // Use this for initialization
    void Start()
    {
        // StartUpDownShake(5.0f);
    }

    /// <summary>
    /// ����� �����ִ� �Լ�
    /// </summary>
    public void StopShake()
    {
        updown = false;
        leftright = false;
        this.gameObject.transform.localPosition = position;
        offset = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// ���Ʒ��� ���� �ִ� �Լ�
    /// </summary>
    /// <param name="decay"></param> ���� �ð�
    public void StartUpDownShake(float decay)
    {
        startTime = Time.time;
        updownDecaytime = decay + Time.fixedTime - startTime;
        position = this.gameObject.transform.localPosition;
        updown = true;
      //  Debug.Log("Start");
    }

    /// <summary>
    /// �¿�/�� ���� �ִ� �Լ�
    /// </summary>
    /// <param name="decay"></param> ���� �ð�
    public void StartLeftRightShake(float decay)
    {
        startTime = Time.time;
        leftrightDecaytime = decay + Time.fixedTime - startTime;
        position = this.gameObject.transform.localPosition;
        leftright = true;
    }

    // public void 

    // Update is called once per frame
    public void Update()
    {
        //UpDownShake();
        FixedUpdate();
    }

/// <summary>
/// ���� �Լ��� ������Ʈ�ϴ� �Լ�
/// </summary>
    public void FixedUpdate()
    {
        if (updown) UpDownShake();
        if (leftright) LeftRightShake();
    }

    /// <summary>
    /// ������ �������� �����ϴ� �Լ�
    /// </summary>
    /// <param name="_amp"></param> ����
    /// <param name="_freq"></param> ������
    public void SetOption(float _amp, float _freq) // ����, ������ 
    {
        amp = _amp;
        freq = _freq;
    }

    public void Reset()
    {
        if (updown == true || leftright == true) this.gameObject.transform.localPosition = position;
        updown = false;
        leftright = false;
        offset = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// �� �Ʒ��� ���°��� ������Ʈ�ϴ� �Լ�
    /// </summary>
    public void UpDownShake()
    {
        
        float totaltime = Time.fixedTime - startTime;
        if (totaltime < updownDecaytime)
        {
            Vector3 pos = this.gameObject.transform.localPosition;

            pos -= offset;

            offset.y = Mathf.Sin(2 * 3.14159f * (totaltime * freq) + phase) * amp * (updownDecaytime - totaltime) / updownDecaytime;

            pos += offset;

            this.gameObject.transform.localPosition = pos;
        }
        else
        {
            updown = false;
            this.gameObject.transform.localPosition = position;
            offset = new Vector3(0, 0, 0);
        }
    }

    /// <summary>
    /// �¿�� ���°��� ������Ʈ�ϴ� �Լ�
    /// </summary>
    public void LeftRightShake()
    {
        float totaltime = Time.fixedTime - startTime;
        if (totaltime < leftrightDecaytime)
        {
            Vector3 pos = this.gameObject.transform.localPosition;

            pos -= offset;

            offset.x = Mathf.Sin(2 * 3.14159f * (totaltime * freq) + phase) * amp * (leftrightDecaytime - totaltime) / leftrightDecaytime;

            pos += offset;

            this.gameObject.transform.localPosition = pos;
        }
        else
        {
            offset = new Vector3(0, 0, 0);
            this.gameObject.transform.localPosition = position;
            leftright = false;
        }
    }
}
