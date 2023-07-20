using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    public TextMeshProUGUI myText;
    private int InitialMoney = 10000;

    void Start()
    {
        myText.text = "Rs " + InitialMoney.ToString();
    }

    void Update()
    {
        
    }
}