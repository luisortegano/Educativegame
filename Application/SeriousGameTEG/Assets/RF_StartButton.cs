﻿using UnityEngine;
using System.Collections;

public class RF_StartButton : MonoBehaviour {

	public GameObject RF_ConfigurationGO;
	RF_Configuration rf_Config;
	
	RF_Configuration getRF_Config (){
		if(this.rf_Config==null){
			this.rf_Config = RF_ConfigurationGO.GetComponent<RF_Configuration>();
		}
		return this.rf_Config;
	}
	
	public GameObject imageCenter;
	public GameObject ContainerPanel;

	public void clickStart (){
		this.gameObject.SetActive(false);
		ImageCenter ic = imageCenter.GetComponent<ImageCenter>();
		ic.loadImages();
		GameController gc = ContainerPanel.GetComponent<GameController>();
		ContainerPanel.SetActive(true);
		gc.initGameController();
		gc.setImagesTable();
	}
}
