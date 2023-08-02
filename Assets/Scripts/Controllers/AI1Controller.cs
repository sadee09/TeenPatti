using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AI1Controller : PlayerController
{
  public Image uiFill;
  public int Duration;
  private int remainingDuration;

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
        Begin(Duration);

        StartCoroutine(PerformAITurn());
    }

    private IEnumerator PerformAITurn()
    {
        yield return new WaitForSeconds(5f);

        gameManager.StartNextTurn();
    }

    void Begin(int Second) 
    {
      remainingDuration = Second;
      StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
      while(remainingDuration >= 0) 
      {
        uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
        remainingDuration--;
      yield return new WaitForSeconds(1f);
      }
        EndTurn();
    }

    public override void EndTurn()
    {
        Debug.Log("End of AI1 turn");
    }
}
