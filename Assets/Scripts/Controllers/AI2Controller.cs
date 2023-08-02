using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;


public class A2Controller : PlayerController
{
    [SerializeField] private TextMeshProUGUI betText;
    [SerializeField] private TextMeshProUGUI moneyText;
    public GameObject activering;
    private int totalMoney = 10000;
    private int currentBet = 0;
    private int lastBet = 0;

    public Image uiFill;
    public int Duration;
    private int remainingDuration;

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
        Debug.Log("Start AI2 turn");
         Begin(Duration);
    }

    void Begin(int Second) 
    {
      remainingDuration = Second;
      StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
      while(remainingDuration >= 0) 
      {
        uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
        remainingDuration--;
      yield return new WaitForSeconds(1f);
      }
        gameManager.StartNextTurn();
    }

    private void PlaceBet()
    {
        // Calculate the bet amount
        currentBet = (lastBet == 0) ? Random.Range(10, 21) : lastBet * 2;
        totalMoney -= currentBet;
        Debug.Log("AI2 bets: " + currentBet);

        // Update the bet and total money UI
        UpdateUI();
    }

    public override void EndTurn()
    {
        Debug.Log("End of AI2 turn");
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

