using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager_sc : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text scoreText;

    [SerializeField]
    private TMPro.TMP_Text highestscoreText;

    [SerializeField]
    private Sprite[] liveSprites;

    [SerializeField]
    private Image livesImg;

    [SerializeField]
    private TMPro.TMP_Text gameOverText;

    [SerializeField]
    private TMPro.TMP_Text restartText;

    [SerializeField]
    private GameManager_sc gameManager_sc;

    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        gameManager_sc = GameObject.Find("Game_Manager").GetComponent<GameManager_sc>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        livesImg.sprite = liveSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        gameManager_sc.GameOver();
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
