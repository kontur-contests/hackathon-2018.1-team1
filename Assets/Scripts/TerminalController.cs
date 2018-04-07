using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalController : MonoBehaviour {

    private GameManager gm;

    public bool isEnd = false;
    public float timeToComplete = 1.5f;

    private float localTimeToComplete = float.MaxValue;

	void Start () {
        gm = Camera.main.GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        localTimeToComplete = timeToComplete;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isEnd)
        {
            localTimeToComplete -= Time.fixedDeltaTime;

            if (localTimeToComplete <= 0)
            {
                gm.SuccessLevel();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        localTimeToComplete = timeToComplete;
    }
}
