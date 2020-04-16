using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{
    // private fields
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI highestNumberOfEnemyKills;
    private int highScore;
    private int mostKills;

    private void Start() {
        // method is called as soon as this scene starts
        HighScore();
        HighestNumberOfEnemyKills();
    }

    public void PlayGame(){
        SceneManager.LoadScene("Story");
    }

    public void Quit(){  
        // Quits the program when the Quit button is clicked 
        Application.Quit();
    }

    // displays the highscore in the HighScore option on the main menu
    void HighScore(){
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString();
    }

    // displays the highest number of kills in the HighScore option on the main menu
    void HighestNumberOfEnemyKills()
    {
        mostKills = PlayerPrefs.GetInt("HighestNumOfEnemyKills");
        highestNumberOfEnemyKills.text = mostKills.ToString();
    }

    // resets the highscore and the most kills if the user decides to (a button in the highScore menu on the main menu)
    public void ClearHighScore(){
        PlayerPrefs.DeleteKey("HighScore");
        highScore = 0;
        PlayerPrefs.SetInt("HighScore",highScore);
        HighScore();

        PlayerPrefs.DeleteKey("HighestNumOfEnemyKills");
        mostKills = 0;
        PlayerPrefs.SetInt("HighestNumOfEnemyKills", mostKills);
        HighestNumberOfEnemyKills();
    }
}