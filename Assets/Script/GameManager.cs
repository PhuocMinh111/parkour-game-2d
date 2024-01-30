using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public const string PLAYER_COLOR_PREF = "playerColor";
    public const string PLATFORM_COLOR_PREF = "playerColor";
    [SerializeField] private Player player;
    [SerializeField] Platform_generator Platform_Generator;
    [SerializeField] private Transform CameraLimiter;
    [SerializeField] private InGame_UI InGame_UI;
    [SerializeField] private TextMeshProUGUI coin_text;
    [SerializeField] private TextMeshProUGUI score_text;
    [SerializeField] private TextMeshProUGUI highest_score_text;
    public int coin;
    public float distance = 5;
    private float highestScrore;

    [HideInInspector] public int playerHealth;

    private void Awake()
    {
        instance = this;
        coin_text.text = PlayerPrefs.GetInt("coins", 0).ToString("#,#");
        score_text.text = PlayerPrefs.GetFloat("score", 0).ToString("#,#");
        highestScrore = PlayerPrefs.GetFloat("highestScore", 0);
        highest_score_text.text = highestScrore.ToString();

        ChangePlayerColor(LoadColor(PLAYER_COLOR_PREF));
    }

    private void Update()
    {


        CameraLimiter.transform.position = new Vector2(player.gameObject.transform.position.x, CameraLimiter.transform.position.y);


        if (distance < player.transform.position.x)
            distance = player.transform.position.x;
    }
    public void UpdateHealth(int health) => InGame_UI.UpdateHealth(health);
    public void PlayerUnlock() => player._begin = true;
    public void RestartLevel()
    {
        Save();
        SceneManager.LoadScene(0);
    }
    public void ChangeFlatformColor(Color color)
    {
        Transform[] levelparts = Platform_Generator.levelPart;
        for (int i = 0; i < levelparts.Length; i++)
        {
            Transform level = levelparts[i];
            for (int n = 0; n < level.childCount; n++)
            {
                SpriteRenderer sr = level.GetChild(n).GetComponent<SpriteRenderer>();
                if (sr == null)
                {
                    continue;
                }
                string sortingLayer = sr.sortingLayerName;
                if (sortingLayer == "Platfrom")
                {
                    level.GetChild(n).GetComponent<SpriteRenderer>().color = color;
                }
            }
        }
        SavePlatformColor(color);
    }

    public void ChangePlayerColor(Color color)
    {
        player.GetComponent<SpriteRenderer>().color = color;
        SavePlayerColor(color);
    }

    public bool EnoughMoney(int price)
    {
        int coins = PlayerPrefs.GetInt("coins", 0);
        if (coins > price)
        {

            PlayerPrefs.SetInt("coins", coins - price);
            return true;
        }
        return false;

    }
    private void Save()
    {
        int lastCoin = PlayerPrefs.GetInt("coins");
        PlayerPrefs.SetInt("coins", lastCoin + coin);
        float lastPoint = PlayerPrefs.GetFloat("point");
        PlayerPrefs.SetFloat("score", distance * coin);
        SavePlayerColor(player.GetComponent<SpriteRenderer>().color);
        if (distance * 9.8 > highestScrore)
        {

            PlayerPrefs.SetFloat("highestScore", distance);
            highest_score_text.text = highestScrore.ToString();
        }
    }

    public void SavePlayerColor(Color playerColor)
    {
        PlayerPrefs.SetString(PLAYER_COLOR_PREF, JsonUtility.ToJson(playerColor));
    }
    public void SavePlatformColor(Color platformColor)
    {
        PlayerPrefs.SetString(PLATFORM_COLOR_PREF, JsonUtility.ToJson(platformColor));
    }

    public Color LoadColor(string playerPrefName)
    {
        string colorJson = PlayerPrefs.GetString(playerPrefName);
        if (string.IsNullOrEmpty(colorJson)) return Color.white;
        Debug.Log(colorJson);
        Color color = JsonUtility.FromJson<Color>(colorJson);
        return color;
    }
}
