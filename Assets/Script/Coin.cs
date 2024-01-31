using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>() != null)
        {
            AudioManager.instance.PlaySfx(Sound.Coin);
            GameManager.instance.coin++;
            Destroy(gameObject);
        }
    }
}
