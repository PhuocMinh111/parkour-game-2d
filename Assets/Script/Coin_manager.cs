using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject coin;
    [SerializeField] private int maxCoin;
    [SerializeField] private int minCoin = 3;
    [SerializeField] private float coin_spacing;
    [SerializeField] private float changeToSpawn = 50;
    private float levelWidth;
    void Start()
    {
        int spawnChange = Random.Range(0, 100);
        if (spawnChange < changeToSpawn)
        {

            for (int i = 0; i < 5; i++)
            {
                int coin_number = Random.Range(minCoin, 5);
                Vector2 spawnPosition = new Vector2(transform.position.x + (i - coin_number / 2) * coin_spacing, transform.position.y);
                Instantiate(coin, spawnPosition, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame

}
