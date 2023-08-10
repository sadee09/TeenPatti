using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    public TMP_Text Total;
    public TMP_Text playerMoney;
    public TMP_Text ai1Money;
    public TMP_Text ai2Money;

    [HideInInspector] public int totalMoney = 0; // Store the total money in an int variable

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
    }

    // Method to update AI1's money and text
    public void UpdateAI1Money(int amount)
    {
        int currentMoney = int.Parse(ai1Money.text);
        currentMoney += amount;
        ai1Money.text = currentMoney.ToString();
    }

    // Method to update AI2's money and text
    public void UpdateAI2Money(int amount)
    {
        int currentMoney = int.Parse(ai2Money.text);
        currentMoney += amount;
        ai2Money.text = currentMoney.ToString();
    }
}
