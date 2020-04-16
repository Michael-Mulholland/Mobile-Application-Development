using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightDiagonalSpawner : MonoBehaviour
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
        rb.velocity = new Vector3(-3, -1 * speed);
    }
}
