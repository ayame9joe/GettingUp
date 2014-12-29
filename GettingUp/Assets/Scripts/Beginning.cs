using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Beginning : MonoBehaviour {

	public static int isFirstTimeOpened = 1;

	public GameObject startingPanel;
	public GameObject mainPanel;
	// Use this for initialization
	void Start () {
		if (isFirstTimeOpened == 1) {

			mainPanel.SetActive (false);
		}

	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	public void OnStart ()
	{
		mainPanel.SetActive (true);
		startingPanel.SetActive (false);
		isFirstTimeOpened = 0;	
	}
}
