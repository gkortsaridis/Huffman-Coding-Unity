using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class HuffmanSteps : MonoBehaviour {

	private List<string> Letters;
	private List<string> LettersTemp;
	private List<int> LettersFreq;
	private List<string> Output;
	private List<string> ConstantCode;

	private int step = 0;

	[SerializeField] Text inputShow;

	string inp = "George";


	// Use this for initialization
	void Start () {

		Letters = new List<string>();
		LettersTemp = new List<string>();
		LettersFreq = new List<int>();
		Output = new List<string> ();
		ConstantCode = new List<string> ();

	}

	void huffmanCoding(int leng){
		
		
		//int leng = LettersTemp.Count - 1;

		Output.Clear ();
		//LettersTemp.Clear ();
		//LettersFreq.Clear ();

		while(leng > 0)
		{
			string temp = LettersTemp [leng];
			
			for(int i=0; i<temp.Length; i++)
			{
				for(int j=0; j<Letters.Count; j++)
				{
					if((temp[i]+"") == (""+Letters[j]))
					{
						Output[j]=Output[j]+"0";
						Debug.Log("Bazo 0 sto "+Letters[j]);
					}
					
				}
			}
			
			temp = LettersTemp [leng-1];
			
			for(int i=0; i<temp.Length; i++)
			{
				for(int j=0; j<Letters.Count; j++)
				{
					if((temp[i]+"") == (""+Letters[j])) 
					{
						Output[j]=Output[j]+"1";
						Debug.Log("Bazo 1 sto "+Letters[j]);
					}
				}
			}
			
			LettersTemp[leng-1] += LettersTemp[leng];
			LettersFreq[leng-1] += LettersFreq[leng];
			leng--;
			sortArrays();
		}
		
		for(int i=0; i<Output.Count; i++)
		{
			Debug.Log("Letter : "+Letters[i] + " Code : "+Output[i]);
		}
		
		
	}
	
	
	void sortArrays(){
		
		bool foundone;
		string tempobj;
		int tempInt;
		
		foundone = true; 
		while(foundone) { 
			foundone = false; 
			for(int i = 0; i<LettersFreq.Count - 1; i++)
			{ 
				if(LettersFreq[i] < LettersFreq[i + 1]) 
				{ 
					tempobj = LettersTemp[i + 1]; 
					tempInt = LettersFreq[i + 1];
					LettersTemp[i + 1] = LettersTemp[i]; 
					LettersFreq[i + 1] = LettersFreq[i];
					LettersTemp[i] = tempobj;
					LettersFreq[i] = tempInt;
					foundone = true; 
				}
			}
		}
		
	}

	void createLetterArray(int step){
		

		int foundPos;



		Letters.Clear ();
		LettersTemp.Clear ();
		LettersFreq.Clear ();
		Output.Clear ();
		ConstantCode.Clear ();

		Letters.Add ("" + inp [0]);
		LettersFreq.Add (1);
		
		for (int i=1; i<step; i++) {


			foundPos = -1;

			for (int j=0; j<Letters.Count; j++) {
				if (("" + inp [i]) == Letters [j])
					foundPos = j;
			}
			
			
			if (foundPos != -1) {
				LettersFreq [foundPos]++;
			} else {
				Letters.Add ("" + inp [i]);
				LettersFreq.Add (1);
			}

		}

		for (int i=0; i<Letters.Count; i++) {
			LettersTemp.Add (Letters [i]);
			Output.Add ("");
		}
		
		sortArrays ();

		
	}

	void OnGUI(){

		if (step <= inp.Length) {

			GUI.Label (new Rect (20, Screen.height / 8 - 20, 80, 20), "Found Letter");
			GUI.Label (new Rect (20 + 80, Screen.height / 8 - 20, 80, 20), "Letters Freq");

			for (int i=0; i<Letters.Count; i++) {
				//Debug.Log (Letters[i] + " : " + LettersFreq[i]);
				GUI.Label (new Rect (20, Screen.height / 8 + i * 20, 80, 20), "" + LettersTemp [i]);
				GUI.Label (new Rect (20 + 80, Screen.height / 8 + i * 20, 80, 20), "" + LettersFreq [i]);
			}


			if (GUI.Button (new Rect (Screen.width - 100, Screen.height - 60, 100, 60), "Next Step")) {
				step++;
				if (step != inp.Length + 1) {
					createLetterArray (step);
				}
			}
			if (GUI.Button (new Rect (0, Screen.height - 60, 100, 60), "Prev. Step")) {
				if (step > 0) {
					step--;
					createLetterArray (step);
				}
			}

			string toshow = inp;
			if (step >= 1) {
				toshow = "";
				for (int i=0; i<step-1; i++)
					toshow += inp [i];

				toshow += "<b>" + inp [step - 1] + "</b>";

				for (int i=step; i<inp.Length; i++)
					toshow += inp [i];
			}
			inputShow.text = toshow;
		} else {

			if (GUI.Button (new Rect (Screen.width - 100, Screen.height - 60, 100, 60), "Next Step")) {
				step++;
				//if (step != inp.Length + 1) {
					huffmanCoding (step - inp.Length);
				//}
			}
			if (GUI.Button (new Rect (0, Screen.height - 60, 100, 60), "Prev. Step")) {
				if (step > 0) {
					step--;
					//createLetterArray (step);
				}
			}

				//huffmanCoding(step - );

		}

	}



}
