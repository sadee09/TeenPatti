using UnityEngine;
using TMPro;
using System.Collections;

public class AI1Controller : PlayerController
{
    [SerializeField] private TextMeshProUGUI betText;
    [SerializeField] private TextMeshProUGUI moneyText;
    public GameObject activering;
    private int totalMoney = 10000;
    private int currentBet = 0;
    private int lastBet = 0;

    private void Awake()
    {
        gameManager = FindObjectOfType<PlayerManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager script not found in the scene!");
        }
        else
        {
            gameManager.AddPlayer(this);
        }
    }

    void Start()
    {
        activering.SetActive(false);
    }

    private IEnumerator PerformAITurn()
    {
        float thinkingTime = Random.Range(3f, 10f); // Random thinking time between 3 to 10 seconds
        yield return new WaitForSeconds(thinkingTime);

        // Place the bet after the coroutine time is over
        PlaceBet();

        // Simulate end of turn and notify the GameManager
        gameManager.StartNextTurn();
    }

    public override void StartTurn()
    {
        StartCoroutine(PerformAITurn());
        activering.SetActive(true);
        Debug.Log("Start AI1 turn");
    }

    private void PlaceBet()
    {
        // Calculate the bet amount
        currentBet = (lastBet == 0) ? Random.Range(10, 21) : lastBet * 2;
        totalMoney -= currentBet;
        Debug.Log("AI1 bets: " + currentBet);

        // Update the bet and total money UI
        UpdateUI();
    }

    public override void EndTurn()
    {
        Debug.Log("End of AI1 turn");
        activering.SetActive(false);
    }

    // Helper method to update the bet and total money UI
    private void UpdateUI()
    {
        if (betText != null)
            betText.text = currentBet.ToString();

        if (moneyText != null)
            moneyText.text = totalMoney.ToString();
    }
}
