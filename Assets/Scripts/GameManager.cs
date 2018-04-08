using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {
	private int score;
	public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;

    private int playerHealth = 100;

    public GameObject playerPrefab;
    private GameObject terminalA;

    private GameObject player;

    private void Start()
    {
        terminalA = GameObject.FindWithTag("TerminalA");

        if (playerPrefab != null)
        {
            var player = Instantiate(playerPrefab);
            var position = terminalA.transform.position;
            position.z = -5;

            player.transform.position = position;

            var cameraFollow = Camera.main.GetComponent<CameraFollow>();
            cameraFollow.target = player;

            this.player = player;
        }
    }

    public void TakeDamage(int damage = 10)
    {
        playerHealth -= damage;

        healthText.text = "health " + playerHealth;

        player.GetComponent<FlashTint>().Blink();

        if (playerHealth <= 0)
        {
            playerHealth = 0;

            player.GetComponent<Animator>().SetTrigger("DeathTrigger");
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -10f));
            player.GetComponent<TargetJoint2D>().enabled = false;
            StartCoroutine(GameOverAfterTimeout());
        }
    }

    IEnumerator GameOverAfterTimeout ()
    {
        yield return new WaitForSeconds(2f);
        GameOverLevel();
    }

    public void IncrementScore(int count = 10) {
		score += count;
        scoreText.text = "score " + score;
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
