using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class MoneyData
{
    public int playerMoney;
    public int ai1Money;
    public int ai2Money;
}

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    public TMP_Text Total;
    public TMP_Text playerMoneyText;
    public TMP_Text ai1MoneyText;
    public TMP_Text ai2MoneyText;

    [HideInInspector] public int totalMoney = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadMoney();
    }

    // Method to update the total money and text
    public void UpdateTotalMoney(int amount)
    {
        totalMoney += amount;
        Total.text = totalMoney.ToString();
    }

    // Method to update player's money and text
    public void UpdatePlayerMoney(int amount)
    {
        int currentMoney = int.Parse(playerMoneyText.text);
        currentMoney += amount;
        playerMoneyText.text = currentMoney.ToString();
        SaveMoney();
    }

    // Method to update AI1's money and text
    public void UpdateAI1Money(int amount)
    {
        int currentMoney = int.Parse(ai1MoneyText.text);
        currentMoney += amount;
        ai1MoneyText.text = currentMoney.ToString();
        SaveMoney();
    }

    // Method to update AI2's money and text
    public void UpdateAI2Money(int amount)
    {
        int currentMoney = int.Parse(ai2MoneyText.text);
        currentMoney += amount;
        ai2MoneyText.text = currentMoney.ToString();
        SaveMoney();
    }

    // Save money data to PlayerPrefs as JSON
    private void SaveMoney()
    {
        MoneyData moneyData = new MoneyData
        {
            playerMoney = int.Parse(playerMoneyText.text),
            ai1Money = int.Parse(ai1MoneyText.text),
            ai2Money = int.Parse(ai2MoneyText.text)
        };

        string json = JsonUtility.ToJson(moneyData);
        PlayerPrefs.SetString("MoneyData", json);
    }

    // Load money data from PlayerPrefs and update UI
    private void LoadMoney()
    {
        if (PlayerPrefs.HasKey("MoneyData"))
        {
            string json = PlayerPrefs.GetString("MoneyData");
            MoneyData moneyData = JsonUtility.FromJson<MoneyData>(json);

            playerMoneyText.text = moneyData.playerMoney.ToString();
            ai1MoneyText.text = moneyData.ai1Money.ToString();
            ai2MoneyText.text = moneyData.ai2Money.ToString();
        }
    }
}
