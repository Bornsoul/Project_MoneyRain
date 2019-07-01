using UnityEngine;
using System.Collections;
/// <summary>
/// Allows dragging of the specified target object by mouse or touch, optionally limiting it to be within the UIPanel's clipped rectangle.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Drag Object")]
public class UIJoystick : MonoBehaviour {
    public UISprite m_pBack = null;
    public UISprite m_pCenter = null;
    Color m_pColor;

	static UIJoystick[] joysticks ; 					// A static collection of all joysticks
	static bool enumeratedJoysticks = false;
	
	/// <summary>
	/// Target object that will be dragged.
	/// </summary>
	
	public Transform target;
	public Vector3 scale = Vector3.one;
	public float radius = 40f;								// the radius for the joystick to move
	internal bool mPressed = false;
    internal E_CSDIRECT m_eDirect;

	public bool centerOnPress = true; 
	Vector3 userInitTouchPos;
	
	//Joystick vars
	public int tapCount;	
	public bool normalize = false; 							// Normalize output after the dead-zone?
	public Vector2 position; 								// [-1, 1] in x,y
	public float deadZone = 2f;								// Control when position is output
	public float fadeOutAlpha = 0.2f;
	public float fadeOutDelay = 1f;	
	public UIWidget[] widgetsToFade;						// UIWidgets that should fadeIn/Out when centerOnPress = true
	public Transform[] widgetsToCenter;						// GameObjects to Center under users thumb when centerOnPress = true
	private float lastTapTime = 0f;
	public float doubleTapTimeWindow = 0.5f;				// time in Seconds to recognize a double tab
	public GameObject doubleTapMessageTarget;
	public string doubleTabMethodeName;
	
	void Awake() {
		userInitTouchPos = Vector3.zero;
	
	}
	
	void Start () {
        m_pColor = Color.white;
        m_pColor.a = 0.0f;
        m_pBack.color = m_pColor;
        m_pCenter.color = m_pColor;
        m_eDirect = E_CSDIRECT.E_CENTER;
		if (centerOnPress) {
			
			StartCoroutine("fadeOutJoystick");
		}
	}
	
    IEnumerator Cor_On()
    {
        StopCoroutine("Cor_Off");
        m_pColor.a = 0.0f;
        for (float i = 0.0f; i < 1.0f;i+=Time.deltaTime)
        {
            m_pColor.a += 7.0f * Time.deltaTime;
            m_pBack.color = m_pColor;
            m_pCenter.color = m_pColor;
            m_pBack.transform.localScale = Vector3.Lerp(m_pBack.transform.localScale, Vector3.one, 10.0f * Time.deltaTime);
            if (m_pColor.a >= 0.7f) break;
            yield return null;
        }
            yield return null;
    }

    IEnumerator Cor_Off()
    {
        StopCoroutine("Cor_On");
        
        for (float i = 0.0f; i < 1.0f; i += Time.deltaTime)
        {
            m_pColor.a -= 7.0f * Time.deltaTime;
            m_pBack.color = m_pColor;
            m_pCenter.color = m_pColor;
            m_pBack.transform.localScale = Vector3.Lerp(m_pBack.transform.localScale, Vector3.zero, 10.0f * Time.deltaTime);
            if (m_pColor.a <= 0.0f) break;
            yield return null;
        }
        yield return null;
    }

    public void GetOutOff()
    {
        m_pColor.a = 0.0f;
        m_pBack.color = m_pColor;
        m_pCenter.color = m_pColor;
        mPressed = false;
    }

	IEnumerator fadeOutJoystick() {
        
        yield return new WaitForSeconds (fadeOutDelay);
		foreach(UIWidget widget in widgetsToFade) {
			Color lastColor = widget.color;
			Color newColor = lastColor;
			newColor.a = fadeOutAlpha;
			TweenColor.Begin(widget.gameObject, 0.5f, newColor).method = UITweener.Method.EaseOut;
		}	
    }
	


	/// <summary>
	/// Create a plane on which we will be performing the dragging.
	/// </summary>

