using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
	private int points;

	void IncreasePoints() {
		points += 10;
	}

	int getPoints() {
		return points;
	}

	// Use this for initialization
	void Start () {
		points = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
