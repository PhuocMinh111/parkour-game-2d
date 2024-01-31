using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
struct ColorToSell
{
    public Color color;
    public int price;
}

[Serializable]
struct PlayerColor
{
    public Color color;
    public int price;
}
public enum PurchaseType
{
    playerPurchase,
    platFormPurchase
}
public class Shop_UI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI notifyTxt;
    [Header("Platform color")]
    [SerializeField] private ColorToSell[] PlatformColorList;
    [SerializeField] private GameObject PlatformColorButton;
    [SerializeField] private Transform PlatformParent;
    [SerializeField] private Image PlatformImage;

    [Header("Player color")]
    [SerializeField] private PlayerColor[] PlayerColorList;
    [SerializeField] private GameObject PlayerColorButton;
    [SerializeField] private Transform PlayerParent;
    [SerializeField] private Image PlayerImage;


    void Start()
    {
        coinText.text = PlayerPrefs.GetInt("coins", 0).ToString();
        PlayerImage.color = GameManager.instance.LoadColor(GameManager.PLAYER_COLOR_PREF);
        for (int i = 0; i < PlatformColorList.Length; i++)
        {
            Color color = PlatformColorList[i].color;
            int price = PlatformColorList[i].price;
            GameObject newButton = Instantiate(PlatformColorButton, PlatformParent);
            Debug.Log(newButton.GetComponent<Button>());
            // newButton.GetComponent<Button>().onClick.AddListener(TaskOnClick);
            newButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = price.ToString();
            newButton.transform.GetChild(1).GetComponent<Image>().color = color;
            newButton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(color, price, PurchaseType.platFormPurchase));
        }
        for (int i = 0; i < PlayerColorList.Length; i++)
        {
            Color color = PlayerColorList[i].color;
            int price = PlayerColorList[i].price;
            GameObject newButton = Instantiate(PlayerColorButton, PlayerParent);

            newButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = price.ToString();
            newButton.transform.GetChild(1).GetComponent<Image>().color = color;
            newButton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(color, price, PurchaseType.playerPurchase));
        }


    }

    // Update is called once per frame
    private void PurchaseColor(Color color, int price, PurchaseType purchaseType)
    {
        Debug.Log("change color");
        if (GameManager.instance.EnoughMoney(price))
        {
            coinText.text = PlayerPrefs.GetInt("coins").ToString();
            Notify("Purchase Successfully !", 3);
            if (purchaseType == PurchaseType.platFormPurchase)
            {
                GameManager.instance.ChangeFlatformColor(color);
                PlatformImage.color = color;
            }
            else if (purchaseType == PurchaseType.playerPurchase)
            {
                GameManager.instance.ChangePlayerColor(color);
                PlayerImage.color = color;

            }
        }
        else
        {
            Notify("Not Enough coin !", 3);
        }
    }



    IEnumerator Notify(string text, float delay)
    {
        notifyTxt.text = text;
        yield return new WaitForSeconds(delay);
        notifyTxt.text = "Tap To Buy";
    }

}
