using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGame_UI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI coin;
    [SerializeField] private TextMeshProUGUI point;
    private GameManager gameManager;
    void Awake()
    {
        // coin = transform.Find("Coin").GetComponent<TextMeshProUGUI>();
        // point = transform.Find("Point").GetComponent<TextMeshProUGUI>();
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    public void UpdatePoint()
    {
        string coinStr = gameManager.coin > 0 ? gameManager.coin.ToString() : "0";
        coin.text = "Coins: " + coinStr;
        point.text = "Point: " + gameManager.score.ToString();
    }
}
