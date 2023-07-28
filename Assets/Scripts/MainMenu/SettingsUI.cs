using System.ComponentModel;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SettingsUI : MonoBehaviour
{
    public float fadeTime = 1f;
    public RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    private static SettingsUI instance;

    // Singleton instance property
    public static SettingsUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SettingsUI>();
                if (instance == null)
                {
                    // Since there is no instance found, create a new GameObject with the SettingsUI component.
                    GameObject settings = new GameObject("Settings");
                    instance = settings.AddComponent<SettingsUI>();
                    instance.rectTransform = settings.AddComponent<RectTransform>();
                    instance.canvasGroup = settings.AddComponent<CanvasGroup>();
                    DontDestroyOnLoad(settings); // Persist the instance across scenes
                }
            }
            return instance;
        }
    }
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
    
}
