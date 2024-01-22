using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGame_UI : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    [SerializeField] private bool pauseGame;
    [SerializeField] TextMeshProUGUI coin;
    [SerializeField] TextMeshProUGUI distance;

    [SerializeField] Transform healthBar;

    private int health = 3;

    void Start()
    {

    }
    // Update is called once per frame
    private void UpdateInfo()
    {
        coin.text = GameManager.instance.coin.ToString();
        distance.text = "Point :" + GameManager.instance.distance.ToString("#,#");
    }
    private void FixedUpdate()
    {
        Debug.Log(GameManager.instance.playerHealth);
        UpdateInfo();



    }
    public void UpdateHealth(int health)
    {
        Debug.Log(health);
        for (int i = 0; i < healthBar.childCount; i++)
        {
            if (i > health - 1)
            {
                healthBar.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                healthBar.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void PauseGame()
    {
        if (pauseGame)
        {
            Time.timeScale = 1;
            pauseGame = false;
        }
        else
        {
            Time.timeScale = 0;
            pauseGame = true;
        }
    }
}
