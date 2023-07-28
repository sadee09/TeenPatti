
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
  public GameObject cards;
  public Transform tf_BoxCard;
  public Transform[] arr_Tf_player, arr_Tf_AI1, arr_Tf_AI2;
  public List<GameObject> listCard = new List<GameObject>();
  public List<GameObject> playerCardsList = new List<GameObject>();
  public List<GameObject> ai1CardsList = new List<GameObject>();
  public List<GameObject> ai2CardsList = new List<GameObject>();

  public CanvasGroup canvasSee;
  public CanvasGroup canvasSideShow;
  public CanvasGroup canvasPlayerPanel;
  public GameObject playerPanel;

  public GameObject gameGirl;
  public GameObject gameGirlSit;

  public float fadeTime = 1f;

  public Button packBtn;
  public Button blindBtn;
  public Button seeBtn;
  public Button sideShowBtn;
  public Button chaalBtn;
 

  public enum PlayerType{
    player,
    ai1,
    ai2
  }
  public List<PlayerType> playerTypes = new List<PlayerType>();

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
        card.GetComponent<UICard>().value = i + 1;
        //card.GetComponent<UICard>().suit = GetCardSuit(SpriteGame.instance.arr_Cards[i]); 
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

  IEnumerator SplitCards()
  {
    for (int i = 0; i < 3; i++)
    {
      yield return new WaitForSeconds(0.5f);
      MoveCardToTransform(playerCardsList[i], arr_Tf_player[i]);
      gameGirlSit.SetActive(true);
      gameGirl.SetActive(false);


      yield return new WaitForSeconds(0.5f);
      MoveCardToTransform(ai1CardsList[i], arr_Tf_AI1[i]);
      gameGirlSit.SetActive(false);
      gameGirl.SetActive(true);


      yield return new WaitForSeconds(0.5f);
      MoveCardToTransform(ai2CardsList[i], arr_Tf_AI2[i]);
    }

    gameGirl.SetActive(false);
    gameGirlSit.SetActive(true);
    playerPanel.GetComponent<CanvasGroup>().interactable = true;
    

    canvasSee.DOFade(1, fadeTime);
    canvasSideShow.DOFade(1, fadeTime);
    seeBtn.interactable = true;

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
  }

  public void SideShowCard()
  {
    StartCoroutine(RotateCardsList(ai2CardsList));
  }

  private IEnumerator RotateCardsList(List<GameObject> cardsList)
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


  public void PackCard()
  {
    StartCoroutine(PackCardsToDeck());
  }

  private IEnumerator PackCardsToDeck()
  {
    canvasSee.DOFade(0, fadeTime);
    canvasSideShow.DOFade(0, fadeTime);

    gameGirlSit.SetActive(false);
    gameGirl.SetActive(true);

    for (int i = 0; i < 3; i++)
    {
        yield return new WaitForSeconds(0.5f);
        MoveCardToTransform(playerCardsList[i], tf_BoxCard);
    }

    gameGirl.SetActive(false);
    gameGirlSit.SetActive(true);
    // Clear the lists as the cards are now back in the deck.
    playerCardsList.Clear();
  }

  public void OnSettingsButtonClick()
  {
    // Get the SettingsUI instance
    SettingsUI settingsPanel = SettingsUI.Instance;

    // Show the settings panel if it exists
    if (settingsPanel != null)
    {
        settingsPanel.PanelIn();
    }
  }



  public void DetermineWinningHand()
  {
    HandEvaluator.HandType playerHandType = HandEvaluator.GetHandType(playerCardsList);
    HandEvaluator.HandType ai1HandType= HandEvaluator.GetHandType(ai1CardsList);
    HandEvaluator.HandType ai2HandType = HandEvaluator.GetHandType(ai2CardsList);

    if(playerHandType > ai1HandType && playerHandType > ai2HandType)
    {
      Debug.Log("Player Wins " + playerHandType);
    }
    else if (ai1HandType > playerHandType && ai1HandType > ai2HandType)
    {
      Debug.Log("AI1 Wins " + ai1HandType);
    }
    else if (ai2HandType > playerHandType && ai2HandType > ai1HandType)
    {
      Debug.Log("AI2 Wins " + ai2HandType);
    }
    else
    {
      Debug.Log("It's a tie");
    }
  }
}