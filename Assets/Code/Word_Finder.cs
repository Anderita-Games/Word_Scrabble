using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Word_Finder : MonoBehaviour {
	public TextAsset All_Words;
	public string[] Compatible_Words;
	int Text_Length;
	public UnityEngine.UI.Text Input;
	public UnityEngine.UI.Text Output;
	int Counted_Words;

	// Use this for initialization
	void Start () {
		Text_Length = 84099; //TODO: Make this dynamic
	}

	public void Find_Words () {
		Counted_Words = 0;
		//Organize usable levels into char[]
		char[] Usable_Characters = new char[Input.text.Length];
		for (int i = 0; i < Input.text.Length; i++) {
			Usable_Characters[i] = Input.text[i];
		}
		//Find length of array
		Word_Count(Usable_Characters, true);
		Compatible_Words = new string[Counted_Words];
		//Find Compatible words
		Word_Count(Usable_Characters, false);
		//Putting it into textbox
		Output.text = "Words: ";
		for (int i = 0; i < Compatible_Words.Length; i++) {
			if (i + 1 != Compatible_Words.Length) {
				Output.text = Output.text + Compatible_Words[i] + ", ";
			}else {
				Output.text = Output.text + "and " + Compatible_Words[i];
			}
		}
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
