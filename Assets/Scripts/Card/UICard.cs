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
 
void Start()
{
    string str = Front_Cards.sprite.name;
    char lastChar = str[str.Length - 1];

    if (char.IsDigit(lastChar))
    {
        if (int.TryParse(lastChar.ToString(), out int parsedValue))
        {
            value = parsedValue;
        }
        else
        {
            // Handle the case where parsing fails, e.g., set a default value or show an error message.
            Debug.LogError("Failed to parse value from sprite name: " + str);
            value = 0; // Set a default value or some appropriate value in case of failure.
        }
    }
    else
    {
        // Handle face cards (A, K, Q, J)
        switch (lastChar)
        {
            case '0':
              value = 10;
              break;
            case 'A':
                value = 1;
                break;
            case 'K':
                value = 13;
                break;
            case 'Q':
                value = 12;
                break;
            case 'J':
                value = 11;
                break;
            default:
                // Handle unknown characters, set a default value, or show an error message.
                Debug.LogError("Invalid character in sprite name: " + lastChar);
                value = 0; // Set a default value or some appropriate value when the character is unknown.
                break;
        }
    }
}



}
