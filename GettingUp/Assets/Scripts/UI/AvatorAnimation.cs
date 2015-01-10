using UnityEngine;
using System.Collections;

public class AvatorAnimation : MonoBehaviour {

	public Animator avatorAnim;
	bool isBoy = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		avatorAnim.SetBool ("isBoy", isBoy);

	}

	public void OnToggleBoy ()
	{
		if (isBoy) {
			isBoy = false;		
		}
		else
		{
			isBoy = true;
		}
	}
}
