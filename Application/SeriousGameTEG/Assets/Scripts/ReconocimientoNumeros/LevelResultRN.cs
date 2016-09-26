using System;

[Serializable]
public class LevelResultRN {
	public string date;
	public int challengeTime;
	public int expendedTime ;
	public int hits;
	public int maxHits;
	public int fails;
	public int maxFails;
	public bool winGame;

	public LevelResultRN  (){
		date = DateTime.Now.ToString();
	}

	public LevelResultRN (int challengeTime, int expendedTime, int maxHits, int hits, int maxFails, int fails, bool winGame){
		date = DateTime.Now.ToString();
		this.challengeTime=challengeTime;
		this.expendedTime = expendedTime;
		this.maxHits = maxHits;
		this.hits=hits;
		this.maxFails=maxFails;
		this.fails=fails;
		this.winGame=winGame;
	}
}