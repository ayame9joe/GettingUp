using UnityEngine;
using System.Collections;

public class AndroidTouch : MonoBehaviour {

	// 记录手指触屏的位置  
	Vector2 m_screenpos = new Vector2();  
	
	// Use this for initialization  
	void Start () { 
		
		// 允许多点触屏  
		Input.multiTouchEnabled = true;  

	}
	
	// Update is called once per frame
	void Update () {

		#if !UNITY_EDITOR && ( UNITY_IOS || UNITY_ANDROID )  
		MobileInput();   
		#else  
		DesktopInput();  
		#endif  
	
	}

	void MobileInput() 
	{ 
				if (Input.touchCount <= 0) 
						return;  
		
				if (Input.touchCount == 1) { // 1个手指触摸屏幕 
						// 开始触屏  
						if (Input.touches [0].phase == TouchPhase.Began) { 
								// 记录手指触屏的位置  
								m_screenpos = Input.touches [0].position;  
						} 
			// 手指移动  
			else if (Input.touches [0].phase == TouchPhase.Moved) { 
								//移动摄像机  
								Camera.main.transform.Translate (new Vector3 (Input.touches [0].deltaPosition.x * Time.deltaTime, Input.touches [0].deltaPosition.y * Time.deltaTime, 0));  
						} 
			
						// 手指离开屏幕  
						if (Input.touches [0].phase == TouchPhase.Ended && 
								Input.touches [0].phase != TouchPhase.Canceled) { 
								Vector2 pos = Input.touches [0].position;  
				
								// 手指水平移动  
								if (Mathf.Abs (m_screenpos.x - pos.x) > Mathf.Abs (m_screenpos.y - pos.y)) { 
										if (m_screenpos.x > pos.x) { 
												//手指向左划动  
										} else { 
												//手指向右划动  
										} 
								} else {   // 手指垂直移动 
										if (m_screenpos.y > pos.y) { 
												//手指向下划动  
										} else { 
												//手指向上划动  
										} 
								} 
						} 
				}
		}

		void DesktopInput() 
		{ 
			// 记录鼠标左键的移动距离  
			float mx = Input.GetAxis("Mouse X");  
			float my = Input.GetAxis("Mouse Y");  
			
			if (  mx!= 0 || my !=0 ) 
			{ 
				//鼠标左键  
				if (Input.GetMouseButton(0)) 
				{ 
					Camera.main.transform.Translate(new Vector3(mx*Time.deltaTime, my * Time.deltaTime, 0));  
				} 
			} 
		}
}
