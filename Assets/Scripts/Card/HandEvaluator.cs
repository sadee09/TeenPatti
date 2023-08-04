using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class HandEvaluator
{
    public enum HandType
    {
        HighCard,
        Pair,
        Color,
        Sequence,
        PureSequence,
        Trail
    }

    public static HandType GetHandType(List<GameObject> cardsList)
    {
        Debug.Log("Sorted Card Values:");
        foreach (var card in cardsList)
        {
            UICard uiCard = card.GetComponent<UICard>();
            if (uiCard.value == 0)
            {
                Debug.Log("here");
            }

            Debug.Log("Card Value: " + uiCard.value + ", Suit: " + uiCard.suit);
        }

        if (IsTrail(cardsList))
        {
            return HandType.Trail;
        }

        if (IsPureSequence(cardsList))
        {
            return HandType.PureSequence;
        }

        if (IsSequence(cardsList))
        {
            return HandType.Sequence;
        }
        
        if (IsColor(cardsList))
        {
            return HandType.Color;
        }


        if (IsPair(cardsList))
        {
            return HandType.Pair;
        }

        // If no other hand type matched, it's a high card
        return HandType.HighCard;
    }

    public static bool IsTrail(List<GameObject> cardsList)
    {
        return cardsList[0].GetComponent<UICard>().value == cardsList[1].GetComponent<UICard>().value &&
               cardsList[1].GetComponent<UICard>().value == cardsList[2].GetComponent<UICard>().value;
    }

    public static bool IsPureSequence(List<GameObject> cardsList)
    {
        return IsSequence(cardsList) && IsColor(cardsList);
    }

    public static bool IsSequence(List<GameObject> cardsList)
    {
        return (cardsList[0].GetComponent<UICard>().value == cardsList[1].GetComponent<UICard>().value - 1 &&
               cardsList[1].GetComponent<UICard>().value == cardsList[2].GetComponent<UICard>().value -1) ||
    
               (cardsList[0].GetComponent<UICard>().value == 2 && cardsList[1].GetComponent<UICard>().value == 3 &&
               cardsList[1].GetComponent<UICard>().value == 14);
    }

    public static bool IsColor(List<GameObject> cardsList)
    {
        return cardsList[0].GetComponent<UICard>().suit == cardsList[1].GetComponent<UICard>().suit &&
               cardsList[1].GetComponent<UICard>().suit == cardsList[2].GetComponent<UICard>().suit;
    }

    public static bool IsPair(List<GameObject> cardsList)
    {
        return cardsList[0].GetComponent<UICard>().value == cardsList[1].GetComponent<UICard>().value ||
               cardsList[1].GetComponent<UICard>().value == cardsList[2].GetComponent<UICard>().value;
    }

    // Custom comparer to sort the cards based on their values

    public class CardComparer : IComparer<GameObject>
    {
        public int Compare(GameObject card1, GameObject card2)
        {
            int value1 = card1.GetComponent<UICard>().value;
            int value2 = card2.GetComponent<UICard>().value;
            
            return value1.CompareTo(value2);
        }
    }
    public static class Sorter
    {
        public static List<GameObject> GetSortedCards(List<GameObject> cards)
        {
            cards.Sort(new CardComparer());
            return cards;
        }
    }
}
