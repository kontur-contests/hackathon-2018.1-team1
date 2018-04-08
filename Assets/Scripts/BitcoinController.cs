using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitcoinController : MonoBehaviour { 
	public GameObject UI;
	private GameManager gm;

    public int points = 10;
	// Use this for initialization
	void Start () {
		gm = Camera.main.GetComponent<GameManager> ();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gm.IncrementScore(points);
        Destroy(gameObject);
    }
}
