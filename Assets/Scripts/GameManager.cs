using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	private int score;
	public Text scoreText;
    public Text healthText;

    private int playerHealth = 100;

    public GameObject playerPrefab;
    private GameObject terminalA; 

    private void Start()
    {
        terminalA = GameObject.FindWithTag("TerminalA");

        if (playerPrefab != null)
        {
            var player = Instantiate(playerPrefab);
            var position = terminalA.transform.position;
            position.z = 0;

            player.transform.position = position;

            var cameraFollow = Camera.main.GetComponent<CameraFollow>();
            cameraFollow.target = player;
        }
    }

    public void TakeDamage(int damage = 10)
    {
        playerHealth -= damage;

        healthText.text = "health: " + playerHealth;

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            GameOverLevel();
        }
    }

    public void IncrementScore() {
		score += 10;
        scoreText.text = "score: " + score;
	}

    public void ChangeMenuToGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void SuccessLevel()
    {
        SceneManager.LoadScene("LevelComplete");
    }

    public void GameOverLevel()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
