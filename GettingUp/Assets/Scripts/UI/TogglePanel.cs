using UnityEngine;
using System.Collections;

public class TogglePanel : MonoBehaviour {

	public static int isFirstTimeOpened = 1;

	public GameObject startingPanel;
	public GameObject mainPanel;
	public GameObject howToPlayPanel;
	public GameObject settingPanel;
	public GameObject storePanel;
	public GameObject dataPanel;

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


	public void OnHowToPlay ()
	{
		mainPanel.SetActive (false);
		startingPanel.SetActive (false);
		storePanel.SetActive (false);
		settingPanel.SetActive (false);
		howToPlayPanel.SetActive (true);
	}
	
	public void OnBackToHome ()
	{
		mainPanel.SetActive (true);
		startingPanel.SetActive (false);
		storePanel.SetActive (false);
		settingPanel.SetActive (false);
		howToPlayPanel.SetActive (false);
	}
	
	public void OnSetting ()
	{
		mainPanel.SetActive (false);
		startingPanel.SetActive (false);
		storePanel.SetActive (false);
		settingPanel.SetActive (true);
		howToPlayPanel.SetActive (false);
	}
	
	public void OnStore ()
	{
		mainPanel.SetActive (false);
		startingPanel.SetActive (false);
		storePanel.SetActive (true);
		settingPanel.SetActive (false);
		howToPlayPanel.SetActive (false);
	}

	public void OnShowData ()
	{
		dataPanel.SetActive(true);
	}
}
