using UnityEngine;
using System.Collections;

public class CSLayer : MonoBehaviour
{
    public UIRoot m_pRoot = null;
    public UIPanel m_pPanel = null;
    public GameObject m_pAnchor = null;
    public Camera m_pCamera = null;
    public UICamera m_pUICamera = null;


    private float m_fAlpha = 1.0f;
    private int m_nDepth = 0;
    private int m_nSwitchNum = -1;
    private LayerMask m_pLayerMast;
   
    // Use this for initialization
    void Awake()
    {



    }

    void Start()
    {
        
    }

    public void Enter(LayerMask pCullLayer, int nDepth, int nSwitchNum)
    {
        m_pLayerMast = pCullLayer;
        transform.gameObject.layer = pCullLayer; 
        m_pCamera.cullingMask = (1 << LayerMask.NameToLayer("TransparentFX")) | (1 << pCullLayer);
        m_pUICamera.eventReceiverMask = (1 << LayerMask.NameToLayer("TransparentFX")) | (1 << pCullLayer);
        m_pPanel.depth = nDepth;
        m_pCamera.depth = nDepth;

        m_nSwitchNum = nSwitchNum;
       
    }

    void OnDestroy()
    {

    }

    public LayerMask GetLayerMast()
    {
        return m_pLayerMast;
    }
	
	
	// Update is called once per frame
	void Update () {
       
	}
}
