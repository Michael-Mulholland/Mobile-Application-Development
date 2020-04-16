using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;    // text mesh pro library
using Utilities;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // == public fields ==
    public int StartingLives { get { return startingLives; } }
    public int RemainingLives { get { return remainingLives; } }
    public static GameController instance;
    public int playerScore = 0;
    public int numberOfEnemyKills = 0;

    // == private fields ==
    [SerializeField] private List<WaveConfig> waveConfigList;
    [SerializeField] private AudioClip waveReadySound;

    public static int HighestNumOfEnemyKills = 0;
    private static int highScore = 0;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private SoundController sc;
    private int coinsCollected = 0; 
    private int startingLives = 3;
    private static int remainingLives;
    private int remainingEnemyCount;
    private int remainingNumOfWaves;
    private int startingWave = 0;
    private int numberOfEnemyWaves = 3;
    private float spawnTime = 3.0f;
    private float spawnEnemyBoss = 5.0f;
    private float nextWaveSeconds = 5.0f;
    private float loadSceneSeconds = 5.0f;

    // health hearts
    public int livesLeft;
    public int numberOfHearts;
    public Image[] hearts;
    public Sprite fullHearts;
    public Sprite emptyHearts;

    // == private methods ==
    private void Awake()
    {
        // PlayerPref to save the players highscore and allow the user to see it in the main menu
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore", highScore);
        }

        // PlayerPref to save the players highest number of kills
        if (PlayerPrefs.HasKey("HighestNumOfEnemyKills"))
        {
            HighestNumOfEnemyKills = PlayerPrefs.GetInt("HighestNumOfEnemyKills", HighestNumOfEnemyKills);
        }

        // plays the game theme
        //FindObjectOfType<AudioManager>().Play("GameMusic");
        
        // get an instance of the GameController
        instance = this;
    }

    private void Update()
    {
        // Update the highscore within the game if the player beats it while playing
        highScoreText.text = highScore.ToString();
    }

    private void Start()
    {
        // Call UpdateScore/UpdateNumberOfKills method once the game starts
        UpdateScore();
        UpdateNumberOfKills();

        // number of lifes remaining
        remainingLives = startingLives;

        // StartCoroutine to spawn enemy waves
        StartCoroutine(SpawnAllWaves());

        // find the sound controller
        sc = SoundController.FindSoundController();
    }

    // These functions will be called when the attached GameObject is toggled.
    private void OnEnable()
    {
        Enemy.EnemyKilledEvent += OnEnemyKilledEvent;
        Boss.BossKilledEvent += OnBossKilledEvent;
        PointSpawners.EnemySpawnedEvent += PointSpawners_OnEnemySpawnedEvent;
    }
    private void OnDisable()
    {
        Enemy.EnemyKilledEvent -= OnEnemyKilledEvent;
        Boss.BossKilledEvent -= OnBossKilledEvent;
        PointSpawners.EnemySpawnedEvent -= PointSpawners_OnEnemySpawnedEvent;
    }

    // Method to spawn all the waves
    // Make this a coroutine, wait for the SetupNextWave coroutine
    // to run, and then go to the next element in the list
    private IEnumerator SpawnAllWaves(){

        // get the number of enemy waves in the list
        var numberOfWaves = waveConfigList.Count;
        
        // loop through the list
        for(int waveIndex = startingWave; waveIndex < waveConfigList.Count; waveIndex++){

            // current wave config
            var waveConfig = waveConfigList[waveIndex];

            // if there is more than one wave, start the coroutine to setup the next wave and wait 3 seconds for the next one
            if(numberOfWaves > 1){
                yield return StartCoroutine(SetupNextWave(waveConfig));
                yield return new WaitForSeconds(spawnTime);
            }
            // if there is one wave left, spawn the last wave and wait 3 seconds for the next one.
            // Then spawn the Boss
            else if (numberOfWaves == 1){
                yield return StartCoroutine(SetupNextWave(waveConfig));
                yield return new WaitForSeconds(spawnTime);
                SpawnBoss();
            }

            // take one away when a enemy wave has finished
            numberOfWaves--;
        }
    }

    // spawn the enemy boss
    private void SpawnBoss()
    {
        StartCoroutine(SetupBossWave());
    }

    private IEnumerator SetupBossWave()
    {
        // wait 5 seconds and then spawn the enemy boss
        yield return new WaitForSeconds(spawnEnemyBoss);
        sc.PlayOneShot(waveReadySound);

        EnableBossSpawning();
    }

    private void EnableBossSpawning()
    {
        // find each PointSpawner, call a public method to disable spawning
        foreach (var spawner in FindObjectsOfType<BossPointSpawner>())
        {
            spawner.EnableBossSpawning();
        }
    }

    // spawn one enemy 
    private void PointSpawners_OnEnemySpawnedEvent()
    {
        // take one away everytime an enemy is spawned
        remainingEnemyCount--;
        
        // if there is no enemy left, stop spawning enemy ships
        if(remainingEnemyCount == 0)
        {
            // stop the point spawner from spawning
            DisableSpawning();
        }
    }

    private IEnumerator SetupNextWave(WaveConfig currentWave)
    {
        yield return new WaitForSeconds(nextWaveSeconds);
        sc.PlayOneShot(waveReadySound);
 
        remainingEnemyCount = currentWave.GetEnemiesPerWave();

        // pass the wave config file to the Point Spawner
        FindObjectOfType<PointSpawners>().SetWaveConfig(currentWave);

        EnableSpawning();
    }

    private void DisableSpawning()
    {
        // find each PointSpawner, call a public method to disable spawning
        foreach(var spawner in FindObjectsOfType<PointSpawners>())
        {
            remainingNumOfWaves--;
            spawner.DisableSpawning();
        }
    }

    private void DisableBossSpawning()
    {
        // find each PointSpawner, call a public method to disable spawning
        foreach(var spawner in FindObjectsOfType<BossPointSpawner>())
        {
            spawner.DisableBossSpawning();
        }
    }

    private void EnableSpawning()
    {
        // find each PointSpawner, call a public method to disable spawning
        foreach (var spawner in FindObjectsOfType<PointSpawners>())
        {
            spawner.EnableSpawning();
        }
    }

    private void OnEnemyKilledEvent(Enemy enemy)
    {
        // Get the total number of enemy kills
        numberOfEnemyKills++;
        UpdateNumberOfKills();

        // add the score value for the enemy to the player score
        playerScore += enemy.ScoreValue;
        UpdateScore();
    }

    private void OnBossKilledEvent(Boss boss)
    {
        // Get the total number of enemy kills
        numberOfEnemyKills++;
        UpdateNumberOfKills();

        // add the score value for the enemy to the player score
        playerScore += boss.ScoreValue;

        Invoke("LoadScene", loadSceneSeconds);
        UpdateScore();
    }

    public void UpdateNumberOfKills()
    {
        // get the current games enemy kills - to be viewed on the GameOver scene
        PlayerPrefs.SetInt("currentEnemyKills", numberOfEnemyKills);

        // check to see if the highest number of kills is beat
        // if it is, save the new highest number of kills
        if (numberOfEnemyKills > HighestNumOfEnemyKills)
        {
            // save the new highest number of enemy kills
            // so the player can view it on the MainMenu and GameOverMenu
            HighestNumOfEnemyKills = numberOfEnemyKills;
            PlayerPrefs.SetInt("HighestNumOfEnemyKills", HighestNumOfEnemyKills);
        }
    }


    private void UpdateScore()
    {
        // display on screen
        scoreText.text = playerScore.ToString();

        // get the current game score - to be viewed on the GameOver 
        PlayerPrefs.SetInt("currentScore", playerScore);

        HighScore();
    }

    public void HighScore()
    {
        // check to see if the highscore is beat
        // if it is, save the new highscore
        if(playerScore > highScore)
        {
            highScore = playerScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
    
    // takes one life away from the player and it is saved in PlayerPrefs
    public void LoseOneLife()
    {
        remainingLives--;
       // remainingPlayerLivesText.text = remainingLives.ToString();
        PlayerPrefs.SetInt("RemainingLives", remainingLives);

        if (remainingLives > numberOfHearts)
        {
            remainingLives = numberOfHearts;
        }


        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < remainingLives)
            {
                hearts[i].sprite = fullHearts;
            }
            else
            {
                hearts[i].sprite = emptyHearts;
            }


            if (i < numberOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    // this method is called once the boss is destoryed
    private void LoadScene()
    {
        // load the GameOver scene
        SceneManager.LoadScene("GameOver");
    }
}