using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    // private fields
    private bool speedBoost;
    private float normalSpeed = 0.0f;
    private PlayerMovement playerNormalSpeed;
    private bool doubleHealth;
    public bool shieldUp;
    private bool powerupActive;
    private float powerupLengthCounter;
    private HealthPlayer playerHealth;
    private float normalhealth = 0.0f;
    private HealthPlayer fillHealthBar;
    private float health = 100.0f;
    private float normalhealthBar;
    private Powerups shieldPowerup;
    public bool shieldOn;
    public WeaponsController missile;
    public bool missilePowerup;
    public bool missileOn;
    public bool activateMissiles;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<HealthPlayer>();
        fillHealthBar = FindObjectOfType<HealthPlayer>();
        playerNormalSpeed = FindObjectOfType<PlayerMovement>();
        missile = FindObjectOfType<WeaponsController>();
    }

    // Update is called once per frame
    void Update()
    {
        Active();
    }

    private void Active()
    {
        if (powerupActive)
        {
            powerupLengthCounter -= Time.deltaTime;

            if (missilePowerup)
            {
                missile.activateMissiles = activateMissiles;

                StartCoroutine(StopMissiles());

                powerupActive = false;
            }

            if (speedBoost)
            {
                playerNormalSpeed.speed = normalSpeed * 2.0f;

                StartCoroutine(NormalSpeed());

                powerupActive = false;
            }

            if (doubleHealth)
            {
                //playerHealth.playerCurrentHealth = normalhealth * 2;
                playerHealth.playerCurrentHealth = health;
                // Gives the plyer a full Health bar 
                fillHealthBar.healthBar.fillAmount = playerHealth.playerCurrentHealth / 100;

                powerupActive = false;
            }

            if (shieldUp)
            {
                if (doubleHealth || speedBoost || missilePowerup)
                {
                    // needed as I don't want double health and the shield to be active at the same time
                } else
                {
                    playerHealth.playerCurrentHealth = normalhealth * 200;

                    shieldOn = true;
                    powerupActive = false;
                }
            }

            if (powerupLengthCounter <= 0)
            {
                playerHealth.playerCurrentHealth = normalhealth;
                playerNormalSpeed.speed = normalSpeed;

                powerupActive = false;
            }
        }
    }

    private IEnumerator NormalSpeed()
    {
        yield return new WaitForSeconds(5.0f);

        playerNormalSpeed.speed = normalSpeed;
    }

    private IEnumerator StopMissiles()
    {
        yield return new WaitForSeconds(20.0f);

        missile.activateMissiles = false;
    }

    // add stuff like speed, shield to the function
    public void ActivatePowerup(bool missile, bool speed, bool health, bool shield, float time)
    {
        missilePowerup = missile;
        speedBoost = speed;
        doubleHealth = health;
        shieldUp = shield;
        powerupLengthCounter = time;

        if (missilePowerup)
        {
            activateMissiles = true;
        }

        if (speedBoost)
        {
            normalSpeed = playerNormalSpeed.speed;
        }

        if (doubleHealth)
        {
            // play health pickup sound
            FindObjectOfType<AudioManager>().Play("HealthPowerUP");

            normalhealth = playerHealth.playerCurrentHealth;
            normalhealthBar = fillHealthBar.playerCurrentHealth;
        }

        powerupActive = true;
    }
}