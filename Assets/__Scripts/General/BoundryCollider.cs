using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BoundryCollider : MonoBehaviour
{
    // method to detect if bullet hits boundry collider
    // if it does, destroy bullet
    private void OnTriggerEnter2D(Collider2D collision){
        var bullet = collision.GetComponent<Bullet>();
        var enemy = collision.GetComponent<Enemy>();

        if(bullet){
            Destroy(bullet.gameObject);
        }

        if(enemy){
            Destroy(enemy.gameObject);
        }
    }
}
