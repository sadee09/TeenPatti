using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    public TMP_Text Total;
    public TMP_Text playerMoney;
    public TMP_Text ai1Money;
    public TMP_Text ai2Money;
    
    private int totalMoney = 0; // Store the total money in an int variable

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

    public void MoneyPot()
    {

    }
}
