using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponsController : MonoBehaviour
{
    public bool fireMainWeapon;
    public bool activateMissiles;

    // == private fields ==
    [SerializeField] private float bulletSpeed = 6.0f;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float firingRate = 0.5f;
    [SerializeField] private AudioClip shootClip;
    [SerializeField][Range(0f, 1.0f)] private float shootVolume = 0.5f;

    // Missile
    private GameObject missileParent;
    [SerializeField] private Bullet missile;
    private const string missileParentName = "MissileParent";
    private float shootTime;

    private Coroutine firingCoroutine;
    private GameObject bulletParent;
    private AudioSource audioSource;

    // == private methods ==
    private void Start()
    {
        fireMainWeapon = true;
        activateMissiles = false;

        bulletParent = GameObject.Find("BulletParent");

        if (!bulletParent)
        {
            bulletParent = new GameObject("BulletParent");
        }
        
        audioSource = GetComponent<AudioSource>();

        // get missile parent
        missileParent = GameObject.Find(missileParentName);

        // if missile parent is not there, create it
        if(!missileParent){
            missileParent = new GameObject(missileParentName);
        }

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // implement a coroutine to fire
            firingCoroutine = StartCoroutine(FireCoroutine());
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(firingCoroutine);
        }


        // Time.deltaTime is the time passed since last frame.
        shootTime -= Time.deltaTime;
        
        // if shootTime is less than 0, fire the missle (God love them)
        if (shootTime<0)
        {
            if (activateMissiles)
            {
                Fire();
            }

            shootTime = 1;
        }

    }

    // coroutine returns an IEnumerator type
    private IEnumerator FireCoroutine()
    {
        while(fireMainWeapon)
        {
            // create a bullet
            Bullet bullet = Instantiate(bulletPrefab, bulletParent.transform);
            // give it the same position as the player
            bullet.transform.position = transform.position;
            // play sound - AudioClip, Volume between 0 and 1
            //AudioSource.PlayClipAtPoint(shootClip, Camera.main.transform.position,shootVolume);
            // use a local AudioSource
            audioSource.PlayOneShot(shootClip, shootVolume);

            Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
            rbb.velocity = Vector2.up * bulletSpeed;
            // sleep for short time
            yield return new WaitForSeconds(firingRate); // pick a number!!!
        }
    }

    private void Fire(){
        // plays the ships laser sound
        //FindObjectOfType<AudioManager>().Play("PlayerLaser");

        // instantiate missile on the right side
        Bullet missileRightSide = Instantiate(missile, missileParent.transform);

        // move the missile to the right of the players center point
        missileRightSide.transform.position = transform.position + new Vector3(.2f,0,0);
        // give the missile on the right velocity
        Rigidbody2D rbbMissileRight = missileRightSide.GetComponent<Rigidbody2D>();
        rbbMissileRight.velocity = Vector2.up * 10.0f;

        // instantiate missile on the left side
        Bullet missileLeftSide = Instantiate(missile, missileParent.transform);

        // move the missile to the left of the players center point
        missileLeftSide.transform.position = transform.position + new Vector3(-0.2f,0,0);
        // give the missile on the left velocity
        Rigidbody2D rbbMissileLeft = missileLeftSide.GetComponent<Rigidbody2D>();
        rbbMissileLeft.velocity = Vector2.up * 10.0f;
    }
}
