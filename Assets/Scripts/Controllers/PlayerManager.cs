using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private List<PlayerController> players = new List<PlayerController>();
    private int currentPlayerIndex = 0;
    private bool gameStarted = false;

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
        if (!gameStarted)
        {
            Debug.Log("Game not started yet.");
            return;
        }
        players[currentPlayerIndex].EndTurn();
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        players[currentPlayerIndex].StartTurn();
    }
}
