using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameMaster : MonoBehaviour {
	public UnityEngine.UI.Text Highscore;
	public UnityEngine.UI.Text Score_Text;
	public UnityEngine.UI.Text Time_Left;
	public UnityEngine.UI.Text Letters_Text;
	public UnityEngine.UI.Text Words;
	public UnityEngine.UI.Text Input;

	public TextAsset All_Words;
	int Text_Length;
	public string[] Compatible_Words;
	public int Counted_Words;

	int Time = 60;
	bool Game_Active = true;
	int Score = 0;
	int Words_Found = 0;
	string Chars = "aabcdeefghjklmnoopqrstuuvwxyz";
	char[] Letters;

	// Use this for initialization
	void Start () {
		Text_Length = 84099;
		Refresh();
		StartCoroutine(Timer());
	}
	
	// Update is called once per frame
	void Update () {
		Highscore.text = " Highscore: " + PlayerPrefs.GetInt("Highscore");
		Score_Text.text = " Score: " + Score;
		Time_Left.text = " Time: " + Time;
		Letters_Text.text = " Letters: " + new string(Letters);
		if (Time == 0) {
			Game_Active = false;
			Application.LoadLevel("Start");
		}
		if (Game_Active == false) {
			Time = 0;
		}
	}

	IEnumerator Timer () {
		while (Game_Active == true) {
			yield return new WaitForSecondsRealtime(1);
			Time--;
		}
	}

	public void Submit () {
		if (Game_Active == true) {
			for (int i = 0; i < Compatible_Words.Length; i++) {
				if (Input.text == Compatible_Words[i]) {
					Score++;
					if (Score > PlayerPrefs.GetInt("Highscore")) {
						PlayerPrefs.SetInt("Highscore", Score);
					}
					Words_Found++;
					Words.text = Words.text + Compatible_Words[i] + " ";
					Compatible_Words[i] = "THEBACKDOOR";
					if (Words_Found == Compatible_Words.Length) {
						Refresh();
					}
					break;
				}
			}
		}
	}

	void Refresh () {
		Words.text = "";
		Letters = new char[Random.Range(4,13)];
		for (int i = 0; i < Letters.Length; i++) {
			Letters[i] = Chars[Random.Range(1, Chars.Length)];
		}
		Counted_Words = 0;
		Word_Count(Letters, true);
		Compatible_Words = new string[Counted_Words];
		Word_Count(Letters, false);
	} 

	void Word_Count (char[] Usable_Characters, bool Counting) {
		StreamReader SR = new StreamReader(new MemoryStream(All_Words.bytes));
		int Words_Added = 0;
		for (int i = 0; i < Text_Length; i++) {
			string Current_Line = SR.ReadLine();
			char[] Word_Characters = new char[Current_Line.Length];
			for (int a = 0; a < Current_Line.Length; a++) {
				Word_Characters[a] = Current_Line[a];
			}
			if (Current_Line.Length <= Usable_Characters.Length) {
				int Characters_Used = 0;
				for (int b = 0; b < Usable_Characters.Length; b++) {
					for (int c = 0; c < Current_Line.Length; c++) {
						if (Usable_Characters[b] == Word_Characters[c]) {
							Word_Characters[c] = '\0';
							Characters_Used++;
							break;
						}
					}
				}
				if (Current_Line.Length == Characters_Used) {
					if (Counting == true) {
						Counted_Words++;
					}else {
						Compatible_Words[Words_Added] = Current_Line;
						Words_Added++;
					}
				}
			}
		}
		SR.Close();
	}
}