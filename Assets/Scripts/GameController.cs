using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    public float fadeTime = 1f;

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

    IEnumerator SplitCards()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.5f);
            MoveCardToTransform(playerCardsList[i], arr_Tf_player[i]);

            yield return new WaitForSeconds(0.5f);
            MoveCardToTransform(ai1CardsList[i], arr_Tf_AI1[i]);

            yield return new WaitForSeconds(0.5f);
            MoveCardToTransform(ai2CardsList[i], arr_Tf_AI2[i]);
        }

        canvasSee.DOFade(1, fadeTime);
        canvasSideShow.DOFade(1, fadeTime);

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
        // Loop through the playerCards.cards array and rotate each card.
        for (int i = 0; i < playerCardsList.Count; i++)
        {
            iTween.RotateBy(playerCardsList[i], iTween.Hash("-y", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            playerCardsList[i].GetComponent<UICard>().gob_BackCard.SetActive(false);
        }

    }

    public void SideShowCard()
    {
        // Loop through the playerCards.cards array and rotate each card.
        for (int i = 0; i < ai2CardsList.Count; i++)
        {
            iTween.RotateBy(ai2CardsList[i], iTween.Hash("-y", 0.5f, "easeType", "Linear", "loopType", "none", "time", 0.4f));
            ai2CardsList[i].GetComponent<UICard>().gob_BackCard.SetActive(false);
        }
        
    }
}
