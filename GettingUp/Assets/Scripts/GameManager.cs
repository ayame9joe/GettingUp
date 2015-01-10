using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {


	
	public Text txtUpTime;
	public Text txtExp;
	public Text txtCombo;
	public Text txtBorderOfCombo;
	public Text txtLevel;
	public Text txtCoin;
	public Slider sldBorderOfCombo;
	public Text txtGetUp;
	public Text txtEarnedSeconds;
	//public Text txtUpTimeList;
	public Text dailyData;

	public GameObject DailyData;


	public Image imgLv;
	public Image imgExp;



	public static bool hasCheckedToday = false;
	public static bool onCheck = false;

	public static List<float> upTimeList = new List<float>();

	float upTime;
	float exp;
	int level = 1;
	float nextLevelExp;
	public static int coin;
	int earnedSeconds;

	int originalCoin;
	float originalExp;
	int orinalEarnedSeconds;

	int borderOfExp = 12;
	float borderOfCombo;
	float comboIndex;

	float autoBorderOfCombo;



	int combo;



	int days = 1;

	float upTimeHour;
	float upTimeSecond;
	float upTimeMinute;

	float fullScale;

	// Use this for initialization
	void Start () {


		fullScale = imgExp.transform.localScale.x;

		sldBorderOfCombo.minValue = 4;
		sldBorderOfCombo.maxValue = 10;



		LoadData ();



	}
	
	// Update is called once per frame
	void Update () {


		ShowUI ();
		DayPassed ();
		SaveData ();
		LevelUp ();
		ComboBonus ();
		OnCheckCancel ();



	}


	void ShowUI ()
	{
		txtCombo.text = "Combo: " + combo.ToString ();
		txtLevel.text = "Lv " + level.ToString ();
		txtCoin.text = "×  " + coin.ToString ();
		
		txtBorderOfCombo.text = "Combo Val: " + sldBorderOfCombo.value.ToString ();
		txtEarnedSeconds.text = "Earned " + earnedSeconds + " Extra Seconds for Your Life!"; 

		if (borderOfCombo == sldBorderOfCombo.value) {
			
			txtBorderOfCombo.text = "Set Combo Val as " + borderOfCombo.ToString ();
		}

		if (upTime == 0) {
			txtUpTime.text = System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString() + ":" + System.DateTime.Now.Second.ToString();		
		}
		else
		{
			txtUpTime.text = Mathf.Floor(upTime).ToString() + ":" + upTimeMinute.ToString() + ":" + upTimeSecond.ToString();
		}
	}

	void DayPassed()
	{
		if (System.DateTime.Now.Hour == 0) {
			hasCheckedToday = false;
			upTime = 0;
			upTimeMinute = 0;
			upTimeSecond = 0;
			days++;
		}
	}

	void SaveData()
	{
		PlayerPrefs.SetFloat ("mExp", exp);
		PlayerPrefs.SetInt ("mCoin", coin);
		PlayerPrefs.SetInt ("mCombo", combo);
		PlayerPrefs.SetFloat ("mUpTime", upTime);
		PlayerPrefs.SetInt ("mIsFirstTimeOpened", TogglePanel.isFirstTimeOpened);
		PlayerPrefs.SetInt("mEarnedSeconds", earnedSeconds);
		PlayerPrefs.SetFloat("mBorderOfCombo", borderOfCombo);
	}

	void LoadData()
	{
		exp = PlayerPrefs.GetFloat("mExp");
		coin = PlayerPrefs.GetInt("mCoin");
		combo = PlayerPrefs.GetInt("mCombo");
		upTime = PlayerPrefs.GetFloat("mUpTime");
		TogglePanel.isFirstTimeOpened = PlayerPrefs.GetInt("mIsFirstTimeOpened");
		earnedSeconds = PlayerPrefs.GetInt("mEarnedSeconds");
		borderOfCombo = PlayerPrefs.GetFloat ("mBorderOfCombo");

	}

	void ComboBonus ()
	{
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

	void LevelUp()
	{
		nextLevelExp = 10 * (5 * level + 0.7f * level * level);
		
		if (exp == nextLevelExp) {
			level++;		
		}

		imgExp.transform.localScale = new Vector3 (
			exp  / nextLevelExp * fullScale,
			imgExp.transform.localScale.y,
			imgExp.transform.localScale.z);

	}

	public void OnCheck ()
	{
		if (!hasCheckedToday && onCheck)
		{
			onCheck = false;
			//Debug.Log("uptime:" + upTime);
			upTimeSecond = System.DateTime.Now.Second;
			upTimeMinute = System.DateTime.Now.Minute;

			upTime =  System.DateTime.Now.Hour + (float) upTimeMinute / 60.0f + (float) upTimeSecond / 360;

			originalExp = exp;
			originalCoin = coin;
			orinalEarnedSeconds = earnedSeconds;

			if (upTime > autoBorderOfCombo)
			{
				autoBorderOfCombo = Mathf.Ceil(upTime);
			}
			else {
				autoBorderOfCombo = autoBorderOfCombo;
			}

			if (borderOfExp > upTime)
			{
				//Debug.Log("Yes");
				exp += (borderOfExp - upTime);
				coin += ((int)Mathf.Floor(borderOfExp - upTime) + 1) * level ;
				exp = exp * (1 + (float)combo / 10.0f) * comboIndex;
			}
			else
			{
				//Debug.Log("No");
				exp += 0;
				coin += level;
				//isAlive = false;
			}

			if (upTime < 9)
			{
				earnedSeconds += (int)(9 - System.DateTime.Now.Hour) * 360 - (int)(60 - upTimeMinute) * 60 - (int)(60 - upTimeSecond);  
			}
			else
			{
				earnedSeconds += 0;
			}
		
			if (upTime > borderOfCombo)
			{
				combo = 0;
			}
			else {
				combo++;
			}

			upTimeList.Add (upTime);
			//txtUpTimeList.text+= upTimeList[upTimeList.Count - 1].ToString() + "\n";	



			hasCheckedToday = true;
		}

	}

	void OnCheckCancel()
	{
		if (hasCheckedToday) {
			txtGetUp.text = "Hold " + (3 - OnHoldEvent.i) + " Seconds to Reset";	
		}

		//Debug.Log (OnHoldEvent.i);
		if (OnHoldEvent.i > 3 || OnHoldEvent.i == 3) {


			
			exp = originalExp;
			coin = originalCoin;
			earnedSeconds = orinalEarnedSeconds;
			upTime = 0;
			upTimeSecond = 0;
			upTimeMinute = 0;
			txtGetUp.text = "Get Up!";
		}
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
		TogglePanel.isFirstTimeOpened = 1;
		PlayerPrefs.DeleteAll ();
		earnedSeconds = 0;
		
	}




}
