using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // tracks whether the game is paused
    public static bool GamePaused = false;

    // get a game reference
    public GameObject pauseMenuUI;

    private void Awake()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        // if Esc key is pressed, pause game
        if(Input.GetKeyDown(KeyCode.Escape)){

            // if GamePaused is true, then resume game else pause game
            if(GamePaused){
                Resume();
            } else {
                Pause();
            }
        }
    }

    // Resume game
    public void Resume()
    {
        // disable game object - resume
        pauseMenuUI.SetActive(false);
        // scale in which time is passing - 1f means time (the game) will resume as normal
        Time.timeScale = 1f;
        // set to false
        GamePaused = false;
    }

    // Pause game
    private void Pause()
    {
        // enable game object - pause
        pauseMenuUI.SetActive(true);
        // scale in which time is passing - 0f means time (the game) will completely stop
        Time.timeScale = 0f;
        // set to true
        GamePaused = true;
    }

    // loads the menu - selected in the pause menu
    public void LoadMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // quits the application
    public void QuitGame(){
        Debug.Log("Quitting the game.....");
        Application.Quit();
    }
}