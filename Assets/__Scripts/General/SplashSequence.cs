using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// scene management - switch from scene to scene
using UnityEngine.SceneManagement;

public class SplashSequence : MonoBehaviour
{
    void Start(){
        StartCoroutine(ToMainMenu());
    }

    IEnumerator ToMainMenu(){
        // wait 3 seconds on the splash screen then load the MainMenu
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
    }
}
