using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class AI1Controller : PlayerController
{
    [SerializeField] private TextMeshProUGUI betText;
    [SerializeField] private TextMeshProUGUI moneyText;
    public GameObject activering;
    private int totalMoney = 10000;
    private int currentBet = 0;
    private int lastBet = 0;
    private int turn = 0;
    public bool isSeen;
    public GameObject Seen;
    public bool hasPacked = false;
    public static AI1Controller instance;
    
    private int random;
    private MoneyManager moneyManager;
    
    public Button showBtn;
    public CanvasGroup canvasSideShow;

    public GameController gameController;
    public AI2Controller aI2Controller;

    private void Awake()
    {
        instance = this;
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
        int random = Random.Range(1, 100);
        int showrandom = Random.Range(1, 100);

        if (random < 50 && turn == 1 || random < 40 && turn == 2 || random < 30)
        {
            isSeen = false;
            PlaceBet();
        }
        else if (!isSeen && aI2Controller.isSeen && gameController.playerCardSeen && showrandom < 5 || !isSeen && !aI2Controller.isSeen && !gameController.playerCardSeen && showrandom < 5 || !isSeen && aI2Controller.isSeen && !gameController.playerCardSeen && showrandom < 5 || isSeen !&& aI2Controller.isSeen && gameController.playerCardSeen && showrandom < 5)
        {
            OnShow();
        }
        else
        {
            isSeen = true;
            Seen.gameObject.SetActive(true);
            
            if (aI2Controller.isSeen)
            {
                showBtn.gameObject.SetActive(true);
            }

            else if (gameController.playerCardSeen && gameController.hasPacked == false)
            {
                canvasSideShow.DOFade(1, 1.0f);
            }
            
            CardsEvaluator();
        }
    }

    private void CardsEvaluator()
    {
        int random = Random.Range(1, 100);
        int showrandom = Random.Range(1, 100);

        if (isSeen)
        {
            if (GameController.instance.ai1HandType == HandEvaluator.HandType.HighCard)
            {
                if ((random < 60 && turn == 1) || (random < 40 && turn == 2) || (random < 30))
                {
                    PlaceBet();
                }
                else if (aI2Controller.isSeen && gameController.playerCardSeen && showrandom < 10)
                {
                    OnShow();
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
                else if (aI2Controller.isSeen && gameController.playerCardSeen && showrandom < 10)
                {
                    OnShow();
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
                else if (aI2Controller.isSeen && gameController.playerCardSeen && showrandom < 10)
                {
                    OnShow();
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
                else if (aI2Controller.isSeen && gameController.playerCardSeen && showrandom < 10)
                {
                    OnShow();
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
                else if (aI2Controller.isSeen && gameController.playerCardSeen && showrandom < 10)
                {
                    OnShow();
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
                else if (aI2Controller.isSeen && gameController.playerCardSeen && showrandom < 10)
                {
                    OnShow();
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
            currentBet = Random.Range(0, 2) == 0 ? 10 : 20;
        }
        else
        {
            currentBet = lastBet * 2;
        }

        totalMoney -= currentBet;
        Debug.Log("AI1 bets: " + currentBet);

        UpdateUI();

        gameManager.StartNextTurn();
    }

    public void Pack()
    {
        hasPacked = true;
        Seen.gameObject.SetActive(false);
        Debug.Log("Pack");

        if (GameController.instance != null)
        {
            foreach (GameObject card in GameController.instance.ai1CardsList)
            {
                card.SetActive(false);
            }
        }
        canvasSideShow.DOFade(0, 1.0f);

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

    private IEnumerator WinnerText()
    {
        yield return new WaitForSeconds(2.5f);
        gameController.DetermineWinningHand();
    }

    public void OnShow()
    {
        StartCoroutine(gameController.RotateCardsList(gameController.playerCardsList));
        StartCoroutine(gameController.RotateCardsList(gameController.ai1CardsList));
        StartCoroutine(gameController.RotateCardsList(gameController.ai2CardsList));
        PlaceBet();
        GameController.instance.seeBtn.interactable = false;
        gameManager.StartNextTurn();
        StartCoroutine(WinnerText());
        gameController.RestartGame();
    }
}