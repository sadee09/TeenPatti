using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    protected PlayerManager gameManager;

    public string playerName { get; set; }

    public abstract void StartTurn();

    public abstract void EndTurn();
}
