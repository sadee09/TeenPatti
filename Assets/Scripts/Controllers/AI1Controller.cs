using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class AI1Controller : PlayerController
{
    [SerializeField] private TextMeshProUGUI betText;
    [SerializeField] private TextMeshProUGUI moneyText;
    public GameObject activering;
    private int totalMoney = 10000;
    private int currentBet = 0;
    private int lastBet = 0;
    private int turn = 0;
    private bool isSeen;
    private int random;
    private MoneyManager moneyManager;
    private GameController gameController;

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
        random = Random.Range(1, 100);
    }

    private IEnumerator PerformAITurn()
    {
        float thinkingTime = Random.Range(3f, 10f);
        yield return new WaitForSeconds(thinkingTime);
        seeCards();
    }

    public override void StartTurn()
    {
        StartCoroutine(PerformAITurn());
        activering.SetActive(true);
        Debug.Log("Start AI1 turn");
        turn += 1;
        Debug.Log(turn);
    }

    private void seeCards()
    {
        if (random < 50 && turn == 1)
        {
            isSeen = false;
            PlaceBet();
        }
        else if( random < 30 && turn == 2) 
        {
            isSeen = false;
            PlaceBet();
        }
        else if (random < 10 && turn == 3)
        {
            isSeen = false;
            PlaceBet();
        }
        else
        {
            isSeen = true;
            CardsEvaluator();
        }
    }

    private void CardsEvaluator()
    {
        if (isSeen)
        {
            if (GameController.instance.ai1HandType == HandEvaluator.HandType.HighCard)
            {
                if ((random < 60 && turn == 1) || (random < 40 && turn == 2) || (random < 30))
                {
                    PlaceBet();
                }
                else 
                {
                    Pack();
                }
            }
            else if (GameController.instance.ai1HandType == HandEvaluator.HandType.Pair)
            {
                if ((random < 95 && turn == 1) || (random < 90 && turn == 2) || (random < 80))
                {
                    PlaceBet();
                }
                else
                {
                    Pack();
                }
            }
            else if (GameController.instance.ai1HandType == HandEvaluator.HandType.Color)
            {
                if ((random < 100 && turn == 1) || (random < 95 && turn == 2) || (random < 85))
                {
                    PlaceBet();
                }
                else
                {
                    Pack();
                }
            }
            else if (GameController.instance.ai1HandType == HandEvaluator.HandType.Sequence)
            {
                if ((random < 100 && turn == 1) || (random < 98 && turn == 2) || (random < 95))
                {
                    PlaceBet();
                }
                else
                {
                    Pack();
                }
            }
            else if (GameController.instance.ai1HandType == HandEvaluator.HandType.PureSequence)
            {
                if ((random < 100 && turn == 1) || (random < 100 && turn == 2) || (random < 95))
                {
                    PlaceBet();
                }
                else
                {
                    Pack();
                }
            }
            else if (GameController.instance.ai1HandType == HandEvaluator.HandType.Trail)
            {
                if ((random < 100 && turn == 1) || (random < 100 && turn == 2) || (random < 98))
                {
                    PlaceBet();
                }
                else
                {
                    Pack();
                }
            }
        }
    }

    private void PlaceBet()
    {
        if (lastBet == 0)
        {
            // If it's the first bet, choose randomly between 10 and 20
            currentBet = Random.Range(0, 2) == 0 ? 10 : 20;
        }
        else
        {
            // If it's not the first bet, double the last bet
            currentBet = lastBet * 2;
        }

        totalMoney -= currentBet;
        Debug.Log("AI1 bets: " + currentBet);

        UpdateUI();

        gameManager.StartNextTurn();
    }

    public void Pack()
    {
        Debug.Log("Pack");

        if (GameController.instance != null)
        {
            foreach (GameObject card in GameController.instance.ai1CardsList)
            {
                card.SetActive(false);
            }
        }
        gameManager.StartNextTurn();

        gameManager.PlayerPack();
    }

    public override void EndTurn()
    {
        Debug.Log("End of AI1 turn");
        activering.SetActive(false);
    }

    private void UpdateUI()
    {
        if (betText != null)
            betText.text = currentBet.ToString();
            MoneyManager.instance.UpdateTotalMoney(currentBet);

        if (moneyText != null)
            moneyText.text = totalMoney.ToString();
    }
}