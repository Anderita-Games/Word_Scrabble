using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_UI : MonoBehaviour {
	public UnityEngine.UI.Text Highscore;

	// Use this for initialization
	void Start () {
		Highscore.text = " Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
	}

	public void Start_Game () {
		Application.LoadLevel("Game");
	}
}