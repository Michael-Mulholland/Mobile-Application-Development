using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    // text mesh pro library
using UnityEngine.SceneManagement;

public class GameOverDetails : MonoBehaviour
{
    // === private fields ===
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI highestKillsText;
    [SerializeField] private TextMeshProUGUI gameScoreText;
    [SerializeField] private TextMeshProUGUI gameKillsText;
    private int highScore = 0;
    private int highestNumOfKills = 0;
    private int gameScore = 0;
    private int gameNumOfKills = 0;

    private void Awake()
    {
        // Call method straight away to load details
        GetGameDetails();
    }

    private void GetGameDetails()
    {
        // get the high score from the Game Controler using PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString();

        // get the highest number of kills from the Game Controler using PlayerPrefs
        highestNumOfKills = PlayerPrefs.GetInt("HighestNumOfEnemyKills");
        highestKillsText.text = highestNumOfKills.ToString();

        // get the current game score from the Game Controler using PlayerPrefs
        gameScore = PlayerPrefs.GetInt("currentScore");
        gameScoreText.text = gameScore.ToString();

        // get the current number of kills from the Game Controler using PlayerPrefs
        gameNumOfKills = PlayerPrefs.GetInt("currentEnemyKills");
        gameKillsText.text = gameNumOfKills.ToString();
    }

    // loads the main menu
    public void LoadMenu()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        SceneManager.LoadScene("MainMenu");
    }

    // quits the application
    public void QuitGame()
    {
        Debug.Log("Quitting the game.....");
        Application.Quit();
    }
}
