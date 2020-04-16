using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    // Missile
    private GameObject missileParent;
    [SerializeField] private Bullet missile;
    private const string missileParentName = "MissileParent";
    private float shootTime;
    private AudioSource audioSource;
    public bool activateMissile;

    // Start is called before the first frame update
    void Start()
    {
        activateMissile = false;

        audioSource = GetComponent<AudioSource>();

        // get missile parent
        missileParent = GameObject.Find(missileParentName);

        // if missile parent is not there, create it
        if (!missileParent)
        {
            missileParent = new GameObject(missileParentName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime is the time passed since last frame.
        shootTime -= Time.deltaTime;

        // if shootTime is less than 0, fire the missle (God love them)
        if (shootTime < 0)
        {
            Fire(activateMissile);
            shootTime = 3;
        }
    }

    public void Fire(bool missActive)
    {
        if (missActive)
        {
            Debug.Log("IN");
        }
        else
        {
            Debug.Log("NOPE");
        }
        // plays the ships laser sound
        //FindObjectOfType<AudioManager>().Play("PlayerLaser");

        // instantiate missile on the right side
        Bullet missileRightSide = Instantiate(missile, missileParent.transform);

        // move the missile to the right of the players center point
        missileRightSide.transform.position = transform.position + new Vector3(.2f, 0, 0);
        // give the missile on the right velocity
        Rigidbody2D rbbMissileRight = missileRightSide.GetComponent<Rigidbody2D>();
        rbbMissileRight.velocity = Vector2.up * 10.0f;

        // instantiate missile on the left side
        Bullet missileLeftSide = Instantiate(missile, missileParent.transform);

        // move the missile to the left of the players center point
        missileLeftSide.transform.position = transform.position + new Vector3(-0.2f, 0, 0);
        // give the missile on the left velocity
        Rigidbody2D rbbMissileLeft = missileLeftSide.GetComponent<Rigidbody2D>();
        rbbMissileLeft.velocity = Vector2.up * 10.0f;
    }
}