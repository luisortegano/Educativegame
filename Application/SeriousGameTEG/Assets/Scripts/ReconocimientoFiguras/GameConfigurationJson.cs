using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;
using System;

[Serializable]
public class GameConfigurationJson : MonoBehaviour {
    /* Cantidad distintas de figuras que hay en el juego */
	public int AmountFigures;
	
	/* Cantidad que debe descubrir en el juego*/
	public int maxDiscoverFigures;
	
	/* Cantidad posible de fallos */
	public int maxFails;
	
	/* Tiempo del juego */
	public int challengeTime;
}