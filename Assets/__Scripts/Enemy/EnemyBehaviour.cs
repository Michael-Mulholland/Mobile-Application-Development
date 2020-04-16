using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// move left when things start

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBehaviour : MonoBehaviour
{
    // == private fields ==
    [SerializeField] private float speed = 7.0f;

    private Rigidbody2D rb;

    // == private methods ==
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, -1 * speed);
    }

    public void SetMoveSpeed(float enemySpeed){
        this.speed = enemySpeed;
    }
}
