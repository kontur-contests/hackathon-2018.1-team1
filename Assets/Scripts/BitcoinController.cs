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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        gm.IncrementScore();
        Destroy(gameObject);
    }
}
