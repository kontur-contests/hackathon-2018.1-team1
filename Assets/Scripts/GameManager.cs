using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	private int score;
	public Text text;

	public void IncrementScore() {
		score += 10;
		text.text = "score: " + score;
	}

    public void ChangeMenuToGame()
    {
        SceneManager.LoadScene("Main");
    }
}
