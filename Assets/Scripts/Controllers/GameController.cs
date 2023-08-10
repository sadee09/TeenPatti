using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
  public GameObject cards;
  public Transform tf_BoxCard;
  public Transform[] arr_Tf_player, arr_Tf_AI1, arr_Tf_AI2;
  public List<GameObject> listCard = new List<GameObject>();
  public List<GameObject> playerCardsList = new List<GameObject>();
  public List<GameObject> ai1CardsList = new List<GameObject>();
  public List<GameObject> ai2CardsList = new List<GameObject>();
  public static GameController instance;
  public bool hasPacked = false;

  public CanvasGroup canvasSee;
  public CanvasGroup canvasSideShow;
  public bool playerCardSeen = false;
  public CanvasGroup canvasPlayerPanel;
  public GameObject playerPanel;

  public GameObject gameGirl;
  public GameObject gameGirlSit;

  public float fadeTime = 1f;

  public Button seeBtn;
  public Button sideShowBtn;

  public TMP_Text winnerText;

  public HandEvaluator.HandType ai1HandType;
  public HandEvaluator.HandType ai2HandType;
  public MeController meController;
  public AI1Controller ai1Controller;
  public AI2Controller ai2Controller;
  public PlayerManager playerManager;

  private Dictionary<string, System.Action<int>> moneyUpdateMethods = new Dictionary<string, System.Action<int>>();
  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
  }

  void Start()
  {
    if (SpriteGame.instance != null)
    {
        InstanceCard();
    }
    else
    {
        StartCoroutine(WaitForCardLoading());
    }
    playerPanel.GetComponent<CanvasGroup>().interactable = false;
    seeBtn.interactable = false;
    sideShowBtn.interactable = false;
    
  }

  IEnumerator WaitForCardLoading()
  {
    while (SpriteGame.instance == null)
    {
        yield return null;
    }

    InstanceCard();
  }

  public void InstanceCard()
  {
    for (int i = 0; i < SpriteGame.instance.arr_Cards.Length; i++)
    {
        GameObject card = Instantiate(cards, tf_BoxCard.position, Quaternion.identity);
        card.transform.SetParent(tf_BoxCard, false);
        card.GetComponent<UICard>().Front_Cards.sprite = SpriteGame.instance.arr_Cards[i];
        listCard.Add(card);
    }
    DistributeCardsToPlayers();
  }

  private void DistributeCardsToPlayers()
  {
    for (int i = 0; i < 3; i++)
    {
  
      int rdPlayer = Random.Range(0, listCard.Count);
      playerCardsList.Add(listCard[rdPlayer]);
      listCard.RemoveAt(rdPlayer);
  
      int rdAI1 = Random.Range(0, listCard.Count);
      ai1CardsList.Add(listCard[rdAI1]);
      listCard.RemoveAt(rdAI1);
  
      int rdAI2 = Random.Range(0, listCard.Count);
      ai2CardsList.Add(listCard[rdAI2]);
      listCard.RemoveAt(rdAI2);
    }
  
    StartCoroutine(SplitCards());
  }
  private void DistributeSpecificCardsToPlayers(List<int> playerIndices, List<int> ai1Indices, List<int> ai2Indices)
  {
      foreach (int index in playerIndices)
      {
          playerCardsList.Add(listCard[index]);
      }

      foreach (int index in ai1Indices)
      {
          ai1CardsList.Add(listCard[index]);
      }

      foreach (int index in ai2Indices)
      {
          ai2CardsList.Add(listCard[index]);
      }
      StartCoroutine(SplitCards());
  }


  IEnumerator SplitCards()
  {
    for (int i = 0; i < 3; i++)
    {
      yield return new WaitForSeconds(0.5f);
      gameGirl.SetActive(true);
      MoveCardToTransform(ai1CardsList[i], arr_Tf_AI1[i]);
      gameGirl.SetActive(false);
      gameGirlSit.SetActive(true);
        
      yield return new WaitForSeconds(0.5f);
      MoveCardToTransform(playerCardsList[i], arr_Tf_player[i]);
      gameGirl.SetActive(true);
      gameGirlSit.SetActive(false);

      yield return new WaitForSeconds(0.5f);
      MoveCardToTransform(ai2CardsList[i], arr_Tf_AI2[i]);

    }

    yield return new WaitForSeconds(0.5f);
    gameGirl.SetActive(false);
    gameGirlSit.SetActive(true);
    playerPanel.GetComponent<CanvasGroup>().interactable = true;
      
    canvasSee.DOFade(1, fadeTime);
  }

  private void MoveCardToTransform(GameObject card, Transform targetTransform)
  {
    card.transform.SetParent(targetTransform, true);
    iTween.MoveTo(card, iTween.Hash(
        "position", targetTransform.position,
        "easeType", iTween.EaseType.linear,
        "time", 0.4f));
  }

  public void RotatePlayerCard()
  {
    StartCoroutine(RotateCardsList(playerCardsList));
    playerCardSeen = true;
  }
    
  public void SideShowCard()
  {
    StartCoroutine(RotateCardsList(ai1CardsList));
    HandEvaluator.HandType ai1HandType = HandEvaluator.GetHandType(ai1CardsList);
    HandEvaluator.HandType playerHandType = HandEvaluator.GetHandType(playerCardsList);

    Debug.Log(ai1HandType);
    Debug.Log(playerHandType);

    if (ai1HandType > playerHandType)
    {
      Debug.Log("AI1 has higher cards");
        PackCard(playerCardsList);
        meController.UpdateMoneyText();
        playerManager.PlayerPack();
    }
    else if (playerHandType > ai1HandType)
    {
      Debug.Log("Player has higher cards");
        PackCard(ai1CardsList);
        meController.UpdateMoneyText();
        playerManager.PlayerPack(1);
    }
    else
    {
      Debug.Log("else block is running");
        // If hand types are equal, determine the highest card value
        UICard ai1HighestCard = GetHighestCard(ai1CardsList);
        UICard playerHighestCard = GetHighestCard(playerCardsList);

        if (ai1HighestCard.value > playerHighestCard.value)
        {
            PackCard(playerCardsList);
            meController.UpdateMoneyText();
            playerManager.PlayerPack();
        }
        else
        {
            PackCard(ai1CardsList);
            meController.UpdateMoneyText();
            playerManager.PlayerPack(1);
        }
    }
  }

  public IEnumerator RotateCardsList(List<GameObject> cardsList)
  {
    for (int i = 0; i < cardsList.Count; i++)
    {
      yield return new WaitForSeconds(0.6f);
      iTween.RotateBy(cardsList[i], iTween.Hash(
          "-y", 0.5f,
          "easeType", "easeInOutQuad",
          "loopType", "none",
          "time", 0.1f
      ));

      cardsList[i].GetComponent<UICard>().gob_BackCard.SetActive(false);
    }
  }
    

  public void Pack()
  {
      playerPanel.SetActive(false);
      hasPacked = true;
      PackCard(playerCardsList);
  }
    
  public void PackCard(List<GameObject> cardsToPack)
  {
    StartCoroutine(PackCardsToDeck(cardsToPack));
  }

  private IEnumerator PackCardsToDeck(List<GameObject>cardsToPack)
  {
    canvasSee.DOFade(0, fadeTime);
    canvasSideShow.DOFade(0, fadeTime);

    gameGirlSit.SetActive(false);
    gameGirl.SetActive(true);

    yield return new WaitForSeconds(1.5f);
    foreach (var card in cardsToPack)
    {
        yield return new WaitForSeconds(0.5f);
        MoveCardToTransform(card, tf_BoxCard);
    }

    gameGirl.SetActive(false);
    gameGirlSit.SetActive(true);
  }

  public void OnSettingsButtonClick()
  {
        SettingsUI.GetInstance().PanelIn();
  }
    public void DetermineWinningHand()
    {
       
        playerCardsList = HandEvaluator.Sorter.GetSortedCards(playerCardsList);
        ai1CardsList = HandEvaluator.Sorter.GetSortedCards(ai1CardsList);
        ai2CardsList = HandEvaluator.Sorter.GetSortedCards(ai2CardsList);

        HandEvaluator.HandType playerHandType = HandEvaluator.GetHandType(playerCardsList);
        HandEvaluator.HandType ai1HandType = HandEvaluator.GetHandType(ai1CardsList);
        HandEvaluator.HandType ai2HandType = HandEvaluator.GetHandType(ai2CardsList);
        
        moneyUpdateMethods.Add("You", MoneyManager.instance.UpdatePlayerMoney);
        moneyUpdateMethods.Add("AI1", MoneyManager.instance.UpdateAI1Money);
        moneyUpdateMethods.Add("AI2", MoneyManager.instance.UpdateAI2Money);

        if (ai2Controller.hasPacked)
        {
            CompareWhenPacked(playerCardsList, ai1CardsList, "You", "AI1");
        }
        else if (ai1Controller.hasPacked)
        {
            CompareWhenPacked(playerCardsList, ai2CardsList, "You", "AI2");
        }
        else if (instance.hasPacked)
        {
            CompareWhenPacked(ai1CardsList, ai2CardsList, "AI1", "AI2");
        }
       
        else if (playerHandType > ai1HandType && playerHandType > ai2HandType && instance.hasPacked == false)
        {
         winnerText.text = "You Win : " + playerHandType;
         MoneyManager.instance.UpdatePlayerMoney(MoneyManager.instance.totalMoney);
        }
        else if (ai1HandType > playerHandType && ai1HandType > ai2HandType && ai1Controller.hasPacked == false)
        {
            winnerText.text = "AI1 Wins : " + ai1HandType;
            MoneyManager.instance.UpdateAI1Money(MoneyManager.instance.totalMoney);
        }
        else if (ai2HandType > playerHandType && ai2HandType > ai1HandType && ai2Controller.hasPacked == false)
        {
            winnerText.text = "AI2 Wins : " + ai2HandType;
            MoneyManager.instance.UpdateAI2Money(MoneyManager.instance.totalMoney);
        }
        else if (ai1HandType == playerHandType)
        {
            DetermineTieBreaker(playerCardsList, ai1CardsList, "You", "AI1");
        }
        else if (ai1HandType == ai2HandType || instance.hasPacked)
        {
            DetermineTieBreaker(ai1CardsList, ai2CardsList, "AI1", "AI2");
        }
        else if (ai2HandType == playerHandType || ai1Controller.hasPacked)
        {
            DetermineTieBreaker(playerCardsList, ai2CardsList, "You", "AI2");
        }
    }


    private void CompareWhenPacked(List<GameObject> hand1, List<GameObject> hand2, string hand1Name, string hand2Name)
    {
        HandEvaluator.HandType hand1Type = HandEvaluator.GetHandType(hand1);
        HandEvaluator.HandType hand2Type = HandEvaluator.GetHandType(hand2);

        if (hand1Type > hand2Type)
        {
            winnerText.text = hand1Name + " Win : " + hand1Type;
            moneyUpdateMethods[hand1Name](MoneyManager.instance.totalMoney);

        }
        else if (hand2Type > hand1Type)
        {
            winnerText.text = hand2Name + " Wins : " + hand2Type;
            moneyUpdateMethods[hand2Name](MoneyManager.instance.totalMoney);

        }
        else
        {
            DetermineTieBreaker(hand1, hand2, hand1Name, hand2Name);
        }
    }
    
    private void DetermineTieBreaker(List<GameObject> cards1, List<GameObject> cards2, string winnerLabel1,
        string winnerLabel2)
    {
        UICard highestCard1 = GetHighestCard(cards1);
        UICard highestCard2 = GetHighestCard(cards2);

        if (highestCard1.value > highestCard2.value)
        {
            winnerText.text = winnerLabel1 + " Win : Highest Card";
            moneyUpdateMethods[winnerLabel1](MoneyManager.instance.totalMoney);
        }
        else if (highestCard2.value > highestCard1.value)
        {
            winnerText.text = winnerLabel2 + " Win : Highest Card";
            moneyUpdateMethods[winnerLabel2](MoneyManager.instance.totalMoney);
        }
    }

    private UICard GetHighestCard(List<GameObject> cards)
    {
        UICard highestCard = null;
        foreach (var card in cards)
        {
            UICard uiCard = card.GetComponent<UICard>();
            if (highestCard == null || uiCard.value > highestCard.value)
            {
                highestCard = uiCard;
            }
            else if(uiCard.value == highestCard.value)
            {
                int nextCardIndex = (cards.IndexOf(card) + 1) % 3;
                UICard nextCard = cards[nextCardIndex].GetComponent<UICard>();
                if (nextCard.value > highestCard.value)
                {
                    highestCard = nextCard;
                }
            }
        }
        return highestCard;
    }

  public void EndGame()
  {
    DetermineWinningHand();
  }

  public void RestartGame()
  {
    StartCoroutine(RestartAfterDelay(7.0f));
  }

  private IEnumerator RestartAfterDelay(float delayInSeconds)
  {
    yield return new WaitForSeconds(delayInSeconds);

    // Restart the game after the delay
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}