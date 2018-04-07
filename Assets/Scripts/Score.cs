using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	private int points;
	public Text text;

	void IncreasePoints() {
		points += 10;
	}

	int getPoints() {
		return points;
	}

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		points = 0;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "score: " + points;
	}
}
