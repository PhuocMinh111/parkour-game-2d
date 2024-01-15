using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public int coin;


    private void Awake()
    {
        instance = this;
    }


    public void RestartLevel() => SceneManager.LoadScene(0);
}
