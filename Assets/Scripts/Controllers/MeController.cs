using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using JetBrains.Annotations;

public class MeController : PlayerController
{
    public Button seeButton;
    public TMP_Text blindButtonText;
    public TextMeshProUGUI myText;
    private int totalMoney = 10000;
    public GameObject Blindbutton;
    public GameObject Panel;
    public GameObject addButton;
    public GameObject subButton;
    public GameObject packButton;
    public GameObject activering;
    public Button seeBtn;
    public Button sideShowBtn;
    public CanvasGroup canvasSideShow;
    public bool seen;
    public TextMeshProUGUI total;
    public GameController gameController;
    public AI1Controller ai1Controller;
    public static MeController instance;
    private int newValue;
    
    private void Awake()
    {
        // Add a listener to the "See" button so it can react to clicks
        seeButton.onClick.AddListener(SeeButtonClicked);

        gameManager = FindObjectOfType<PlayerManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager script not found in the scene!");
        }
        else
        {
            // Register this player with the GameManager
            gameManager.AddPlayer(this);
        }
    }

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        myText.text = 10.ToString();
        total.text = totalMoney.ToString();
        packButton.SetActive(false);
        activering.SetActive(false);
        Panel.SetActive(false);
        subButton.SetActive(false);
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        int currentValue = int.Parse(myText.text);

        addButton.SetActive(currentValue + 1000 <= totalMoney);
        subButton.SetActive(currentValue - 10 >= 10);
        Blindbutton.SetActive(currentValue < totalMoney);
    }

    public void Add()
    {
        int currentValue = int.Parse(myText.text);
        newValue = currentValue + 100;

        if (newValue <= totalMoney)
        {
            myText.text = newValue.ToString();
        }

        UpdateButtons();
    }

    public void Sub()
    {
        int currentValue = int.Parse(myText.text);
        int newValue = currentValue - 100;

        if (newValue >= 10)
        {
            myText.text = newValue.ToString();
        }

        UpdateButtons();
    }

    private void SeeButtonClicked()
    {
        seen = true;

        if (ai1Controller.isSeen && ai1Controller.hasPacked == false)
        {
            canvasSideShow.DOFade(1, 1.0f);
        }

        blindButtonText.text = "Chal";
        PackButton();
    }

    public void UpdateMoneyText()
    {
        int sourceMoney;
        if (int.TryParse(myText.text, out sourceMoney))
        {
            totalMoney -= sourceMoney;
            total.text = totalMoney.ToString();
            MoneyManager.instance.UpdateTotalMoney(sourceMoney);
        }

        gameManager.StartNextTurn();
    }

    public void UpdateMoneyWhenShow()
    {
        int sourceMoney;
        if (int.TryParse(myText.text, out sourceMoney))
        {
            MoneyManager.instance.UpdateTotalMoney(sourceMoney);
        }
    }

    public void PackButton()
    {
        if (seen)
        {
            packButton.SetActive(true);
        }
    }

    public void Pack()
    {
        if (GameController.instance != null)
        {
            foreach (GameObject card in GameController.instance.playerCardsList)
            {
                card.SetActive(false);
            }
        }
        canvasSideShow.DOFade(1, 1.0f);
        gameManager.StartNextTurn();
        gameManager.PlayerPack();
    }

    public void OnShow()
    {
        seeBtn.gameObject.SetActive(false);
        sideShowBtn.gameObject.SetActive(false);
        StartCoroutine(gameController.RotateCardsList(gameController.playerCardsList));
        StartCoroutine(gameController.RotateCardsList(gameController.ai1CardsList));
        StartCoroutine(gameController.RotateCardsList(gameController.ai2CardsList));
        UpdateMoneyWhenShow();
        StartCoroutine(WinnerText());
        gameController.RestartGame();
    }

    private IEnumerator WinnerText()
    {
        yield return new WaitForSeconds(2.5f);
        gameController.DetermineWinningHand();
        Panel.SetActive(false);
    }

    public override void StartTurn()
    {
        Panel.SetActive(true);
        seeBtn.interactable = true;
        sideShowBtn.interactable = true;
        activering.SetActive(true);
        Debug.Log("Start of players turn");
        newValue = 10;
        myText.text = newValue.ToString();
    }

    public override void EndTurn()
    {
        Panel.SetActive(false);
        activering.SetActive(false);
        sideShowBtn.interactable = false;
        Debug.Log("End of players turn");
    }
}
