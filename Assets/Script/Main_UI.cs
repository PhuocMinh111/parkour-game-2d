using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_UI : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject endgame_UI;
    private void Start()
    {
        endgame_UI = transform.Find("EndGame_UI").gameObject;
    }
    public void SwitchMenuTo(GameObject ui)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        ui.gameObject.SetActive(true);
    }
    public void StartGame() => GameManager.instance.PlayerUnlock();


    public void SwitchToEndgame()
    {
        SwitchMenuTo(endgame_UI);
        endgame_UI.GetComponent<EndGame_UI>().UpdatePoint();
    }
}
