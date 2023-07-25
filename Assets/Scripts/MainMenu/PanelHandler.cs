using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    // Start is called before the first frame update
    [SerializeField] private float verticalMovement = 30f;
    [SerializeField] private float moveTime = 0.1f;
    [SerializeField]  private float scaleAmount = 1.1f;

    private Vector3 startPos;
    private Vector3 startScale;
    
    private void Start()
    {
        startPos = transform.position;
        startScale = transform.localScale;
    }

    // Update is called once per frame
    private IEnumerator MovePanel(bool startAnimation)
    {
        Vector3 endPos;
        Vector3 endScale;

        float elapsedTime = 0f;
        while (elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;
            if (startAnimation)
            {
                endPos = startPos + new Vector3(0f, verticalMovement, 0f);
                endScale = startScale * scaleAmount;
            }
            else
            {
                endPos = startPos;
                endScale = startScale;
            }

            Vector3 lerpPos = Vector3.Lerp(transform.position, endPos, (elapsedTime / moveTime));
            Vector3 lerpScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / moveTime));

            transform.position = lerpPos;
            transform.localScale = lerpScale;

            yield return null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Select the panel
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //DeSelect the panel
        eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(MovePanel(true));
    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartCoroutine(MovePanel(false));
    }
}
