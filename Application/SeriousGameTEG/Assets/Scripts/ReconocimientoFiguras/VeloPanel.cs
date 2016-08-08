using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class VeloPanel : MonoBehaviour {

	[Obsolete("Los elementos se deben ir asociando al velo de manera dinamica")]
	public Text FinalText;

	public static int HELP = 0;
	public static int END = 1;
	public List<GameObject> flyers;

	public void displayFlyer (int flyer){
		Debug.Log("Display flyer "+ flyer);
		flyers[flyer].SetActive(true);
	}

	public void hideFlyer (int flyer){
		Debug.Log("Hide flyer "+ flyer);
		flyers[flyer].SetActive(false);
	}

}
