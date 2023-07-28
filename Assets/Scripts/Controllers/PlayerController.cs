using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    protected PlayerManager gameManager;

    public abstract void StartTurn();

    public abstract void EndTurn();
}
