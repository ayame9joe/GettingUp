using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


	public Text txtUpTime;
	public Text txtExp;
	public Text txtCombo;
	public Text txtBorderOfCombo;
	public Text txtLevel;
	public Text txtCoin;
	public Slider sldBorderOfCombo;
	public Text txtGetUp;


	public Image imgLv;
	public Image imgExp;

	public GameObject startingPanel;
	public GameObject mainPanel;
	public GameObject howToPlayPanel;
	public GameObject settingPanel;
	public GameObject storePanel;

	public static bool hasCheckedToday = false;

	float upTime;
	float exp;
	float nextLevelExp;
	int level = 1;
	public static int coin;

	int originalCoin;
	float originalExp;

	int borderOfExp = 12;
	float borderOfCombo;
	float comboIndex;

	float autoBorderOfCombo;

	int combo;

	bool isAlive = true;

	float[] upTimeSequence;
	int days = 1;
	float upTimeSecond;
	float upTimeMinute;
	// Use this for initialization
	void Start () {
		sldBorderOfCombo.minValue = 4;
		sldBorderOfCombo.maxValue = 10;

		exp = PlayerPrefs.GetFloat("mExp");
		coin = PlayerPrefs.GetInt("mCoin");
		combo = PlayerPrefs.GetInt("mCombo");
		upTime = PlayerPrefs.GetFloat("mUpTime");
		Beginning.isFirstTimeOpened = PlayerPrefs.GetInt("mIsFirstTimeOpened");

		/*if (upTime != 0) {
			hasCheckedToday = true;		
		}*/
		upTimeSequence = new float[days];

	
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log (hasCheckedToday);
		if (hasCheckedToday) {
			txtGetUp.text = "Hold " + (3 - OnHoldEvent.i) + " Seconds to Reset";	
		}

		if (OnHoldEvent.i == 3) {
			txtGetUp.text = "Get Up!";	
		}
		for (int i = 0; i < upTimeSequence.Length - 1; i++)
		{
			Debug.Log (upTimeSequence[i]);
		}

		if (upTime == 0) {
			txtUpTime.text = System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + ":" + System.DateTime.Now.Second.ToString();		
		}
		else
		{
			txtUpTime.text = Mathf.Floor(upTime).ToString() + ":" + upTimeMinute.ToString() + ":" + upTimeSecond.ToString();
		}
		//txtUpTime.text = upTime.ToString();
		txtExp.text = "Exp: " + exp.ToString ();
		txtCombo.text = "Combo: " + combo.ToString ();
		txtLevel.text = "Lv " + level.ToString ();
		txtCoin.text = "×  " + coin.ToString ();
		txtBorderOfCombo.text = "Border of Combo: " + sldBorderOfCombo.value.ToString ();

		PlayerPrefs.SetFloat ("mExp", exp);
		PlayerPrefs.SetInt ("mCoin", coin);
		PlayerPrefs.SetInt ("mCombo", combo);
		PlayerPrefs.SetFloat ("mUpTime", upTime);
		PlayerPrefs.SetInt ("mIsFirstTimeOpened", Beginning.isFirstTimeOpened);


		if (borderOfCombo == sldBorderOfCombo.value) {
		
			txtBorderOfCombo.text = "Set Combo Val as " + borderOfCombo.ToString ();
		}


		//Debug.Log (upTime);
		if (System.DateTime.Now.Hour == 0) {
			hasCheckedToday = false;
			upTime = 0;
			upTimeMinute = 0;
			upTimeSecond = 0;
			days++;
		}

		nextLevelExp = 10 * (5 * level + 0.7f * level * level);

		exp = 10;
		Debug.Log (imgExp.rectTransform.sizeDelta.x);


		imgExp.rectTransform.sizeDelta = new Vector2(exp * 100 / nextLevelExp, imgExp.rectTransform.sizeDelta.y);
		if (exp == nextLevelExp) {
			level++;		
		}

		/*switch (borderOfCombo) {
		case 4.0f:
			comboIndex = 1.7f;
			break;
		case 5.0f:
			comboIndex = 1.5f;
			break;
		case 6.0f:
			comboIndex = 1.3f;
			break;
		case 7.0f:
			comboIndex = 1.2f;
			break;
		case 8.0f:
			comboIndex = 1.1f;
			break;
		case 9.0f:
			comboIndex = 1.05f;
			break;
		}*/

		if (borderOfCombo < 5) {
			comboIndex = 1.5f;	
		}
		else if (borderOfCombo < 6) {
			comboIndex = 1.3f;	
		}
		else if (borderOfCombo < 7) {
			comboIndex = 1.2f;	
		}
		else if (borderOfCombo < 8) {
			comboIndex = 1.1f;	
		}
		else if (borderOfCombo < 9) {
			comboIndex = 1.05f;	
		}



	
	}

	public void OnCheck ()
	{
		if (!hasCheckedToday)
		{

			upTimeSecond = System.DateTime.Now.Second;
			upTimeMinute = System.DateTime.Now.Minute;
			upTime =  System.DateTime.Now.Hour + (float) upTimeMinute / 60.0f + (float) upTimeSecond / 360;
			upTimeSequence[days - 1] = upTime;
			originalExp = exp;
			originalCoin = coin;
			if (upTime > autoBorderOfCombo)
			{
				autoBorderOfCombo = Mathf.Ceil(upTime);
			}
			else {
				autoBorderOfCombo = autoBorderOfCombo;
			}

			if (borderOfExp > upTime)
			{
				exp += (borderOfExp - upTime);
				coin += ((int)Mathf.Floor(borderOfExp - upTime) + 1) * level ;
				exp = exp * (1 + (float)combo / 10.0f) * comboIndex;
			}
			else
			{
				exp += 0;
				coin += level;
				//isAlive = false;
			}
		
			if (upTime > borderOfCombo)
			{
				combo = 0;
			}
			else {
				combo++;
			}
			hasCheckedToday = true;
		}
		else
		{
			GameManager.hasCheckedToday = false;
			exp = originalExp;
			coin = originalCoin;
			upTime = 0;
			upTimeSecond = 0;
			upTimeMinute = 0;
			txtGetUp.text = "Get Up!";
		}
	}

	public void OnCheckCanceling ()
	{

	}

	public void OnSettingUpComboValue ()
	{
		borderOfCombo = sldBorderOfCombo.value;

	}

	public void OnReset()
	{
		exp = 0;
		coin = 0;
		level = 1;
		combo = 0;
		upTime = 0;
		upTimeSecond = 0;
		upTimeMinute = 0;
		hasCheckedToday = false;
		borderOfExp = 12;
		Beginning.isFirstTimeOpened = 1;
		PlayerPrefs.DeleteAll ();

	}

	public void OnHowToPlay ()
	{
		mainPanel.SetActive (false);
		startingPanel.SetActive (false);
		storePanel.SetActive (false);
		settingPanel.SetActive (false);
		howToPlayPanel.SetActive (true);
	}
}
