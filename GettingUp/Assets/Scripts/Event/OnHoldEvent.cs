using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnHoldEvent : MonoBehaviour,IPointerDownHandler,IPointerUpHandler 
{
	public static int i;

	
	public void OnPointerDown(PointerEventData eventData)
	{

		if (!GameManager.hasCheckedToday)
		{

		}
		else
		{
			StartCoroutine("StartCount");
		}
			
	}
	
	public void OnPointerUp(PointerEventData eventData)
	{
		if (GameManager.hasCheckedToday)
		{
			StopCount ();
		}
		else
		{
			GameManager.onCheck = true;
		}

	}

	IEnumerator StartCount(){
		for (i=0; i>-1; i++){
			yield return new WaitForSeconds(1);

		}
	}
	
	void StopCount(){
		
		if (i > 3 || i == 3){
			Debug.Log("3 Seconds Passed");
			GameManager.hasCheckedToday = false;
		}
		
		else{
			//Do your less than 3 seconds stuff here.
		}
		StopCoroutine("StartCount");
		i = 0;
	}
}
