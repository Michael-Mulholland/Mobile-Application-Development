using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// require component - must have a Rigidbody in order to work
[RequireComponent(typeof(Rigidbody2D))]
public class CoinMovement : MonoBehaviour
{
    // [SerializeField] TO VIEW IN THE INSPECTOR // [Range(1.0f, 10.0f)] for range
    [SerializeField]  private float speed = 3.0f;

    // private variables
    private Rigidbody2D rb;

    // private methods
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, -1 * speed);
    }
}