	public void OnPress2 (bool pressed) {
		if (target != null) {
			mPressed = pressed;

			if (pressed) {
				//StopAllCoroutines();
                StopCoroutine("fadeOutJoystick");
				if (Time.time < lastTapTime +doubleTapTimeWindow) {
					
					if (doubleTapMessageTarget != null && doubleTabMethodeName != "") {
						doubleTapMessageTarget.SendMessage(doubleTabMethodeName,SendMessageOptions.DontRequireReceiver);
						 tapCount ++;                                
					} else {
						Debug.LogWarning("Double Tab on Joystick but no Reciever or Methodename available");
					}
				} else {
					tapCount = 1; 
				}
				lastTapTime = Time.time;
				//set Joystick to fingertouchposition
				//Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.lastTouchPosition);
                Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.GetTouch(0).pos);
				float dist = 0f;
				
				Vector3 currentPos = ray.GetPoint(dist);
				currentPos.z = 0;
				if (centerOnPress) {
					userInitTouchPos = currentPos;
					foreach(UIWidget widget in widgetsToFade) {
                        TweenColor.Begin(widget.gameObject, 0.1f, Color.white).method = UITweener.Method.EaseIn;
					}
					foreach(Transform widgetTF in widgetsToCenter) {
						widgetTF.position = userInitTouchPos;
					}
				} else {
					userInitTouchPos = target.position;
					OnDrag(Vector2.zero);
				}
				
			} else {
				ResetJoystick ();
			}
		}
	}

	/// <summary>
	/// Drag the object along the plane.
	/// </summary>

	void OnDrag (Vector2 delta)
	{
		//Debug.Log("delta " +  delta + " delta.magnitude = " + delta.magnitude);

	//	Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.lastTouchPosition);
        Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.GetTouch(0).pos);
		float dist = 0f;
		
		Vector3 currentPos = ray.GetPoint(dist);
		Vector3 offset = currentPos - userInitTouchPos;

		if (offset.x != 0f || offset.y != 0f) {
			offset = target.InverseTransformDirection(offset);
			offset.Scale(scale);
			offset = target.TransformDirection(offset);

            offset.y = 0.0f;
			offset.z = 0f;
		}
      if(offset.x>0.0f)
      {
          m_eDirect = E_CSDIRECT.E_RIGHT;
      }
      else if(offset.x<0.0f)
      {
          m_eDirect = E_CSDIRECT.E_LEFT;
      }
      else
      {
          m_eDirect = E_CSDIRECT.E_CENTER;
      }


		target.position = userInitTouchPos +offset;

		Vector3 zeroZpos = target.position;
		zeroZpos.z = 0f;
		target.position = zeroZpos;
		// Calculate the length. This involves a squareroot operation,
		// so it's slightly expensive. We re-use this length for multiple
		// things below to avoid doing the square-root more than one.

		float length = target.localPosition.magnitude;
		
		if (length < deadZone) {
			// If the length of the vector is smaller than the deadZone radius,
			// set the position to the origin.
			position = Vector2.zero;
			target.localPosition = position;
		} else if (length > radius) {
			target.localPosition = Vector3.ClampMagnitude(target.localPosition, radius);
			position = target.localPosition;
		}
		
		
		if (normalize) {
			// Normalize the vector and multiply it with the length adjusted
			// to compensate for the deadZone radius.
			// This prevents the position from snapping from zero to the deadZone radius.
			position = position / radius * Mathf.InverseLerp (radius, deadZone, 1);
		}
	}



	/// <summary>
	/// Apply the dragging momentum.
	/// </summary>

	void Update () {
        if (JHGameData_Mng.I._GSencer == false) return;
        if (JHGameData_Mng.I._GamePause == true) return;
        if (JHGameData_Mng.I._GameCycle == true) return;


        if(Input.GetMouseButtonDown(0))
        {
            if(mPressed==false)
            {
                
                Vector3 st = JHGame_Scene.I._UILayer.m_pCamera.ScreenToWorldPoint(Input.mousePosition);
                if (st.y >= 0.75f) return;
                StartCoroutine("Cor_On");
                transform.position = st;
                OnPress2(true);
               
            }
        }
        if (mPressed==true)
        {
            OnDrag(Vector2.zero);
        }
        if(Input.GetMouseButtonUp(0))
        {
            m_eDirect = E_CSDIRECT.E_CENTER;
            StartCoroutine("Cor_Off");
            OnPress2(false);
        }

		/*if (!enumeratedJoysticks) {
			// Collect all joysticks in the game, so we can relay finger latching messages
			joysticks = FindObjectsOfType(typeof(UIJoystick)) as UIJoystick[];
			
			enumeratedJoysticks = true;
		}*/
	}
	
	void ResetJoystick () {
		// Release the finger control and set the joystick back to the default position
		tapCount = 0;
		position = Vector2.zero;
		target.position = userInitTouchPos;
		if (centerOnPress) {
			StartCoroutine("fadeOutJoystick");
		}
	}
	
	public void Disable () {
		gameObject.SetActive(false);
		enumeratedJoysticks = false;
	}
}

