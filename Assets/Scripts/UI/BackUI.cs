
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class BackUI : MonoBehaviour
{
    public float fadeTime = 1f;
    public RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    // Start is called before the first frame update
    public void PanelIn()
    {
      canvasGroup.alpha = 0;
      rectTransform.transform.localPosition = new Vector3(0, -1000, 0);
      rectTransform.DOAnchorPos(new Vector2(0, 0), fadeTime).SetEase(Ease.OutExpo);
      canvasGroup.DOFade(1, fadeTime);
    }
    
    public void PanelOut()
    {
      canvasGroup.alpha = 1f;
      rectTransform.transform.localPosition = new Vector3(0, 0, 0);
      rectTransform.DOAnchorPos(new Vector2(0, -1000), fadeTime).SetEase(Ease.InFlash);
    }

    public void OnMainMenu()
    {
      SceneManager.LoadScene("MainMenu");
    }
}
