using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    public TMP_Text Total;
    public TMP_Text playerMoney;
    public TMP_Text ai1Money;
    public TMP_Text ai2Money;

    [HideInInspector] public int totalMoney = 0;
    private static int startingPlayerMoney = 10000; 
    private static int startingAI1Money = 10000; // Initial player money value
    private static int startingAI2Money = 10000; // Initial player money value


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
        // Initialize player money to the starting value
        playerMoney.text = startingPlayerMoney.ToString();
        ai1Money.text = startingAI1Money.ToString(); 
        ai2Money.text = startingAI2Money.ToString();
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
        int currentMoney = int.Parse(playerMoney.text);
        currentMoney += amount;
        playerMoney.text = currentMoney.ToString();

        // Update the static starting player money value
        startingPlayerMoney = currentMoney;

    }

    // Method to update AI1's money and text
    public void UpdateAI1Money(int amount)
    {
        int currentMoney = int.Parse(ai1Money.text);
        currentMoney += amount;
        ai1Money.text = currentMoney.ToString();
        
        startingAI1Money = currentMoney;

    }

    // Method to update AI2's money and text
    public void UpdateAI2Money(int amount)
    {
        int currentMoney = int.Parse(ai2Money.text);
        currentMoney += amount;
        ai2Money.text = currentMoney.ToString();
        
        startingAI2Money = currentMoney;

    }
}