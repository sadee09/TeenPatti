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

      // Extract the suit part of the sprite name after the "_"
      int underscoreIndex = str.IndexOf('_') ;
      string suitPart = str.Substring(underscoreIndex + 1, str.Length - underscoreIndex - 2);

      // Handle the suit type
      switch (suitPart)
      {
          case "spades":
              suit = SuitType.Spades;
              break;
          case "hearts":
              suit = SuitType.Hearts;
              break;
          case "clubs":
              suit = SuitType.Clubs;
              break;
          case "diamonds":
              suit = SuitType.Diamonds;
              break;
      }
      // Check if the last two characters are digits (0-9) to handle numeric values (e.g., "10")
      if (str.Length >= 2 && char.IsDigit(str[str.Length - 1]) && char.IsDigit(str[str.Length - 2]))
      {
          if (int.TryParse(str.Substring(str.Length - 2), out int parsedValue))
          {
              value = parsedValue;
          }
      }
      else
      {
          // Handle face cards (A, K, Q, J) and single-digit numeric values (2-9)
          char lastChar = str[str.Length - 1];
          switch (lastChar)
          {
              case 'A':
                  value = 14;
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
                  if (char.IsDigit(lastChar))
                  {
                      value = int.Parse(lastChar.ToString());
                  }
                  break;
          }
      }
  }
}
