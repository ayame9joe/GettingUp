using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DataPanel : MonoBehaviour {

	int i = 0;
	public Text dailyData;
	LineRenderer lineRenderer;
	bool hasDrawDays = false;
	// Use this for initialization
	void Start () {

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

		for (int i = 0; i < GameManager.upTimeList.Count - 1; i++) {
			lineRenderer.SetVertexCount(GameManager.upTimeList.Count - 1);
			lineRenderer.SetPosition(i, new Vector3( -240 + i * 50, GameManager.upTimeList[i] * 10, 0));
			
			
			
		}

		if 	(!hasDrawDays)
		{
			foreach (float f in GameManager.upTimeList) {
				i++;
				Text t = GameObject.Instantiate(dailyData) as Text;
				t.transform.parent = GameObject.Find("DailyData").transform;
				t.transform.localPosition = new Vector3( -240 + (GameManager.upTimeList.Count-1) * 50, -200);
				t.transform.localScale = new Vector3(1, 1, 1);
				t.text = (GameManager.upTimeList.Count-1) + "Days";
				
				if (i == GameManager.upTimeList.Count )
				{
					hasDrawDays = true;
				}
			}

		}


	
	}
}
