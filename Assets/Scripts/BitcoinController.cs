using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitcoinController : MonoBehaviour { 
	public GameObject UI;
	private GameManager gm;
	// Use this for initialization
	void Start () {
		gm = Camera.main.GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Debug.Log ("collision");
		gm.IncrementScore ();
		Destroy (gameObject);
	}
}
