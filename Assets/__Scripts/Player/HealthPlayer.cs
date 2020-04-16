using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthPlayer : MonoBehaviour
{
    // set up an initial health value,
    // set an amount of damage per enemy - start with 5
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] public float playerStartHealth = 100;
    public float playerCurrentHealth;
    public float damage = 25;

    // delegate type to use for event
    public delegate void PlayerKilled(HealthPlayer playerHealth);
    // create static method to be implemented in the listener
    public static PlayerKilled PlayerKilledEvent;

    public WeaponsController mainWeapon;
    private bool deactivateWeapon;
    private bool enableWeapon;

    private GameController gc;
    private Vector3 startPosition;

    private SpriteRenderer sr;
    private PolygonCollider2D pc2d;
    private WeaponsController wc;
    private PlayerMovement pm;
    private ParticleSystem ps;

    // Health Bar 
    public Image healthBar;

    [SerializeField] private GameObject explosionFX;
    // sounds for getting hit by bullet, spawning
    [SerializeField] private AudioClip dieSound;
    private SoundController sc;
    private float explosionDuration = 1.0f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        pc2d = GetComponent<PolygonCollider2D>();
        wc = GetComponent<WeaponsController>();
        pm = GetComponent<PlayerMovement>();
        ps = GetComponentInChildren<ParticleSystem>();

        mainWeapon = FindObjectOfType<WeaponsController>();
        deactivateWeapon = false;
        enableWeapon = true;
        playerCurrentHealth = playerStartHealth;

        gc = FindObjectOfType<GameController>();

        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // amount of damage to take away from player
    public void TakeDamage(float amount){
        playerCurrentHealth -= amount;

        healthBar.fillAmount = playerCurrentHealth / playerStartHealth;

        if(playerCurrentHealth <= 0){
            Die();
        }
    }

    private void Die()
    {
        StartCoroutine(DieCoroutine());
    }

    // use the triggerEnter method to see if it gets hit by enemy
    private void OnTriggerEnter2D(Collider2D whatHitMe)
    {
        var enemy = whatHitMe.GetComponent<Enemy>();
        var boss = whatHitMe.GetComponent<Boss>();
        var bullet = whatHitMe.GetComponent<EnemyBullet>();

        if (enemy)
        {       
            if(playerCurrentHealth > 0){
                TakeDamage(damage);
            }
        }

        if(boss)
        {       
            if(playerCurrentHealth > 0){
                TakeDamage(damage);
            }
        }

        if (bullet)
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

            if (playerCurrentHealth > 0)
            {
                TakeDamage(damage);
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (sc)
        {
            sc.PlayOneShot(clip);
        }
    }

    private IEnumerator DieCoroutine()
    {
        // disable components - makes the player disappear
        DisableComponents();
        mainWeapon.fireMainWeapon = deactivateWeapon;
        GameObject explosion = Instantiate(explosionPrefab);
        //explosion.transform.position = transform.position;
        // tell the game controller lost one life
        gc.LoseOneLife();

        yield return new WaitForSeconds(1.5f);
        if(gc.RemainingLives > 0)
        {
            Respawn();
        }
        else{
            SceneManager.LoadScene ("GameOver");
        }

    }

    private void DisableComponents()
    {
        // SpriteREnderer, PolygonCollider2d, Weaspons, Movement
        SetComponentsEnabled(false);
        // stop partical sysatem
        ps.Stop();
    }

    private void EnableComponents()
    {
        SetComponentsEnabled(true);
        
        // play partical sysatem
        ps.Play();
    }

    private void SetComponentsEnabled(bool status)
    {
        sr.enabled = status;
        pc2d.enabled = status;
        wc.enabled = status;
        pm.enabled = status;
    }

    private void Respawn()
    {
        // set the player back to the start position
        // reset the player health
        // re-enable all the components to make the player visible.
        transform.position = startPosition;

        mainWeapon.fireMainWeapon = enableWeapon;

        // Gives the plyer health of 
        playerCurrentHealth = playerStartHealth;

        // Gives the plyer a full Health bar 
        healthBar.fillAmount = playerCurrentHealth / playerStartHealth;
        EnableComponents();
    }

    private void PublishPlayerKilledEvent()
    {
        // make sure somebody is listening
        if(PlayerKilledEvent != null){
            PlayerKilledEvent(this);            
        }
    } 
}
