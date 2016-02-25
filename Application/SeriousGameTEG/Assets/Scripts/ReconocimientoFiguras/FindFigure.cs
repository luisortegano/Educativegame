using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class FindFigure : MonoBehaviour {
	
	float time = 1.0f;
	float timeLeft = 1.0f;
	
	public RawImage image;
	
    
    
    void Update (){
		if (0 < timeLeft){
			timeLeft -= Time.deltaTime;
		}else{
			
			timeLeft = time;
		}
    }
}
