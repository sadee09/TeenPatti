using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject cards;
    public Transform tf_BoxCard;
    public Transform[] arr_Tf_player, arr_Tf_AI1, arr_Tf_AI2;


    void Start()
    {
        if (SpriteGame.instance != null)
        {
            InstanceCard();
        }
        else
        {
            StartCoroutine(WaitForCardLoading());
        }// Check if Cards.instance is not null before calling InstanceCard

    }
    IEnumerator WaitForCardLoading()
    {
        // Wait until Cards.instance is loaded
        while (SpriteGame.instance == null)
        {
            yield return null;
        }

        // Call InstanceCard once Cards.instance is loaded
        InstanceCard();
    }

    

    public void InstanceCard()
    {

        for (int i = 0; i < SpriteGame.instance.arr_Cards.Length; i++)
        {
            GameObject card = Instantiate(cards, tf_BoxCard.position, Quaternion.identity);
            card.transform.SetParent(tf_BoxCard, false);
            card.GetComponent<UICard>().Front_Cards.sprite = SpriteGame.instance.arr_Cards[i];
        }

        
    }
}
