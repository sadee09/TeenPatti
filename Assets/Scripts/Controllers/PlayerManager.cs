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
        Debug.Log("Next turn started");
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
                GameController.instance.winnerText.text = players[0].gameObject.name + " Wins!";
                players[currentPlayerIndex].EndTurn();
                gameController.EndGame();
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
                GameController.instance.winnerText.text = "Player " + players[0].gameObject.name + " Wins!";
                players[currentPlayerIndex].EndTurn();
                gameController.EndGame();
                gameController.RestartGame();
            }
        }
    }

}
