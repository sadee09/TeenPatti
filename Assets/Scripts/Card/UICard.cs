using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UICard : MonoBehaviour
{
    // Start is called before the first frame update
    public Image Front_Cards;
    public GameObject gob_BackCard;
    public int value; 
    public SuitType suit; // Add this property to represent the suit of the card

    public enum SuitType
    {
        Spades,
        Hearts,
        Clubs,
        Diamonds
    }
 

}
