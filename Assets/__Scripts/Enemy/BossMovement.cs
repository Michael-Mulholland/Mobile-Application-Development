using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BossMovement : MonoBehaviour
{
    // == private fields ==
    private Rigidbody2D rb;
    [SerializeField]  private float speed = 5f;

    // boss direction ( left - right )
    private bool direction = true;

    // Start is called before the first frame update
    void Start()
    {
         // gets run once
        rb = GetComponent<Rigidbody2D>();       
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, -1 * speed);

        // https://answers.unity.com/questions/690884/how-to-move-an-object-along-x-axis-between-two-poi.html
        if(rb.transform.position.y <= 3.5f){
            rb.velocity = new Vector2(0, 0);  

            if (direction){
                transform.Translate (Vector2.right * speed * Time.deltaTime,0);
            }else{
                transform.Translate (-Vector2.right * speed * Time.deltaTime,0);
            }
            if(transform.position.x >= 2.2f) {
                direction = false;
            }
            
            if(transform.position.x <= -2.2) {
                direction = true;
            }      
        }
    }
}