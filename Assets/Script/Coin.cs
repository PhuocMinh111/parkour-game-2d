using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : Notifier
{
    // Start is called before the first frame update
     private Player player;
    private void OnEnable ()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>() != null)
        {

            player.collectCoin.Invoke();
            GameManager.instance.coin++;
            Destroy(gameObject);
        }
    }
}
