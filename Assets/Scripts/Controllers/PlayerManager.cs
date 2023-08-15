using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<PlayerController> players = new List<PlayerController>();
    private int currentPlayerIndex = 1;
    private bool gameStarted = false;
    public GameController gameController;
    public PlayerController ai1Controller;
    public AI2Controller ai2Controller; 

    void Start()
    {
        StartGame();
    }
    public void AddPlayer(PlayerController player)
    {
        players.Add(player);
    }

    public void StartGame()
    {
        gameStarted = true;
        StartNextTurn();
    }

    public void StartNextTurn()
    {
        players[currentPlayerIndex].EndTurn();
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        players[currentPlayerIndex].StartTurn();
    }

    public void PlayerPack()
    {
        int i = currentPlayerIndex - 1;

        if (i < 0)
        {
            i = players.Count - 1;
        }

        if (i >= 0 && i < players.Count)
        {
            PlayerController removedPlayer = players[i];
            players.RemoveAt(i);
            currentPlayerIndex = i % players.Count;

            if (players.Count == 1)
            {
                StartCoroutine(GameController.instance.RotateCardsList(GameController.instance.playerCardsList));
                StartCoroutine(GameController.instance.RotateCardsList(GameController.instance.ai1CardsList));
                StartCoroutine(GameController.instance.RotateCardsList(GameController.instance.ai2CardsList));

                PlayerController remainingPlayer = players[0];

                // Check the type of the remaining player
                if (remainingPlayer == ai1Controller)
                {
                    // If the remaining player is AI1Controller
                    GameController.instance.winnerText.text = "AI1 Wins!";
                }
                else if (remainingPlayer == ai2Controller)
                {
                    // If the remaining player is AI2Controller
                    GameController.instance.winnerText.text = "AI2 Wins!";
                }
                else
                {
                    // If the remaining player is not AI, assume it's the player-panel
                    GameController.instance.winnerText.text = "You Win!";
                }

                players[currentPlayerIndex].EndTurn();
                gameController.RestartGame();
            }
        }
    }

    public void PlayerPack(int currentPlayerIndex)
    {
        int i = currentPlayerIndex - 1;
        int index = 0;
        foreach(PlayerController player in players)
        {
            if (player == ai1Controller)
            {
                i = index;
            }
            index++;
        }
        
        if (i < 0)
        {
            i = players.Count - 1;
        }

        if (i >= 0 && i < players.Count)
        {
            PlayerController removedPlayer = players[i];
            players.RemoveAt(i);
            currentPlayerIndex = i % players.Count;

            if (players.Count == 1)
            {
                StartCoroutine(GameController.instance.RotateCardsList(GameController.instance.playerCardsList));
                StartCoroutine(GameController.instance.RotateCardsList(GameController.instance.ai1CardsList));
                StartCoroutine(GameController.instance.RotateCardsList(GameController.instance.ai2CardsList));
                PlayerController remainingPlayer = players[0];

                if (remainingPlayer == ai1Controller)
                {

                    GameController.instance.winnerText.text = "AI1 Wins!";
                }
                else if (remainingPlayer == ai2Controller)
                {

                    GameController.instance.winnerText.text = "AI2 Wins!";
                }
                else
                {

                    GameController.instance.winnerText.text = "You Win!";
                }

                players[currentPlayerIndex].EndTurn();
                gameController.RestartGame();
            }
            this.currentPlayerIndex = currentPlayerIndex+1;
        }
    }
}
