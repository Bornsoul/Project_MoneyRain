using UnityEngine;
using System.Collections;

/// <summary>
/// 카메라는 흔들어주는 클래스
/// </summary>
public class JHShake : MonoBehaviour
{
    float updownDecaytime = 0.0f;
    float leftrightDecaytime = 0.0f;
    Vector3 offset = new Vector3(0, 0, 0);
    Vector3 position;
    float startTime;
    float amp = 5.03f; // 진폭 . 상하로 이동하는 정도. 값이 클수록 더 크게 진동 .
    float freq = 10f; // 진동수. 초당 상하 운동 횟수. 값이 클수록 더 빨 진동 .
    float phase = 0f; // 위상 . 함수의 시작 포지션

    bool updown;
    bool leftright;

    // Use this for initialization
    void Start()
    {
        // StartUpDownShake(5.0f);
    }

    /// <summary>
    /// 흔듬을 멈춰주는 함수
    /// </summary>
    public void StopShake()
    {
        updown = false;
        leftright = false;
        this.gameObject.transform.localPosition = position;
        offset = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// 위아래로 흔들어 주는 함수
    /// </summary>
    /// <param name="decay"></param> 흔드는 시간
    public void StartUpDownShake(float decay)
    {
        startTime = Time.time;
        updownDecaytime = decay + Time.fixedTime - startTime;
        position = this.gameObject.transform.localPosition;
        updown = true;
      //  Debug.Log("Start");
    }

    /// <summary>
    /// 좌우/로 흔들어 주는 함수
    /// </summary>
    /// <param name="decay"></param> 흔드는 시간
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
/// 흔드는 함수를 업데이트하는 함수
/// </summary>
    public void FixedUpdate()
    {
        if (updown) UpDownShake();
        if (leftright) LeftRightShake();
    }

    /// <summary>
    /// 진폭과 진동수를 설정하는 함수
    /// </summary>
    /// <param name="_amp"></param> 진폭
    /// <param name="_freq"></param> 진동수
    public void SetOption(float _amp, float _freq) // 진폭, 진동수 
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
    /// 위 아래로 흔드는것을 업데이트하는 함수
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
    /// 좌우로 흔드는것을 업데이트하는 함수
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
