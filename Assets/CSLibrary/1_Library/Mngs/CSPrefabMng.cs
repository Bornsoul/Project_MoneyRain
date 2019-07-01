using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public enum E_RESOURCETYPE
{
    E_PERMANENT = 0,
    E_ONEC,

    E_MAX,
}


public class CSPrefabMng : CSSingleton<CSPrefabMng> {

    public List<GameObject> m_pPermanent_PrefabList = null; //! 전체 프리펩 저장 리스트(영구적)

    public List<GameObject> m_pOnec_Prefab = null; //! 씬에서 저장되는 프리펩 리스트(씬에서만 한정)

    public Dictionary<string, GameObject> m_pLive_PrefabList = null; //! 현재 살아있는 프리펩 리스트

	
	void Awake () {
        DontDestroyOnLoad(this);
        m_pPermanent_PrefabList = new List<GameObject>();

        m_pOnec_Prefab = new List<GameObject>();

        m_pLive_PrefabList = new Dictionary<string, GameObject>();

	}

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy()/CSPrefabMng.cs");

        m_pLive_PrefabList.Clear();
        m_pLive_PrefabList = null;

        m_pOnec_Prefab.Clear();
        m_pOnec_Prefab = null;

        m_pPermanent_PrefabList.Clear();
        m_pPermanent_PrefabList = null;
    }
	
	
    public GameObject CreatePrefab(GameObject pParent, string sPrefabName, string sTitleName)
    {
        if(pParent==null)
        {
            #if _debug
            Debug.Log("CSPrefabMng_Exception : " + pParent.name + "is Null");
            #endif
            return null;
        }
        if (CheckOverLapObject(sPrefabName, sTitleName) == true)
        {
            string sObjectName = null;
            if (sTitleName == "")
                sObjectName = sPrefabName;
            else
                sObjectName = sTitleName;
            #if _debug
            Debug.Log("CSPrefabMng_Exception : " + sObjectName + "is OverLapObject");
            #endif
            return null;
        }

        List<GameObject> pUseResourceList = GetReousrceList(sPrefabName, E_SCENE.E_MAX);

        if (pUseResourceList == null)
        {
            #if _debug
            Debug.Log(" NULL Resource [CreatePrefab/CSPrefabMng.cs]");
            #endif
            return null;
        }

        foreach(GameObject pObj in pUseResourceList)
        {
            if(pObj.transform.name==sPrefabName)
            {
                GameObject pCreatePrefab = Instantiate(pObj) as GameObject;
                if (sTitleName != "")
                    pCreatePrefab.transform.name = sTitleName + "(Clone)";

                pCreatePrefab.transform.parent = pParent.transform;
                pCreatePrefab.transform.localPosition = pObj.transform.position;
                pCreatePrefab.transform.localScale = pObj.transform.localScale;

                m_pLive_PrefabList.Add(pCreatePrefab.transform.name, pCreatePrefab);
                return pCreatePrefab;
            }
        }
        #if _debug
            Debug.Log(" No Find PrefabName ->" + sPrefabName + " [CreatePrefab/CSPrefabMng.cs]");
        #endif
        return null;
    }


    public GameObject CreatePrefab(string sParentName, string sPrefabName, string sTitleName)
    {
        GameObject pParent = GameObject.Find(sParentName);
        if (pParent == null)
        {
#if _debug
            Debug.Log("CSPrefabMng_Exception : " + pParent.name + "is Null");
#endif
            return null;
        }
       
        if (CheckOverLapObject(sPrefabName, sTitleName) == true)
        {
            string sObjectName = null;
            if (sTitleName == "")
                sObjectName = sPrefabName;
            else
                sObjectName = sTitleName;
#if _debug
            Debug.Log("CSPrefabMng_Exception : " + sObjectName + "is OverLapObject");
#endif
            return null;
        }

        List<GameObject> pUseResourceList = GetReousrceList(sPrefabName, E_SCENE.E_MAX);

        if (pUseResourceList == null)
        {
#if _debug
            Debug.Log(" NULL Resource [CreatePrefab/CSPrefabMng.cs]");
#endif
            return null;
        }

        foreach (GameObject pObj in pUseResourceList)
        {
            if (pObj.transform.name == sPrefabName)
            {
                GameObject pCreatePrefab = Instantiate(pObj) as GameObject;
                if (sTitleName != "")
                    pCreatePrefab.transform.name = sTitleName + "(Clone)";

                pCreatePrefab.transform.parent = pParent.transform;
                pCreatePrefab.transform.localPosition = pObj.transform.position;
                pCreatePrefab.transform.localScale = pObj.transform.localScale;

                m_pLive_PrefabList.Add(pCreatePrefab.transform.name, pCreatePrefab);
                return pCreatePrefab;
            }
        }
#if _debug
            Debug.Log(" No Find PrefabName ->" + sPrefabName + " [CreatePrefab/CSPrefabMng.cs]");
#endif
        return null;
    }


    public GameObject CreatePrefab(string sPrefabName, string sTitleName)
    {
       
        if (CheckOverLapObject(sPrefabName, sTitleName) == true)
        {
            string sObjectName = null;
            if (sTitleName == "")
                sObjectName = sPrefabName;
            else
                sObjectName = sTitleName;
#if _debug
            Debug.Log("CSPrefabMng_Exception : " + sObjectName + "is OverLapObject");
#endif
            return null;
        }

        List<GameObject> pUseResourceList = GetReousrceList(sPrefabName, E_SCENE.E_MAX);

        if (pUseResourceList == null)
        {
#if _debug
            Debug.Log(" NULL Resource [CreatePrefab/CSPrefabMng.cs]");
#endif
            return null;
        }

        foreach (GameObject pObj in pUseResourceList)
        {
            if (pObj.transform.name == sPrefabName)
            {
                GameObject pCreatePrefab = Instantiate(pObj) as GameObject;
                if (sTitleName != "")
                    pCreatePrefab.transform.name = sTitleName + "(Clone)";

                pCreatePrefab.transform.localPosition = pObj.transform.position;
                pCreatePrefab.transform.localScale = pObj.transform.localScale;

                m_pLive_PrefabList.Add(pCreatePrefab.transform.name, pCreatePrefab);
                
                return pCreatePrefab;
            }
        }
#if _debug
            Debug.Log(" No Find PrefabName ->" + sPrefabName + " [CreatePrefab/CSPrefabMng.cs]");
#endif
        return null;
    }


    /// <summary>
    /// 리소스 로드
    /// </summary>
    /// <param name="sPrefabName"></param>
    /// <param name="eResourceLoadPos"></param>
    /// <returns></returns>
    public List<GameObject> GetReousrceList(string sPrefabName, E_SCENE eResourceLoadPos)
    {
        List<GameObject> pResource = null;

        string sSceneName = GetSceneName(eResourceLoadPos.ToString());


        //pResource = GetOnecResourceList();
        pResource = m_pOnec_Prefab;
        if (pResource==null)
        {
            Debug.Log("CSPrefabMng_Exception :Scene is Null");
            return null;
        }

        return ResourceLoad(sSceneName, sPrefabName, pResource);
        
    }


    /// <summary>
    /// 메모리상으로 리소스를 로딩
    /// </summary>
    /// <param name="sCurSceneName"></param>
    /// <param name="sPrefabName"></param>
    /// <param name="pList"></param>
    List<GameObject> ResourceLoad(string sCurSceneName, string sPrefabName, List<GameObject> pList)
    {
        foreach (GameObject obj in m_pOnec_Prefab)   //! 해당(씬) 리스트에 이미 프리펩이 있을 경우 리소스를 로드하지 않는다.
        {
            if (obj.transform.name == sPrefabName)
            {
                return pList;
            }
        }
        foreach (GameObject obj in m_pPermanent_PrefabList)   //! 전체 리스트에 이미 프리펩이 있을 경우 리소스를 로드하지 않는다.
        {
            if (obj.transform.name == sPrefabName)
            {
                return m_pPermanent_PrefabList;
            }
        }
 
       // string sPath = sCurSceneName + "/1_Prefabs/" + sPrefabName;
        string sPath = "1_Prefabs/" + sPrefabName;
        //Debug.Log(sPath);
        GameObject ResourcePrefabGame = (GameObject)Resources.Load(sPath, typeof(GameObject));
        if (ResourcePrefabGame == null)
        {
            #if _debug
            Debug.Log("CSPrefabMng_Exception : " + sPath + " is Null Prefab");
            #endif
            return null;
        }

        ResourcePrefabGame.name = sPrefabName;

        if (ResourcePrefabGame != null)
        {
            pList.Add(ResourcePrefabGame);
            return pList;
        }
        else
        {
            #if _debug
            Debug.Log(sPath + "<---  failed Resource.load [ResourceLoad/CSPrefabMng.cs]");
            #endif
            return null;
        }
    }



    //@@@@@@@@@@@@@@@ 폴더 불러오기 실패.."
   /* IEnumerator Cor_Load(string sPath)
    {
        string filePath = "jar:file://" + Application.dataPath + "!/assets/1_Prefabs.unity3d";
        CSDirector.I.GetDeviceDebug().Log(filePath);

        using (WWW asset = WWW.LoadFromCacheOrDownload(filePath, 1))
        {
            yield return asset;
            if (asset.error != null)
            {
                CSDirector.I.GetDeviceDebug().Log("ERROR");
            }
            else
            {
                CSDirector.I.GetDeviceDebug().Log("LOAD " + asset.text);
            }
        }
      
        yield return null;
    }
    */
    /// <summary>
    /// 1_Prefabs/이후 경로 폴더를 넣으면 그 폴더안에있는 모든 리소스를 저장리스트에 저장한다.
    /// 영구 저장리스트(m_pPermanent_PrefabList)에 넣을 경우 게임시작시 넣는것이 좋다. 게임중간에 넣으면 m_pOnec_Prefab와 중복되어 문제 생길 수 있음
    /// </summary>
    /// <param name="eType"></param> 저장할 리스트 타입(영구, 씬 일회용)
    /// <param name="sName"></param> 1_Prefabs/이후 경로(.prefab뺴도됨)
 /*   public void ResourceLoadinList_Folder(E_RESOURCETYPE eType, string sName)
    {
        string sPath = "Assets/Resources/1_Prefabs/" + sName;
#if !UNITY_EDITOR
        sPath = Application.streamingAssetsPath;
#endif
        
        //  StartCoroutine(Cor_Load(sPath));
        CSDirector.I.GetDeviceDebug().Log("Preffab-" + sPath);
        DirectoryInfo pDir = new DirectoryInfo(sPath);
        FileInfo[] pInfo = pDir.GetFiles("*.prefab", SearchOption.AllDirectories);
        CSDirector.I.GetDeviceDebug().Log("YES " + pInfo.Length.ToString());
        string sLoadName = "";
        int nStartNum = -1;
        string sLoadName_Filter = "";
        foreach (FileInfo pFile in pInfo)
        {
            CSDirector.I.GetDeviceDebug().Log(pFile.FullName);
            sLoadName = pFile.FullName;
            nStartNum = sLoadName.IndexOf("1_Prefabs");
            sLoadName_Filter = sLoadName.Substring(nStartNum+10);
            sLoadName_Filter= sLoadName_Filter.Replace("\\", "/");
            sLoadName_Filter = sLoadName_Filter.Substring(0, sLoadName_Filter.IndexOf(".prefab"));
            CSDirector.I.GetDeviceDebug().Log(sLoadName_Filter);
            if (eType==E_RESOURCETYPE.E_PERMANENT)
            {
                ResourceLoad("", sLoadName_Filter, m_pPermanent_PrefabList);
            }
            else
            {
                ResourceLoad("", sLoadName_Filter, m_pOnec_Prefab);
            }
        }
    }*/

    /// <summary>
    /// 1_Prefabs/이후 경로 폴더를 넣으면 그 폴더안에있는 모든 리소스를 저장리스트에 저장한다.
    /// 영구 저장리스트(m_pPermanent_PrefabList)에 넣을 경우 게임시작시 넣는것이 좋다. 게임중간에 넣으면 m_pOnec_Prefab와 중복되어 문제 생길 수 있음
    /// </summary>
    /// <param name="eType"></param> 저장할 리스트 타입(영구, 씬 일회용)
    /// <param name="sName"></param> 1_Prefabs/이후 경로(.prefab뺴도됨)
    public void ResourceLoadinList_Folder(E_RESOURCETYPE eType, string sPath)
    {
        string sPathIn = "1_Prefabs/" + sPath;
        GameObject[] pObject = Resources.LoadAll<GameObject>(sPathIn);
        foreach(GameObject pObj in pObject)
        {
            m_pPermanent_PrefabList.Add(pObj);
          //  CSDirector.I.GetDeviceDebug().Log(pObj.name);
        }


    }

    /// <summary>
    /// 해당 프리펩은 리소스 리스트에 저장한다
    /// 영구 저장리스트(m_pPermanent_PrefabList)에 넣을 경우 게임시작시 넣는것이 좋다. 게임중간에 넣으면 m_pOnec_Prefab와 중복되어 문제 생길 수 있음
    /// </summary>
    /// <param name="eType"></param> 저장할 리스트 타입(영구, 씬 일회용)
    /// <param name="sName"></param> 1_Prefabs/이후 경로(.prefab뺴도됨)
    public void ResourceLoadinList(E_RESOURCETYPE eType, string sName)
    {
        if (eType == E_RESOURCETYPE.E_PERMANENT)
        {
            ResourceLoad("", sName, m_pPermanent_PrefabList);
        }
        else
        {
            ResourceLoad("", sName, m_pOnec_Prefab);
        }
    }



    /// <summary>
    /// 프리펩 제거
    /// </summary>
    /// <param name="sName"></param>
    public void DestroyPrefab(string sName)
    {
        if (m_pLive_PrefabList == null)
        {
            return;
        }

        if (m_pLive_PrefabList.Count == 0)
        {
#if _debug
            Debug.Log("CreatePrefabList is Zero [DestroyPrefab/HPrefabMng.cs]");
#endif
            return;
        }

        if (m_pLive_PrefabList.ContainsKey(sName))
        {
            Destroy(m_pLive_PrefabList[sName]);
            m_pLive_PrefabList[sName] = null;
            m_pLive_PrefabList.Remove(sName);
        }
    }

    /// <summary>
    /// 프리펩 제거
    /// </summary>
    /// <param name="pObj"></param>
    public void DestroyPrefab(GameObject pObj)
    {
        if (pObj == null) return;
        string sDeleteName = pObj.name;
        if (m_pLive_PrefabList == null)
        {
            return;
        }

        if (m_pLive_PrefabList.Count == 0)
        {
#if _debug
            Debug.Log("CreatePrefabList is Zero [DestroyPrefab/HPrefabMng.cs]");
#endif
            return;
        }

        if (m_pLive_PrefabList.ContainsKey(sDeleteName))
        {
            Destroy(m_pLive_PrefabList[sDeleteName]);
            m_pLive_PrefabList[sDeleteName] = null;
            m_pLive_PrefabList.Remove(sDeleteName);
            pObj = null;
        }
    }

    /// <summary>
    /// 살아있는 모든 프리펩을 제거
    /// </summary>
    public void DestroyAllPrefabs()
    {
        if (m_pLive_PrefabList == null)
            return;

        if (m_pLive_PrefabList.Count == 0)
        {
#if _debug
            Debug.Log("CreatePrefabsList.Count == 0   [DestroyPrefab/HPrefabMng.cs]");            
#endif
            return;
        }

        foreach (GameObject obj in m_pLive_PrefabList.Values)
        {
            if (obj != null)
            {
                Destroy(obj);
                continue;
            }
        }

        m_pLive_PrefabList.Clear();

        System.GC.Collect();
        Resources.UnloadUnusedAssets();       //! 메모리 삭제하는건데 한 번 주석걸어봄
    }

    /// <summary>
    /// 씬에서 저장되는 리소스 저장소 제거(씬넘어갈때 호출해야함)
    /// </summary>
    public void DestroyOncePrefabs()
    {
        if (CSLayerMng.I!=null)
        {
            CSLayerMng.I.DestroyList();
        }
        
        m_pOnec_Prefab.Clear();
    }


    /// <summary>
    /// 현재 씬에 있는 리소스 저장 리스트를 가져오는 함수
    /// </summary>
    /// <returns></returns>
   /* List<GameObject> GetOnecResourceList()
    {
        GameObject pObj = GameObject.Find("_Scene");
        CSScene pScene = pObj.GetComponent<CSScene>();

        if(pScene==null)
        {
            #if _debug
            Debug.Log("CSPrefabMng_Exception : SceneResourceList is Null");
            #endif
            return null;
        }
        return pScene.m_pResourceList;
    }*/

    /// <summary>
    /// 씬이름에 맞게 string만들기
    /// </summary>
    /// <param name="sName"></param>
    /// <returns></returns>
    public string GetSceneName(string sName)
    {
        if (sName == string.Empty) return null;

        int nStartNum = sName.IndexOf("_");
        string sSceneName = sName.Substring(nStartNum + 1);
        return sSceneName;
    }


    /// <summary>
    /// 살아있는 프리펩중에서 해당 이름을 가진 프리펩이 있는지 검사(중복검사)
    /// </summary>
    /// <param name="sPrefabName"></param>
    /// <param name="sTitleName"></param>
    /// <returns></returns> 중복이면 true
    bool CheckOverLapObject(string sPrefabName, string sTitleName)
    {
        string sObjectName = null;
        if (sTitleName == "")
            sObjectName = sPrefabName + "(Clone)";
        else
            sObjectName = sTitleName + "(Clone)";

        return m_pLive_PrefabList.ContainsKey(sObjectName);
    }

    
}
