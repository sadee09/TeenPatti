using UnityEngine;
using DG.Tweening;

public class PopUp : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float animationDuration = 0.5f;

    public void Start()
    {
        canvasGroup.alpha = 0f;
        transform.localScale = Vector3.zero;

        canvasGroup.DOFade(1f, animationDuration).SetDelay(0.2f);
        transform.DOScale(new Vector3(0.8875f, 0.85f, 1f), animationDuration).SetEase(Ease.OutBack);
    }

    public void OnClose()
    {
        transform.DOScale(Vector3.zero, animationDuration).OnComplete(DestroyMe);
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}