using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// scene management - switch from scene to scene
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(PlayGame());
    }

    IEnumerator PlayGame()
    {
        // wait 3 seconds on the splash screen then load the MainMenu
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("Normal");
    }
}
