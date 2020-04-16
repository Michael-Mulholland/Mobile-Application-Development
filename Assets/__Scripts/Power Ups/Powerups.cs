using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    // public fields
    public bool missile;
    public bool speedBoost;
    public bool doubleHealth;
    public float powerupLength;

    //public bool shieldUp;
    public bool activeShield = false;

    // private fields
    private PowerupManager powerManager;
    public GameObject shield;
    
    [SerializeField] private float speed = 5.0f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        powerManager = FindObjectOfType<PowerupManager>();
        //shield = GetComponent<Shield>();
        activeShield = false;
        shield.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(0, -1 * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMovement>();

        if (player)
        {
            powerManager.ActivatePowerup(missile, speedBoost, doubleHealth, shield, powerupLength);
        }

        gameObject.SetActive(false);
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
