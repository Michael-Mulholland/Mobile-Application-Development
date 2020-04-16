using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    // == private fields ==
    [SerializeField] private float bulletSpeed = 6.0f;
    [SerializeField] private EnemyBullet bulletPrefab;
    [SerializeField] private float firingRate = 0.5f;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] [Range(0f, 1.0f)] private float shootVolume = 0.5f;

    private Coroutine firingCoroutine;
    private GameObject bulletParent;
    private AudioSource audioSource;
    private float shootTime;

    public bool fire;

    // == private methods ==
    private void Start()
    {
        bulletParent = GameObject.Find("BulletParent");

        if (!bulletParent)
        {
            bulletParent = new GameObject("BulletParent");
        }

        audioSource = GetComponent<AudioSource>();

        fire = false;
    }

    void Update()
    {
        shootTime -= Time.deltaTime;
        if (shootTime < 0)
        {
            Fire();
            shootTime = 1;
        }
    }

    private void Fire()
    {
        // create a bullet
        EnemyBullet bullet = Instantiate(bulletPrefab, bulletParent.transform);
        // give it the same position as the player
        bullet.transform.position = transform.position;
        // play sound - AudioClip, Volume between 0 and 1
        //AudioSource.PlayClipAtPoint(shootClip, Camera.main.transform.position,shootVolume);
        // use a local AudioSource
        audioSource.PlayOneShot(shootClip, shootVolume);

        // play health pickup sound
        //FindObjectOfType<AudioManager>().Play("HealthPowerUP");

        Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
        rbb.velocity = Vector2.down * bulletSpeed;
    }
}
