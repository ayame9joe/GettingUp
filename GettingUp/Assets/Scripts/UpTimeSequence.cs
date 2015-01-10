using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UpTimeSequence : MonoBehaviour {


	LineRenderer lineRenderer;

	List<float> upTimeList = new List<float>();

	int upTimeHour;
	int upTimeSecond;
	int upTimeMinute;

	float upTime;

	bool isAdded = false;

	public Text txtUpTimeList;

	public Text dailyData;

	// Use this for initialization
	void Start () {
		txtUpTimeList.text = "";

		lineRenderer = this.GetComponent<LineRenderer> ();
		//设置材质
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		//设置颜色
		lineRenderer.SetColors(Color.red, Color.yellow);
		//设置宽度
		lineRenderer.SetWidth(0.02f, 0.02f);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (isAdded) {
			upTime = 0;
			isAdded = false;

		}

		for (int i = 0; i < upTimeList.Count - 1; i++) {
			lineRenderer.SetVertexCount(upTimeList.Count - 1);
			lineRenderer.SetPosition(i, new Vector3( -240 + i * 50, upTimeList[i] * 10, 0));


			
		}

	
	}

	public void AddToList ()
	{

		upTimeSecond = System.DateTime.Now.Second;
		upTimeMinute = System.DateTime.Now.Minute;
		upTimeHour = System.DateTime.Now.Hour;

		upTime = upTimeHour + (float) upTimeMinute / 60.0f + (float) upTimeSecond / 360;

		if (!isAdded)
		{
			upTimeList.Add (upTime);
			isAdded = true;
			txtUpTimeList.text+= upTimeList[upTimeList.Count - 1].ToString() + "\n";	
			Text t = GameObject.Instantiate(dailyData) as Text;
			t.transform.parent = GameObject.Find("DailyData").transform;
			t.transform.localPosition = new Vector3( -240 + (upTimeList.Count-1) * 50, -200);
			t.transform.localScale = new Vector3(1, 1, 1);
			t.text = (upTimeList.Count-1) + "Days";

		}




	}
}
