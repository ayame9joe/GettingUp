using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Product : MonoBehaviour {

	public string name;
	public int price;
	public string description;

	public Text txtName;
	public Text txtPrice;

	void Update () {
		txtName.text = "Name: " + name;
		txtPrice.text = "Price: " + price.ToString ();
	}

	public void OnBuy ()
	{
		if (GameManager.coin > this.price)
		{
			GameManager.coin -= this.price;
		}
		// Add this item to item list.
	}
}
