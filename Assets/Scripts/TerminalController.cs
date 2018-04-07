using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalController : MonoBehaviour {

    private GameManager gm;

    public bool isEnd = false;
    public float timeToComplete = -.5f;

    private float localTimeToComplete = -1;

	// Use this for initialization
	void Start () {
        gm = Camera.main.GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ON TRIGGER ENTER");

        localTimeToComplete = timeToComplete;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("ON TRIGGER");

        if (isEnd)
        {
            localTimeToComplete -= Time.fixedDeltaTime;

            Debug.Log("LOCAL TIME:" + localTimeToComplete + " " + Time.fixedDeltaTime);

            if (localTimeToComplete <= 0)
            {
                Debug.Log("END LOCAL TIME");

                gm.SuccessLevel();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("ON TRIGGER EXIT");
        localTimeToComplete = timeToComplete;
    }
}
