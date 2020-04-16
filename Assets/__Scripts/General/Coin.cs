using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // public fields
    public int CoinValue { get{ return coinValue; } }

    // delegate type to use for event
    public delegate void CoinCollected(Coin coin);

    // create static method to be implemented in the listener
    public static CoinCollected CoinCollectedEvent;

    // private variables
    [SerializeField] private int coinValue = 10;

    // private methods
    private void OnTriggerEnter2D(Collider2D collision)// whatHitMe is normally collision
    {
        // parameter = what ran into me
        // if the player hit, then destroy the player and the current enemy rectangle

        // the object that hit me. If it has a PlayerMOvement component then true
        //var coin = collision.GetComponent<CoinMovement>();
        var player = collision.GetComponent<PlayerMovement>();
        Debug.Log("Coin Collected");

        // if player != null
        if(player)
        {    
            Debug.Log("COIN!!!!!!!!!!!!!");
            
            Destroy(gameObject);
            PublishCoinCollectedEvent();
        }
    }    

    private void PublishCoinCollectedEvent()
    {
        // make sure somebody is listening
        if(CoinCollectedEvent != null){
            CoinCollectedEvent(this);            
        }
    }
}
