using UnityEngine;
using TMPro;

public class TotalMoneyPot : MonoBehaviour
{
    public TextMeshProUGUI myText;
    public TextMeshProUGUI sourceText;
    public GameObject Blindbutton;
    public GameObject Panel;

    private int TotalMoney;

    public void UpdateMoneyText()
    {
        int sourceMoney;
        if (int.TryParse(sourceText.text, out sourceMoney))
        {
            TotalMoney += sourceMoney;
            myText.text = TotalMoney.ToString();
        }

        Panel.SetActive(false);
    }
}
