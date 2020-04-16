using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // == public fields ==
    // used from GameController enemy.ScoreValue
    public int ScoreValue { get { return scoreValue; } }
    // used in PLayerHealth
    public int DamageValue { get { return damageValue; } }

    // delegate type to use for event
    public delegate void BossKilled(Boss boss);

    // create static method to be implemented in the listener
    public static BossKilled BossKilledEvent;
    public int health = 100;
    public int damage = 20;

    // == private fields ==
    private SoundController sc;
    private float explosionDuration = 1.0f;
    [SerializeField] private int scoreValue = 20;
    [SerializeField] private int damageValue = 5;
    // sounds for explosion, crash, bullet and spawning
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip spawnSound;

    // == private methods ==
    private void Start()
    {
        sc = SoundController.FindSoundController();
        PlaySound(spawnSound);
    }

    // amount of damage to take away from enemy
    public void TakeDamage(int amount){
        health -= amount;
        Debug.Log("Enemy Health: " + health);

        if(health <= 0){
            Die();
        }
    }

    void Die()
    {
        // publish the event to the system to give the player points
        PublishBossKilledEvent();
        Destroy(gameObject);
    }

    private void PlaySound(AudioClip clip)
    {
        if (sc)
        {
            sc.PlayOneShot(clip);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // parameter = what ran into me
        // if the player hit, then destroy the player
        // and the current enemy rectangle

        var player = collision.GetComponent<PlayerMovement>();
        var bullet = collision.GetComponent<Bullet>();

        if(player)  // if (player != null)
        {
            // play crash sound here
            PlaySound(crashSound);
            // destroy the player and the rectangle
            // give the player points/coins

            if(health > 0){
                TakeDamage(damage);
            }
        }

        if(bullet)
        {
            // play die sound
            PlaySound(dieSound);
            // destroy bullet
            Destroy(bullet.gameObject);
            // publish the event to the system to give the player points
            //PublishEnemyKilledEvent();
            // show the explosion
            GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
            Destroy(explosion, explosionDuration);
           
            if(health > 0){
                TakeDamage(damage);
            }
        }
    }

    private void PublishBossKilledEvent()
    {
        // make sure somebody is listening
        if(BossKilledEvent != null)   
        {
            BossKilledEvent(this);
        }
    }  
}
