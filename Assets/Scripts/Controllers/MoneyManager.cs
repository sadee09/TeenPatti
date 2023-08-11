using UnityEngine;
using TMPro;

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
        UpdateTotalMoney(0); // Update the total money initially
    }

    public void UpdateTotalMoney(int amount)
    {
        totalMoney += amount;
        Total.text = totalMoney.ToString();
    }

    public void UpdatePlayerMoney(int amount)
    {
        int currentMoney = int.Parse(playerMoneyText.text);
        currentMoney += amount;
        playerMoneyText.text = currentMoney.ToString();
        UpdateTotalMoney(amount);
    }

    public void UpdateAI1Money(int amount)
    {
        int currentMoney = int.Parse(ai1MoneyText.text);
        currentMoney += amount;
        ai1MoneyText.text = currentMoney.ToString();
        UpdateTotalMoney(amount);
    }

    public void UpdateAI2Money(int amount)
    {
        int currentMoney = int.Parse(ai2MoneyText.text);
        currentMoney += amount;
        ai2MoneyText.text = currentMoney.ToString();
        UpdateTotalMoney(amount);
    }
}
