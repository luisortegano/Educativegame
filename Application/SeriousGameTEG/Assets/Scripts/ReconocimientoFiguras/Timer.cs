using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	float timeLeft = 300.0f;
	
	public Text text;
	
	void Update()
	{
		if (0 < timeLeft){
			timeLeft -= Time.deltaTime;
			text.text = Mathf.Round(timeLeft).ToString();
			if(timeLeft < 0){
				Debug.Log("Finlizo el juego");
			}
		}
	}
}
