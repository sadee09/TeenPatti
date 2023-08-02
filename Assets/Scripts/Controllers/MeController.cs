using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MeController : PlayerController
{
    public Button seeButton;
    public TMP_Text blindButtonText;
    public TextMeshProUGUI myText;
    public GameObject Blindbutton;
    public GameObject Panel;
    public GameObject addButton;
    public GameObject subButton;
    public TextMeshProUGUI total;
    public GameObject packButton;
    public GameObject activering;

    private bool AddActive;
    private bool SubActive;
    private bool seen;
    private int TotalMoney;

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
        // Disable the subtract button and set myText to 10
        subButton.SetActive(false);
        myText.text = 10.ToString();

        // Set the packButton to initially inactive
        packButton.SetActive(false);
        Panel.SetActive(false);
        activering.SetActive(false);
    }

    // Add method for doubling the value displayed in myText
    public void Add()
    {
        if (!SubActive)
        {
            int currentValue = int.Parse(myText.text);
            int newValue = currentValue * 2;
            myText.text = newValue.ToString();
            SubActive = true;
            AddActive = false;
            addButton.SetActive(false);
            subButton.SetActive(true);
        }
    }

    // Subtract method for halving the value displayed in myText
    public void Sub()
    {
        if (!AddActive)
        {
            int currentValue = int.Parse(myText.text);
            int newValue = currentValue / 2;
            myText.text = newValue.ToString();
            AddActive = true;
            SubActive = false;
            addButton.SetActive(true);
            subButton.SetActive(false);
        }
    }

    // Method called when the "See" button is clicked
    private void SeeButtonClicked()
    {
        seen = true;

        // Change the text of the blindButton when the "See" button is clicked
        blindButtonText.text = "Chal";

        // Double the value displayed in myText
        int currentValue;
        if (int.TryParse(myText.text, out currentValue))
        {
            myText.text = (currentValue * 2).ToString();
        }

        // Call the PackButton method to check if the packButton should be activated
        packButton.SetActive(true);
    }

    // Method to update the TotalMoney and the text displayed in total
    public void UpdateMoneyText()
    {
        int sourceMoney;
        if (int.TryParse(myText.text, out sourceMoney))
        {
            TotalMoney += sourceMoney;
            total.text = TotalMoney.ToString();
        }

        gameManager.StartNextTurn();
    }

    // Method called when the "Pack" button is clicked
    public void Pack()
    {
        // Check if the GameController instance exists and destroy the cards in the playerCardsList
        if (GameController.instance != null)
        {
            foreach (GameObject card in GameController.instance.playerCardsList)
            {
                Destroy(card);
            }
        }

        // starts next players turn
        gameManager.StartNextTurn();
    }

    public override void StartTurn()
    {
        Debug.Log("My turn started");
        Panel.SetActive(true);
        activering.SetActive(true);
    }

    public override void EndTurn()
    {
        Debug.Log("My turn ended");
        Panel.SetActive(false);
        activering.SetActive(false);
    }
}
