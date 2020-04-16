using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


// use this to manage collisions
public class Enemy : MonoBehaviour
{
    // === public fields ===
    public delegate void EnemyKilled(Enemy enemy);
    // create static method to be implemented in the listener
    public static EnemyKilled EnemyKilledEvent;
    public int health = 100;
    public int damage = 100;
    [SerializeField] public int scoreValue = 20;

    // == private fields ==
    [SerializeField] private int damageValue = 5;
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip spawnSound;
    private float explosionDuration = 1.0f;
    private SoundController sc;
    private Bullet bullet;

    private void Start()
    {
        // Find the sound controller and play the spawn sound
        sc = SoundController.FindSoundController();
        //PlaySound(spawnSound);
    }

    // used from GameController enemy.ScoreValue
    public int ScoreValue { 
        set { scoreValue = value; }
        get { return scoreValue; } 
    }

    // used in PLayerHealth
    public int DamageValue { 
        set { damageValue = value; }
        get { return damageValue; } 
    }

    // amount of damage to take away from enemy
    public void TakeDamage(int amount){
        health -= amount;

        // if health is less the 0, Die() is called
        if(health <= 0){
            Die();
        }
    }

    void Die()
    {
        // publish the event to the system to give the player points
        PublishEnemyKilledEvent();
        // destroy game object
        Destroy(gameObject);
    }

    // for sound
    private void PlaySound(AudioClip clip)
    {
        if (sc)
        {
            sc.PlayOneShot(clip);
        }
    }

    // method for collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMovement>();
        var bullet = collision.GetComponent<Bullet>();

        // if the player hits the enemy
        if (player) 
        {
            // play crash sound here
            PlaySound(crashSound);

            // if health is greater than 0, TakeDamage() is called
            if (health > 0){
                TakeDamage(damageValue);
            }
        }

        // if a bullet hits the enemy
        if (bullet)
        {
            // play die sound
            PlaySound(dieSound);
            //destroy bullet
            Destroy(bullet.gameObject);

            // show the explosion
            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
            Destroy(explosion, explosionDuration);

            // if health is greater than 0, TakeDamage() is called
            if (health > 0){
                TakeDamage(damageValue);
            }
        }
    }

    private void PublishEnemyKilledEvent()
    {
        // make sure somebody is listening
        if (EnemyKilledEvent != null)   
        {
            EnemyKilledEvent(this);
        }
    }
}