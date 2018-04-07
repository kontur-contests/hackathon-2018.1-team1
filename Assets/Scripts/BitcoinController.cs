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

	void OnTriggerEnter(Collider other) {
		gm.IncrementScore ();
		Destroy (gameObject);
	}
}
