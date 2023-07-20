using UnityEngine;
using TMPro;

public class AddandSub : MonoBehaviour
{
    public GameObject addButton;
    public GameObject subButton;
    public TextMeshProUGUI myText;

    private bool AddActive;
    private bool SubActive;

    void Start()
    {
        subButton.SetActive(false);
        myText.text = 10.ToString();
    }

    public void Add()
    {
        if(!SubActive)
        {
            int currentValue = int.Parse(myText.text);
            int newValue = currentValue * 2;
            myText.text = newValue.ToString();
            SubActive = true;
            AddActive = false;
            addButton.SetActive(false);
            subButton.SetActive(true);
        }
    }

    public void Sub()
    {
        if (!AddActive)
        {
            int currentValue = int.Parse(myText.text);
            int newValue = currentValue / 2;
            myText.text = newValue.ToString();
            AddActive = true;
            SubActive = false;
            addButton.SetActive(true);
            subButton.SetActive(false);
        }
    }
}
