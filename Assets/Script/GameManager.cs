using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;

    [SerializeField] private Player player;
    [SerializeField] private Transform CameraLimiter;
    [SerializeField] private InGame_UI InGame_UI;
    public int coin;
    public float distance = 5;
    [HideInInspector] public int playerHealth;

    private void Awake()
    {
        instance = this;

    }

    private void Update()
    {


        CameraLimiter.transform.position = new Vector2(player.gameObject.transform.position.x, CameraLimiter.transform.position.y);


        if (distance < player.transform.position.x)
            distance = player.transform.position.x;
    }
    public void UpdateHealth(int health) => InGame_UI.UpdateHealth(health);
    public void PlayerUnlock() => player._begin = true;
    public void RestartLevel() => SceneManager.LoadScene(0);
}
