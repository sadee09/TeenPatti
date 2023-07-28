using UnityEngine;
using System.Collections;

public class AI1Controller : PlayerController
{
    private void Awake()
    {   
        gameManager = FindObjectOfType<PlayerManager>();
        if (gameManager == null)
        {   
            Debug.LogError("GameManager script not found in the scene!");
        }
        else
        {
            gameManager.AddPlayer(this);
        }
    }
    
    public override void StartTurn()
    {
        Debug.Log("Start Ai1 turn");

        StartCoroutine(PerformAITurn());
    }

    private IEnumerator PerformAITurn()
    {
        yield return new WaitForSeconds(5f);

        gameManager.StartNextTurn();;
    }

    public override void EndTurn()
    {
        Debug.Log("End of AI1 turn");
    }
}
