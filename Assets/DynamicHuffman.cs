using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DynamicHuffman : MonoBehaviour {

	public InputField input;
	public Button submitBut;
	public Text huffmanResult;
	public Text constantResult;

	public bool testing;

	private List<string> Letters;
	private List<string> LettersTemp;
	private List<int> LettersFreq;
	private List<string> Output;
	private List<string> ConstantCode;


	// Use this for initialization
	void Start () {

		Letters = new List<string>();
		LettersTemp = new List<string>();
		LettersFreq = new List<int>();
		Output = new List<string> ();
		ConstantCode = new List<string> ();

		submitBut.onClick.AddListener(() => { StartChecking();});
	}

	void StartChecking(){

		//Emptying our Arrays
		Letters.Clear ();
		LettersTemp.Clear ();
		LettersFreq.Clear ();
		Output.Clear ();
		ConstantCode.Clear ();

		//Reading our input Text and filling our Arrays
		createLetterArray ();

		//Study our arrays and use Huffman Coding
		huffmanCoding ();

		//Displaying the results
		displayResults ();

		//Create the constant coding
		constantCoding ();

	}

	private void constantCoding()
	{
		constantResult.text = "CONSTANT CODING RESULTS\n";

		string temp = System.Convert.ToString (Letters.Count - 1, 2);
		int wordLength = temp.Length;

		for(int i=0; i<Letters.Count; i++)
		{
			string binaryString = System.Convert.ToString (i, 2);

			string output = "";
			for(int j=0; j<wordLength-binaryString.Length; j++) output+="0";
			output+=binaryString;
			binaryString = output;

			ConstantCode.Add(binaryString);
			constantResult.text += "Letter : "+Letters[i] + " , Code : "+binaryString+"\n";

			Debug.Log ("CONSTANT | Letter : "+Letters[i]+" , Code : "+binaryString);
		}

	}


	private void displayResults()
	{
		huffmanResult.text = "HUFFMAN CODING RESULTS\n";
		for (int i=0; i<Letters.Count; i++)
			huffmanResult.text += "Letter : " + Letters [i] + " Coded : " +reverseString( Output [i] )+"\n";

	}

	private string reverseString(string input)
	{
		string output = "";
		for (int i=input.Length-1; i>=0; i--)
			output += input [i];

		return output;
	}


	void huffmanCoding(){


		int leng = LettersTemp.Count - 1;

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
						//Debug.Log("Bazo 0 sto "+Letters[j]);
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
						//Debug.Log("Bazo 1 sto "+Letters[j]);
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


	void createLetterArray(){
			
		string inp;
		if (!testing)
		inp = input.text;
		else inp = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabbbbbbbbbbbbbccccccccccccddddddddddddddddeeeeeeeeefffff";

		int foundPos;

		//Debug.Log ("Input Length : "+inp.Length);

		Letters.Add ("" + inp [0]);
		LettersFreq.Add (1);

		for (int i=1; i<inp.Length; i++)
		{
			
			foundPos = -1;
			//Debug.Log("Checking : "+inp[i]+" Letter Array Count : "+Letters.Count);

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
			//Debug.Log (Letters[i] + " : " + LettersFreq[i]);
			LettersTemp.Add (Letters [i]);
			Output.Add ("");
		}

		/*sortArrays ();
		for (int i=0; i<Letters.Count; i++)
		{
			Debug.Log (LettersTemp[i] + " : " + LettersFreq[i]);
		}*/
		
	}


}
