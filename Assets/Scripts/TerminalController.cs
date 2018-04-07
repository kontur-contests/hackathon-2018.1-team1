using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalController : MonoBehaviour {
	//public GameManager gm;
	public bool isEnd;

	// Use this for initialization
	void Start () {
		
	}

	public Vector3 getStartPosition() {
		return transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (isEnd) {
			//gm.levelSuccess ();
		}
	}
}
