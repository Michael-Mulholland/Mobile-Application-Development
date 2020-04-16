using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] spawnObjects;
    public Transform[] spawnLocations;
    private float spawnPowerupTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPowerup());
    }

    private IEnumerator SpawnPowerup()
    {
        yield return new WaitForSeconds(15.0f); // pick a number!!!

        // Spawn powerup randomly and at a random position
        Instantiate(spawnObjects[Random.Range(0, spawnObjects.Length)], spawnLocations[Random.Range(0, spawnLocations.Length)]);
        StartCoroutine(SpawnPowerup());
    }
}
