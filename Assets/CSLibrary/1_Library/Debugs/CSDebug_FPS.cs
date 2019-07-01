using UnityEngine;
using System.Collections;

public class CSDebug_FPS : MonoBehaviour {

    public UILabel ui_Label = null;

    public float m_fInterval = 0.5f;

    private float accum = 0;
    private int frames = 0;
    private float timeleft;
    float fps;

    void Start()
    {
     
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        if (timeleft <= 0.0)
        {
            fps = accum / frames;

            timeleft = m_fInterval;
            accum = 0.0f;
            frames = 0;
        }

        ui_Label.text = "FPS: " + fps;
    }
}
