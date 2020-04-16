using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject shield;
    public PowerupManager powerupManager;
    private HealthPlayer playerHealth;
    private float normalhealth = 100.0f;
    private bool activeShield;

    void Start()
    {
        activeShield = false;
        shield.SetActive(false);
        powerupManager = FindObjectOfType<PowerupManager>();
        playerHealth = FindObjectOfType<HealthPlayer>();
    }

    void Update()
    {
        if (powerupManager.shieldOn)
        {
            activeShield = true;
            shield.SetActive(true);
            StartCoroutine(ShieldOff());

        }
        if (powerupManager.shieldOn == false)
        {
            activeShield = false;
            shield.SetActive(false);
        }
    }

    private IEnumerator ShieldOff()
    {
        yield return new WaitForSeconds(10.0f); // pick a number!!!
        activeShield = false;
        shield.SetActive(false);
        playerHealth.playerCurrentHealth = normalhealth;
        powerupManager.shieldOn = false;
    }

    // getter and setter
    public bool ActiveShield
    {
        get
        {
            return activeShield;
        }
        set
        {
            activeShield = value;
        }
    }
}
